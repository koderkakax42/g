using Godot;
using System.Text.Json;
using SaveGame;

public partial class fader : CanvasLayer
{
      private const string SAVE_PATH = "user://save.json";
     [Export]public static String ScenePath {get;set;}

   
    private AnimationPlayer _animationPlayer;


private void chec_save()
{
    try
    { 
     string absolutrath =  ProjectSettings.GlobalizePath(SAVE_PATH);

         if(File.ReadAllText(absolutrath) == "{}")
         {
            return;
         }


        ScenePath= "res://scene/save/save_game.tscn";
        meny.saveload -= chec_save;

    }
    catch(Exception ex)
    {
       GD.Print($"{ex.Message} fader.");
    }
}
    public override void _Ready()
    {
        meny.saveload += chec_save;

        _animationPlayer = GetNode<AnimationPlayer>("AnimationPlayer");

        if (_animationPlayer == null)
        {
            GD.PrintErr("AnimationPlayer не найден!");
            _animationPlayer = GetNode<AnimationPlayer>("AnimationPlayer");
        }

        _animationPlayer.AnimationFinished += OnAnimationFinished;
        _animationPlayer.Play("FadeIn");
   
         GetWindow().MinSize = new Vector2I(480,280 );
        GetWindow().MaxSize = new Vector2I(1920,960);
        // Подключаемся к сигналу size_changed
        GetWindow().Connect("size_changed", new Callable(this, nameof(OnWindowSizeChanged)));
    }

    private void OnWindowSizeChanged()
    {
        Vector2 newSize = GetWindow().Size;
        
		Scale = new Vector2(newSize.X , newSize.Y );
    }



    private void OnAnimationFinished(StringName animName)
    {

        if (animName.ToString().Equals("FadeIn"))
        {
         //   chec_save();
             _animationPlayer.Play("FadeOut");

        }
        else if (animName.ToString().Equals("FadeOut"))
        {
            //Отписываемся от сигнала
            _animationPlayer.AnimationFinished -= OnAnimationFinished;
            //Как только FadeOut закончился, загружаем новую сцену

            LoadNewScene();
        }
    }


    private void LoadNewScene()
    {
        SceneTree tree = GetTree();
        tree.ChangeSceneToFile(ScenePath);
    }

    
}