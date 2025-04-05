using Godot;
using System.Threading.Tasks;
using System.Xml.XPath;
using System;






public partial class enemy : CharacterBody2D 
{
   [Export] public int Health = 100;  
  [Export]public int Speed = 350 ;

  GameData gameData = new GameData();

  public int Damage = 10 ;

  private Node2D? target = null!;
public Area2D Body = null!;
 float time = 10;
[Export]public PackedScene moneyscene{get;set;} = null!;
 spawn spawn = new spawn();
   private Texture2D Texture2D ;
   Sprite2D air = new Sprite2D(); 

  
 [Export] public int CoinSpawnChance = 100;
  private AnimatedSprite2D _animatedSprite = null!;
  
       public override void _Ready()
    {
     
     
      Body = GetNode<Area2D>("hitbox");

       
        Body.BodyEntered += OnBodyEntered;
       if (Body == null)
        {
          GD.PrintErr("null body enemy");
          return;
        }

        target = GetTree().GetFirstNodeInGroup("Player")as Node2D;
        if(target == null)
        {
            GD.PrintErr("player error 404");
           return;
        }

       _animatedSprite = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
     
      Damage = 10;

    }
    private void spawnmoney()
{

     var bullet = (money)moneyscene.Instantiate() as money;
        GetParent().AddChild(bullet);
        bullet.GlobalPosition = GlobalPosition;
   
}
private void timer()
{
 Godot.Timer timetolive = new Godot.Timer();
      AddChild(timetolive);
      timetolive.WaitTime = time;
      timetolive.Timeout += _QueueFree;
      timetolive.Start();
}
public void mark()
{
   if (GodotObject.IsInstanceValid(air))
   {
     Texture2D = GD.Load<Texture2D>("res://sprait/mark.png");
     air.Texture = Texture2D;
    AddChild(air);
     air.GlobalPosition = GlobalPosition;
     
     timer();
    
    }  
    else
    {
      air = new Sprite2D();
      GD.Print("air delete but restavresion");
      mark();
    }
}
  
  private void _QueueFree()
  {
    air.QueueFree();
    GD.Print("time stop");
    
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
  public void savedata()
  {
    gameData.enemyhp = Health;
 //   GD.Print("save enemy is truy");
  }    

}



