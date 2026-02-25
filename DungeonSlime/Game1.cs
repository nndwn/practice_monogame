using MonoGameLibrary;
using Microsoft.Xna.Framework.Media;
using DungeonSlime.Scenes;
using Gum.Forms;
using Gum.Forms.Controls;
using MonoGameGum;


namespace DungeonSlime;

public class Game1 : Core
{
    private Song _themeSong;
    
    public Game1() : base("Dungeon Slime", 1280, 720, false)
    {

    }

    protected override void Initialize()
    {
        base.Initialize();
       Audio.PlaySong(_themeSong);
       InitializedGum();
       ChangeScene(new TitleScene());
    }

    protected override void LoadContent()
    {
      _themeSong = Content.Load<Song>("audio/theme");
    }

    private void InitializedGum()
  {
     GumService.Default.Initialize(this, DefaultVisualsVersion.V3);

     GumService.Default.ContentLoader.XnaContentManager = Core.Content;
     FrameworkElement.KeyboardsForUiControl.Add(GumService.Default.Keyboard);
     FrameworkElement.GamePadsForUiControl.AddRange(GumService.Default.Gamepads);
     FrameworkElement.TabReverseKeyCombos.Add(
      new KeyCombo()
      {
        PushedKey = Microsoft.Xna.Framework.Input.Keys.Up
      }
     );
     GumService.Default.CanvasWidth = GraphicsDevice.PresentationParameters.BackBufferWidth / 4.0f;
     GumService.Default.CanvasHeight = GraphicsDevice.PresentationParameters.BackBufferHeight / 4.0f;
     GumService.Default.Renderer.Camera.Zoom = 4.0f;

  }
}
