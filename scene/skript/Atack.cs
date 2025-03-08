using Godot;
using System;

public partial class Atack : Area2D
{
    public Vector2 Direction { get; set; }
    public float Speed = 900;
    public Player Player { get; set; } // Ссылка на игрока, выпустившего пулю
    private Timer _collisionTimer;
    private Timer timetolive; 
    private CollisionShape2D _collisionShape;
      public float time_bul = 7f;
  
    
   

    public override void _Ready()
    {
        _collisionShape = GetNode<CollisionShape2D>("CollisionShape2D"); // Получаем CollisionShape
        BodyEntered += OnBodyEntered;

        // Создаем и настраиваем таймер
        _collisionTimer = new Timer();
        AddChild(_collisionTimer);
        _collisionTimer.OneShot = true; // Запускаем только один раз
        _collisionTimer.Start();
        if(_collisionTimer.OneShot == true)
        {
            live_bullet();

        }

    }
    
 

    private void live_bullet()
    {
      timetolive = new Timer();
      AddChild(timetolive);
      timetolive.WaitTime = time_bul;
      timetolive.OneShot= true;
      timetolive.Timeout += _QueueFree;
      timetolive.Start(); 

    }
    private void _QueueFree(){QueueFree();}


    public override void _PhysicsProcess(double delta)
    {
        GlobalPosition += Direction * Speed * (float)delta;

        // Удаляем пулю, если она ушла далеко за пределы экрана
     
    }

    public void SetDirection(Vector2 targetPosition)
    {
        Direction = (targetPosition - GlobalPosition).Normalized();
        Rotation = Mathf.Atan2(Direction.Y, Direction.X); // Поворачиваем пулю в направлении движения
    }

    private void OnBodyEntered(Node2D body)
    {
       // GD.Print(Direction +"   "+Speed+"   "+body+"    "+Player);
        // Проверяем, что столкнулись с врагом и что это не сам игрок
        if (body is enemy enemy && Player != null)
        {
             // GD.Print(Direction +"   "+Speed+"   "+body+"    "+Player);
            enemy.TakeDamage(20); // Наносим урон врагу (значение урона можно настроить)
            QueueFree(); // Удаляем пулю после столкновения
        }
    }

    

    
}