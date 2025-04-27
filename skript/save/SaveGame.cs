using Godot;
using System.Text.Json;

namespace SaveGame 
{
  public partial class SaveGame : Node2D
  {
   public static Dictionary<string,float>? save{get;set;}= new Dictionary<string, float> {{"",0f}};
   public static Dictionary<string,JsonElement>? load{get;set;} = null;
    
    
   private const string SAVE_PATH = "user://save.json";
    private static string absolutrath =  ProjectSettings.GlobalizePath(SAVE_PATH);
    public virtual void Save_data_Game()
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
            Console.WriteLine($"Ошибка сохранения: {ex.Message}");
        }
      
    }
    
    public virtual void delsave()
    {
         string json = JsonSerializer.Serialize( new JsonSerializerOptions { WriteIndented = true });
            // Записываем в файл
            File.WriteAllText(absolutrath, json);

    }
    public virtual void LoadGame()
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
  }
}
