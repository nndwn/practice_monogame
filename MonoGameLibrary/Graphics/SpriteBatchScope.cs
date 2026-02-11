using System;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

public class SpriteBatchScope : IDisposable
{
    private readonly SpriteBatch _spriteBatch;
    public SpriteBatchScope(
        SpriteBatch spriteBatch,
        SpriteSortMode sortMode = SpriteSortMode.Deferred,
        BlendState blendState = null,
        SamplerState samplerState = null,
        DepthStencilState depthStencilState = null,
        RasterizerState rasterizerState = null,
        Effect effect = null,
        Matrix? transformMatrix = null)
    {
        _spriteBatch = spriteBatch;
        _spriteBatch.Begin(
            sortMode,
            blendState,
            samplerState,
            depthStencilState,
            rasterizerState,
            effect,
            transformMatrix
        );
    }

    #pragma warning disable CA1816 // Dispose methods should call SuppressFinalize
    public void Dispose()
    {
        _spriteBatch.End();
    }

}

