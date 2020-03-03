using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using AsteroidGame;

namespace AsteroidGame.VisualObjects
{
    public abstract class ImageObject : VisualObject
    {
        private Image _Image;

        protected ImageObject(Point Position, Point Direction, Size Size, Image Image) : base(Position, Direction, Size)
        {
            _Image = Image;
        }

        public override void Draw(Graphics g)
        {
            var position = new PointF(_Position.X, _Position.Y);
            g.DrawImage(_Image, _Position.X, _Position.Y, _Size.Width, _Size.Height);
        }
    }
}
