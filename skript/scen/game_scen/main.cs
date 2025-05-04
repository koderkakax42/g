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
