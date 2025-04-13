using Godot;
using System;

public partial class inventory : Node
{
      private float _timer = 0.0f;  
  private Node2D target;
  [Export] public PackedScene BulletScene { get; set; }

    private TouchScreenButton _attack;     // Кнопка атаки
    private bool _isAttacking = false;           // Состояние атаки

    public override void _Ready()
    {
        target = GetTree().GetFirstNodeInGroup("Player")as Node2D;



        _attack = GetNode<TouchScreenButton>("attak");
        if (_attack == null)
        {
            GD.PrintErr("Ошибка: Кнопка атаки не найдена!");
            return;
        }
        // Подключаемся к сигналам pressed и released
        _attack.Pressed += OnAttackButtonPressed;
        _attack.Released += OnAttackButtonReleased;
    }

    public override void _ExitTree()
    {
         _attack.Pressed -= OnAttackButtonPressed;
         _attack.Released -= OnAttackButtonReleased;
    }

    private void OnAttackButtonPressed()
    {
        _isAttacking = true;
        GD.Print("Кнопка атаки нажата!");
        // Здесь может быть код, который выполняет действие при нажатии кнопки
    }

    private void OnAttackButtonReleased()
    {
        _isAttacking = false;
        GD.Print("Кнопка атаки отпущена!");
        // Здесь может быть код, который выполняет действие при отпускании кнопки
    }

   // public override void _Process(double delta)
   // //    {
//            _timer += (float)delta;
  //          GetParent().AddChild(bulletInstance);
   //     }
   // }


}