using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dino.Classes
{
    public class Bird
    {
        public Transform transform;
        int frameCount = 0;
        int animationCount = 0;

        public Bird(PointF poc, Size size)
        {
            transform = new Transform(poc, size);   

        }

        public void DrawSprite(Graphics g)
        {
            frameCount++;
            if (frameCount <= 10)
                animationCount = 0;
            else if (frameCount > 10 && frameCount <= 20)
                animationCount = 1;
            else if (frameCount > 20)
                frameCount = 0;

            // Определяем прямоугольник, в котором будет отрисовано изображение
            Rectangle destRect = new Rectangle(
                (int)transform.position.X,
                (int)transform.position.Y,
                transform.size.Width,
                transform.size.Height
            );

            // Отрисовываем часть изображения из GameController.spritesheet
            Rectangle srcRect = new Rectangle(262+92*animationCount, 6, 83, 71); // Область исходного изображения
            g.DrawImage(GameController.sprintesheet, destRect, srcRect, GraphicsUnit.Pixel);
        }


    }
}
