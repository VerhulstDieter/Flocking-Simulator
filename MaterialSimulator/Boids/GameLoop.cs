using Raylib_cs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;


namespace MaterialSimulator.Game
{
    public class GameLoop
    {
        public Pond pond;

        public GameLoop()
        {
            pond = new Pond(1600, 900);  
        }
        public void Run()
        {
            pond.Draw();
            pond.Update();
        }
    }
}
