using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Dino.Classes
{
    public class Physics
    {
        public Transform transform;
        float gravity;
        float a;

        public bool isJumping;
        public bool isCrouching;

        public Physics(PointF position, Size size)
        {
            transform = new Transform(position, size);
            gravity = 0;
            a = 0.4f;

            isJumping = false;
            isCrouching = false;    
        }

        public void ApplyPhysics()
        {
            CalculatePhysics();
        }

        public void CalculatePhysics()
        {
            // Плавное падение или продолжение прыжка
            if (transform.position.Y < 150 || isJumping)
            {
                //transform.position.Y = 150;
                transform.position.Y += gravity;
                gravity += a;  // Увеличиваем гравитацию для падения
            }

            // Если персонаж достиг земли, остановить прыжок
            if (transform.position.Y >= 150)
            {
                transform.position.Y = 150; // Устанавливаем точку, где персонаж "стоит" на земле
                isJumping = false;
                gravity = 0; // Сбрасываем гравитацию, когда игрок на земле
            }

            // Если персонаж приседает, скорректируем его положение
            if (isCrouching)
            {
                transform.position = new PointF(transform.position.X, 175);
            }
        }

        // Этот метод контролирует перемещение персонажа вниз (приседание)
        public void Crouch()
        {
            if (!isJumping && !isCrouching) 
            {
                isCrouching = true;
                transform.size = new Size(transform.size.Width, 25); 
                // Корректируем положение только если персонаж на земле
                if (transform.position.Y == 150)
                {
                    transform.position = new PointF(transform.position.X, transform.position.Y + 25);
                }
            }
        }

        public void StandUp()
        {
            if (isCrouching) // Подниматься можно только если персонаж не в прыжке
            {
                isCrouching = false;
                transform.size = new Size(transform.size.Width, 50); // Восстанавливаем нормальную высоту
                if (transform.position.Y == 175)
                {
                    transform.position = new PointF(transform.position.X, transform.position.Y - 25);
                }

            }
        }

        public bool Collide() 
        {
            // Проверка на столкновение с кактусами
            foreach (var cactus in GameController.cactuses)
            {
                if (IsCollision(cactus.transform))
                {
                    HandleCollision();
                    GameController.cactuses.Remove(cactus); // Удаляем кактус после столкновения
                    return true;
                }
            }

            // Проверка на столкновение с птицами
            foreach (var bird in GameController.birds)
            {
                if (IsCollision(bird.transform))
                {
                    HandleCollision();
                    GameController.birds.Remove(bird); // Удаляем птицу после столкновения
                    return true;
                }
            }

            return false;
        }

        private bool IsCollision(Transform otherTransform)
        {
            // Проверка столкновения между двумя объектами
            PointF delta = new PointF(
                (transform.position.X + transform.size.Width / 2) - (otherTransform.position.X + otherTransform.size.Width / 2),
                (transform.position.Y + transform.size.Height / 2) - (otherTransform.position.Y + otherTransform.size.Height / 2)
            );

            return Math.Abs(delta.X) <= transform.size.Width / 2 + otherTransform.size.Width / 2 &&
                   Math.Abs(delta.Y) <= transform.size.Height / 2 + otherTransform.size.Height / 2;
        }

        private void HandleCollision()
        {
            Player player = GameController.player;
            player.lives--; // Уменьшаем жизни

            if (player.lives <= 0)
            {
                MessageBox.Show("Ur result is " + player.score);
                player.score = 0;
                GameController.Init(); // Перезапускаем игру, когда жизни закончились
                player.lives = 3; // Восстанавливаем 3 жизни
            }
        }


        public void AddForse()
        {
            if (!isJumping)
            {
                isJumping = true;
                gravity = -9;
            }
        }

    }
}
