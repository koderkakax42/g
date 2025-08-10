using Godot;
using System;

public partial class Boss : Enemy
{
    public static string progress = "0";
    ProgressBar helth;
    	public PackedScene BulletScene = null!;
    bool atack = true;
	float attaimer;
    Vector2 direction;
    Godot.Timer timetolive;
    AnimationTree animation = new AnimationTree();
    AnimationNodeStateMachinePlayback _stateMachine;
    public override void _Ready()
    {
        Body = GetNode<Area2D>("hitbox");

        Body.BodyEntered += OnBodyEntered;
        helth = GetNode<ProgressBar>("ProgressBar");
        target = GetTree().GetFirstNodeInGroup("Player") as Node2D;
        animation = GetNode<AnimationTree>("AnimationPlayer/AnimationTree");
        animation.Active = true;
        _stateMachine = (AnimationNodeStateMachinePlayback)animation.Get("parameters/playback");
        _stateMachine.Start("Start");
        BulletScene = GD.Load<PackedScene>("res://scene/atack/atack/atack.tscn");


        Health = 10;
        Damage = 100;
        Speed = 800;
        helth.MaxValue = Health;
        helth.Value = Health;
    }

    public override void TakeDamage(int damage)
    {
        bullshit();
        helth.Value = Health;
        if (Health <= 0)
        {
            playerwin();
        }
    }
    private void playerwin()
    {
        SaveGame.saveprogres(progress);

		SceneTree tree = GetTree();
        Fader.ScenePath = "res://scene/ui/meny/meny.tscn";
        tree.ChangeSceneToFile("res://scene/scen/load_scen/fader.tscn");
	}
    
    private void bullshit()
    {
        var bullet = (Atack)BulletScene.Instantiate();
        GetParent().AddChild(bullet);
        Atack.areaelementnomber = effectAtack();
        bullet.elementatack(0, bullet);
        bullet.damage = 0;
        bullet.GlobalPosition = GlobalPosition;
        bullet.SetDirection(target.GlobalPosition);
        bullet.Enemy = this;

        _stateMachine.Travel("atack");
        Speed = 0;

        timetolive = new Godot.Timer();
        AddChild(timetolive);
        timetolive.WaitTime = 2;
        timetolive.OneShot = true;
        timetolive.Timeout += startrun;
        timetolive.Start();
    }

    private void OnBodyEntered(Node2D body)
	{
		// GD.Print(Damage);
		// Проверяем, что столкнулись с врагом и что это не сам игрок
		if (body is Player player&&atack)
		{
			atack = false;
			//GD.Print(Damage);
			player.DamageEnemys(Damage);
			TakeDamage(10);
		}


	}

    private void startrun()
    {
        Speed = 800;
    }

    private int[] effectAtack()
    {
        int[] nomber = new int[5];
        Random random = new Random();
        for (int i = 0; i < Atack.areaelementnomber.Length;)
        {
            nomber[i] = random.Next(0, 6);
            i++;
        }
        return nomber;
    }

    public override void _PhysicsProcess(double delta)
    {
        if (!atack && attaimer >= 2)
        {
            attaimer = 0;
            atack = true;
        }

        attaimer += (float)delta;



        if (target == null) return;

        direction = (target.GlobalPosition - GlobalPosition).Normalized();


        Velocity = direction * Speed;

        MoveAndSlide();
        

    }
}
