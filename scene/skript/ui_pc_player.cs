using Godot;
using System;
using System.Xml.XPath;

public partial class ui_pc_player : Control
{
	ProgressBar value;
	public Player player;
   
    public void _on_xp()
	{
	  GD.Print("player xp =" +Player.xp);
	  value.Value=Player.xp;
	}
	
}
