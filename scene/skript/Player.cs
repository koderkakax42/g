using Godot;

public partial class Player: CharacterBody2D
{
    [Export]
    public int Speed { get; set; } = 400;
     
     private AnimatedSprite2D _animatedSprite;
     
    public override void _Ready ()
    {

       _animatedSprite = GetNode<AnimatedSprite2D>("AnimatedSprite2D");

    }

    public void GetInput()
    {

        Vector2 inputDirection = Input.GetVector("ui_left", "ui_right", "ui_up", "ui_down");
        Velocity = inputDirection * Speed;
    }

    public override void _PhysicsProcess(double delta)
    {

         if(Input.IsActionPressed("ui_right")
         ||Input.IsActionPressed("ui_left")
         ||Input.IsActionPressed("ui_up")
         ||Input.IsActionPressed("ui_down") )
         _animatedSprite.Play("run");
         else
         _animatedSprite.Stop();

        GetInput();
        MoveAndSlide();
    }
}