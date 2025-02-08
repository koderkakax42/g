using Godot;

public partial class Player: CharacterBody2D
{

  private float _timer = 0.0f;  
    [Export]
    public int Speed { get; set; } = 400;

  public Node2D targ;
  [Export] public PackedScene BulletScene { get; set; } // Сцена пули
  [Export] public float BulletSpeed = 400.0f;       // Скорость пули
    
  public override void _UnhandledInput(InputEvent @event)
  {
        
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
     

       if(Input.IsActionPressed("bullit") )
       {
         
          GetParent().AddChild(bulletInstance);
          
       }
       
    }


     
     private AnimatedSprite2D _animatedSprite;

     public int xp = 200 ;

     public Vector2 inputDirection;
     
    public override void _Ready ()
    {
        
       targ =GetTree().GetFirstNodeInGroup("enemy")as Node2D;

       _animatedSprite = GetNode<AnimatedSprite2D>("AnimatedSprite2D");

    }

    public void GetInput()
    {
         
        Vector2 inputDirection = Input.GetVector("ui_left", "ui_right", "ui_up", "ui_down");
        Velocity = inputDirection * Speed;
    }

    public override void _PhysicsProcess(double delta)
    {
          _timer += (float)delta;
      if (Input.IsActionPressed("bullit"))
        {

           Shoot();
          _timer = 0.0f;
        }

        
         

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

         if (Input.IsActionPressed("bullit")){
          _animatedSprite.Play("attak");

          
          Velocity = inputDirection*0;

         }

        GetInput();
        MoveAndSlide();
    }
}