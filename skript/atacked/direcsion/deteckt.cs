using Godot;
using System;
using System.Collections.Generic;
using System.Linq;


public partial class Deteckt : Area2D
{

  Enemy Enemy;

  public List<Enemy> _markedEnemies { get; } = new List<Enemy>();  // Список врагов с метками


  public override void _Ready()
  {
    BodyEntered += OnBodyEntered;

  }





  private void OnBodyEntered(Node node)
  {
    //GD.Print($"node : {node}");

    if (node is Enemy enemy)
    {
      // enemyglobpos = enemy.GlobalPosition;
      Enemy = enemy;
      //GD.Print(Enemy);
    }

  }
  public override void _PhysicsProcess(double delta)
  {
    GlobalPosition = new Vector2(GetGlobalMousePosition().X + 22, GetGlobalMousePosition().Y + 22);
  }

  public override void _Process(double delta)
  {

  }


  public void MarkTarget()
  {
    if (Enemy != null)
    {
      // Добавляем врага в список помеченных, если он еще не там
      if (!_markedEnemies.Contains(Enemy))
      {
        _markedEnemies.Add(Enemy);

        Enemy.mark();

        return;
      }
      // Нашли врага, больше не ищем
    }


    return;
  }



}
