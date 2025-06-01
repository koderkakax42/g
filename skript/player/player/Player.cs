using Godot;
using System.Text;
using System;
using System.Collections.Generic;
using System.Linq;

public partial class Player : CharacterBody2D
{
	public static event Action dead = delegate { };
	public String TargetScenePath = "res://scene/scen/load_scen/fader.tscn";
	public  event Action startbullit = delegate { };
	[Export] UiPcPlaer UI { get; set; } = null!;
	public int Health = 400;
	public Vector2 inputDirection;
	public PackedScene BulletScene = null!; // Сцена пули
	[Export] public float Speed = 900;
	[Export] public float FireRate = 2f; // Выстрелов в секунду
	[Export] public float BulletSpeed = 400f;
	private int ValueMoney = 0;
	private float _timeSinceLastFire = 0f;
	public AnimatedSprite2D _animatedSprite = null!;
	public string money = "0";
	public Enemy enemy2;
	[Export] Deteckt? deteckt = null;
#if DEBUG
	private static PackedScene scene;
	bool debag = false;
	public void star()
	{
		debag = true;
		scene = GD.Load<PackedScene>("res://scene/debag/console.tscn");
		GD.Print("gd.print debag ");
		var i = (Console)scene.Instantiate();
		GetParent().AddChild(i);
		i.GlobalPosition = GetGlobalMousePosition();

	}
#endif
	public override void _Ready()
	{
		_animatedSprite = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
		BulletScene = GD.Load<PackedScene>("res://scene/atack/atack/atack.tscn");

		if (UI == null)
			GD.Print("null UI ");
	}
	private void health()
	{
		Health = 400;
	}
	public void DamageEnemys(int damage)
	{
		if (damage == Health)
		{
			GD.Print(damage);
			GD.Print(Health);
		}
		Health -= damage;

		UI._on_xp(Health);

	}
	private void LoadNewScene()
	{
		health();
		Speed = 400;
		// Получаем SceneTree
		SceneTree tree = GetTree();

		// Останавливаем текущую сцену.  Это ВАЖНО.
		tree.ChangeSceneToFile(TargetScenePath);
	}
	public override void _PhysicsProcess(double delta)
	{


		if (Input.IsActionPressed("ui_right")
		 || Input.IsActionPressed("ui_left")
		 || Input.IsActionPressed("ui_up")
		 || Input.IsActionPressed("ui_down"))
		{
			_animatedSprite.Play("run");
		}
		else
		{
			_animatedSprite.Pause();
		}


		_timeSinceLastFire += (float)delta;


		// Стрельба
		if (Input.IsActionPressed("shoot") && _timeSinceLastFire >= 1f / FireRate)
		{
			Shoot();
			_timeSinceLastFire = 0f;
		}
		if (Input.IsActionPressed("markshoot") && _timeSinceLastFire >= 1f / FireRate)
		{
			Shoot(1);
			_timeSinceLastFire = 0f;
		}

		if (Input.IsActionPressed("mark"))
		{
			deteckt.MarkTarget();
		}



		if (Health <= 0)
		{
			_animatedSprite.Play("dead");
			deads();
		}
		GetInput();
		MoveAndSlide();
	}
	public void deads()
	{
		dead.Invoke();
		Fader.ScenePath = "res://scene/ui/meny/meny.tscn";

		Speed = 0;
		Velocity = inputDirection * Speed;

		var i = new Godot.Timer();
		AddChild(i);
		i.WaitTime = 1.5f;
		i.OneShot = true;
		i.Timeout += LoadNewScene;
		i.Start();
	}

	private void Shoot()
	{
		if (BulletScene == null)
		{
			GD.PrintErr("BulletScene is not assigned!");
			return;
		}
		var bullet = (Atack)BulletScene.Instantiate();
		GetParent().AddChild(bullet);
		bullet.GlobalPosition = GlobalPosition;
		atackelement(bullet);
		bullet.SetDirection();
		bullet.Speed = BulletSpeed;
		bullet.Player = this; //  Добавляем ссылку на игрока, чтобы пуля знала, кто ее выпустил
	}
	private void Shoot(int? @int)
	{
		if (deteckt._markedEnemies.Count > 0)
		{
			foreach (Enemy enemy in deteckt._markedEnemies)
			{
				enemy2 = enemy;
			}

			var bullet = (Atack)BulletScene.Instantiate();
			GetParent().AddChild(bullet);
			bullet.GlobalPosition = GlobalPosition;
			atackelement(bullet);
			bullet.SetDirection(enemy2.GlobalPosition);
			bullet.Speed = BulletSpeed;
			bullet.Player = this;
		}
		else
		{
			GD.Print("not shoot");
			return;
		}
	}

	public void GetInput()
	{
		inputDirection = Input.GetVector("ui_left", "ui_right", "ui_up", "ui_down");
		Velocity = inputDirection * Speed;
	}
	public void moneyvalue()
	{
		ValueMoney++;
		money = ValueMoney.ToString();
		UI._on_vale(money);
	}

	private void atackelement(Atack atack)
	{

		for (int i = 0; i < UI.slotarei.Count();)
		{
			Atack.areaelementnomber[i]  = UI.slotarei[i].Qkod.Remove(1).ToInt();
			i++;
		}

		atack.elementarreislot = UI.slotarei;
		atack.elementatack(0,atack);
	}

}
