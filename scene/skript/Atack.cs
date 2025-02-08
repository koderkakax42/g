using Godot;
using System;


public partial class Atackvect : CharacterBody2D
{
    [Export]
    public int Speed { get; set; } = 400;
    public Node2D targ;
    public AnimatedSprite2D animatedSprite2D;
    public override void _Ready()
    {
      targ =GetTree().GetFirstNodeInGroup("enemy")as Node2D;
    }
    public override void _Input(InputEvent @event)
    {
       if (targ !=GetTree().GetFirstNodeInGroup("enemy"))
       {
        GD.Print("sis is sparta");
       }
    }

    public override void _PhysicsProcess(double delta)
    {
        if(targ == null) return;
       Vector2 _target = (targ.GlobalPosition - GlobalPosition).Normalized();
        // LookAt(_target);
      Velocity = _target * Speed;
        if(targ != null)animatedSprite2D.Play("run");
            MoveAndSlide();
        
    }
}

public partial class Atack : Area2D
{

    [Export] public int Damage = 10;      
  

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
