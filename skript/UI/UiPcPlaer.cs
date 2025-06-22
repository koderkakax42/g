using Godot;
using System;
using System.Collections.Generic;

public partial class UiPcPlaer : PanelContainer
{
	public static event Action time_stop = delegate { };
	public static event Action save = delegate { };
	[Export] public Slot[] slotarei = new Slot[5];
	public static int nomber_open_chest = 0;
	private int chest_nomber = 0;
	public PackedScene chest { get; set; }
	[Export] Label text { get; set; }
	[Export] public ProgressBar value { get; set; }
	[Export] Player player;
	SaveGame Save = new SaveGame();
	Spawn spawn = new Spawn();
	PackedScene meny;
	[Export] public Shest cheste = new Shest();

	public override void _Ready()
	{

		SpeedSettings.save += saveplayer;

		chest = GD.Load<PackedScene>("res://scene/inventori/shest.tscn");

		meny = GD.Load<PackedScene>("res://scene/ui/setting/speed_settings.tscn");

		GetWindow().MinSize = new Vector2I(480, 280);
		GetWindow().MaxSize = new Vector2I(1920, 960);
		// Подключаемся к сигналу size_changed

		GetWindow().Connect("size_changed", new Callable(this, nameof(OnWindowSizeChanged)));

	}
	public void _on_xp(int health)
	{
		value.Value = health;
	}

	public void _on_vale(string money)
	{
		text.Text = money;
	}
	private void _on_chest()
	{

		if (cheste.Visible != true&&nomber_open_chest == 0)
		{
			cheste.Visible = true;
			nomber_open_chest = 1;
			time_stop?.Invoke();
		}


	}


	private void OnWindowSizeChanged()
	{
		Vector2 newSize = GetWindow().Size;
	}

	private void saveplayer()
	{
		if (SaveGame.save != null)
		{
			SaveGame.save.Clear();
		}
		if (!IsInstanceValid(this))
		{
			return;
		}
		foreach (Player player in GetTree().GetNodesInGroup("Player"))
		{
			GD.Print(1);

			if (IsInstanceValid(player))
			{
				GD.Print(5);
				SaveGame.save = new Dictionary<string, float>();

				SaveGame.save.TryAdd("player positon x", player.GlobalPosition.X);
				SaveGame.save.TryAdd("player position y", player.GlobalPosition.Y);
				SaveGame.save.TryAdd("player health", player.Health);
				SaveGame.save.TryAdd("player money ", player.money.ToFloat());
				GD.Print(4);
				saveenemy();
			}
		}

	}
	private void saveenemy()
	{
		foreach (Enemy enemy in GetTree().GetNodesInGroup("enemy"))
		{
			GD.Print(3);
			if (IsInstanceValid(enemy))
			{
				GD.Print(4);

				SaveGame.save.TryAdd(enemy.EnemyId + "enemy X position", enemy.GlobalPosition.X);
				SaveGame.save.TryAdd(enemy.EnemyId + "enemy Y position", enemy.GlobalPosition.Y);
				SaveGame.save.TryAdd(enemy.EnemyId + "health enemy", enemy.Health);

			}
		}
		GD.Print(7);

	}

	private void _on_button()
	{

		if (save == null)
		{
			GD.Print("null save ");
		}


		saveplayer();
	save?.Invoke();

		Save.Save_data_Game();


		GD.Print(" save is truy . ");
	}
	private void _on_meny()
	{
		if (nomber_open_chest == 0)
		{
			if (time_stop == null)
			{
				GD.Print("null time stop");
			}
			time_stop?.Invoke();


			var i = (SpeedSettings)meny.Instantiate();
			GetParent().AddChild(i);
			i.GlobalPosition = new Vector2(player.GlobalPosition.X - 200, player.GlobalPosition.Y - 200);
			nomber_open_chest = 1;
		}
	}
}
