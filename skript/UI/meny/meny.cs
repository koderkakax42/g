using Godot;
using System;



public partial class Meny : Control
{
    // Путь к сцене, которую нужно загрузить

    public void _on_button_pressed()
    {
        Fader.ScenePath = "res://scene/scen/game_scen/main.tscn";
        Fader.chec_save();
        LoadNewScene();
    }


    private void LoadNewScene()
    {
        // Получаем SceneTree
        SceneTree tree = GetTree();

        // Останавливаем текущую сцену.  Это ВАЖНО.
       
        tree.ChangeSceneToFile("res://scene/scen/load_scen/fader.tscn");

    }
    public override void _Ready()
    {

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

    private void _apgreid_button()
    {
        Fader.ScenePath = "res://scene/ui/meny/appdait.tscn";
        LoadNewScene();
    }
}
