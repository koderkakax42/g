using Godot;
using System;
using System.Collections.Generic;
using System.Linq;


	public partial class deteckt : Area2D
	{
		public Vector2 enemyglobpos;
    private List<Vector2> _targetMarkers = new List<Vector2>(); // Список меток
    public List<enemy>? _markedEnemies {get;} = new List<enemy>();  // Список врагов с метками
        Player player = new Player();
	 
        public override void _Ready()
        {	
		  BodyEntered += OnBodyEntered;	
		  
        }
     private  void OnBodyEntered(Node node)
		{
     
      if (node is enemy enemy)
		  {
        enemyglobpos = enemy.GlobalPosition;
		  }
		}
    public override void _PhysicsProcess(double delta)
    {
		GlobalPosition = GetGlobalMousePosition();
        
    }

     public void MarkTarget(Vector2 vector2)
    {
        // Ищем врага в небольшом радиусе от клика мыши
         _markedEnemies.Clear();
        var enemies = GetTree().GetNodesInGroup("enemy").OfType<enemy>().ToList();
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
    }


	}

