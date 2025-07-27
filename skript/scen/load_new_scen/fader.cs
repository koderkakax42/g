using Godot;
using System.Text.Json;
using System;
using System.IO;


public partial class Fader : CanvasLayer
{
    private const string SAVE_PATH = "user://save.json";
    public static String ScenePath { get; set; }

    public static bool load = false;
    public static bool Load = false;
    private AnimationPlayer _animationPlayer;


    public static void chec_save()
    {

        try
        {
            GD.Print("save poisc");
            string absolutpath = ProjectSettings.GlobalizePath(SAVE_PATH);
            if (File.Exists(absolutpath))
            {

                string json = File.ReadAllText(absolutpath);

                if (string.IsNullOrWhiteSpace(json))
                {
                    GD.Print("fader 2");
                    return;
                }
                else
                {
                    ScenePath = "res://scene/save/save_game.tscn";
                }
            }
            else
            {
                GD.Print(absolutpath);
            }

        }
        catch (Exception ex)
        {
            GD.Print($"{ex.Message} fader.");
        }
    }
    public override void _Ready()
    {

        _animationPlayer = GetNode<AnimationPlayer>("AnimationPlayer");

        if (_animationPlayer == null)
        {
            GD.PrintErr("AnimationPlayer не найден!");
            _animationPlayer = GetNode<AnimationPlayer>("AnimationPlayer");
        }

        _animationPlayer.AnimationFinished += OnAnimationFinished;
        _animationPlayer.Play("FadeIn");

        GetWindow().MinSize = new Vector2I(480, 280);
        GetWindow().MaxSize = new Vector2I(1920, 960);
        // Подключаемся к сигналу size_changed
        GetWindow().Connect("size_changed", new Callable(this, nameof(OnWindowSizeChanged)));


    }

    private void OnWindowSizeChanged()
    {
        Vector2 newSize = GetWindow().Size;

        Scale = new Vector2(newSize.X, newSize.Y);
    }



    private void OnAnimationFinished(StringName animName)
    {

        if (animName.ToString().Equals("FadeIn"))
        {

            _animationPlayer.Play("FadeOut");

        }
        else if (animName.ToString().Equals("FadeOut"))
        {
            //Отписываемся от сигнала
            _animationPlayer.AnimationFinished -= OnAnimationFinished;
            //Как только FadeOut закончился, загружаем новую сце

            LoadNewScene();


        }
    }


    private void LoadNewScene()
    {
        SceneTree tree = GetTree();
        tree.ChangeSceneToFile(ScenePath);

        if (load)
        {         
            Main.load();
            load = false;
        }
    }

}
