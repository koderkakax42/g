using Godot;
using System;

public partial class save_game : SaveGame.SaveGame
{
	public static event Action new_game = delegate{};
	public static event Action save_playe_game=delegate{};
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
      ui_pc_player.save += pr;
	  ui_pc_player.save += Save_data_Game;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
	public void _on_new_game()
	{
      new_game?.Invoke();
	}
	public void _on_save()
	{
      save_playe_game?.Invoke();
	}
	public void _on_undo()
	{

	}
	private void pr()
	{
		GD.Print(data + " it is work ");
	}

}
