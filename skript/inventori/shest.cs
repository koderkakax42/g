using Godot;
using System;

public partial class shest : Control
{
	public static event Action time_start;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public void _on_button() 
	{
		time_start?.Invoke();
	   ui_pc_player.nomber_open_chest=0;			
       QueueFree();
	}
}
