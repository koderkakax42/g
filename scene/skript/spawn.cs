using Godot;
using System;

public partial class spawn : Node2D
{
    [Export] 
	public PackedScene EnemyScene {get;set;}


    [Export]
	 public float SpawnInterval = 10.0f;

    [Export] 
	public int MaxEnemies = 10; 


   


    [Export]
	 public Vector2[] SpawnPoints; 


    [Export]
	 public Vector2 SpawnAreaSize = Vector2.One * 100f;

    private float _timer = 0.0f;                      // Таймер
    private int _enemyCount = 0;                      // Количество врагов на сцене
   

    public override void _Process(double delta)
    {
        _timer += (float)delta;
        if (_enemyCount==0)
        {
            SpawnEnemy();
        }

        if (_timer >= SpawnInterval && _enemyCount < MaxEnemies)
        {
            SpawnEnemy();
            _timer = 0.0f;
        }
    }

    private void SpawnEnemy()
    {
        if (EnemyScene == null)
        {
            GD.PrintErr("Ошибка: Сцена врага не задана!");
            return;
        }
        
        // Инстанцируем сцену врага
        Node2D enemyInstance = EnemyScene.Instantiate<Node2D>();

         
                

            enemyInstance.Position = GlobalPosition;
      
        
        
        // Добавляем врага на сцену
        AddChild(enemyInstance);

        // Инкрементируем счетчик врагов
        _enemyCount++;
        enemyInstance.TreeExited += () => { _enemyCount--; }; //Уменьшаем счётчик врагов при удалении
    }
}