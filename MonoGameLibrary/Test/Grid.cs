using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MonoGameLibrary.Test;

public class Grid
{
    private readonly Texture2D _pixel;

    private readonly SpriteBatch _spriteBatch;

    private readonly GraphicsDevice _graphicsDevice;

    private DebugVisual _grid;

    public Grid(GraphicsDevice graphicsDevice , SpriteBatch spriteBatch , DebugVisual debugVisual )
    {
        _grid = debugVisual;
        _spriteBatch = spriteBatch;
        _graphicsDevice = graphicsDevice;
        _pixel = new Texture2D(graphicsDevice , 1, 1);
        _pixel.SetData([Color.White]);
    }

    public void Draw()
    {
        Console.WriteLine(_grid.grid.Visible);
        if (!_grid.grid.Visible) return;

        int width = _graphicsDevice.PresentationParameters.BackBufferWidth;
        int height = _graphicsDevice.PresentationParameters.BackBufferHeight;
        int size = _grid.grid.Size;
        int offsetX = _grid.grid.OffsetX;
        int offsetY = _grid.grid.OffsetY;
        int lineThickness = _grid.grid.LineThickness;
        Color color = _grid.grid.Color;

        // Align start positions with offset and cell size
        int startX = -((offsetX % size) + size) + (offsetX % size);
        if (startX > 0) startX -= size;
        int startY = -((offsetY % size) + size) + (offsetY % size);
        if (startY > 0) startY -= size;

        // Vertical lines
        for (int x = startX; x <= width; x += size)
        {
            _spriteBatch.Draw(_pixel, new Rectangle(x, 0, lineThickness, height), color);
        }

        // Horizontal lines
        for (int y = startY; y <= height; y += size)
        {
            _spriteBatch.Draw(_pixel, new Rectangle(0, y, width, lineThickness), color);
        }
    }

}
