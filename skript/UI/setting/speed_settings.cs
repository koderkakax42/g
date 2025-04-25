using Godot;
using System;

public partial class speed_settings : Button
{
	ui_pc_player ui = new ui_pc_player() ;
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
	ui_pc_player.nomber_open_chest = 0;		
		 time_start?.Invoke();
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
		QueueFree();
	}
	private void _on_button_2()
	{
		ui.saveplayer();
		pressed();
		LoadNewScene();
	}
}
