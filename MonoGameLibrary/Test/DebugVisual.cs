
using System;
using Microsoft.Xna.Framework;


namespace MonoGameLibrary.Test;

public struct GridSettings
{
    public bool Visible;
    public int Size;
    public Color Color ;
    public int LineThickness ;

    public int OffsetX;
    public int OffsetY;

}

public class DebugVisual
{
    private static readonly Lazy<DebugVisual> _instance = new(()=> new DebugVisual());
    private DebugVisual(){}
    public static DebugVisual Instance => _instance.Value;

    public GridSettings grid = new()
    {
        Visible = false,
        Size = 20,
        Color = Color.Red * 0.5f,
        LineThickness = 1,
        OffsetX = 0,
        OffsetY = 0
    };
}