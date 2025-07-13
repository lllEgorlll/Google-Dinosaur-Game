using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dino.Classes
{
    public class Road
    {
        public Transform transform;

        public Road(PointF pos, Size size)
        {
            transform = new Transform(pos, size);
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
            Rectangle srcRect = new Rectangle(2300, 112, 100, 17); // Область исходного изображения
            g.DrawImage(GameController.sprintesheet, destRect, srcRect, GraphicsUnit.Pixel);

            //g.DrawImage(GameController.sprintesheet, new Rectangle(new Point((int)transform.position.X, (int)transform.position.Y)), new Size(transform.size.Width, transform.size.Height),2300,112,100,17, GraphicsUnit.Pixel);
        }
    }
}
