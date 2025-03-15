using Godot;
using System;

public partial class meny : Node
{
  
    [Export]
    public String TargetScenePath ; // Путь к сцене, которую нужно загрузить


    public void _on_button_pressed()
    {
        if (TargetScenePath == null)
        {
            GD.PrintErr("TargetScenePath is not assigned!");
            return; // Важно: выйти из функции, если путь не задан
        }
       fader.ScenePath = "res://scene/main.tscn";
       LoadNewScene();
    }


     private void LoadNewScene()
    {
        // Получаем SceneTree
        SceneTree tree = GetTree();
        
        // Останавливаем текущую сцену.  Это ВАЖНО.
        tree.ChangeSceneToFile(TargetScenePath);
    }
}
   