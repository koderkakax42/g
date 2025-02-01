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
	 public bool SpawnRandomly = true; 


    [Export]
	 public Vector2[] SpawnPoints; 


    [Export]
	 public Vector2 SpawnAreaSize = Vector2.One * 100f;

    private float _timer = 0.0f;                      // Таймер
    private int _enemyCount = 0;                      // Количество врагов на сцене
    private Random _random = new Random();

    public override void _Process(double delta)
    {
        _timer += (float)delta;

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

        // Устанавливаем позицию врага
        if (SpawnRandomly)
        {
            // Спавн в случайной точке в области
            Vector2 spawnPosition = Position + new Vector2(
                (float)_random.NextDouble() * SpawnAreaSize.X - SpawnAreaSize.X / 2f,
                (float)_random.NextDouble() * SpawnAreaSize.Y - SpawnAreaSize.Y / 2f);

            enemyInstance.Position = spawnPosition;
        }
        else
        {
            // Спавн в заданной точке
            if (SpawnPoints.Length > 0)
            {
                int spawnPointIndex = _random.Next(SpawnPoints.Length);
                enemyInstance.Position = SpawnPoints[spawnPointIndex];
            }
            else
            {
                // Если точек спавна нет
                enemyInstance.Position = Position;
                 GD.PrintErr("Ошибка: Точки спавна не заданы!");
            }
           
        }
        
        // Добавляем врага на сцену
        AddChild(enemyInstance);

        // Инкрементируем счетчик врагов
        _enemyCount++;
        enemyInstance.TreeExited += () => { _enemyCount--; }; //Уменьшаем счётчик врагов при удалении
    }
}