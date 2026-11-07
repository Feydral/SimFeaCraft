using OpenTK.Windowing.Desktop;
using OpenTK.Mathematics;

namespace SimFeaCraft;

public class Game : GameWindow
{
    int windowWidth;
    int windowHeight;

    public Game(int windowWidth, int windowHeight) : base(GameWindowSettings.Default, NativeWindowSettings.Default)
    {
        this.windowWidth = windowWidth;
        this.windowHeight = windowHeight;
        CenterWindow(new Vector2i(windowWidth, windowHeight));
    }

    public override void Run()
    {

    }
}

class Program
{
    public static void Main()
    {
        Game game = new Game(1270, 840);
        game.Run();
    }
}