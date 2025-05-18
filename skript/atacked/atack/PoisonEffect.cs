using Godot;
using System;

public partial class PoisonEffect : Area2D
{
    public Vector2 direction;
    public Atack atack;
    public override void _PhysicsProcess(double delta)
    {
        if (atack != null)
        {
         GlobalPosition += direction * +150 * (float)delta;          
        }
    }
    public override void _Ready()
    {
       
        base._Ready();

        AreaEntered += OnAreaEntered;

        var time = new Godot.Timer();
        AddChild(time);
        time.WaitTime = 2;
        time.OneShot = true;
        time.Timeout += dead ;
        time.Start();

    }
    private void OnAreaEntered(Area2D area)
    {
        var node = area.GetParent();
        if (node is Enemy enemy && atack != null)
        {
            enemy.TakeDamage(10);
            enemy.effect();
        }
    }

    public void Directionset()
    {
        direction = GlobalPosition - atack.GlobalPosition+ new Vector2(1,1);
    }

    private void dead()
    {
        QueueFree();
        // atack.poisonEffects.RemoveAt(0);
    }
}
