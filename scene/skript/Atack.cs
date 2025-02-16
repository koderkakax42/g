using Godot;

namespace Atack
{
    public partial class Atack : CharacterBody2D
    {
        [Export] public int Speed = 350;
        [Export] public int Damage = 10; // Урон теперь передается через Atack

        private Node2D target;

        public override void _Ready()
        {
            target = GetTree().GetFirstNodeInGroup("enemy") as Node2D;
            if (target == null)
            {
                GD.PrintErr("enemy error 404");
            }
        }

        public override void _PhysicsProcess(double delta)
        {
            if (target == null) return; // Exit если нет врага.

            Vector2 direction = (target.GlobalPosition - GlobalPosition).Normalized();
            Velocity = direction * Speed;
            MoveAndSlide();

            // Проверяем столкновения после MoveAndSlide()
            for (int i = 0; i < GetSlideCollisionCount(); i++)
            {
                KinematicCollision2D collision = GetSlideCollision(i);
                if (collision.GetCollider() is Node2D colliderNode)
                {
                    if (colliderNode is enemy enemy)
                    {
                        // Наносим урон врагу при столкновении. Damage берется из класса Atack
                        enemy.TakeDamage(Damage);
                        QueueFree(); // Уничтожаем пулю после попадания
                        break; // Прекращаем цикл, чтобы не нанести урон несколько раз за кадр
                    }
                }
            }
        }
    }

    // Больше не нужен класс AtackDirection
    // Удалите этот класс.  Вся логика перемещена в Atack.
}