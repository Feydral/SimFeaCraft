using OpenTK.Windowing.Desktop;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Graphics.OpenGL4;

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

    protected override void OnResize(ResizeEventArgs args)
    {
        GL.Viewport(0, 0, args.Width, args.Height);
        windowWidth = args.Width;
        windowHeight = args.Height;
        base.OnResize(args);
    }

    protected override void OnLoad()
    {
        base.OnLoad();
    }

    protected override void OnUnload()
    {
        base.OnUnload();
    }

    protected override void OnUpdateFrame(FrameEventArgs args)
    {
        base.OnUpdateFrame(args);
    }

    protected override void OnRenderFrame(FrameEventArgs args)
    {
        GL.ClearColor(1.0f, 0.0f, 0.0f, 1.0f);
        GL.Clear(ClearBufferMask.ColorBufferBit);

        Context.SwapBuffers();

        base.OnRenderFrame(args);
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