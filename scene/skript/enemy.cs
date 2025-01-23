using Godot;

public partial class enemy : CharacterBody2D
{
      int speed = 1000; 
    public override void _PhysicsProcess(double delta)
    {
		Move();
        MoveAndSlide();
    }
     
	 public void Move()
	 {
       if ( GetNode<CharacterBody2D>("Player") != null)
	   {
         LookAt(GetNode<CharacterBody2D>("Player").GlobalPosition);
		 Vector2 direction = (GetNode<CharacterBody2D>("Player").GlobalPosition - GlobalPosition).Normalized(); 
        Velocity = direction * speed;
	   }
      else
	  {

        Velocity = Vector2.Zero;

	  }

	 }




}
