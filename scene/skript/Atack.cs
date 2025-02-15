using Godot;

namespace Atack
{
 public partial class Atack :CharacterBody2D
  {
   [Export] public  int Speed = 350 ;
  private Node2D target;


    public override void _Ready()
    {
       

        target = GetTree().GetFirstNodeInGroup("enemy")as Node2D;
        if(target == null)
        {
            GD.PrintErr("plaer error 404");
        
        }
    }

   public  override void _PhysicsProcess(double delta)
    {
       
    

      Vector2 direction = (target.GlobalPosition-GlobalPosition).Normalized();
      
       Velocity = direction*Speed;
       MoveAndSlide();
    }
  }

  public partial class AtackDirection : Area2D
 {
  
 
    [Export] public int Damage = 10;      
   

    private void OnAreaEntered(Area2D area)
    {
        // Проверяем, попала ли пуля во врага
         if (area.GetParent() is enemy enemy)
         {
             // Наносим урон врагу
             enemy.TakeDamage(Damage);
              // Удаляем пулю
             QueueFree();
         }

    }
    public override void _Ready()
    {
      AreaEntered += OnAreaEntered;

   
    }

     public override void _ExitTree()
    {
      AreaEntered -= OnAreaEntered;
    }
   
    


 }
    



} 