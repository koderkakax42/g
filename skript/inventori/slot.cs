using Godot;
using System;
using System.Threading;

public partial class Slot : PanelContainer
{
	bool slot = false;
	public static event Action<Slot> slotchoise = delegate { };
	[Export] Label label;
	public string Qkod;
	[Export] TextureRect texture;
	Texture2D texture2D;
	[Export] Area2D Area;
	float timer;
	public void element(int nomber)
	{
		switch (nomber)
		{
			case 0:
				texture2D = GD.Load<Texture2D>("res://sprait/item/a/air.png");
				texture.Texture = texture2D;
				break;
			case 1:
				texture2D = GD.Load<Texture2D>("res://sprait/item/m/mars.png");
				texture.Texture = texture2D;
				break;
			case 2:
				texture2D = GD.Load<Texture2D>("res://sprait/item/n/neiron.png");
				texture.Texture = texture2D;
				break;
			case 3:
				texture2D = GD.Load<Texture2D>("res://sprait/item/p/poison.png");
				texture.Texture = texture2D;
				break;
			case 4:
				texture2D = GD.Load<Texture2D>("res://sprait/item/s/sun.png");
				texture.Texture = texture2D;
				break;
			case 5:
				texture2D = GD.Load<Texture2D>("res://sprait/item/t/terror.png");
				texture.Texture = texture2D;
				break;
			default:
				break;
		}
	}
	public override void _Process(double delta)
	{
		if (Input.IsActionPressed("leftPressed") && slot&&timer>=0.2f)
		{
			slot = false;
			GD.Print(Name);
			timer = 0;
			slotchoise?.Invoke(this);
		}
		timer += (float)delta;
	}
	public override void _Ready()
	{
		Area.AreaEntered += OnAreaEntered;
		Area.AreaExited += OnAreaExit;


		var i = GetGroups();
		if (!i.Contains("ui") && i.Contains("slot"))
		{
			UiPcPlaer.time_stop += VisibleOn;
			Shest.time_start += VisibleOff;
		}

	}

	private void VisibleOn()
	{
		if(IsInstanceValid(this))
		Visible = true;
	}
	private void VisibleOff()
	{
		if(IsInstanceValid(this))
		Visible = false;
	}

	private void OnAreaEntered(Area2D area)
	{
		if (Visible)
		{
			
			if (area is Deteckt)
			{

				slot = true;
			}
		}
	}
	private void OnAreaExit(Area2D area)
	{
		if (area is Deteckt)
		{
			
			slot = false;
		}
	}
}
