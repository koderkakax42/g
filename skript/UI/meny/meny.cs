using Godot;
using System;

public partial class meny : Node
{
    public String TargetScenePath = "res://scene/scen/load_scen/fader.tscn"; // Путь к сцене, которую нужно загрузить
    

    public void _on_button_pressed()
    {
        if (TargetScenePath == null)
        {
            GD.PrintErr("TargetScenePath is not assigned!");
            return; // Важно: выйти из функции, если путь не задан
        }
       fader.ScenePath = "res://scene/scen/game_scen/main.tscn";
       LoadNewScene();
       fader.chec_save();
    }


     private void LoadNewScene()
    {
        // Получаем SceneTree
        SceneTree tree = GetTree();
        
        // Останавливаем текущую сцену.  Это ВАЖНО.
        tree.ChangeSceneToFile(TargetScenePath);
    }
    public override void _Ready()
    {
       
         GetWindow().MinSize = new Vector2I(480,280 );
        GetWindow().MaxSize = new Vector2I(1920,960);
        // Подключаемся к сигналу size_changed
        GetWindow().Connect("size_changed", new Callable(this, nameof(OnWindowSizeChanged)));
    }

    private void OnWindowSizeChanged()
    {
        Vector2 newSize = GetWindow().Size;
        
	//	windows.Scale = new Vector2(newSize.X , newSize.Y );
    }
}
   