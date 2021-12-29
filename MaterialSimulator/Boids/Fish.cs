using Raylib_cs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace MaterialSimulator.Game
{
    public  class Fish
    {
        public Guid id;
        public FishType type;
        public int size;
        public int viewRadius = 100;
        public int speed = 2;
        public Vector2 center;
        public Vector2 point1;
        public Vector2 point2;
        public Vector2 point3;
        public Vector2 direction;

        public Fish(int centerX, int centerY, int size, FishType type, Vector2 startDir)
        {
            id = new Guid();
            this.type = type;
            center = new Vector2(centerX, centerY);
            direction = startDir;
            this.size = size;
        }


        public void UpdateDirection()
        {
            direction = Vector2.Normalize(direction);
            point1 = center + ((size * 2) * direction);

            point2 = new Vector2(center.X, center.Y);
            point3 = new Vector2(center.X, center.Y);
        }

        public void Update()
        {
            UpdateDirection();
            center += direction * speed;
        }

        public void Draw()
        {
            //Raylib.DrawCircleLines((int)center.X, (int)center.Y, viewRadius, Color.RED);
            if(type == FishType.Fish)
            {
                Raylib.DrawCircleLines((int)center.X, (int)center.Y, size, Color.BLUE);
            } else
            {
                Raylib.DrawCircleLines((int)center.X, (int)center.Y, size, Color.RED);
            }
            Raylib.DrawTriangleLines(point1, point2, point3, Color.WHITE);
        }

        public double GetDistance(Fish f)
        {
            return Math.Sqrt(Math.Pow(center.X - f.center.X, 2) + Math.Pow(center.Y - f.center.Y, 2));
        }
    }
}
