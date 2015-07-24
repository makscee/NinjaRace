using System;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Data;
using System.Data.Sql;
using System.Configuration;
using VitPro;

class DBUtils
{
    static string connectionString = 
        ConfigurationManager.ConnectionStrings["NinjaRace.Properties.Settings.DatabaseConnectionString"].ConnectionString;

    public static void Init()
    {
        LoadLevelNames();
    }
    private static List<string> LevelNames;

    public static List<string> GetLevelNames() 
    {
        if (LevelNames == null)
            LoadLevelNames();
        return LevelNames; 
    }

    private static void LoadLevelNames()
    {
        LevelNames = new List<string>();
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            SqlCommand command = new SqlCommand(
                "SELECT * FROM Levels where name not like '%[_]S%';",
                connection);
            connection.Open();
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                LevelNames.Add(reader.GetString(0));
            }
        }
    }

    public static Level GetLevel(String name)
    {
        int levelX = 0, levelY = 0;
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            SqlCommand command = new SqlCommand(
                "SELECT * FROM Levels where name='" + name + "';",
                connection);
            connection.Open();

            SqlDataReader reader = command.ExecuteReader();

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    levelX = reader.GetInt32(1);
                    levelY = reader.GetInt32(2);
                }
            }
            else
            {
                throw new Exception("DB: Objects not found.");
            }
            reader.Close();
        }

        Level level = new Level(new Tiles(levelX, levelY), name);
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            SqlCommand command = new SqlCommand(
                "SELECT * FROM Tiles where level='" + level.Name + "';",
                connection);
            connection.Open();

            SqlDataReader reader = command.ExecuteReader();

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    String sType = reader.GetString(5).Trim();
                    Type type = Type.GetType(sType);
                    Tile t = (Tile)type.GetConstructor(new Type[] { }).Invoke(new object[] { });
                    t.ID = reader.GetInt32(0);
                    if(!reader.IsDBNull(1))
                        t.Link = reader.GetInt32(1);
                    level.Tiles.AddTile(reader.GetInt32(2), reader.GetInt32(3), t);
                }
            }
            else
            {
                throw new Exception("DB: Objects not found.");
            }
            reader.Close();
        }
        return level;
    }

    public static void StoreTiles(Level level)
    {
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            connection.Open();
            string name = level.Name.ToUpper();
            new SqlCommand("delete from Tiles where Level='" + level.Name + "';", connection).ExecuteNonQuery();
            new SqlCommand("delete from Levels where Name='" + level.Name + "';", connection).ExecuteNonQuery();
            SqlCommand command = new SqlCommand("insert into Levels ([Name], [WIDTH], [HEIGHT])"
                + "values (@name, @width, @height);", connection);
            command.Parameters.AddWithValue("@name", level.Name);
            command.Parameters.AddWithValue("@width", level.Tiles.GetLength(1));
            command.Parameters.AddWithValue("@height", level.Tiles.GetLength(0));
            command.ExecuteNonQuery();
            for (int y = 1; y < level.Tiles.GetLength(0); y++)
                for (int x = 1; x < level.Tiles.GetLength(1); x++)
                {
                    Tile t = level.Tiles.GetTile(x, y);
                    if(t == null)
                        continue;

                    command = new SqlCommand(
                          "INSERT INTO [dbo].[Tiles] ([ID], [X_POSITION], [Y_POSITION], [LEVEL], [TYPE]) "
                          + "VALUES (@id, @x_position, @y_position, @level, @type);",
                          connection);
                    command.Parameters.AddWithValue("@id", Tiles.GetID(x, y));
                    command.Parameters.AddWithValue("@x_position", x);
                    command.Parameters.AddWithValue("@y_position", y);
                    command.Parameters.AddWithValue("@level", level.Name);
                    command.Parameters.AddWithValue("@type", t.GetType().ToString());
                    command.ExecuteNonQuery();
                }
            connection.Close();
            UpdateLinks(level.Tiles);
        }
    }

    private static void UpdateLinks(Tiles tiles)
    {
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            connection.Open();
            for (int y = 1; y < tiles.GetLength(0); y++)
                for (int x = 1; x < tiles.GetLength(1); x++)
                {
                    Tile t = tiles.GetTile(x, y);
                    if (t == null || t.Link == -1)
                        continue;

                    int tileID = t.ID;
                    int linkID = t.Link;

                    SqlCommand command = new SqlCommand(
                          "update [dbo].[Tiles] set LINK=@link " + 
                          "where ID=@id",
                          connection);
                    command.Parameters.AddWithValue("@link", linkID);
                    command.Parameters.AddWithValue("@id", tileID);
                    command.ExecuteNonQuery();
                }
            connection.Close();
        }
    }
}