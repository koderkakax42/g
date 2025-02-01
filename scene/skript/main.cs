using Godot;


public partial class main : Node2D
{
	private NavigationRegion2D air_got;

	private NavigationRegion2D yuor_got;

    public override void _Ready()
    {
      var scene = GD.Load<PackedScene>("res://scene/air_got.tscn");
	  
	  var instance = scene.Instantiate();
       AddChild(instance);


    }
}