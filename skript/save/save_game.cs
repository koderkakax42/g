using Godot;
using System;
using System.Diagnostics;

public partial class save_game : SaveGame.SaveGame
{
	public static event Action new_game = delegate{};
	public static event Action save_playe_game=delegate{};
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	  Player.dead += delsave;
	  ui_pc_player.save += Save_data_Game;
	}
	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
	public void _on_new_game()
	{
	   fader.ScenePath = "res://scene/scen/game_scen/main.tscn";

       new_game?.Invoke();

	   LoadNewScene();
	}
	public void _on_save()
	{
	  fader.ScenePath = "res://scene/scen/game_scen/main.tscn";
      
      save_playe_game?.Invoke();
	}
	public void _on_undo()
	{
       fader.ScenePath = "res://scene/ui/meny/meny.tscn";
	}

	 private void LoadNewScene()
    {
        SceneTree tree = GetTree();
        tree.ChangeSceneToFile("res://scene/scen/load_scen/fader.tscn");
    }
}
