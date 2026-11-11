using OpenTK.Windowing.Desktop;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Graphics.OpenGL4;
using StbImageSharp;

namespace SimFeaCraft;

public class Game : GameWindow
{
    int windowWidth;
    int windowHeight;

    List<Vector3> vertices =
    [
        new Vector3(-0.5f, 0.5f, 0.5f),
        new Vector3(0.5f, 0.5f, 0.5f),
        new Vector3(0.5f, -0.5f, 0.5f),
        new Vector3(-0.5f, -0.5f, 0.5f),

        new Vector3(0.5f, 0.5f, 0.5f),
        new Vector3(0.5f, 0.5f, -0.5f),
        new Vector3(0.5f, -0.5f, -0.5f),
        new Vector3(0.5f, -0.5f, 0.5f),

        new Vector3(0.5f, 0.5f, -0.5f),
        new Vector3(-0.5f, 0.5f, -0.5f),
        new Vector3(-0.5f, -0.5f, -0.5f),
        new Vector3(0.5f, -0.5f, -0.5f),

        new Vector3(-0.5f, 0.5f, -0.5f),
        new Vector3(-0.5f, 0.5f, 0.5f),
        new Vector3(-0.5f, -0.5f, 0.5f),
        new Vector3(-0.5f, -0.5f, -0.5f),

        new Vector3(-0.5f, 0.5f, -0.5f),
        new Vector3(0.5f, 0.5f, -0.5f),
        new Vector3(0.5f, 0.5f, 0.5f),
        new Vector3(-0.5f, 0.5f, 0.5f),

        new Vector3(-0.5f, -0.5f, 0.5f),
        new Vector3(0.5f, -0.5f, 0.5f),
        new Vector3(0.5f, -0.5f, -0.5f),
        new Vector3(-0.5f, -0.5f, -0.5f),
    ];

    List<Vector2> uvs =
    [
        new Vector2(0f, 1f),
        new Vector2(1f, 1f),
        new Vector2(1f, 0f),
        new Vector2(0f, 0f),

        new Vector2(0f, 1f),
        new Vector2(1f, 1f),
        new Vector2(1f, 0f),
        new Vector2(0f, 0f),

        new Vector2(0f, 1f),
        new Vector2(1f, 1f),
        new Vector2(1f, 0f),
        new Vector2(0f, 0f),

        new Vector2(0f, 1f),
        new Vector2(1f, 1f),
        new Vector2(1f, 0f),
        new Vector2(0f, 0f),

        new Vector2(0f, 1f),
        new Vector2(1f, 1f),
        new Vector2(1f, 0f),
        new Vector2(0f, 0f),

        new Vector2(0f, 1f),
        new Vector2(1f, 1f),
        new Vector2(1f, 0f),
        new Vector2(0f, 0f),
    ];

    uint[] indices =
    {
        0, 1, 2,
        2, 3, 0,

        4, 5, 6,
        6, 7, 4,

        8, 9, 10,
        10, 11, 8,

        12, 13, 14,
        14, 15, 12,

        16, 17, 18,
        18, 19, 16,

        20, 21, 22,
        22, 23, 20
    };

    float yRotation = 0.0f;
    float zRotation = 0.0f;

    int vertexArrayObject;
    int vertexBufferObject;
    int elementBufferObject;

    int texture;
    int textureBufferObject;

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
        Logger.Info("main", "OnLoad called.");

        // vao and vbo
        vertexArrayObject = GL.GenVertexArray();
        vertexBufferObject = GL.GenBuffer();

        GL.BindBuffer(BufferTarget.ArrayBuffer, vertexBufferObject);
        GL.BufferData(BufferTarget.ArrayBuffer, vertices.Count * Vector3.SizeInBytes, vertices.ToArray(), BufferUsageHint.StaticDraw);

