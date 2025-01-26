using Godot;
using System;

public partial class Atack : Area2D
{
	public string TargetGroup = "enemy";

	public override void _Ready()
	{
        BodyEntered += _OnBodyEntered;

	}
	 private void _OnBodyEntered(Node2D body)
	{
		 if (TargetGroup != "enemy" && body.IsInGroup(TargetGroup))
       return;

	   QueueFree();


	}
	public override void _Process(double delta)
	{



	}
}
