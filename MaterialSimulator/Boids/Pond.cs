using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace MaterialSimulator.Game
{
    public class Pond
    {
        public readonly int Width;
        public readonly int Height;
        public readonly List<Fish> Fishes = new List<Fish>();
        private readonly Random Rand = new Random();

        public Pond(int width, int height, int boidCount = 200)
        {
            (Width, Height) = (width, height);
            for(int i = 0; i < PredatorCount; i++)
            {
                float xdir = (float)(Rand.NextDouble() * 2 - 1);
                float ydir = 0 - xdir;
                Fishes.Add(new Fish(Rand.Next(0, Width), Rand.Next(0, Height), 5, FishType.Predator, new Vector2(xdir, ydir)));
            }
            for (int i = 0; i < boidCount - PredatorCount; i++)
            {
                float xdir = (float)(Rand.NextDouble() * 2 - 1);
                float ydir = 0 - xdir;
                Fishes.Add(new Fish(Rand.Next(0, Width), Rand.Next(0, Height),5,FishType.Fish,new Vector2(xdir,ydir)));
            }
        }

        public void Update(bool bounceOffWalls = true)
        {
            foreach(Fish fish in Fishes)
            {
                (double flockXvel, double flockYvel) = Flock(fish, 50, .003);
                (double alignXvel, double alignYvel) = Align(fish, 50, .02);
                (double avoidXvel, double avoidYvel) = Avoid(fish, 20, .001);
                (double predXvel, double predYval) = Predator(fish, 150, .00005);
                fish.direction.X += (float)(flockXvel + avoidXvel + alignXvel + predXvel);
                fish.direction.Y += (float)(flockYvel + avoidYvel + alignYvel + predYval);
            }
            foreach(Fish fish in Fishes)
            {
                fish.Update();
                if (bounceOffWalls)
                    BounceOffWalls(fish);
            }
        }

        public void Draw()
        {
            foreach (Fish fish in Fishes)
            {
                fish.Draw();
            }
        }

        private (double avoidXvel, double avoidYvel) Avoid(Fish fish, double distance, double power)
        {
            var neighbors = Fishes.Where(x => x.GetDistance(fish) < distance);
            (double sumClosenessX, double sumClosenessY) = (0, 0);
            foreach (var neighbor in neighbors)
            {
                double closeness = distance - fish.GetDistance(neighbor);
                sumClosenessX += (fish.center.X - neighbor.center.X) * closeness;
                sumClosenessY += (fish.center.Y - neighbor.center.Y) * closeness;
            }
            return (sumClosenessX * power, sumClosenessY * power);
        }

        private (double alignXvel, double alignYvel) Align(Fish fish, double distance, double power)
        {
            var neighbors = Fishes.Where(x => x.GetDistance(fish) < distance);
            double meanXvel = neighbors.Sum(x => x.direction.X) / neighbors.Count();
            double meanYvel = neighbors.Sum(x => x.direction.Y) / neighbors.Count();
            double dXvel = meanXvel - fish.direction.X;
            double dYvel = meanYvel - fish.direction.Y;

            return (dXvel * power, dYvel * power);
        }

        private (double flockXvel, double flockYvel) Flock(Fish fish, double distance, double power)
        {
            var neighbors = Fishes.Where(x => x.GetDistance(fish) < distance);
            double meanX = neighbors.Sum(x => x.center.X) / neighbors.Count();
            double meanY = neighbors.Sum(x => x.center.Y) / neighbors.Count();
            double deltaCenterX = meanX - fish.center.X;
            double deltaCenterY = meanY - fish.center.Y;
            return (deltaCenterX * power, deltaCenterY * power);
        }

        private void BounceOffWalls(Fish fish)
        {
            float pad = 20;
            float turn = .5f;
            if (fish.center.X < pad)
                fish.direction.X += turn;
            if (fish.center.X > Width - pad)
                fish.direction.X -= turn;
            if (fish.center.Y < pad)
                fish.direction.Y += turn;
            if (fish.center.Y > Height - pad)
                fish.direction.Y -= turn;
        }

        public int PredatorCount = 3;

        private (double xVel, double yVel) Predator(Fish fish, double distance, double power)
        {
            (double sumClosenessX, double sumClosenessY) = (0, 0);
            for (int i = 0; i < PredatorCount; i++)
            {
                Fish predator = Fishes[i];
                double distanceAway = fish.GetDistance(predator);
                if (distanceAway < distance)
                {
                    double closeness = distance - distanceAway;
                    sumClosenessX += (fish.center.X - predator.center.X) * closeness;
                    sumClosenessY += (fish.center.Y - predator.center.Y) * closeness;
                }
            }
            return (sumClosenessX * power, sumClosenessY * power);
        }
    }
}
