using Godot;
using System;
using System.Collections.Generic;
using System.Linq;


	public partial class deteckt : Area2D
	{
		
    private List<Vector2> _targetMarkers = new List<Vector2>(); // Список меток
    public List<enemy>? _markedEnemies {get;} = new List<enemy>();  // Список врагов с метками
        Player player = new Player();
	   public Vector2 y;
        public override void _Ready()
        {	
		  BodyEntered += OnBodyEntered;	
          BodyExited += OnBodyExited;
		  
        }
        private void OnBodyEntered(Node node)
		{
			GD.Print("i see u ?");
          if (node is enemy enemy)
		  {
            y = enemy.GlobalPosition;
		  }
		}
		private void OnBodyExited(Node node)
		{
          GD.Print(node);

		}
    public override void _PhysicsProcess(double delta)
    {
		GlobalPosition = GetGlobalMousePosition();
        
    }

     public void MarkTarget()
    {
        // Ищем врага в небольшом радиусе от клика мыши
         _markedEnemies.Clear();
        var enemies = GetTree().GetNodesInGroup("enemy").OfType<enemy>().ToList();
        foreach (var enemy  in  enemies)
        {
            if (IsInstanceValid(enemy) )
            {
                // Добавляем врага в список помеченных, если он еще не там
                if (!_markedEnemies.Contains(enemy))
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

