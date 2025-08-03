using Godot;
using System;

public partial class Appdait : Control
{
    CollisionShape2D collision = new CollisionShape2D();
    private void LoadNewScene()
    {
        // Получаем SceneTree
        SceneTree tree = GetTree();

        // Останавливаем текущую сцену.  Это ВАЖНО.

        tree.ChangeSceneToFile("res://scene/scen/load_scen/fader.tscn");

    }
    public override void _Ready()
    {
        
        RectangleShape2D shape2D = new RectangleShape2D();
        shape2D.Size = new Vector2((float)361.519, (float)7961.123);
        collision.Shape = shape2D;
        collision.GlobalPosition = new Vector2((float)3258.855, (float)-2680.216);
        collision.GlobalRotation = (float)-43.0;


        GetWindow().MinSize = new Vector2I(480, 280);
        GetWindow().MaxSize = new Vector2I(1920, 960);
        // Подключаемся к сигналу size_changed
        GetWindow().Connect("size_changed", new Callable(this, nameof(OnWindowSizeChanged)));
    }

    private void OnWindowSizeChanged()
    {
        Vector2 newSize = GetWindow().Size;

        //	windows.Scale = new Vector2(newSize.X , newSize.Y );
    }

    private void _on_hp()
    {
        Fader.ScenePath = "res://scene/scen/game_scen/main.tscn";
        Fader.d(collision);
        LoadNewScene();
    }
    private void _on_atack()
    {
        Fader.d(collision);
        Fader.ScenePath = "res://scene/scen/game_scen/main.tscn";
        LoadNewScene();
    }
    private void _on_strong()
    {
        Fader.d(collision);
        Fader.ScenePath = "res://scene/scen/game_scen/main.tscn";
        LoadNewScene();
    }
    private void _on_beck()
    {
        Fader.ScenePath = "res://scene/ui/meny/meny.tscn";
        LoadNewScene();
    }
    private void _on_speed()
    {
        Fader.d(collision);
        Fader.ScenePath = "res://scene/scen/game_scen/main.tscn";
        LoadNewScene();
    }
    private void _one_atack()
    {
        Fader.d(collision);
        Fader.ScenePath = "res://scene/scen/game_scen/main.tscn";
        LoadNewScene();
    }
}