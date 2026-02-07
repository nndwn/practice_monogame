using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MonoGameLibrary.Graphics;

public class Grid
{
    private Texture2D _pixel;

    public int CellSize { get; set; } = 32;
    public Color Color { get; set; } = new Color(255, 255, 255, 64);
    public int LineThickness { get; set; } = 1;
    public bool Visible { get; set; } = false;

    public int OffsetX { get; set; } = 0;
    public int OffsetY { get; set; } = 0;

    public Grid() {}

    public Grid(int cellSize, Color color, int thickness = 1)
    {
        CellSize = Math.Max(1, cellSize);
        Color = color;
        LineThickness = Math.Max(1, thickness);
        // Do not create GPU resources in the constructor — the GraphicsDevice
        // may not be initialized yet. Pixel texture will be created lazily in Draw().
    }

    private void EnsurePixel()
    {
        if (_pixel == null)
        {
            _pixel = new Texture2D(Core.GraphicsDevice , 1, 1);
            _pixel.SetData([Color.White]);
        }
    }

    /// <summary>
    /// Explicitly create GPU resources (safe to call after GraphicsDevice is initialized).
    /// </summary>
    public void Initialize()
    {
        EnsurePixel();
    }

    /// <summary>
    /// Draws the grid to the screen using the provided spriteBatch. Call while between SpriteBatch.Begin/End.
    /// </summary>
    public void Draw(SpriteBatch spriteBatch)
    {
        if (!Visible) return;

        EnsurePixel();

        int width = Core.GraphicsDevice.PresentationParameters.BackBufferWidth;
        int height = Core.GraphicsDevice.PresentationParameters.BackBufferHeight;

        // Align start positions with offset and cell size
        int startX = -((OffsetX % CellSize) + CellSize) + (OffsetX % CellSize);
        if (startX > 0) startX -= CellSize;
        int startY = -((OffsetY % CellSize) + CellSize) + (OffsetY % CellSize);
        if (startY > 0) startY -= CellSize;

        // Vertical lines
        for (int x = startX; x <= width; x += CellSize)
        {
            spriteBatch.Draw(_pixel, new Rectangle(x, 0, LineThickness, height), Color);
        }

        // Horizontal lines
        for (int y = startY; y <= height; y += CellSize)
        {
            spriteBatch.Draw(_pixel, new Rectangle(0, y, width, LineThickness), Color);
        }
    }

}
