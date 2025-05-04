using Godot;
using System;
using System.Collections.Generic;


public partial class Deteckt : Area2D
{
    		public Vector2 enemyglobpos;
    private List<Vector2> _targetMarkers = new List<Vector2>(); // Список меток
    public List<Enemy>? _markedEnemies {get;} = new List<Enemy>();  // Список врагов с метками
	 
        public override void _Ready()
        {	
		  BodyEntered += OnBodyEntered;

        }
     
     private  void OnBodyEntered(Node node)
		{
      GD.Print($"node : {node}");
    
      if (node is Enemy enemy)
		  {
        enemyglobpos = enemy.GlobalPosition;
		  }
    
		}
    public override void _PhysicsProcess(double delta)
    {
		GlobalPosition = new Vector2(GetGlobalMousePosition().X+15 ,GetGlobalMousePosition().Y+15);
        
    }

  /*   public void MarkTarget(Vector2 vector2)
    {
        // Ищем врага в небольшом радиусе от клика мыши
         _markedEnemies.Clear();
        var enemies = GetTree().GetNodesInGroup("enemy").OfType<Enemy>().ToList();
        foreach (var enemy  in  enemies)
        {
            if (IsInstanceValid(enemy) )
            {
                // Добавляем врага в список помеченных, если он еще не там
                if (!_markedEnemies.Contains(enemy)&& vector2.DistanceTo(enemy.GlobalPosition) <= 50 )
                {
                    _markedEnemies.Add(enemy);
                     
                    enemy.mark();

                   return;
                }
                 // Нашли врага, больше не ищем
            }
        }
       
        return;
    }*/


}
