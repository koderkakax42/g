using Godot;
using System;

public partial class Console : TextEdit
{
    	int i ;
		bool y;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		
		
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		y = int.TryParse(Text,out i);
	
		switch(i)
		{
			case 1 : 
				  QueueFree();
			break;
			case 2 :
					 foreach(Player player in GetTree().GetNodesInGroup("save"))
					 {
						player.Health = 0;
					 }
			break;
			default : 
				  GD.Print("console mode ");
			break;		  
				 
		}
	}
}
