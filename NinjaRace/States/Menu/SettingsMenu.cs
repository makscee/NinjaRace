using VitPro;
using VitPro.Engine;
using System;

class SettingsMenu : Menu
{
    Button bvsync;
    Button bfullscreen;
    public SettingsMenu()
    {
        Label vsync = new Label("VSYNC", 30, 180);
        vsync.Anchor = new Vec2(0.3, 0.7);
        AddElement(vsync);

        bvsync = new Button(Program.Settings.VSync ? "TRUE" : "FALSE",
            () =>
            {
                Program.Settings.VSync = !Program.Settings.VSync;
                bvsync.Text = Program.Settings.VSync ? "TRUE" : "FALSE";
                Program.Settings.Apply();
            }, 30, 180);
        bvsync.Anchor = new Vec2(0.7, 0.7);
        AddElement(bvsync);

        Label fullscreen = new Label("FULL SCREEN", 30, 180);
        fullscreen.Anchor = new Vec2(0.3, 0.5);
        AddElement(fullscreen);

        bfullscreen = new Button(Program.Settings.Fullscreen ? "TRUE" : "FALSE",
            () =>
            {
                Program.Settings.Fullscreen = !Program.Settings.Fullscreen;
                bfullscreen.Text = Program.Settings.Fullscreen ? "TRUE" : "FALSE";
                Program.Settings.Apply();
            }, 30, 180);
        bfullscreen.Anchor = new Vec2(0.7, 0.5);
        AddElement(bfullscreen);

        Button controls = new Button("EDIT CONTROLS", 
            () => { Program.Manager.NextState = new EditControls(); }, 40, 300);
        controls.Anchor = new Vec2(0.5, 0.2);
        AddElement(controls);

        
    }
}