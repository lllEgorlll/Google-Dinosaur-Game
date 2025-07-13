
using Dino.Classes;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace Dino
{
    public partial class Form1 : Form
    {
        Player player;
        Timer mainTimer;
        Timer scoretimer;

        Timer flashTimer;
        Brush livesBrush;
        bool isFlashing = false;

        int level = 1;
        int speedMultiplier = 1;
        public Form1()
        {
            InitializeComponent();

            this.Width = 700;
            this.Height = 300;
            this.DoubleBuffered = true;
            this.Paint += new PaintEventHandler(DrawGame);
            this.KeyUp += new KeyEventHandler(OnKeyboardUp);
            this.KeyDown += new KeyEventHandler(OnKeyboardDown);

            mainTimer = new Timer();
            mainTimer.Interval = 10;
            mainTimer.Tick += new EventHandler(Update);

            scoretimer = new Timer();
            scoretimer.Interval = 100;
            scoretimer.Tick += new EventHandler(ScoreUpdate);



            Init();
        }

        private void OnKeyboardDown(object sender, KeyEventArgs e)
        {

            if (e.KeyCode == Keys.Down)
            {
                if (!player.physics.isJumping) // Запрещаем приседание во время прыжка
                {
                    player.physics.Crouch(); // Используем метод Crouch из Physics, который автоматически обновляет состояние
                }

            }
        }

        private void OnKeyboardUp(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Up:
                    if (!player.physics.isCrouching && !player.physics.isJumping) // Если персонаж не приседает, то можно прыгать
                    {
                        //player.physics.isCrouching = false;
                        player.physics.AddForse(); // Начинаем прыжок
                    }
                    break;
                case Keys.Down:
                    player.physics.StandUp(); // Используем метод StandUp из Physics, чтобы встать
                    break;
            }

        }

        public void Init()
        {
            GameController.Init();
            player = new Player(new PointF(50, 149), new Size(50, 50));
            player.lives = 3; // Устанавливаем 3 жизни
            player.score = 0; // Сбрасываем счет

            // Настройка таймера для мигания
            flashTimer = new Timer();
            flashTimer.Interval = 200; // Мигание на 200 мс
            flashTimer.Tick += new EventHandler(ResetLivesColor);

            livesBrush = Brushes.Black; // Изначально текст черного цвета

            level = 1;
            speedMultiplier = 1;
            player.SetSprite(Game2.Properties.Resources.sprite); // начальный спрайт

            mainTimer.Start();
            scoretimer.Start();

            Refresh();
        }

        public void ScoreUpdate(object sender, EventArgs e)
        {
            player.score++; // Увеличиваем счет

            this.Text = "Dino - Score: " + player.score;
            Refresh();

            // Проверка для увеличения уровня
            if (player.score == 100 && level == 1)
            {
                LevelUp(2, 2, Game2.Properties.Resources.sprite___цветной_дино);
            }
            else if (player.score == 200 && level == 2)
            {
                LevelUp(3, 3, Game2.Properties.Resources.sprite_car);
            }
            Refresh();
        }

        private void LevelUp(int newLevel, int newSpeedMultiplier, Image newSprite)
        {
            level = newLevel;
            GameController.IncreaseSpeed(newSpeedMultiplier, newSprite);
        }


        public void Update(object sender, EventArgs e)
        {

            if (player.physics.Collide()) // Проверяем на столкновение
            {
                player.lives--; // Уменьшаем количество жизней
                if (player.lives <= 0) // Если жизни закончились
                {
                    Init(); // Перезапускаем игру
                }
                // Меняем цвет текста на красный при столкновении
                livesBrush = Brushes.Red;
                isFlashing = true;
                flashTimer.Start(); // Запускаем таймер для возврата цвета
            }
            player.physics.ApplyPhysics();
            GameController.MoveMap();
            Refresh();
        }

        private void ResetLivesColor(object sender, EventArgs e)
        {
            if (isFlashing)
            {
                livesBrush = Brushes.Black; // Возвращаем цвет в черный
                isFlashing = false;
                flashTimer.Stop(); // Останавливаем таймер
            }
        }

        private void DrawGame(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;

            // Вызываем метод для отображения жизней
            DrawLives(g);

            player.DrawSprite(g);
            GameController.DrawObjects(g);


        }

        public void DrawLives(Graphics g)
        {
            // Настройка шрифта и цвета для текста
            Font font = new Font("Arial", 12, FontStyle.Bold);
            //Brush brush = Brushes.Black;

            Image heartImage = Game2.Properties.Resources.сердечко;
            //Image heartImage = Image.FromFile("Resources/сердечко.png");

            // Размер и позиция иконки сердца
            int iconSize = 20;
            int iconX = this.Width - 107;
            int iconY = 18;

            // Рисуем иконку сердца
            g.DrawImage(heartImage, iconX, iconY, iconSize, iconSize);

            // Позиция текста в правом верхнем углу
            string livesText = "Lives: " + player.lives;
            g.DrawString(livesText, font, livesBrush, this.Width - 80, 20); // 80 пикселей от правого края
        }
    }
}
