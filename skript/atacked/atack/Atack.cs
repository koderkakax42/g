using Godot;
using System;
using System.Collections.Generic;
using System.Linq;


public partial class Atack : Area2D
{
  int invertor = 1;
  byte atacdelete = 0;
  bool atactwulve = false;
  Vector2 startglobalposition;
  bool neironactiveyion = false;
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
  public event Action deleteeffect = delegate { };

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
    timetolive.Timeout += dead;
    timetolive.Start();



  }
  private void dead()
  {
    if (atactwulve && atacdelete <= 2)
    {
      atacdelete++;

      GD.Print("atack" + atacdelete);

      for (int i = 0; i < 2; i++)
      {
        PackedScene y = GD.Load<PackedScene>("res://scene/atack/atack/atack.tscn"); ;
        Atack atack = (Atack)y.Instantiate();
        GetParent().AddChild(atack);
        atack.atacdelete = atacdelete;
        atack.GlobalPosition = new Vector2(GlobalPosition.X + 10 * i, GlobalPosition.Y + 10 * i);
        atack.SetDirection(Player.GlobalPosition*-1);
        atack.Player = Player;
        if (invertor >= 1)
        {
          atack.invertor = -1;
        }
        else
        {
          invertor = 1;
          atack.invertor = 1;
        }
        
        for (int e = 0; e < areaelementnomber.Count(); e++)
        {
          elementatack(areaelementnomber[e]);
        }
      }
      
    }
    else
    {
      atacdelete = 0;
    }

    BodyEntered -= OnBodyEntered;
    QueueFree();
  }


  public override void _PhysicsProcess(double delta)
  {
    if (neironactiveyion)
    {
      Direction = (GlobalPosition - Player.GlobalPosition).Normalized();
      Rotation = (float)Math.Atan2(Direction.Y, Direction.X);
    }
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
    Direction = ((targetPosition - GlobalPosition)*invertor).Normalized();
    Rotation = Mathf.Atan2(Direction.Y, Direction.X); // Поворачиваем пулю в направлении движения
  }
  public void SetDirection()
  {
    Direction = ((GetGlobalMousePosition() - GlobalPosition)*invertor).Normalized();
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

      dead();
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
        neironactiveyion = true;
        startglobalposition = Player.GlobalPosition;
        break;
      case 3:
        timepoison(this);
        break;
      case 4:

        collision.Scale = new Vector2(collision.Scale.X * 1.5f, collision.Scale.Y * 1.5f);
        break;
      case 5:
        atactwulve = true;
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