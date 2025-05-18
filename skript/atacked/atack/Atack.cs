using Godot;
using System;
using System.Collections.Generic;


public partial class Atack : Area2D
{
 	[Export] PackedScene sceneeffect;
  AnimatedSprite2D animated;
  public static int damage = 20;
  public Vector2 Direction { get; set; }
  public float Speed = 900;
  public Player Player { get; set; } = new Player();
  public float time_bul = 7f;
  [Export] CollisionShape2D collision;
  public static int[] areaelementnomber = new int[5];
  // public List<PoisonEffect> poisonEffects = new List<PoisonEffect>(3);
 

  public override void _Ready()
  {
    BodyEntered += OnBodyEntered;
    // Создаем и настраиваем таймер
    live_bullet();
  sceneeffect = GD.Load<PackedScene>("res://scene/atack/effect/poisoneffect.tscn");
     
    animated = GetNode<AnimatedSprite2D>("CollisionShape2D/AnimatedSprite2D");
  }



  private void live_bullet()
  {
    Godot.Timer timetolive = new Godot.Timer();
    AddChild(timetolive);
    timetolive.WaitTime = time_bul;
    timetolive.OneShot = true;
    timetolive.Timeout += _QueueFree;
    timetolive.Start();



  }
  private void _QueueFree()
  {
    BodyEntered -= OnBodyEntered;
    QueueFree();
  }


  public override void _PhysicsProcess(double delta)
  {
    GlobalPosition += Direction * Speed * (float)delta;

    switch (areaelementnomber[0])
    {
      case 0:
        animated.Play("air");
        break;
      case 1:
        animated.Play("mars");
        break;
      case 2:
        animated.Play("neiron");
        break;
      case 3:
        animated.Play("poison");
        break;
      case 4:
        animated.Play("sun");
        break;
      case 5:
        animated.Play("woter");
        break;
      default:
        break;
    }

  }

  public void SetDirection(Vector2 targetPosition)
  {
    Direction = (targetPosition - GlobalPosition).Normalized();
    Rotation = Mathf.Atan2(Direction.Y, Direction.X); // Поворачиваем пулю в направлении движения
  }
  public void SetDirection()
  {
    Direction = (GetGlobalMousePosition() - GlobalPosition).Normalized();
    Rotation = Mathf.Atan2(Direction.Y, Direction.X); // Поворачиваем пулю в направлении движения
  }



  private void OnBodyEntered(Node2D body)
  {

    // GD.Print(Direction +"   "+Speed+"   "+body+"    "+Player);
    // Проверяем, что столкнулись с врагом и что это не сам игрок
    if (body is Enemy enemy && Player != null)
    {
      if (areaelementnomber[0] == 3)
      {
        enemy.effect();
      }
      // GD.Print(Direction +"   "+Speed+"   "+body+"    "+Player);
      enemy.TakeDamage(damage);
      BodyEntered -= OnBodyEntered;// Наносим урон врагу (значение урона можно настроить)
      QueueFree(); // Удаляем пулю после столкновения
    }
  }

  public void elementatack(int nomberelement)
  {
    switch (nomberelement)
    {
      case 0:

        break;
      case 1:

        break;
      case 2:

        break;
      case 3:
       timepoison(this);
        break;
      case 4:

        collision.Scale = new Vector2(collision.Scale.X + 2, collision.Scale.Y + 2);
        break;
      case 5:

        break;
      default:
        break;
    }
  }

  public void timepoison(Atack atack)
	{

		var time = new Godot.Timer();
		AddChild(time);
		time.WaitTime = 0.5;
		time.OneShot = false;
		time.Timeout += spawnpoison;
		time.Start();
	}
  private void spawnpoison()
  {
    if (sceneeffect != null)
    {
      PoisonEffect scen = (PoisonEffect)sceneeffect.Instantiate();
      GetParent().AddChild(scen);
      scen.GlobalPosition = GlobalPosition;

      scen.atack = this;
      scen.Directionset();
    }
    else
    {
      GD.Print("null poison effect");
    }
		// poisonEffects.Add(scen);
  }
}