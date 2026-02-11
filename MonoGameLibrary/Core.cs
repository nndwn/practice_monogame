using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGameLibrary.Input;
using MonoGameLibrary.Audio;
using MonoGameLibrary.Test;
using MonoGameLibrary.Scenes;

namespace MonoGameLibrary;
public class Core : Game
{
    internal static Core s_instance;

    public static Core Instance => s_instance;

    private static Scene s_activeScene;

    private static Scene s_nextScene;

    public static GraphicsDeviceManager Graphics {get; private set; }

    public static new GraphicsDevice GraphicsDevice {get ; private set;}

    public static SpriteBatch SpriteBatch {get; private set; }

    public static new ContentManager Content {get; private set;}

    public static InputManager Input { get; private set; }

    public static bool ExitOnEscape { get; set; }

    public DebugVisual Visual {get; private set;}

    public Grid Grid {get; private set;}

    public static AudioContoller Audio {get; private set;}
    
    public Core(string title, int width, int height, bool fullScreen)
    {
        // Ensure that multiple cores are not created.
        if (s_instance != null)
        {
            throw new InvalidOperationException($"Only a single Core instance can be created");
        }
        
        // Store reference to engine for global member access.
        s_instance = this;

        // Create a new graphics device manager.
        Graphics = new GraphicsDeviceManager(this)
        {
            // Set the graphics defaults.
            PreferredBackBufferWidth = width,
            PreferredBackBufferHeight = height,
            IsFullScreen = fullScreen
        };

        // Apply the graphic presentation changes.
        Graphics.ApplyChanges();

        // Set the window title.
        Window.Title = title;

        // Set the core's content manager to a reference of the base Game's
        // content manager.
        Content = base.Content;

        // Set the root directory for content.
        Content.RootDirectory = "Content";
 
        // Mouse is visible by default.
        IsMouseVisible = true;
        ExitOnEscape = true;

        Visual = DebugVisual.Instance;
    }

    protected override void Initialize()
    {
        base.Initialize();

        // Set the core's graphics device to a reference of the base Game's
        // graphics device.
        GraphicsDevice = base.GraphicsDevice;

        // Create the sprite batch instance.
        SpriteBatch = new SpriteBatch(GraphicsDevice);
        Input = new InputManager();
        Grid = new Grid(GraphicsDevice, SpriteBatch, Visual);
        Audio = new AudioContoller();

    }

    protected override void UnloadContent()
    {
        Audio.Dispose();
        base.UnloadContent();
    }

    protected override void Update(GameTime gameTime)
    {
        // Update the input manager.
        Input.Update(gameTime);

        Audio.Update();

        if (ExitOnEscape && Input.Keyboard.IsKeyDown(Keys.Escape))
        {
            Exit();
        }

        if (s_nextScene != null)
        {
            TransitionScene();
        }

        if (s_activeScene != null)
        {
            s_activeScene.Update(gameTime);
        }

        base.Update(gameTime);
    }

    protected override void Draw (GameTime gameTime)
    {
        if (s_activeScene != null)
        {
            s_activeScene.Draw(gameTime);
        }
        base.Draw(gameTime);
    }

    public static void ChangeScene(Scene next)
    {
        if (s_activeScene != next)
        {
            s_nextScene = next;
        }
    }

    private static void TransitionScene()
    {
        if (s_activeScene != null)
        {
            s_activeScene.Dispose();
        }

        GC.Collect();
        s_activeScene = s_nextScene;

        s_nextScene = null;

        if (s_activeScene != null)
        {
            s_activeScene.Initialize();
        }
    }
}