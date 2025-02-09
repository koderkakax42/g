using Godot;
using System;

public partial class Atacked : Area2D
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
public partial class Atack : CharacterBody2D{
[Export]public int speed = 300;
[Export] public Node2D enemy;

    public override void _Ready()
    {
          enemy = GetTree().GetFirstNodeInGroup("enemy")as Node2D;
    }

    public override void _PhysicsProcess(double delta)
    {
        Vector2 ttt =(enemy.GlobalPosition-GlobalPosition).Normalized();
        Velocity = ttt * speed;
        MoveAndSlide();
    }


}

