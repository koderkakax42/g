using Godot;
using System;

public partial class Main : Node2D
{
	bool time;
	

	public override void _PhysicsProcess(double delta)
	{
	}
	public override void _Ready()
	{
		//sceneeffect = GD.Load<PackedScene>("res://scene/atack/atack/poisoneffect.tscn");

		Node parent = GetParent();
		if (parent != null)
		{
			// Работаем с parent
		}
		else
		{
			GD.PrintErr("Родитель не найден!");
		}
		UiPcPlaer.time_stop += timestop;
		Shest.time_start += timestart;
		SpeedSettings.time_start += timestart;
		Player.dead += off;
		
	}
	private void off()
	{

		UiPcPlaer.time_stop -= timestop;
		Shest.time_start -= timestart;
		SpeedSettings.time_start -= timestart;
		Player.dead -= off;
		
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
