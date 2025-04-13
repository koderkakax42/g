using Godot;
using System;

public partial class money : Area2D
{
	public enemy enemy { get; set; } = null!; // Ссылка на игрока, выпустившего пулю
      
	 public float time = 10.0f;
	 private void live_bullet()
    {
      Godot.Timer timetolive = new Godot.Timer();
      AddChild(timetolive);
      timetolive.WaitTime = time;
      timetolive.Timeout += _QueueFree;
      timetolive.Start(); 

    }
	 private void _QueueFree(){QueueFree();}
   
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
            CallDeferred(MethodName._QueueFree);
        }
    }

   
}
