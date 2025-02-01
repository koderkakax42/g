using Godot;
using System;

public partial class Atack : Area2D
{
    [Export] public float Speed = 400.0f; // Скорость пули
    [Export] public int Damage = 10;        // Урон пули
    public Vector2 Direction { get; set; }   // Направление движения пули

    public override void _PhysicsProcess(double delta)
    {
        // Движение пули
        Position += Direction * Speed * (float)delta;

        // Удаление пули, если она вылетела за границы экрана
        if (Position.X < -100 || Position.X > 1500 ||
            Position.Y < -100 || Position.Y > 1000) // можно сделать проверку по размеру экрана
            {
               QueueFree();
            }
    }

    private void OnAreaEntered(Area2D area)
    {
        // Проверяем, попала ли пуля во врага
         if (area.GetParent() is enemy enemy)
         {
             // Наносим урон врагу
             enemy.TakeDamage(Damage);
              // Удаляем пулю
             QueueFree();
         }

    }
    public override void _Ready()
    {
      AreaEntered += OnAreaEntered;
    }

     public override void _ExitTree()
    {
      AreaEntered -= OnAreaEntered;
    }
}