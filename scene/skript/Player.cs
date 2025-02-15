using Godot;

public partial class Player: CharacterBody2D
{

     public int Damage = 10;
    [Export]public int Speed { get; set; } = 400;
   [Export] public PackedScene BulletScene { get; set; } // Сцена пули
    
    
 

     private AnimatedSprite2D _animatedSprite;

     public int xp = 200 ;

     public Vector2 inputDirection;
     
  private void Shoot()
    {
        if (BulletScene == null)
        {
            GD.PrintErr("Ошибка: Сцена пули не задана!");
            return;
        }


        Atack.Atack bulletInstance = BulletScene.Instantiate<Atack.Atack>();

        bulletInstance.Position = GlobalPosition;

          GetParent().AddChild(bulletInstance);
          
       
       
    }


     
    public override void _Ready ()
    {
       
        

       _animatedSprite = GetNode<AnimatedSprite2D>("AnimatedSprite2D");

    }
    private void OnAreaEntered(Area2D area)
    {
        // Проверяем, попала ли пуля во врага
         if (area.GetParent() is enemy enemy)
         {
             // Наносим урон врагу
             enemy.TakeDamage(Damage);

         }

         
    }  
    public void _XPBAR(ProgressBar Value)
    {
      Value.Value = xp / 2;

      

    }


     public void enamyDemage(int damage)
    {
       xp -= damage;

        if (xp <= 0)
        {
          
            QueueFree();
        }
    }

    public void GetInput()
    {
         
        Vector2 inputDirection = Input.GetVector("ui_left", "ui_right", "ui_up", "ui_down");
        Velocity = inputDirection * Speed;
    }

    public override void _PhysicsProcess(double delta)
    {
         
      if (Input.IsActionPressed("ui_atack"))
        {

           Shoot();
          
         
          _animatedSprite.Play("attak");

          
          Velocity = inputDirection*50;
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


        GetInput();
        MoveAndSlide();
    }
}