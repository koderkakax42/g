using Godot;
using System;

public partial class Player : CharacterBody2D
{
	Vector2	velocity = new Vector2();
	
	 int speed = 200;
	
	public void _PhysicsProcess(float delta)
	{
		  velocity = Vector2.Zero;
		if (Input.IsActionPressed("ui_right"))
		{
			velocity.X += 1;
		}
		if (Input.IsActionPressed("ui_left"))
		{
			velocity.X -= 1;
		}
		if (Input.IsActionPressed("ui_down"))
		{
			velocity.Y += 1;
		}
		if (Input.IsActionPressed("ui_up"))
		{
			velocity.Y -= 1;
		}

		velocity = velocity.Normalized() * speed;
		MoveAndSlide();
	}
}
