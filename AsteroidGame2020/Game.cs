using AsteroidGame2020;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AsteroidGame
{
    static class Game
    {
        private static BufferedGraphicsContext __Context;
        private static BufferedGraphics __Buffer;

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
            Graphics g = form.CreateGraphics();
            __Buffer = __Context.Allocate(g, new Rectangle(0, 0, Width, Height));

            var timer = new Timer { Interval = 100 };
            timer.Tick += OnTimerTick;
            timer.Start();
        }

        private static void OnTimerTick(object sender, EventArgs e)
        {
            Update();
            Draw();
        }

        private static VisualObject[] __GameObjects;
        public static void Load()
        {
            __GameObjects = new VisualObject[30];
            for (var i = 0; i < __GameObjects.Length / 2; i++)
                __GameObjects[i] = new VisualObject(
                    new Point(600, i * 20),
                    new Point(15 - i, 20 - i),
                    new Size(20, 20));

           /* for (var i = __GameObjects.Length / 2; i < __GameObjects.Length; i++)
                __GameObjects[i] = new Star(
                    new Point(600, i * 20),
                    new Point(-i, 0),
                    20); пропали большие звезды??*/

            for (var i = __GameObjects.Length / 2; i < __GameObjects.Length; i++)
                __GameObjects[i] = new SmallStar(
                    new Point(600, i * 20),
                    new Point(-i, 0),
                    5);

            

           // var image = AsteroidGame2020.Properties.Resources.Asteroid;
            //var image_object = new ImageObject(new Point(0, 5), new Point(5, 7), new Size(70, 70), image);

        }




        public static void Draw()
        {
            var g = __Buffer.Graphics;
            g.Clear(Color.Black);

            //g.DrawRectangle(Pens.White, new Rectangle(50, 50, 200, 200));
            //g.FillEllipse(Brushes.Red, new Rectangle(100, 50, 70, 120));

            foreach (var visual_object in __GameObjects)
                visual_object.Draw(g);

            __Buffer.Render();
        }

        public static void Update()
        {
            foreach (var visual_object in __GameObjects)
                visual_object.Update();
        }
    }
}

