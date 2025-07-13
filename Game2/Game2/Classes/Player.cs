using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace Dino.Classes
{
    public class Player
    {
        public Physics physics;
        public int framesCount = 0;
        public int animationCount = 0;
        public int score = 0;
        public int lives = 3;
        public Image sprite;

        public Player (PointF position, Size sise)
        {
            physics = new Physics (position, sise);
            framesCount = 0;    
            score = 0;
            lives = 3;
            sprite =  GameController.sprintesheet;
        }

        public void SetSprite(Image newSprite)
        {
            sprite = newSprite;
        }

        public void DrawSprite(Graphics g)
        {
            if (physics.isCrouching)
            {
                DrawNeededSprite(g, 1870, 40, 109, 51, 118, 1.35f);
            }
            else
            {
                DrawNeededSprite(g, 1518, 0, 79, 91, 88, 1);
            }
        }

        public void DrawNeededSprite(Graphics g, int srcX, int srcY, int width, int height, int delta, float multiplier)
        { 
            framesCount++;
            if (framesCount <= 10)
                animationCount = 0;
            else if (framesCount >10 && framesCount <= 20)
                animationCount = 1;
            else if (framesCount >20)
                framesCount = 0;

            //g.DrawImage(GameController.sprintesheet, new Rectangle(new Point((int)physics.transform.position.X, (int)physics.transform.position.Y), new Size((int)(physics.transform.size.Width*multiplier), physics.transform.size.Height)), srcX+delta*animationCount,srcY,width,height,GraphicsUnit.Pixel);

            // Определяем целевой прямоугольник, куда будем рисовать
            Rectangle destRect = new Rectangle(
                (int)physics.transform.position.X,
                (int)physics.transform.position.Y,
                (int)(physics.transform.size.Width * multiplier),
                physics.transform.size.Height
            );

            // Определяем исходный прямоугольник для обрезки из спрайта
            Rectangle srcRect = new Rectangle(
                srcX + delta * animationCount,
                srcY,
                width,
                height
            );

            // Рисуем изображение с помощью DrawImage
            g.DrawImage(GameController.sprintesheet, destRect, srcRect, GraphicsUnit.Pixel);

        }
    }
}
