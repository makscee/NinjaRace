using System;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Data;
using System.Data.Sql;
using VitPro;

class DBUtils
{
    static string connectionString =
        "Data Source=(LocalDB)\\v11.0;AttachDbFilename=C:\\Users\\Admin\\Documents" 
        + "\\GitHub\\NinjaRace\\NinjaRace\\Database.mdf;Integrated Security=True";

    public static Tiles GetTiles(String level)
    {
        int levelX = 0, levelY = 0;
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            SqlCommand command = new SqlCommand(
                "SELECT * FROM Levels where name='" + level + "';",
                connection);
            connection.Open();

            SqlDataReader reader = command.ExecuteReader();

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    levelX = reader.GetInt32(2);
                    levelY = reader.GetInt32(1);
                }
            }
            else
            {
                throw new Exception("DB: Objects not found.");
            }
            reader.Close();
        }

        Tiles tiles = new Tiles(levelX, levelY);
        tiles.level = level;
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            SqlCommand command = new SqlCommand(
                "SELECT * FROM Tiles;",
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
                    tiles.AddTile(reader.GetInt32(3), reader.GetInt32(2), t);
                }
            }
            else
            {
                throw new Exception("DB: Objects not found.");
            }
            reader.Close();
        }
        return tiles;
    }

    public static void StoreTiles(Tiles tiles)
    {
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            connection.Open();
            new SqlCommand("delete from Tiles where Level='" + tiles.level + "';", connection).ExecuteNonQuery();
            for (int i = 1; i < tiles.GetLength(0); i++)
                for (int j = 1; j < tiles.GetLength(1); j++)
                {
                    Tile t = tiles.GetTile(i, j);
                    if(t == null)
                        continue;

                    SqlCommand command = new SqlCommand(
                          "INSERT INTO [dbo].[Tiles] ([X_POSITION], [Y_POSITION], [LEVEL], [TYPE]) "
                          + "VALUES (@x_position, @y_position, @level, @type);",
                          connection);
                    command.Parameters.AddWithValue("@x_position", j);
                    command.Parameters.AddWithValue("@y_position", i);
                    command.Parameters.AddWithValue("@level", tiles.level);
                    command.Parameters.AddWithValue("@type", t.GetType().ToString());
                    command.ExecuteNonQuery();
                }
            connection.Close();
        }
    }
}