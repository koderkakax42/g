using Godot;
using System;
using System.Collections.Generic;

namespace save
{
    public class save
    {
        private string path = "user://savegame.json";
       public void Save_status(int Health, int money)
       {
        var BigDate = new Godot.Collections.Dictionary
        {
            {"  ",Health},
            {"   ",money}
        };

        string json_effect = Json.Stringify(BigDate);

        using var file = FileAccess.Open(path, FileAccess.ModeFlags.Write);
        file.StoreString(json_effect);

       }

  public Dictionary<string, object> LoadData()
    {
        if (!FileAccess.FileExists(savePath))
        {
            GD.Print("Файл сохранения не найден!");
            return null;
        }

        using var file = FileAccess.Open(savePath, FileAccess.ModeFlags.Read);
        if (file == null)
        {
            GD.Print("Ошибка при открытии файла!");
            return null;
        }

        string json = file.GetAsText();

        try
        {
            var loadedData = JsonSerializer.Deserialize<Dictionary<string, object>>(json);
            GD.Print("Данные успешно загружены!");
            return loadedData;
        }
        catch (JsonException ex)
        {
            GD.Print("Ошибка при десериализации JSON: ", ex.Message);
            return null;
        }л
    }
}
}