using Godot;
using System;

public partial class slot : PanelContainer
{
	public string Qkod ;
	[Export] TextureRect texture;
	Texture2D texture2D ;
	
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
					texture2D= GD.Load<Texture2D>("res://sprait/item/t/terror.png");
					texture.Texture = texture2D;
			  break;    
			default:
			break;
		}
	}
	public override void _Process(double delta)
	{
	  if (Input.IsActionPressed("mark")&& GetGlobalMousePosition() == GlobalPosition)
	  {
			GlobalPosition = GetGlobalMousePosition();
	  }
	}
}
