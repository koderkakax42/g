using Godot;
using System;
using System.Text.RegularExpressions;


public partial class Shest : PanelContainer
{
	Slot slot1;
	
    int i = 0 ;
	public static event Action time_start;
	public override void _Ready()
	{
	  foreach(Slot slote3  in  GetTree().GetNodesInGroup("slot"))
	  {
		slote3.Qkod = "0" + slote3.Name;
		GD.Print(slote3.Qkod);
	  }
		Enemy.enemydeads += slots;
		Slot.slotchoise += ChengeSlot;
		//slotchoise+=ChengeSlot;
	}

	private void slots()
	{
	  foreach(Slot slote  in  GetTree().GetNodesInGroup("slot"))
	  {
		Random random = new Random();
		int nomber = 0 ;
		nomber = random.Next(0,6);
		var y = slote.GetGroups();
		if(!y.Contains("ui")&& y.Contains("slot"))
		{
		slote.Qkod = nomber.ToString() +  slote.Name;
		slote.element(nomber);
		//GD.Print(slote.Qkod + "  " + i++);
		}
	  }
	  i = 0;

	}

	private void ChengeSlot(Slot slot)
	{

		if(slot1 == null)
		{
          slot1 = slot;
		  slot1.GlobalPosition = new Vector2( slot1.GlobalPosition.X, slot1.GlobalPosition.Y-20 );
		  GD.Print("shest 1");
		  return;
		}
		else
		{
		
				slot.GlobalPosition = new Vector2( slot.GlobalPosition.X , slot.GlobalPosition.Y+20 );
				  GD.Print("shest 2");
		}

		if (slot1 == slot)
		{
			//slot.GlobalPosition = new Vector2( slot.GlobalPosition.X , slot.GlobalPosition.Y+20 );
				  GD.Print("shest 3.1");
				  slot1 = null;
		}
		else
		{
				  GD.Print("shest 3");

			string slotcode = slot.Qkod.Remove(1);
			string slot1code = slot1.Qkod.Remove(1);

			slot.Qkod = slot1code + slot.Name;
			slot1.Qkod = slotcode + slot1.Name;

			slot.element(slot1code.ToInt());
			slot1.element(slotcode.ToInt());


			slot1.GlobalPosition = new Vector2( slot1.GlobalPosition.X , slot1.GlobalPosition.Y+20 );
			slot.GlobalPosition = new Vector2( slot.GlobalPosition.X , slot.GlobalPosition.Y-20 );

			slot1 = null;
		}

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
