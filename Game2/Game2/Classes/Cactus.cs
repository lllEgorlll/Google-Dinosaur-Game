using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dino.Classes
{
    public class Cactus
    {
        public Transform transform;
        private int scrX;
        Random r = new Random();

        public Cactus(PointF pos, Size size)
        {
            transform = new Transform(pos, size);
            ChooseRandomPic();
        }

        public void ChooseRandomPic()
        {
            int rnd = r.Next(0, 5);
            switch (rnd)
            {
                case 0:
                    scrX = 754;
                    break;
                case 1:
                    scrX = 804;
                    break;
                case 2:
                    scrX = 704;
                    break;
                case 3:
                    scrX = 654;
                    break;
                case 4:
                    scrX = 851;
                    break;

            }
        }

        public void DrawSprite(Graphics g)
        {


            // Определяем прямоугольник, в котором будет отрисовано изображение
            Rectangle destRect = new Rectangle(
                (int)transform.position.X,
                (int)transform.position.Y,
                transform.size.Width,
                transform.size.Height
            );

            // Отрисовываем часть изображения из GameController.spritesheet
            if (scrX <= 804)
            {
                Rectangle srcRect = new Rectangle(scrX, 0, 48, 100); // Область исходного изображения
                g.DrawImage(GameController.sprintesheet, destRect, srcRect, GraphicsUnit.Pixel);
            }
            else if (scrX <=  851)
            {
                Rectangle srcRect = new Rectangle(scrX, 0, 100, 100); // Область исходного изображения
                g.DrawImage(GameController.sprintesheet, destRect, srcRect, GraphicsUnit.Pixel);
            }
                


            //g.DrawImage(GameController.sprintesheet, new Rectangle(new Point((int)transform.position.X, (int)transform.position.Y)), new Size(transform.size.Width, transform.size.Height),2300,112,100,17, GraphicsUnit.Pixel);
        }


    }
}
