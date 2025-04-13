using Godot;
using System.Threading.Tasks;
using System.Xml.XPath;
using System;






public partial class enemy : CharacterBody2D 
{
   [Export] public int Health = 100;  
  [Export]public int Speed = 350 ;
  public int Damage = 10 ;

  private Node2D? target = null!;
public Area2D Body = null!;
 float time = 10;
public PackedScene moneyscene{get;set;} = null!;
 spawn spawn = new spawn();
   private Texture2D Texture2D ;
   Sprite2D air; 
  private AnimatedSprite2D _animatedSprite = null!;
  
  [Export] public string EnemyId { get; set; } = Guid.NewGuid().ToString(); // Уникальный ID
    
    public SaveGame.GameData GetSaveData()
    {
      GD.Print(EnemyId + " save enemy");
        return new SaveGame.GameData()
        {
            EnemyId = this.EnemyId,
            enemyposition = this.GlobalPosition,
            enemyhp = Health
        };
    }

    public void LoadData()
    {
      
    }
    public override void _Ready()
    {
      moneyscene=GD.Load<PackedScene>("res://scene/drop/money.tscn");
     
      Body = GetNode<Area2D>("hitbox");

        Body.BodyEntered += OnBodyEntered;

        target = GetTree().GetFirstNodeInGroup("Player")as Node2D;

       _animatedSprite = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
     

    }
    private void spawnmoney()
    {
        var bullet = (money)moneyscene.Instantiate() as money;
        GetParent().AddChild(bullet);
        bullet.GlobalPosition = GlobalPosition;
    }
public void mark()
{
   air = new Sprite2D();
     Texture2D = GD.Load<Texture2D>("res://sprait/ui/mark/mark.png");
     air.Texture = Texture2D;
     AddChild(air);
     air.GlobalPosition = GlobalPosition;
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
 

}



