using Godot;
using System.IO;
using System.Text.Json;

namespace SaveGame 
{
  public  partial class SaveGame : Node2D
  {
   public static GameData data = new GameData();
   private const string SAVE_PATH = "user://save.json";
    public static void Save_data_Game()
    {
      
      DirAccess.MakeDirRecursiveAbsolute("user://");
      using(var File = Godot.FileAccess.Open(SAVE_PATH,Godot.FileAccess.ModeFlags.Write))
      {
        File.StoreString("{}");
      }

        
        var options = new JsonSerializerOptions
        {
            WriteIndented = true,  // Читаемый формат
            IncludeFields = true   // Сохранять поля (не только свойства)
        };
        
        string? json = JsonSerializer.Serialize(data, options);
       // Получаем абсолютный путь
        string absolutePath = ProjectSettings.GlobalizePath("user://save.json");
        File.WriteAllText(absolutePath,json);
     
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
            return JsonSerializer.Deserialize<GameData>(json);
        }
        catch
        {
            GD.PrintErr("Ошибка загрузки, создаем новое сохранение");
            return new GameData();
        }
    }

    public override void _Ready()
    {

    }

  }

    public struct GameData
    {
    public float PlayerPositionX { get; set; }
     public float PlayerPositionY { get; set; }
    public int Health {get;set;}
    public int money { get; set; }
    public Vector2 enemyposition {get;set;}
    public int enemyhp{get;set;}
    public int  enemynamber{get;set;}
     public string EnemyId { get; set; }

    }
    
}
