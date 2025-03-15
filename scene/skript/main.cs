using Godot;


public partial class main : Node2D
{

    public override void _PhysicsProcess(double delta)
    {
        if (ui_pc_player.time == true)
        {
            Engine.TimeScale = 0.001;
        }
        else
        {
            Engine.TimeScale = 1;
        }
    }
}