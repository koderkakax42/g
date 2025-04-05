using Godot;
using System;
using System.Threading.Tasks;

public partial class fader : CanvasLayer
{
   

     [Export]public static String ScenePath {get;set;}

    private AnimationPlayer _animationPlayer;

    public override void _Ready()
    {
        _animationPlayer = GetNode<AnimationPlayer>("AnimationPlayer");

        if (_animationPlayer == null)
        {
            GD.PrintErr("AnimationPlayer не найден!");
            _animationPlayer = GetNode<AnimationPlayer>("AnimationPlayer");
            return; // Важно: выходим из _Ready(), если AnimationPlayer не найден
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
        
	//	windows.Scale = new Vector2(newSize.X , newSize.Y );
    }
    //Этот метод вызывается AnimationPlayer, когда анимация заканчивается
    private void OnAnimationFinished(StringName animName)
    {

        if (animName.ToString().Equals("FadeIn"))
        {
            GD.Print("FadeIn Finished");
            //После того, как FadeIn закончился, воспроизвести FadeOut
             _animationPlayer.Play("FadeOut");
        }
        else if (animName.ToString().Equals("FadeOut"))
        {
            GD.Print("FadeOut Finished");
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