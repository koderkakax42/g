using Godot;
using System;
using System.Text.RegularExpressions;
using System.Xml.XPath;

public partial class ui_pc_player : Control
{
	public static int on_open_chest = 0;

	private bool true_chest = false;

	[Export] public PackedScene chest;

	[Export]Label text{get;set;}

	[Export]private ProgressBar value{get;set;}

	[Export]Player player;

    public void _on_xp()
	{
	  GD.Print("player xp =" +Player.xp);
	  value.Value=Player.xp;
	}

	public void _on_vale_text(string money)
	{
		text.Text = "  "+money;
	}
	private void _open_chest()
	{
      True_open_chest();
    
	if (chest == null)
	{
		GD.PushError("null chest");
		return;
	}
	  

      switch (on_open_chest)
	  {
		case  1 :
		      GD.Print("one one zero one ");
		      if (true_chest== true)
			  {
				OpenChest();
			  }
		break;

		case 0 :
		     if (true_chest== false)
			 {
				
				GD.PushError(on_open_chest);

				on_open_chest++;
				_open_chest();
			 }
		break;

		default:
		       on_open_chest = 1;
		break;
	  }

	}

	private void True_open_chest()
	{
	  on_open_chest = Math.Clamp(on_open_chest , 0 , 1 );
	  //on_open_chest = 0;
      true_chest = Convert.ToBoolean(on_open_chest);
	}

	private void OpenChest()
	{
    var scene = (shest)chest.Instantiate();			
	GetParent().AddChild(scene);
	scene.GlobalPosition = player.GlobalPosition;
	}
	
}
