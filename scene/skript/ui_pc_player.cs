using Godot;
using System;
using System.Xml.XPath;

public partial class ui_pc_player : Control
{
	public static int nomber_open_chest = 0;
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
      switch (chest_nomber)
	  {
		case 1 : 
		         chest_nomber++;
		         Openchest();
		break;		 
		default:
		        chest_nomber = 1;

				if (nomber_open_chest <= 0)
				{
					Openchest();
				} 
		break;
	  }
	}
    private void Openchest()
	{
		var cheste = (shest)chest.Instantiate() as shest;
	  AddChild(cheste);
	  cheste.GlobalPosition = player.GlobalPosition;

	  nomber_open_chest++;
	}
	
}
