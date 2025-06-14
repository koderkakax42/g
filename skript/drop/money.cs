using Godot;
using System;

public partial class Money : Area2D
{
     Godot.Timer timetolive;
	public Enemy enemy { get; set; } = null!; // Ссылка на игрока, выпустившего пулю
      
	 public float time = 10.0f;
	 private void live_bullet()
    {
      timetolive = new Godot.Timer();
      AddChild(timetolive);
      timetolive.WaitTime = time;
      timetolive.Timeout += _QueueFree;
      timetolive.Start(); 

    }
	 private void _QueueFree()
     {
        timetolive.Timeout -= _QueueFree;
        BodyEntered -= OnBodyEntered;
       QueueFree();
     }
   
    public override void _Ready()
    {
        live_bullet();
        // Добавьте здесь любой код инициализации монеты
        BodyEntered += OnBodyEntered; // Важно подключить сигнал
    }

    private void OnBodyEntered(Node2D body)
    {
        // Проверяем, что столкнулись с игроком
        if (body is Player player)
        {
            // Добавляем значение монеты к счету игрока (предполагается, что у Player есть метод AddScore)
            player.moneyvalue();

            // Удаляем монету из сцены, используя CallDeferred
            _QueueFree();
        }
    }

   
}
