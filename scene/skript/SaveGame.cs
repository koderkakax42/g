using Godot;
using System;

namespace SaveGame
{
  public class GameDataPlayer
  {
   
   public void SaveGame(string path, GameData data)
   {
    using (var file = FileAccess.Open(path, FileAccess.ModeFlags.Write))
    {
        if (file != null)
        {
            // Сохраняем позицию игрока (Vector2)
            file.StoreFloat(data.PlayerPosition.X);
            file.StoreFloat(data.PlayerPosition.Y);

            // Сохраняем здоровье (int) — преобразуем в uint
            file.Store32((uint)data.Health);

            // Сохраняем очки (int) — преобразуем в uint
            file.Store32((uint)data.Score);
        }
        else
        {
            GD.PrintErr("Failed to save game data.");
        }
    }
   }
   public GameData LoadGame(string path)
   {
    GameData data = new GameData();

    using (var file = FileAccess.Open(path, FileAccess.ModeFlags.Read))
    {
        if (file != null)
        {
            // Загружаем позицию игрока (Vector2)
            float x = file.GetFloat();
            float y = file.GetFloat();
            data.PlayerPosition = new Vector2(x, y);

            // Загружаем здоровье (int) — преобразуем из uint в int
            data.Health = (int)file.Get32();

            // Загружаем очки (int) — преобразуем из uint в int
            data.Score = (int)file.Get32();
        }
        else
        {
            GD.PrintErr("Failed to load game data.");
            return null;
        }
    }

    return data;
   }
  }
}  
    public  class GameData
    {
    public Vector2 PlayerPosition { get; set; }
    public int Health ;
    public int Score { get; set; }
    }
