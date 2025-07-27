using Godot;
using System;
using System.Text.RegularExpressions;


public partial class Shest : PanelContainer
{
	Slot slot1;


	public static event Action time_start;
	public override void _Ready()
	{
		foreach (Slot slote3 in GetTree().GetNodesInGroup("slot"))
		{
			slote3.Qkod = "0" + slote3.Name;
		}
		Player.dead += offolover;
		Enemy.enemydeads += slots;
		Slot.slotchoise += ChengeSlot;
		//slotchoise+=ChengeSlot;
	}
	private void offolover()
	{
		Player.dead -= offolover;
		Enemy.enemydeads -= slots;
		Slot.slotchoise -= ChengeSlot;
	}

	private void slots()
	{
		if (!IsInstanceValid(this))
		{
			return;
		}
		foreach (Slot slote in GetTree().GetNodesInGroup("slot"))
		{
			Random random = new Random();
			int nomber = 0;
			nomber = random.Next(0, 6);
			var y = slote.GetGroups();
			if (!y.Contains("ui") && y.Contains("slot"))
			{
				slote.Qkod = nomber.ToString() + slote.Name;
				slote.element(nomber);
				//GD.Print(slote.Qkod + "  " + i++);
			}
		}
	

	}

	private void ChengeSlot(Slot slot)
	{

		if (slot1 == null)
		{
			slot1 = slot;
			slot1.GlobalPosition = new Vector2(slot1.GlobalPosition.X, slot1.GlobalPosition.Y - 30);

			return;
		}
		else
		{

			slot.GlobalPosition = new Vector2(slot.GlobalPosition.X, slot.GlobalPosition.Y + 30);

		}

		if (slot1 == slot)
		{
			//slot.GlobalPosition = new Vector2( slot.GlobalPosition.X , slot.GlobalPosition.Y+20 );

			slot1 = null;
		}
		else
		{

			if (IsInstanceValid(slot) && IsInstanceValid(slot1))
			{
				string slotcode = slot.Qkod.Remove(1);


				slot.Qkod = slot1.Qkod;
				slot1.Qkod = slotcode + slot1.Name;

				slot.element(Convert.ToInt32(slot1.Qkod.Remove(1)));
				slot1.element(slotcode.ToInt());


				slot1.GlobalPosition = new Vector2(slot1.GlobalPosition.X, slot1.GlobalPosition.Y + 30);
				slot.GlobalPosition = new Vector2(slot.GlobalPosition.X, slot.GlobalPosition.Y - 30);

				slot1 = null;
			}
		}
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{

	}

	public void _on_button()
	{
		UiPcPlaer.nomber_open_chest = 0;
		Visible = false;
		if (time_start != null)
		{
			time_start?.Invoke();
		}
	}
}
