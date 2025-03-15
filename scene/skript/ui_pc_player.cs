using Godot;
using System;
using System.Xml.XPath;

public partial class ui_pc_player : Control
{
	private int chest_nomber=0;
	[Export] public PackedScene chest {get;set;}
	public static bool time = false;
	[Export]Label text{get;set;}
	[Export]private ProgressBar value{get;set;}
	[Export]Player player ;
    public void _on_xp()
	{
	  GD.Print("player xp =" +Player.xp);
	  value.Value=Player.xp;
	}

	public void _on_vale_text(string money)
	{
		text.Text = money;
	}
	private void _open_chest()
	{
		if (chest_nomber == 1)
		{
			return;
		}
		chest_nomber= 1;

		var cheste = (shest)chest.Instantiate() as shest;
	  AddChild(cheste);
	  cheste.GlobalPosition = player.GlobalPosition;

	   time = true;
	   
	}
}
