using Godot;
using System;

public partial class money : Area2D
{
	public Player Player { get; set; } // Ссылка на игрока, выпустившего пулю
     private Timer timetolive; 
	 public float time = 60f;
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

}
