using Godot;
using System;

public partial class speed_settings : Button
{
	// Called when the node enters the scene tree for the first time.
	public static event Action time_start;
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
	private void _QueueFree(){QueueFree();}

	private void _on_exsit()
	{
		ui_pc_player.nomber_open_chest = 0;		
		GD.Print("exsit meny");
      time_start?.Invoke();
     CallDeferred(MethodName._QueueFree);
	}
}
