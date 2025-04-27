using Godot;
using System;

public partial class speed_settings : Button
{
	public static event Action save;
	public static event Action time_start;
	public override void _Ready()
	{
	
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

 private void pressed()
 {
		 time_start?.Invoke();
	ui_pc_player.nomber_open_chest = 0;		
 }

  private void LoadNewScene()
	{
		SceneTree tree = GetTree();
		tree.ChangeSceneToFile("res://scene/ui/meny/meny.tscn");
	}
	private void _on_exsit()
	{
		pressed();
		QueueFree();
	}
	private void _on_button_pressed()
	{
		pressed();
		LoadNewScene();
	}
	private void _on_button_2()
	{
		pressed();
		save?.Invoke();
		LoadNewScene();
	}

}
