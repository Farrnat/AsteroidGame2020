﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using AsteroidGame.VisualObjects;

namespace AsteroidGame2020.VisualObjects
{
    public class EllipseObject : VisualObject
    {
        public EllipseObject(Point Position, Point Direction, Size Size) : base(Position, Direction, Size)
        {

        }

        public override void Draw(Graphics g)
        {
            g.DrawEllipse(Pens.White, Rect);
        }
    }
}
