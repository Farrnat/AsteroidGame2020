﻿using AsteroidGame.VisualObjects;
using AsteroidGame2020.VisualObjects.Interfaces;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsteroidGame2020.VisualObjects
{
    public class Asteroid : ImageObject, ICollision
    {
        public int Power { get; set; } = 10;
        public Asteroid(Point Position, Point Direction, int ImageSize) : base(Position, Direction, new Size(ImageSize, ImageSize), Properties.Resources.Asteroid)
        {
        }

        public bool CheckCollision(ICollision obj) => Rect.IntersectsWith(obj.Rect);

    }
}
