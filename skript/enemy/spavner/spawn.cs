using Godot;
using System;

public partial class Spawn : Node2D
{
	public PackedScene EnemyScene { get; set; } = null!;

	[Export]
	public float SpawnInterval = 5.0f;

	[Export]
	public int MaxEnemies = 1;

	public Vector2[] SpawnPoints = null!;

	private float _timer = 0.0f;                      // Таймер
	public int _enemyCount = 0;

	public override void _Ready()
	{
		EnemyScene = GD.Load<PackedScene>("res://scene/enemy/enemy/enemy.tscn");
	}

	public override void _Process(double delta)
	{
		_timer += (float)delta;
		if (_enemyCount == 0)
		{
			MaxEnemies = 1;
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
			GD.PrintErr("Ошибка: Сцена врага не задана! spawn");
			return;
		}

		// Инстанцируем сцену врага
		Node2D enemyInstance = EnemyScene.Instantiate<Node2D>();




		enemyInstance.Position = new Vector2(0, 0);



		// Добавляем врага на сцену
		AddChild(enemyInstance);

		// Инкрементируем счетчик врагов
		_enemyCount++;
		enemyInstance.TreeExited += () => { _enemyCount--; }; //Уменьшаем счётчик врагов при удалении
	}
}