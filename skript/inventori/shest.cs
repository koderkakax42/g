using Godot;
using System;
using System.Text.RegularExpressions;


public partial class Shest : PanelContainer
{
    int i = 0 ;
	public static event Action time_start;
	public override void _Ready()
	{
		Enemy.enemydeads += slots;
	}
	private void slots()
	{
	  foreach(Slot slote  in  GetTree().GetNodesInGroup("slot"))
	  {
		Random random = new Random();
		int nomber = random.Next(0,6);
		var y = slote.GetGroups();
		if(!y.Contains("ui")&& y.Contains("slot"))
		{
		slote.Qkod = nomber.ToString() + " " + slote.Name;
		slote.element(nomber);
		GD.Print(slote.Qkod + "  " + i++);
		}
	  }
	  i = 0;

	}
	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public void _on_button() 
	{
		time_start?.Invoke();
	   UiPcPlaer.nomber_open_chest=0;	
	   Visible = false;
	}
}
