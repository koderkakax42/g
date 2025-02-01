using Godot;

public partial class Player: CharacterBody2D
{


    [Export]
    public int Speed { get; set; } = 400;



  [Export] public PackedScene BulletScene { get; set; } // Сцена пули
  [Export] public float BulletSpeed = 400.0f;       // Скорость пули
    
  public override void _UnhandledInput(InputEvent @event)
  {
        if (@event is InputEventMouseButton mouseEvent 
        && mouseEvent.Pressed 
        && mouseEvent.ButtonIndex == MouseButton.Left)
        {
           Shoot();
        }
  }

  private void Shoot()
    {
        if (BulletScene == null)
        {
            GD.PrintErr("Ошибка: Сцена пули не задана!");
            return;
        }

      // Инстанцируем сцену пули
        Atack bulletInstance = BulletScene.Instantiate<Atack>();

       // Устанавливаем позицию пули
        bulletInstance.Position = GlobalPosition;

      // Получаем направление выстрела от игрока к мыши
        var direction = (GetGlobalMousePosition() - GlobalPosition).Normalized();
        bulletInstance.Direction = direction;
        bulletInstance.Speed = BulletSpeed;

       if(Input.IsActionPressed("bullit"))
        GetParent().AddChild(bulletInstance);
    }


     
     private AnimatedSprite2D _animatedSprite;

     public int xp = 200 ;

     public Vector2 inputDirection;
     
    public override void _Ready ()
    {
        var scene = GD.Load<PackedScene>("res://scene/atack.tscn");
        var instance = scene.Instantiate();
        AddChild(instance);

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

         if(xp <= 0)
         {
            _animatedSprite.Play("dead");

            Speed=0;
            Velocity = inputDirection * Speed;
         }

        GetInput();
        MoveAndSlide();
    }
}

public partial class _Player : Area2D
{
  
  
    
    

  



}