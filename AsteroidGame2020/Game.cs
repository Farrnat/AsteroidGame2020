using AsteroidGame2020;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AsteroidGame.VisualObjects;
using AsteroidGame2020.VisualObjects;
using AsteroidGame2020.VisualObjects.Interfaces;

namespace AsteroidGame
{
    static class Game
    {
        private const int __FrameTimeout = 40; //таймаут отрисовки одной сцены

        private static BufferedGraphicsContext __Context;
        private static BufferedGraphics __Buffer;
        private static Timer __Timer;
        private static int _Score = 0;

        public static int Width { get; set; }

        public static int Height { get; set; }

        //static Game()
        //{

        //}

        public static void Initialize(Form form)
        {
            Width = form.Width;
            Height = form.Height;

            __Context = BufferedGraphicsManager.Current;
            var g = form.CreateGraphics();
            __Buffer = __Context.Allocate(g, new Rectangle(0, 0, Width, Height));

            var timer = new Timer { Interval = __FrameTimeout };
            timer.Tick += OnTimerTick;
            timer.Start();
            __Timer = timer;

            form.KeyDown += OnFormKeyDown;
        }

        private static void OnFormKeyDown(object Sender, KeyEventArgs E)
        {
            switch (E.KeyCode)
            {
                case Keys.ControlKey:
                    //__Bullet = new Bullet(__Ship.Position.Y);
                    __Bullets.Add(new Bullet(__Ship.Position.Y));
                    break;

                case Keys.Up:
                    __Ship.MoveUp();
                    break;

                case Keys.Down:
                    __Ship.MoveDown();
                    break;
                    
            }
        }

        private static void OnTimerTick(object sender, EventArgs e)
        {
            Update();
            Draw();
        }

        private static SpaceShip __Ship;

        private static VisualObject[] __GameObjects;
        // private static Bullet __Bullet;
        private static List<Bullet> __Bullets = new List<Bullet>();
         
        public static void Load()
        {
            var game_objects = new List<VisualObject>();

            var rnd = new Random();

            const int stars_count = 30;
            const int star_size = 20;
            const int star_max_speed = 20;
            for (var i = 0; i < stars_count; i++)
                game_objects.Add(new Star(
                    new Point(rnd.Next(0, Width), rnd.Next(0, Height)),
                    new Point(-rnd.Next(0, star_max_speed), 0),
                    star_size));

           /* const int ellipses_count = 20;
            const int ellipses_size_x = 20;
            const int ellipses_size_y = 30;
            for (var i = 0; i < ellipses_count; i++)
                game_objects.Add(new EllipseObject(
                   new Point(rnd.Next(0, Width), rnd.Next(0, Height)),
                    new Point(-rnd.Next(0, star_max_speed), 0),
                   new Size(ellipses_size_x, ellipses_size_y)));*/

            const int smallstars_count = 150;
            const int smallstars_size = 5;
            for (var i = 0; i < smallstars_count; i++)
                game_objects.Add(new SmallStar(
                     new Point(rnd.Next(0, Width), rnd.Next(0, Height)),
                   new Point(-rnd.Next(0, star_max_speed), 0),
                     smallstars_size));

            const int asteroids_count = 15;
            const int asteroid_size = 25;
            const int asteroid_max_speed = 20;
            for (var i = 0; i < asteroids_count; i++)
                game_objects.Add(new Asteroid(
                    new Point(rnd.Next(0, Width), rnd.Next(0, Height)),
                    new Point(-rnd.Next(0, asteroid_max_speed), 0),
                    asteroid_size));

            const int aids_count = 3;
            const int aid_size = 25;
            const int aid_max_speed = 30;
            for(var i =0; i<aids_count; i++)
                game_objects.Add(new Aid(
                    new Point(rnd.Next(0, Width), rnd.Next(0, Height)),
                    new Point(-rnd.Next(0, aid_max_speed), 0),
                    aid_size));






            __GameObjects = game_objects.ToArray();
           // __Bullet = new Bullet(200);
            __Ship = new SpaceShip(new Point(10, 400), new Point(5, 5), new Size(10, 10));
            __Ship.ShipDestroyed += OnShipDestroyed;

        }

        private static void OnShipDestroyed(object Sender, EventArgs E)
        {
            __Timer.Stop();
            __Buffer.Graphics.Clear(Color.DarkBlue);
            __Buffer.Graphics.DrawString("Game over!!!", new Font(FontFamily.GenericSerif, 60, FontStyle.Bold), Brushes.Red, 200, 100);
            __Buffer.Render();
        }


        public static void Draw()
        {
            if (__Ship.Energy <= 0) return;

            var g = __Buffer.Graphics;
            g.Clear(Color.Black);

            //g.DrawRectangle(Pens.White, new Rectangle(50, 50, 200, 200));
            //g.FillEllipse(Brushes.Red, new Rectangle(100, 50, 70, 120));

            foreach (var visual_object in __GameObjects)
                visual_object?.Draw(g);

            //__Bullet?.Draw(g);

            foreach (var bullet in __Bullets)
                bullet.Draw(g);

            __Ship.Draw(g);

            g.DrawString($"Energy: {__Ship.Energy}", new Font(FontFamily.GenericSerif, 14, FontStyle.Italic), Brushes.White, 10, 10);
            g.DrawString($"Score: {_Score}", new Font(FontFamily.GenericSerif, 14, FontStyle.Italic), Brushes.White, 10, 30); 

            __Buffer.Render();
        }

        public static void Update()
        {
            foreach (var visual_object in __GameObjects)
                visual_object?.Update();

            var bullets_to_remove = new List<Bullet>();
            foreach(var bullet in __Bullets)
            {
                bullet.Update();
                if (bullet.Position.X > Width)
                    bullets_to_remove.Add(bullet);
            }
            //__Bullet?.Update();
            

            for (var i = 0; i < __GameObjects.Length; i++)
            {
                var obj = __GameObjects[i];
                if (obj is ICollision)
                {
                    var collision_object = (ICollision)obj;
                    __Ship.CheckCollision(collision_object); 

                    foreach(var bullet in __Bullets.ToArray())
                    {
                        if (bullet.CheckCollision(collision_object))
                       {
                            bullets_to_remove.Add(bullet);
                            __GameObjects[i] = null;
                            MessageBox.Show("Астероид уничтожен!", "Столкновение", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            _Score += 10;
                        }
                    }

                  /* if(__Bullets.Any(b => b.CheckCollision(collision_object)))
                    {
                        __GameObjects[i] = null;
                        MessageBox.Show("Астероид уничтожен!", "Столкновение", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                    foreach (var bullet in __Bullets.Where(b => b.CheckCollision(collision_object)))
                        __Bullets.Remove(bullet);*/
                }
            }

            foreach (var bullet in bullets_to_remove)
                __Bullets.Remove(bullet);
        }
    }
}

