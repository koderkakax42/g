using Godot;
using System;

public partial class money : Area2D
{
	public enemy enemy { get; set; } // Ссылка на игрока, выпустившего пулю

  

  
     private Timer timetolive; 
	 public float time = 10.0f;
	 private void live_bullet()
    {
      timetolive = new Timer();
      AddChild(timetolive);
      timetolive.WaitTime = time;
      timetolive.OneShot= true;
      timetolive.Timeout += _QueueFree;
      timetolive.Start(); 

    }
	 private void _QueueFree(){QueueFree();}
   


  


   

    public override void _Ready()
    {
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
