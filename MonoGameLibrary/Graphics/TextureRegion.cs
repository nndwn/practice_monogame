using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MonoGameLibrary.Graphics;

/// <summary>
/// Represents a rectangular region within a texture.
/// </summary>

public class TextureRegion
{

   
    public Texture2D Texture { get; set; }
    public Rectangle SourceRectangle { get; set; }

    public float TopTextureCoordinate => SourceRectangle.Top / (float)Texture.Height;

    public float BottomTextureCoordinate => SourceRectangle.Bottom / (float)Texture.Height;

    public float LeftTextureCoordinate => SourceRectangle.Left / (float)Texture.Width;

    public float RightTextureCoordinate => SourceRectangle.Right / (float)Texture.Width;
    
    public int Width => SourceRectangle.Width;
    public int Height => SourceRectangle.Height;

    public bool ShowOriginDebug {get; set;} = false;
    public Color OriginDebugColor {get; set;} = Color.White;

    private Texture2D _pixel;

    public TextureRegion(){}

    public TextureRegion(Texture2D texture, int x, int y, int width, int height)
    {
        Texture = texture;
        SourceRectangle = new Rectangle(x, y, width, height);
    }

    public void Draw(SpriteBatch spriteBatch, Vector2 position, Color color)
    {
        Draw(spriteBatch, position, color, 0.0f, Vector2.Zero, Vector2.One, SpriteEffects.None, 0.0f);
    }


    public void Draw(SpriteBatch spriteBatch, Vector2 position, Color color, float rotation, Vector2 origin, float scale, SpriteEffects effects, float layerDepth)
    {
        Draw(
            spriteBatch,
            position,
            color,
            rotation,
            origin,
            new Vector2(scale, scale),
            effects,
            layerDepth
        );
    }
    public void Draw(SpriteBatch spriteBatch, Vector2 position, Color color, float rotation, Vector2 origin, Vector2 scale, SpriteEffects effects, float layerDepth)
    {
        spriteBatch.Draw(
            Texture,
            position,
            SourceRectangle,
            color,
            rotation,
            origin,
            scale,
            effects,
            layerDepth
        );

        if (ShowOriginDebug)
        {
            DrawOriginDebug(spriteBatch, position, origin, scale);
        }
    }
    //  spriteBatch.Draw(_pixel, new Rectangle(0, y, width, LineThickness), Color);
    private void DrawOriginDebug(SpriteBatch spriteBatch, Vector2 position, Vector2 origin, Vector2 scale)
    {
        Vector2 scaleOrigin = origin * scale;
        Vector2 actualOriginPosition = position - scaleOrigin;
        _pixel = new Texture2D(Core.GraphicsDevice , 1, 1);
        _pixel.SetData([Color.White]);

        int lineLength = 15;
        spriteBatch.Draw(
            _pixel,
            new Vector2(actualOriginPosition.X - lineLength, actualOriginPosition.Y),
            null,
            OriginDebugColor,
            0.0f,
            Vector2.Zero,
            new Vector2(lineLength * 2, 2),
            SpriteEffects.None,
            0.0f
        );

        spriteBatch.Draw(
            _pixel,
            new Vector2(actualOriginPosition.X, actualOriginPosition.Y- lineLength),
            null,
            OriginDebugColor,
            0.0f,
            Vector2.Zero,
            new Vector2(2, lineLength * 2),
            SpriteEffects.None,
            0.0f
        );
    }

}
