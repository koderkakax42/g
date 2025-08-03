using Godot;
using System;
using System.Linq;

public partial class Enemy : CharacterBody2D
{
	bool boss = false;
	Vector2 direction;
	Godot.Timer timetolive;
	int poisontime = 0;
	[Export] public int Health = 100;
	[Export] public int Speed = 350;
	public int Damage = 10;
	public static event Action enemydeads = delegate { };
	private Node2D target = null!;
	public Area2D Body = null!;
	float time = 10;
	public PackedScene moneyscene { get; set; } = null!;
	Spawn spawn = new Spawn();
	private Texture2D Texture2D;
	Sprite2D air;
	private AnimatedSprite2D _animatedSprite = null!;
	bool atack = true;
	float attaimer;
	public PackedScene BulletScene = null!;
	public string EnemyId { get; set; } = Guid.NewGuid().ToString();
	AnimationTree animation = new AnimationTree();
	AnimationNodeStateMachinePlayback _stateMachine;

	public void LoadData(float X, float Y, float health, string id, Node2D player)
	{
		GlobalPosition = new Vector2(X, Y);
		Health = (int)health;
		EnemyId = id;
		target = player;
	}
	public override void _Ready()
	{
		EnemyId = EnemyId.Remove(5);
		moneyscene = GD.Load<PackedScene>("res://scene/drop/money.tscn");

		Body = GetNode<Area2D>("hitbox");

		Body.BodyEntered += OnBodyEntered;

		target = GetTree().GetFirstNodeInGroup("Player") as Node2D;

		_animatedSprite = GetNode<AnimatedSprite2D>("AnimatedSprite2D");

		BulletScene = GD.Load<PackedScene>("res://scene/atack/atack/atack.tscn");
		if (boss)
		{
			animation = GetNode<AnimationTree>("AnimationTree");
			animation.Active = true;
			_stateMachine = (AnimationNodeStateMachinePlayback)animation.Get("parameters/playback");
			_stateMachine.Start("Start");
		}
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

	public void effect()
	{
		timetolive = new Godot.Timer();
		AddChild(timetolive);
		timetolive.WaitTime = 1;
		timetolive.Timeout += poison;
		timetolive.Start();
	}

	private void poison()
	{
		Health -= 5;
		if (Health <= 0)
		{
			CallDeferred("dead");
		}
		poisontime++;
		if (poisontime >= 4)
		{
			poisontime = 0;
			GD.Print("poison enemy");
			timetolive.Stop();
			return;
		}
	}

	private void _QueueFree()
	{
		air.QueueFree();
		GD.Print("time stop");

	}

	private void OnBodyEntered(Node2D body)
	{
		// GD.Print(Damage);
		// Проверяем, что столкнулись с врагом и что это не сам игрок
		if (body is Player player&&atack)
		{
			atack = false;
			//GD.Print(Damage);
			player.DamageEnemys(Damage);
			TakeDamage(10);
		}


	}
	public void TakeDamage(int damage)
	{
		Health -= damage;

		if (Health <= 0)
		{
			Godot.Timer timetolive = new Godot.Timer();
			AddChild(timetolive);
			timetolive.WaitTime = 0.3;
			timetolive.OneShot = true;
			timetolive.Timeout += dead;
			timetolive.Start();
		}

		if (boss)
		{
			bullshit();
		}
	}


	private void bullshit()
	{
		var bullet = (Atack)BulletScene.Instantiate();
		GetParent().AddChild(bullet);
		Atack.areaelementnomber = effectAtack();
		bullet.elementatack(0, bullet);
		bullet.damage = 50;
		bullet.GlobalPosition = GlobalPosition;
		bullet.SetDirection(target.GlobalPosition);
		bullet.Enemy = this;

		_stateMachine.Travel("atack");
		Speed = 0;

		timetolive = new Godot.Timer();
		AddChild(timetolive);
		timetolive.WaitTime = 2;
		timetolive.OneShot = true;
		timetolive.Timeout += startrun;
		timetolive.Start();
	}

	private void startrun()
	{
		Speed = 800;
	}

	private int[] effectAtack()
	{
		int[] nomber = new int[5];
		Random random = new Random();
		for (int i = 0; i < Atack.areaelementnomber.Count();)
		{
			nomber[i] = random.Next(0, 6);
			i++;
		}
		return nomber;
	}

	private void dead()
	{
		spawnmoney();
		enemydeads?.Invoke();
		QueueFree();
	}

	public override void _PhysicsProcess(double delta)
	{
		if (!atack && attaimer >= 2)
		{
			attaimer = 0;
			atack = true;
		}

		attaimer += (float)delta;

		if (Health <= 0)
		{
			_animatedSprite.Play("dead");
			return;
		}

		if (target == null) return;

		direction = (target.GlobalPosition - GlobalPosition).Normalized();


		Velocity = direction * Speed;

		if (target != null && !boss)
			_animatedSprite.Play("run");
		else
		{
			if (!boss)
			{
				_animatedSprite.Stop();
			}
		}

		MoveAndSlide();
	}

	public void bossism()
	{
		Health = 1000;
		Damage = 100;
		Speed = 800;
		boss = true;
	}


}
