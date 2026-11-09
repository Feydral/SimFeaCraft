using OpenTK.Windowing.Desktop;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Graphics.OpenGL4;

namespace SimFeaCraft;

public class Game : GameWindow
{
    int windowWidth;
    int windowHeight;

    float[] vertices =
    [
        0.0f, 0.5f, 0.0f,
        -0.5f, -0.5f, 0.0f,
        0.5f, -0.5f, 0.0f,
    ];

    int vertexArrayObject;
    int vertexBufferObject;
    int shaderProgram;

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
        Logger.Info("main", "Test Info!");
        Logger.Warning("main", "Test Warning!");
        Logger.Error("main", "Test Error!");

        vertexArrayObject = GL.GenVertexArray();
        vertexBufferObject = GL.GenBuffer();

        GL.BindBuffer(BufferTarget.ArrayBuffer, vertexBufferObject);
        GL.BufferData(BufferTarget.ArrayBuffer, vertices.Length * sizeof(float), vertices, BufferUsageHint.StaticDraw);

        GL.BindVertexArray(vertexArrayObject);
        GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 0, 0);
        GL.EnableVertexArrayAttrib(vertexArrayObject, 0);

        shaderProgram = GL.CreateProgram();

        int vertexShader = GL.CreateShader(ShaderType.VertexShader);
        GL.ShaderSource(vertexShader, LoadShaderFromFile("Assets/Shaders/DefaultVertexShader.glsl"));
        GL.CompileShader(vertexShader);

        int fragmentShader = GL.CreateShader(ShaderType.FragmentShader);
        GL.ShaderSource(fragmentShader, LoadShaderFromFile("Assets/Shaders/DefaultFragmentShader.glsl"));
        GL.CompileShader(fragmentShader);

        GL.AttachShader(shaderProgram, vertexShader);
        GL.AttachShader(shaderProgram, fragmentShader);

        GL.LinkProgram(shaderProgram);

        GL.DeleteShader(vertexShader);
        GL.DeleteShader(fragmentShader);

        base.OnLoad();
    }

    protected override void OnUnload()
    {
        GL.DeleteBuffer(vertexBufferObject);
        GL.DeleteVertexArray(vertexArrayObject);
        GL.DeleteProgram(shaderProgram);

        base.OnUnload();
    }

    protected override void OnUpdateFrame(FrameEventArgs args)
    {
        base.OnUpdateFrame(args);
    }

    protected override void OnRenderFrame(FrameEventArgs args)
    {
        GL.ClearColor(0.0f, 0.0f, 0.0f, 1.0f);
        GL.Clear(ClearBufferMask.ColorBufferBit);

        GL.UseProgram(shaderProgram);
        GL.BindVertexArray(vertexArrayObject);
        GL.DrawArrays(PrimitiveType.Triangles, 0, 3);

        Context.SwapBuffers();

        base.OnRenderFrame(args);
    }

    static string LoadShaderFromFile(string path)
    {
        return new StreamReader(path).ReadToEnd();
    }
}

class Program
{
    public static void Main()
    {
        Game game = new Game(1280, 720);
        game.Run();
    }
}