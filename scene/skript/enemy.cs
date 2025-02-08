using Godot;
using System.Threading.Tasks;






public partial class enemy : CharacterBody2D 
{
   [Export] public int Health = 100;  // Здоров
 
  [Export]
  public int Speed = 350 ;

  
  
  private NavigationAgent2D _navigationAgent;
  private Node2D target;

  private AnimatedSprite2D _animatedSprite;
    public override void _Ready()
    {
       

        target = GetTree().GetFirstNodeInGroup("Player")as Node2D;
        if(target == null)
        {
            GD.PrintErr("plaer error 404");
        
        }

       _animatedSprite = GetNode<AnimatedSprite2D>("AnimatedSprite2D");


    }
    public void TakeDamage(int damage)
    {
        Health -= damage;

        if (Health <= 0)
        {
            // Уничтожение врага
            QueueFree();
        }
    }

    public override void _PhysicsProcess(double delta)
    {
    
      if (target == null ) return ;

      Vector2 direction = (target.GlobalPosition-GlobalPosition).Normalized();
      
       Velocity = direction*Speed;


       if(target != null)
       _animatedSprite.Play("run");
       else
       _animatedSprite.Stop();
       

        
       

       MoveAndSlide();
    }
    

}



public partial class _enemy : Area2D
{
  [Export]
  public int xp = 100;

  public string TargetGroup = "Atack";

   private AnimatedSprite2D _animatedSprite;
    public override void _Ready()
    {
        BodyEntered += _OnBodyEntered;
        BodyExited += _OnBodyExited; 
    }

    private void _OnBodyEntered(Node2D body)
    {
       if (TargetGroup != "Atack" && body.IsInGroup(TargetGroup))
       return;


       if(xp > 0)
       {
        
        Task.Delay(500);

        xp = xp - 50;
       }
       
      

        if(xp <= 0)
        {
          _animatedSprite.Play("dead");
          
          QueueFree();
        }

      
      
    }
     
     private void _OnBodyExited(Node2D body) 
     {
      if (xp > 0 && xp < 100 )
      {
         Task.Delay(500);

        xp = xp + 1;

      }
       
       if (TargetGroup != "Atack" && body.IsInGroup(TargetGroup))
       return;

     }


}