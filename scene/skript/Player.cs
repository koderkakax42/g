using Godot;
using System;
using System.Collections.Generic;
using System.Linq;

public partial class Player : CharacterBody2D
{

    [Export]
    public String TargetScenePath = "res://scene/meny.tscn";

    private int xp = 400;
     public Vector2 inputDirection;
    [Export] public PackedScene BulletScene; // Сцена пули
    [Export] public float Speed = 900;
    [Export] public float FireRate = 2f; // Выстрелов в секунду
    [Export] public float BulletSpeed = 400f;

    private float _timeSinceLastFire = 0f;
    private List<Vector2> _targetMarkers = new List<Vector2>(); // Список меток
    private List<enemy> _markedEnemies = new List<enemy>(); // Список врагов с метками
    public AnimatedSprite2D _animatedSprite;
    public override void _Ready()
    {
        GD.Print("Hello from C#!");
        _animatedSprite = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
    }

    public void DamageEnemys(int damage)
    {
       xp -= damage;
       if(xp <= 0)
       {
        QueueFree();

         LoadNewScene();
       }
    }
     private void LoadNewScene()
    {
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
         _animatedSprite.Stop();
         }

        _timeSinceLastFire += (float)delta;

       

        // Стрельба
        if (Input.IsActionPressed("ui_atack") && _timeSinceLastFire >= 1f / FireRate)
        {
            Fire();
            _timeSinceLastFire = 0f;
        }
       
     
       

        // Установка метки
        if (Input.IsActionJustPressed("mark"))
        {
            Vector2 mousePos = GetGlobalMousePosition();
            MarkTarget(mousePos);
        }

       
       
        
       

        /* if(xp <= 0)
         {
            _animatedSprite.Play("dead");

            Speed=0;
            Velocity = inputDirection  * Speed;
         }

*/ GetInput();
        MoveAndSlide();
    }

 

    private void Fire()
    {
        // Если есть враги с метками, стреляем по ним
        if (_markedEnemies.Count > 0)
        {
            foreach (var enemy in _markedEnemies)
            {
                if (IsInstanceValid(enemy)) // Проверяем, что враг еще существует
                {
                    ShootAtTarget(enemy.GlobalPosition);
                }
            }
            // Очищаем список помеченных врагов после выстрела
        }
        // Иначе, ищем ближайшего врага и стреляем по нему
        else
        {
            enemy nearestEnemy = FindNearestEnemy();
            if (nearestEnemy != null)
            {
                ShootAtTarget(nearestEnemy.GlobalPosition);
            }
        }

    }

    private void ShootAtTarget(Vector2 targetPosition)
    {
        if (BulletScene == null)
        {
            GD.PrintErr("BulletScene is not assigned!");
            return;
        }

        var bullet = (Atack)BulletScene.Instantiate();
        GetParent().AddChild(bullet);
        bullet.GlobalPosition = GlobalPosition;
        bullet.SetDirection(targetPosition);
        bullet.Speed = BulletSpeed;
        bullet.Player = this; //  Добавляем ссылку на игрока, чтобы пуля знала, кто ее выпустил
    }

    private enemy FindNearestEnemy()
    {
        var enemies = GetTree().GetNodesInGroup("enemy").OfType<enemy>().ToList();
        if (enemies.Count == 0)
        {
            return null;
        }

        enemy nearest = null;
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

    private void MarkTarget(Vector2 position)
    {
        // Ищем врага в небольшом радиусе от клика мыши
        var enemies = GetTree().GetNodesInGroup("enemy").OfType<enemy>().ToList();
        foreach (var enemy in enemies)
        {
            if (IsInstanceValid(enemy) && enemy.GlobalPosition.DistanceTo(position) < 50)
            {
                // Добавляем врага в список помеченных, если он еще не там
                if (!_markedEnemies.Contains(enemy))
                {
                    _markedEnemies.Add(enemy);
                    // TODO: Добавьте визуальную индикацию, что враг помечен (например, спрайт над головой)
                    GD.Print($"Enemy marked: {enemy.Name}");
                }
                return; // Нашли врага, больше не ищем
            }
        }
        GD.Print("No enemy found to mark.");

        //TODO: если не найден враг, то ставить "метку" просто на земле
        _targetMarkers.Add(position);
    }
     public void GetInput()
    {
         
        inputDirection = Input.GetVector("ui_left", "ui_right", "ui_up", "ui_down");
        Velocity = inputDirection * Speed;
    }

    
}
