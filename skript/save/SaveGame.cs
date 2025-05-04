using Godot;
using System.Text.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Diagnostics;


  public partial class SaveGame : Node2D
  {
   public static Dictionary<string,float>? save{get;set;}= new Dictionary<string, float> {{"",0f}};
   public static Dictionary<string,JsonElement>? load{get;set;} = null;
    
    
   private const string SAVE_PATH = "user://save.json";
    private static string absolutrath =  ProjectSettings.GlobalizePath(SAVE_PATH);


    public void Save_data_Game()
    {
        
       try
        {
            GD.Print("save");
            // Сериализуем данные в JSON (с отступами для читаемости)
            string json = JsonSerializer.Serialize(save, new JsonSerializerOptions { WriteIndented = true });
            // Записываем в файл
            File.WriteAllText(absolutrath, json);

        }
        catch (Exception ex)
        {
            GD.Print($"Ошибка сохранения: {ex.Message}");
        }
      
    }
    
    public void delsave()
    {
         string json = JsonSerializer.Serialize( new JsonSerializerOptions { WriteIndented = true });
            // Записываем в файл
            File.WriteAllText(absolutrath, json);

    }
    public void LoadGame()
    {
        if (!File.Exists(absolutrath))
        {
            return ;
        }
        string json = File.ReadAllText(absolutrath);
        json.Trim();
        try
        {
            load = JsonSerializer.Deserialize<Dictionary<string,JsonElement>>(json);
          
            if(load != null)
            foreach(var person in load)
            {
              GD.Print(person.Key + " " + person.Value);
            }

            return;
        }
        catch(Exception ex)
        {
            GD.Print($"{ex.Message} save game eror");
            return;
        }
    }
    public static event Action new_game = delegate{};
	public static event Action save_playe_game=delegate{};
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	  Player.dead += delsave;
	  UiPcPlaer.save += Save_data_Game;
	}

	public override void _Process(double delta)
	{
	}
	public void _on_new_game()
	{
	   Fader.ScenePath = "res://scene/scen/game_scen/main.tscn";

       new_game?.Invoke();

	   LoadNewScene();
	}
	public void _on_save()
	{
	  Fader.ScenePath = "res://scene/scen/game_scen/main.tscn";
      
      save_playe_game?.Invoke();
	}
	public void _on_undo()
	{
       Fader.ScenePath = "res://scene/ui/meny/meny.tscn";
	}

	 private void LoadNewScene()
    {
        SceneTree tree = GetTree();
        tree.ChangeSceneToFile("res://scene/scen/load_scen/fader.tscn");
    }
  }
