using MaterialSimulator.Game;
using Raylib_cs;

namespace HelloWorld
{
    static class Program
    {
        public static GameLoop game = new GameLoop();
        public static void Main()
        {
            Raylib.InitWindow(1600, 900, "Material Simulation");
            Raylib.SetTargetFPS(60);
            while (!Raylib.WindowShouldClose())
            {
                Raylib.BeginDrawing();
                Raylib.ClearBackground(Color.BLACK);
                game.Run();
                Raylib.DrawFPS(12, 12);

                Raylib.EndDrawing();
            }

            Raylib.CloseWindow();
        }
    }
}