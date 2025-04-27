using Godot;
using System;

public partial class Atack : Area2D
{
    public static int damage = 20;
    public Vector2 Direction { get; set; }
    public float Speed = 900;
    public Player Player { get; set; } = new Player();
    public float time_bul = 7f;
  
    public override void _Ready()
    {
        BodyEntered += OnBodyEntered;  
        // Создаем и настраиваем таймер
            live_bullet();


    }
    
 

    private void live_bullet()
    {
      Godot.Timer timetolive = new Godot.Timer();
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
     public void SetDirection()
    {
        Direction = (GetGlobalMousePosition() - GlobalPosition).Normalized();
        Rotation = Mathf.Atan2(Direction.Y, Direction.X); // Поворачиваем пулю в направлении движения
    }
    


    private void OnBodyEntered(Node2D body)
    {

       // GD.Print(Direction +"   "+Speed+"   "+body+"    "+Player);
        // Проверяем, что столкнулись с врагом и что это не сам игрок
        if (body is enemy enemy && Player != null)
        {
           
             // GD.Print(Direction +"   "+Speed+"   "+body+"    "+Player);
            enemy.TakeDamage(damage); // Наносим урон врагу (значение урона можно настроить)
            QueueFree(); // Удаляем пулю после столкновения
        }
    }
}