        GL.BindVertexArray(vertexArrayObject);
        GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 0, 0);
        GL.EnableVertexArrayAttrib(vertexArrayObject, 0);

        Logger.Info("main", "Created the VAO and the VBO!");

        // texture
        texture = GL.GenTexture();
        GL.ActiveTexture(TextureUnit.Texture0);
        GL.BindTexture(TextureTarget.Texture2D, texture);

        GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapS, (int)TextureWrapMode.Repeat);
        GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapT, (int)TextureWrapMode.Repeat);
        GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.Nearest);
        GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Nearest);

        StbImage.stbi_set_flip_vertically_on_load(1);
        ImageResult dirtTexture = ImageResult.FromStream(File.OpenRead("Assets/Textures/GrassBlock.png"), ColorComponents.RedGreenBlueAlpha);

        GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba, dirtTexture.Width, dirtTexture.Height, 0, PixelFormat.Rgba, PixelType.UnsignedByte, dirtTexture.Data);
        GL.BindTexture(TextureTarget.Texture2D, 0);

        textureBufferObject = GL.GenBuffer();
        GL.BindBuffer(BufferTarget.ArrayBuffer, textureBufferObject);
        GL.BufferData(BufferTarget.ArrayBuffer, uvs.Count * Vector2.SizeInBytes, uvs.ToArray(), BufferUsageHint.StaticDraw);

        GL.VertexAttribPointer(1, 2, VertexAttribPointerType.Float, false, 0, 0);
        GL.EnableVertexArrayAttrib(vertexArrayObject, 1);

        Logger.Info("main", "Created the texture!");

        // ebo
        elementBufferObject = GL.GenBuffer();
        GL.BindBuffer(BufferTarget.ElementArrayBuffer, elementBufferObject);
        GL.BufferData(BufferTarget.ElementArrayBuffer, indices.Length * sizeof(uint), indices, BufferUsageHint.StaticDraw);

        Logger.Info("main", "Created the EBO!");

        int vertexShader = GL.CreateShader(ShaderType.VertexShader);
        GL.ShaderSource(vertexShader, LoadShaderFromFile("Assets/Shaders/DefaultVertexShader.glsl"));
        GL.CompileShader(vertexShader);

        int fragmentShader = GL.CreateShader(ShaderType.FragmentShader);
        GL.ShaderSource(fragmentShader, LoadShaderFromFile("Assets/Shaders/DefaultFragmentShader.glsl"));
        GL.CompileShader(fragmentShader);

        // shader and shader program
        shaderProgram = GL.CreateProgram();
        GL.AttachShader(shaderProgram, vertexShader);
        GL.AttachShader(shaderProgram, fragmentShader);

        GL.LinkProgram(shaderProgram);
        string programLog = GL.GetProgramInfoLog(shaderProgram);

        GL.DeleteShader(vertexShader);
        GL.DeleteShader(fragmentShader);

        Logger.Info("main", "Created the shader program!");
        if (!string.IsNullOrWhiteSpace(programLog))
        {
            Logger.Error("main", string.Concat("An error occured while creating the shaders:", '\n', programLog));
        }

        GL.Enable(EnableCap.DepthTest);

        base.OnLoad();
    }

    protected override void OnUnload()
    {
        Logger.Info("main", "OnUnload called.");

        GL.DeleteBuffer(vertexBufferObject);
        GL.DeleteVertexArray(vertexArrayObject);
        GL.DeleteProgram(shaderProgram);
        GL.DeleteBuffer(elementBufferObject);
        GL.DeleteTexture(texture);

        base.OnUnload();
    }

    protected override void OnUpdateFrame(FrameEventArgs args)
    {
        yRotation += (float)args.Time;
        zRotation += (float)args.Time / 2.0f;

        base.OnUpdateFrame(args);
    }

    protected override void OnRenderFrame(FrameEventArgs args)
    {
        GL.ClearColor(0.0f, 0.0f, 0.0f, 1.0f);
        GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

        Matrix4 model = Matrix4.CreateRotationY(yRotation) * Matrix4.CreateRotationZ(zRotation) * Matrix4.CreateTranslation(new Vector3(0.0f, 0.0f, -5.0f));
        Matrix4 view = Matrix4.Identity;
        Matrix4 projection = Matrix4.CreatePerspectiveFieldOfView(MathHelper.DegreesToRadians(60.0f), windowWidth / windowHeight, 0.1f, 100.0f);

        int modelLocation = GL.GetUniformLocation(shaderProgram, "Model");
        int viewLocation = GL.GetUniformLocation(shaderProgram, "View");
        int projectionLocation = GL.GetUniformLocation(shaderProgram, "Projection");

        GL.UniformMatrix4(modelLocation, true, ref model);
        GL.UniformMatrix4(viewLocation, true, ref view);
        GL.UniformMatrix4(projectionLocation, true, ref projection);

        GL.UseProgram(shaderProgram);
        GL.BindVertexArray(vertexArrayObject);
        GL.BindBuffer(BufferTarget.ElementArrayBuffer, elementBufferObject);
        GL.BindTexture(TextureTarget.Texture2D, texture);
        GL.DrawElements(PrimitiveType.Triangles, indices.Length, DrawElementsType.UnsignedInt, 0);

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
        Game game = new Game(720, 720);
        game.Run();
    }
}