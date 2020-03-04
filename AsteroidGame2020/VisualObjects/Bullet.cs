using AsteroidGame.VisualObjects;
using AsteroidGame2020.VisualObjects.Interfaces;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsteroidGame2020.VisualObjects
{
    public class Bullet : CollisionObject
    {
        private int _Score = 0;
        public int Score => _Score;

        private const int __BulletSizeX = 20;
        private const int __BulletSizeY = 5;

        public Bullet(int Position) : base(new Point(0, Position), Point.Empty, new Size(__BulletSizeX, __BulletSizeY))
        {
        }

        

        public override void Draw(Graphics g)
        {
            //g.FillEllipse(Brushes.Red, _Position.X, _Position.Y, _Size.Width, _Size.Height);
            // g.DrawEllipse(Pens.White, _Position.X, _Position.Y, _Size.Width, _Size.Height);

            var rect = Rect;
             g.FillEllipse(Brushes.Red, rect);
            g.DrawEllipse(Pens.White, rect);
        }

        public override void Update()
        {
            _Position = new Point(_Position.X + 3, _Position.Y);
        }
    }
}
