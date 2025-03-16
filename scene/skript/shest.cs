using Godot;
using System;

public partial class shest : Control
{
	public static int off_chest =0;
	

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
	private void _on_button()
	{
       ui_pc_player.on_open_chest =0 ;

		off_chest++;

	

	   QueueFree();
	}
	public void _on_button_save()
	{
    
     off_chest--;

	}
}
