using Godot;
using System.Text;
using System;
using System.Collections.Generic;
using System.Linq;

public partial class Player : CharacterBody2D
{
	public static event Action dead = delegate { };
	public String TargetScenePath = "res://scene/scen/load_scen/fader.tscn";
	UiPcPlaer UI { get; set; } = null!;
	public int Health = 100;
	public Vector2 inputDirection;
	public PackedScene BulletScene = null!; // Сцена пули
	[Export] public float Speed = 900;
	[Export] public float FireRate = 2f; // Выстрелов в секунду
	[Export] public float BulletSpeed = 400f;
	public int ValueMoney { private set; get; } = 0;
	private float _timeSinceLastFire = 0f;
	public AnimatedSprite2D _animatedSprite = null!;
	public Enemy enemy2;
	[Export] Deteckt deteckt = null;

	public override void _Ready()
	{
		_animatedSprite = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
		BulletScene = GD.Load<PackedScene>("res://scene/atack/atack/atack.tscn");		
		CallDeferred("i");
	}
	private void i()
	{
		PackedScene scene = GD.Load<PackedScene>("res://scene/ui/ui_pc_plaer.tscn");
		UI = (UiPcPlaer)scene.Instantiate();
		GetParent().AddChild(UI);
		UI.player = this;
		UI.value.MaxValue = Health;
	}
	private void health()
	{
		Health = 100;
	}
	public void DamageEnemys(int damage)
	{
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
			if (Health >= 1 && _animatedSprite != null)
			{
				_animatedSprite.Pause();
			}
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
			Shoot(0);
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
	private void Shoot(int? nomberreturn)
	{
		if (deteckt._markedEnemies.Count > 0)
		{
			if (deteckt._markedEnemies[0] is Enemy enemy)
			{
				if (IsInstanceValid(enemy))
				{
					enemy2 = enemy;
				}
				else
				{
					deteckt._markedEnemies.Remove(enemy);

					return;

				}
			}
			else
			{
				deteckt._markedEnemies.RemoveAt(0);

				return;

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
		Rotation = Mathf.Atan2(inputDirection.Y, inputDirection.X);
	}
	public void moneyvalue()
	{
		ValueMoney++;
		UI._on_vale(ValueMoney.ToString());
	}

	private void atackelement(Atack atack)
	{

		for (int i = 0; i < Atack.areaelementnomber.Count();)
		{
			Atack.areaelementnomber[i] = UI.slotarei[i].Qkod.Remove(1).ToInt();
			i++;
		}

		atack.elementarreislot = UI.slotarei;
		atack.elementatack(0, atack);
	}

	public void LoadDate(float X, float Y, float health, float Money)
	{
		GlobalPosition = new Vector2(X, Y);
		Health = (int)health;
		ValueMoney = (int)Money;
		UI._on_vale(Money.ToString());
	}
}
