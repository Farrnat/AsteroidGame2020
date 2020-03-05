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
    public class Aid : ImageObject, ICollision
    {
        public int Heal { get; set; } = 10;

        public Aid(Point Position, Point Direction, int ImageSize) : base(Position, Direction, new Size(ImageSize, ImageSize), Properties.Resources.Aid)
        {
        }

        public bool CheckCollision(ICollision obj) => Rect.IntersectsWith(obj.Rect);

    }
}
