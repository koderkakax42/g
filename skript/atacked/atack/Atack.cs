using Godot;
using System;
using System.Collections.Generic;
using System.Linq;


public partial class Atack : Area2D
{
  float magnittaimer = 0;
   List<Enemy> enemyformagnet = new List<Enemy>();
  public Area2D magnit = new Area2D();
  public CollisionShape2D radiusmagnits = new CollisionShape2D();
  public bool atackdiablo = false;
  public int invertor = 1;
  byte atacdelete = 0;
  bool atactwulve = false;
  Vector2 startglobalposition;
  bool neironactiveyion = false;
  public PackedScene sceneeffect;
  AnimatedSprite2D animated;
  public static int damage = 20;
  public Vector2 Direction { get; set; }
  public float Speed = 900;
  public Player Player { get; set; } = new Player();
  public float time_bul = 7f;
  [Export] public CollisionShape2D collision;
  public static int[] areaelementnomber = new int[5];
  // public List<PoisonEffect> poisonEffects = new List<PoisonEffect>(3);
  public Slot[] elementarreislot;

  public override void _Ready()
  {
    magnit.AreaEntered += onmagnet;
    magnit.AreaExited += offmagnet;
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
    if (magnit != null)
    {
      
    }
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
    magnittaimer += 0.005f;
    if (magnittaimer >= 0.1 && enemyformagnet.Count() != null && enemyformagnet.Count() > 0)
    {
      magnittaimer = 0;
      controlmagnetic(magnit, radiusmagnits);
    }

  }

  public void SetDirection(Vector2 targetPosition)
  {
    Direction = (targetPosition - GlobalPosition).Normalized();
    Rotation = Mathf.Atan2(Direction.Y, Direction.X); // Поворачиваем пулю в направлении движения
  }
  public void SetDirection()
  {
    Direction = ((GetGlobalMousePosition() - GlobalPosition) * invertor).Normalized();
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

      CallDeferred("dead");
    }
  }

  public void elementatack(int nomber, Atack atack)
  {
    //GD.Print(nomber + " atack");

    switch (areaelementnomber[nomber])
    {
      case 0:
        air(nomber, atack);
        break;
      case 1:
        mars(nomber, atack);
        break;
      case 2:
        neiron(nomber, atack);
        break;
      case 3:
        timepoison(nomber, atack);
        break;
      case 4:
        Sun(nomber, atack);
        break;
      case 5:
        terror(nomber, atack);
        break;
      default:
        break;
    }
  }

  private void neiron(int nomber, Atack atack)
  {
    neironactiveyion = true;
    startglobalposition = Player.GlobalPosition;

    if (nomber < areaelementnomber.Count())
    {
      elementatack(nomber + 1, atack);
      return;
    }
  }
  private void timepoison(int nomber, Atack atack)
  {
    var time = new Godot.Timer();
    AddChild(time);
    time.WaitTime = 0.5;
    time.OneShot = false;
    time.Timeout += () => spawnpoison(nomber, atack);
    time.Start();
  }

  private void spawnpoison(int nomber, Atack atack)
  {
    if (sceneeffect != null)
    {
      PoisonEffect scen = (PoisonEffect)sceneeffect.Instantiate();
      GetParent().AddChild(scen);
      scen.GlobalPosition = atack.GlobalPosition;
      scen.GlobalPosition += Direction;
      scen.atack = atack;
      scen.Directionset(invertor);
      invertor = invertor * -1;

      if (nomber < areaelementnomber.Count())
      {
        scen.elementatack(nomber + 1, scen);
        return;
      }
    }
    else
    {
      GD.Print("null poison effect");
    }
    // poisonEffects.Add(scen);
  }

  private void Sun(int nomber, Atack atack)
  {
    collision.Scale = new Vector2(collision.Scale.X * 1.5f, collision.Scale.Y * 1.5f);

    if (nomber < areaelementnomber.Count())
    {
      elementatack(nomber + 1, atack);
      return;
    }
  }

  private void terror(int nomber, Atack atack)
  {
    PackedScene y;
    if (atackdiablo)
    {
      y = GD.Load<PackedScene>("res://scene/atack/effect/poisoneffect.tscn"); ;
    }
    else
    {
      y = GD.Load<PackedScene>("res://scene/atack/atack/atack.tscn"); ;
    }
    Atack datack = (Atack)y.Instantiate();
    GetParent().AddChild(datack);
    datack.atacdelete = atacdelete;
    datack.GlobalPosition = GlobalPosition;
    datack.Player = Player;
    datack.SetDirection((GlobalPosition + new Vector2(40, 60)) * invertor);
    if (invertor >= 1)
    {
      datack.invertor = -1;
      invertor = -1;
    }
    else
    {
      invertor = 1;
      datack.invertor = 1;
    }

    if (nomber < areaelementnomber.Count())
    {
      datack.elementatack(nomber + 1, datack);
      return;
    }
  }
  private void air(int nomber, Atack atack)
  {
    return;
  }
  private void mars(int nomber, Atack atack)
  {
    CircleShape2D settingradius = new CircleShape2D();
    settingradius.Radius = 100;
    atack.AddChild(magnit);
    magnit.GlobalPosition = atack.GlobalPosition;
    magnit.AddChild(radiusmagnits);
    radiusmagnits.Shape = settingradius;
    radiusmagnits.GlobalPosition = atack.GlobalPosition;

    controlmagnetic(magnit, radiusmagnits);

    if (nomber < areaelementnomber.Count())
    {
      elementatack(nomber + 1, atack);
    }
  }

  private void controlmagnetic(Area2D magnitic, CollisionShape2D collisionformagnitic)
  {
    for (int u = 0; u < enemyformagnet.Count(); u++)
    {
      Vector2 enemyderection;
      enemyderection = (GlobalPosition - enemyformagnet[u].GlobalPosition ).Normalized();
      enemyformagnet[u].GlobalPosition += enemyderection* (Speed - enemyformagnet[u].Speed)/2 ;
    }
  }
  private void onmagnet(Area2D area)
  {
    Node node = area.GetParent();
    if (node is Enemy enemy)
    {
      enemyformagnet.Add(enemy);
    }
  }
  private void offmagnet(Area2D area)
  {
    Node node = area.GetParent();
    if (node is Enemy enemy)
    {
      enemyformagnet.Remove(enemy);
    }
  }

}