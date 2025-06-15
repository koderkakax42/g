using Godot;
using System;

public partial class PoisonEffect : Atack
{
    float Speedp = 150;
    public Atack atack;

    public override void _PhysicsProcess(double delta)
    {
        if (atack != null)
        {
            GlobalPosition += Direction * Speedp * (float)delta;
        }
    }
    public override void _Ready()
    {
        atackdiablo = true;
        AreaEntered += OnAreaEntered;

        var time = new Godot.Timer();
        AddChild(time);
        time.WaitTime = 2;
        time.OneShot = true;
        time.Timeout += dead;
        time.Start();
        sceneeffect = GD.Load<PackedScene>("res://scene/atack/effect/poisoneffect.tscn");
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

    public void Directionset(int invertor)
    {
        Direction = atack.Direction * -1;
        Direction = new Vector2(Direction.X * invertor, Direction.Y);
    }

    private void dead()
    {
        QueueFree();
        // atack.poisonEffects.RemoveAt(0);
    }
    
}
