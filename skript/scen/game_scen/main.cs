using Godot;
using System;

public partial class Main : Node2D
{
	static bool saveload = false;
	float i = 0;
	float X = 0;
	float Y = 0;
	float health = 0;
	Player player;
	public override void _PhysicsProcess(double delta)
	{

		if (saveload)
		{
			i += (float)delta;
			GD.Print(i);
			if (i >= 1)
			{
				saveload = false;
				LoadDate();
			}
		}
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
		Engine.TimeScale = 0.04;

	}
	private void timestart()
	{
		Engine.TimeScale = 1;
	}

	public void LoadDate()
	{
		System.Console.WriteLine("main loaddate ");
		foreach (Player player in GetTree().GetNodesInGroup("Player"))
		{
			GD.Print("11");
			player.QueueFree();
		}
		foreach (Spawn spawn in GetTree().GetNodesInGroup("spawner"))
		{
			spawn.ProcessMode = ProcessModeEnum.Disabled;
		}
		foreach (Enemy enemy in GetTree().GetNodesInGroup("enemy"))
		{
			GD.Print("22");
			enemy.QueueFree();
		}

		foreach (var person in SaveGame.load)
		{
			if (person.Key.Remove(6) == "player")
			{
				loadplayer(person.Key, person.Value.GetSingle());
			}

			if (person.Key.Length == 21 && person.Key.Substring(5,5) == "enemy")
			{
				loadenemy(person.Key, person.Value.GetSingle());
			}
			else
			{	
				if(person.Key.Length >= 16 )
				GD.Print(person.Key + "  " + person.Key.Length+ "  " + person.Key.Substring(5, 5));
			}
		}
		foreach (Spawn spawn in GetTree().GetNodesInGroup("spawner"))
		{
			spawn.ProcessMode = ProcessModeEnum.Pausable;
		}
	}

	public static void load()
	{
		saveload = true;
		System.Console.WriteLine("main load");
	}
	private void loadplayer(string s, float t)
	{
		switch (s.Substring(7))
		{
			case "positon x":
				X = t;
				break;
			case "position y":
				Y = t;
				break;
			case "health":
				health = t;
				break;
			case "money ":
				PackedScene scene = GD.Load<PackedScene>("res://scene/player/player.tscn");
				 player = (Player)scene.Instantiate();
				AddChild(player);
				player.LoadDate(X, Y, health, t);
				GD.Print("66");
				break;
			default:
				break;
		}
		GD.Print("33");
	}

	private void loadenemy(string s, float t)
	{
		switch (s.Substring(11))
		{
			case "X position":
				X = t;
				break;
			case "Y position":
				Y = t;
				break;
			case "health one":
				health = t;
				PackedScene scene = new PackedScene();
				scene = GD.Load<PackedScene>("res://scene/enemy/enemy/enemy.tscn");
				var enemy = (Enemy)scene.Instantiate();
				AddChild(enemy);
				enemy.LoadData(X, Y, health, s.Remove(5),player);
				break;
			default:
				break;
		}
		GD.Print(44);
	}

}
