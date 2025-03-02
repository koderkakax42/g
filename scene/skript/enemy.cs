using Godot;
using System.Threading.Tasks;
using System.Xml.XPath;






public partial class enemy : CharacterBody2D 
{
   [Export] public int Health = 100;  // Здоров
 
    [Export] public PackedScene moneyscene;
  [Export]
  public int Speed = 350 ;

  public int Damage = 10 ;
  
  private NavigationAgent2D _navigationAgent;
  private Node2D target;

public Area2D Body;
  public Atack atack;

  private AnimatedSprite2D _animatedSprite;

  private void SpavnMoney()
  {
    var money = (money)moneyscene.Instantiate();
    GetParent().AddChild(money);
    money.GlobalPosition = GlobalPosition;
    
  }
    public override void _Ready()
    {

      Body = GetNode<Area2D>("hitbox");

       
       if (Body == null)
        {GD.PrintErr("ytytyyty");}

        Body.BodyEntered += OnBodyEntered;


        target = GetTree().GetFirstNodeInGroup("Player")as Node2D;
        if(target == null)
        {
            GD.PrintErr("plaer error 404");
        
        }

       _animatedSprite = GetNode<AnimatedSprite2D>("AnimatedSprite2D");


    }
   private void OnBodyEntered(Node2D body)
    {
       GD.Print(Damage +"   "+Speed+"   "+body+"    ");
        // Проверяем, что столкнулись с врагом и что это не сам игрок
        if (body is Player player )
        {
          GD.Print(Damage +"  ee "+Speed+"   www"+body+"   rr ");
            player.DamageEnemys(Damage);
        }
    
    }     
    public void TakeDamage(int damage)
    {
        Health -= damage;

        if (Health <= 0)
        {
          
            QueueFree();
            SpavnMoney();
        }
    }


    
    public override void _PhysicsProcess(double delta)
    {
      if (Health <= 0 )
      {
        SpavnMoney();
      }
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



