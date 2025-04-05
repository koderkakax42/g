using Godot;
using System.IO;
using System.Text.Json;

namespace SaveGame
{
public static class SaveSystem
{
    private const string SAVE_PATH = "user://save.json";
    
    public static void SaveGame(GameData data)
    {
        var options = new JsonSerializerOptions
        {
            WriteIndented = true,  // Читаемый формат
            IncludeFields = true   // Сохранять поля (не только свойства)
        };
        
        string json = JsonSerializer.Serialize(data, options);
        File.WriteAllText(SAVE_PATH, json);
        GD.Print(data);
        GD.Print("Игра сохранена!");
    }
    
    public static GameData LoadGame()
    {
        if (!File.Exists(SAVE_PATH))
        {
            GD.Print("Сохранение не найдено, создаем новое");
            return new GameData();
        }
        GD.Print(File.Exists(SAVE_PATH));
        string json = File.ReadAllText(SAVE_PATH);
        try
        {
            return JsonSerializer.Deserialize<GameData>(json) ?? new GameData();
        }
        catch
        {
            GD.PrintErr("Ошибка загрузки, создаем новое сохранение");
            return new GameData();
        }
    }
  }
}  
    public  class GameData
    {
    public Vector2 PlayerPosition { get; set; }
    public int Health {get;set;}
    public int Score { get; set; }
    public Vector2 enemyposition {get;set;}
    public int enemyhp{get;set;}
    public int  enemynamber{get;set;}

    }
