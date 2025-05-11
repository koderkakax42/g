using Godot;
using System;

public partial class Enemy : CharacterBody2D
{
      [Export] public int Health = 100;  
  [Export]public int Speed = 350 ;
  public int Damage = 10 ;
public static event Action enemydeads= delegate{}; 
  private Node2D target = null!;
public Area2D Body = null!;
 float time = 10;
public PackedScene moneyscene{get;set;} = null!;
 Spawn spawn = new Spawn();
   private Texture2D Texture2D ;
   Sprite2D air; 
  private AnimatedSprite2D _animatedSprite = null!;
  
	public string EnemyId { get; set; } = Guid.NewGuid().ToString("N") ;

	public void LoadData()
	{
	  
	}
	public override void _Ready()
	{
	  EnemyId.Substring(0,5);
	  moneyscene=GD.Load<PackedScene>("res://scene/drop/money.tscn");
	 
	  Body = GetNode<Area2D>("hitbox");

		Body.BodyEntered += OnBodyEntered;

		target = GetTree().GetFirstNodeInGroup("Player")as Node2D;

	   _animatedSprite = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
	 

	}
	private void spawnmoney()
	{
		var bullet = (Money)moneyscene.Instantiate() as Money;
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
			enemydeads?.Invoke();
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
