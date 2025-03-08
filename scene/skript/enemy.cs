using Godot;
using System.Threading.Tasks;
using System.Xml.XPath;
using System;






public partial class enemy : CharacterBody2D 
{
   [Export] public int Health = 100;  // Здоров

  [Export]public int Speed = 350 ;


  public int Damage = 10 ;
  
  private NavigationAgent2D _navigationAgent;
  private Node2D target;

public Area2D Body;
  public Atack atack;
[Export]public PackedScene moneyscene;
 

 [Export] public int CoinSpawnChance = 100;
  private AnimatedSprite2D _animatedSprite;
  
       public override void _Ready()
    {
     
     
      Body = GetNode<Area2D>("hitbox");

       
       if (Body == null)
        {GD.PrintErr("null body enemy");}

        Body.BodyEntered += OnBodyEntered;


        target = GetTree().GetFirstNodeInGroup("Player")as Node2D;
        if(target == null)
        {
            GD.PrintErr("player error 404");
        
        }

       _animatedSprite = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
     


    }
    private void spawnmoney()
{

     var bullet = (money)moneyscene.Instantiate() as money;
        GetParent().AddChild(bullet);
        bullet.GlobalPosition = GlobalPosition;
   
}
    
   private void OnBodyEntered(Node2D body)
    {
     
        // Проверяем, что столкнулись с врагом и что это не сам игрок
        if (body is Player player )
        {
  
            player.DamageEnemys(Damage);
        }
    
    }     
    public void TakeDamage(int damage)
    {
        Health -= damage;
        if(Health<=0)
        {
          spawnmoney();
        QueueFree();
         } // SpawnCoin();
          
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



