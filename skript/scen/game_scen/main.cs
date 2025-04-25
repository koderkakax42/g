using Godot;


public partial class main : Node2D
{    bool time;
    public override void _PhysicsProcess(double delta)
    {

    }
    public override void _Ready()
    {
        ui_pc_player.time_stop += timestop;
        shest.time_start += timestart;
        speed_settings.time_start += timestart;
        Player.dead += off;
        
    }
    private void off()
    {
 
        ui_pc_player.time_stop -= timestop;
        shest.time_start -= timestart;
        speed_settings.time_start -= timestart;
        Player.dead -= off;
        GD.Print("follow off");
        
    }

    private void timestop()
    {
        Engine.TimeScale = 0;
    }
    private void timestart()
    {
         Engine.TimeScale = 1;
    }
}