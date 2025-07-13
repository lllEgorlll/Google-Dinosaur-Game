using System;
using System.Collections.Generic;
using System.Drawing;

namespace Dino.Classes

{
    public static class GameController
    {
        public static Player player;
        public static Image sprintesheet;

        public static List<Road> roads;
        public static List<Cactus> cactuses;
        public static List<Bird> birds;

        public static int dangerSpawn = 10;
        public static int countDangerSpawn = 0;

        private static int speedMultiplier = 1;


        private static Random r = new Random(); // Один объект Random

        public static void Init()
        {
            player = new Player(new PointF(50, 149), new Size(50, 50));

            roads = new List<Road>();
            cactuses = new List<Cactus>();
            birds = new List<Bird>();

            sprintesheet = Game2.Properties.Resources.sprite;
            player.SetSprite(sprintesheet);
            GenerateRoad();

            speedMultiplier = 1;
        }

        public static void IncreaseSpeed(int newSpeedMultiplier, Image newSprite)
        {
            speedMultiplier = newSpeedMultiplier;
            sprintesheet = newSprite;
            //sprintesheet = Game2.Properties.Resources.sprite___цветной_дино; ;
            player.SetSprite(sprintesheet); // Меняем спрайт игрока
        }


        public static void MoveMap()
        {

            int speed = 4 * speedMultiplier;

            for (int i = 0; i < roads.Count; i++)
            {
                roads[i].transform.position.X -= speed;
                if (roads[i].transform.position.X + roads[i].transform.size.Width < 0)
                {
                    roads.RemoveAt(i);
                    GetNewRoad();
                }
            }

            for (int i = 0; i < cactuses.Count; i++)
            {
                cactuses[i].transform.position.X -= speed;
                if (cactuses[i].transform.position.X + cactuses[i].transform.size.Width < 0)
                {
                    cactuses.RemoveAt(i);
                }
            }

            for (int i = 0; i < birds.Count; i++)
            {
                birds[i].transform.position.X -= speed;
                if (birds[i].transform.position.X + birds[i].transform.size.Width < 0)
                {
                    birds.RemoveAt(i);
                }
            }

        }

        public static void GetNewRoad()
        {
            Road road = new Road(new PointF(0 + 100 * 9, 200), new Size(100, 17));
            roads.Add(road);
            countDangerSpawn++;

            if (countDangerSpawn >= dangerSpawn)
            {
                dangerSpawn = r.Next(5, 9);
                countDangerSpawn = 0;
                int obj = r.Next(0, 2);

                switch (obj)
                {
                    case 0:
                        Cactus cactus = new Cactus(new PointF(0 + 100 * 9, 150), new Size(50, 50));
                        cactuses.Add(cactus);
                        break;
                    case 1:
                        Bird bird = new Bird(new PointF(0 + 100 * 9, 110), new Size(50, 50));
                        birds.Add(bird);
                        break;

                }

            }
        }

        public static void GenerateRoad()
        {
            for (int i = 0; i < 10; i++)
            {
                Road road = new Road(new PointF(0 + 100 * i, 200), new Size(100, 17));
                roads.Add(road);
                countDangerSpawn++;

            }
        }

        public static void DrawObjects(Graphics g)
        {
            // Отображаем дороги, кактусы и птиц
            foreach (var road in roads)
            {
                road.DrawSprite(g);
            }

            foreach (var cactus in cactuses)
            {
                cactus.DrawSprite(g);
            }

            foreach (var bird in birds)
            {
                bird.DrawSprite(g);
            }

            //DrawLives(g);
        }
    }
}
