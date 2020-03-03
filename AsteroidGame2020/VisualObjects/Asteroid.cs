using AsteroidGame.VisualObjects;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsteroidGame2020.VisualObjects
{
    public class Asteroid : ImageObject
    {
        public Asteroid(Point Position, Point Direction, int ImageSize) : base(Position, Direction, new Size(ImageSize, ImageSize), Properties.Resources.Asteroid)
        {
        }
    }
}
