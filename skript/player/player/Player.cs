using Godot;
using SaveGame;
using System;
using System.Collections.Generic;
using System.Linq;




public partial class Player : CharacterBody2D
{
    public static event Action dead=delegate{};
    enemy enemy1;
    public String TargetScenePath = "res://scene/scen/load_scen/fader.tscn";
    SaveGame.GameData savedata = new SaveGame.GameData();
    ui_pc_player UI{get;set;} = null!; 
    public static int xp = 400;
     public Vector2 inputDirection;
     public PackedScene BulletScene = null!; // Сцена пули
    [Export] public float Speed = 900;
    [Export] public float FireRate = 2f; // Выстрелов в секунду
    [Export] public float BulletSpeed = 400f;
     private int ValueMoney = 0;
    private float _timeSinceLastFire = 0f;
    public AnimatedSprite2D _animatedSprite = null!;
    public string money = null!;
    public enemy enemy2;
    deteckt? deteckt = new deteckt() ;
    public override void _Ready()
    {
       save_game.save_playe_game += LoadGame;

        _animatedSprite = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
        BulletScene = GD.Load<PackedScene>("res://scene/atack/atack/atack.tscn");
        
            if(UI==null)
            GD.Print("null UI ");
    }
    private void health()
    {
        xp = 400;
    }
    private void LoadGame()
    {
        xp = savedata.Health;

        GlobalPosition =new Vector2(savedata.PlayerPositionX,savedata.PlayerPositionY);
       
        ValueMoney = savedata.money;

        GD.Print("load save game.player");
    }
    public void DamageEnemys(int damage)
    {
        if(damage == xp)
        {
          GD.Print(damage);
          GD.Print(xp);
        }
       xp -= damage;

       UI._on_xp();

    

       if(xp <= 0)
       {
        fader.ScenePath="res://scene/ui/meny/meny.tscn";
       }
    }
     private void LoadNewScene()
    {
        health();
        Speed = 400;
        // Получаем SceneTree
        SceneTree tree = GetTree();

        // Останавливаем текущую сцену.  Это ВАЖНО.
        tree.ChangeSceneToFile(TargetScenePath);
    }
    public override void _Process(double delta)
    {
      

        if(Input.IsActionPressed("ui_right")
         ||Input.IsActionPressed("ui_left")
         ||Input.IsActionPressed("ui_up")
         ||Input.IsActionPressed("ui_down") )
         {
         _animatedSprite.Play("run");
         }
         else
         {
         _animatedSprite.Pause();
         }


        _timeSinceLastFire += (float)delta;


        // Стрельба
        if (Input.IsActionPressed("ui_atack") && _timeSinceLastFire >= 1f / FireRate)
        {
            Shoot();
            _timeSinceLastFire = 0f;
        }
         if (Input.IsActionPressed("ui_mark_shoot") && _timeSinceLastFire >= 1f / FireRate)
        {
            Shoot(1);
            _timeSinceLastFire = 0f;
        }

        if (Input.IsActionPressed("mark"))
        {
            deteckt.MarkTarget(deteckt.enemyglobpos);
            
        }


        if(xp <= 0)
         {
           _animatedSprite.Play("dead");
          deads(); 
         }
        GetInput();
        MoveAndSlide();
    }
    public void deads()
    {
       dead.Invoke();

            Speed=0;
            Velocity = inputDirection  * Speed;

            var i = new Godot.Timer();
            AddChild(i);
            i.WaitTime = 1.5f;
            i.OneShot = true;
            i.Timeout += LoadNewScene;
            i.Start();
    }
    
   
    private void Shoot()
    {
        if (BulletScene == null)
        {
            GD.PrintErr("BulletScene is not assigned!");
            return;
        }
         enemy1 = FindNearestEnemy();

        var bullet = (Atack)BulletScene.Instantiate();
        GetParent().AddChild(bullet);
        bullet.GlobalPosition = GlobalPosition;
        bullet.SetDirection();
        bullet.Speed = BulletSpeed;
        bullet.Player = this; //  Добавляем ссылку на игрока, чтобы пуля знала, кто ее выпустил
    }
     private void Shoot(int? @int )
    { 
        if(deteckt._markedEnemies.Count > 0)
        {
            foreach(enemy enemy in deteckt._markedEnemies)
            {
                 enemy2 = enemy;
            }   

        var bullet = (Atack)BulletScene.Instantiate();
        GetParent().AddChild(bullet);
        bullet.GlobalPosition = GlobalPosition;
        bullet.SetDirection(enemy2.GlobalPosition);
        bullet.Speed = BulletSpeed;
        bullet.Player = this;
        }
        else
        {
            GD.Print("not shoot");
        return;
        }
    }
    private  enemy  FindNearestEnemy()
    {
        var enemies = GetTree().GetNodesInGroup("enemy").OfType<enemy>();

        enemy nearest=new enemy();
        if(nearest == null)
        {
            GD.Print("null nerest");
        }

        float minDistance = float.MaxValue;

        foreach (var enemy in enemies)
        {
            if (IsInstanceValid(enemy)) // Проверяем, что враг еще существует
            {
                float distance = GlobalPosition.DistanceTo(enemy.GlobalPosition);
                if (distance < minDistance)
                {
                    minDistance = distance;
                    nearest = enemy;
                }
            }
        }
       
        return nearest;
    }
   
     public void GetInput()
    {
         
        inputDirection = Input.GetVector("ui_left", "ui_right", "ui_up", "ui_down");
        Velocity = inputDirection * Speed;
    }
    public void moneyvalue()
    {
       ValueMoney++;
       money= ValueMoney .ToString();
       UI._on_vale(money);
    }
    public  void SaveGamePlayer()
    {
        savedata.Health = xp;
        savedata.PlayerPositionX = GlobalPosition.X;
        savedata.PlayerPositionY = GlobalPosition.Y;
        savedata.money = ValueMoney ;
        GD.Print("save game player is truy");
    }
}





