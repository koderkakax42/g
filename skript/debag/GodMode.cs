using Godot;
using System;

public partial class GodMode : Button
{
    private void god_mod()
	{
     //okh Player.xp = int.MaxValue;
	  Atack.damage = int.MaxValue;
	  QueueFree();
	}
}
