using System;
using System.Numerics;

namespace Game10003;

public class Cloud
{
    public Vector2 position;
    public Vector2 size;
    public Vector2 velocity;
    public bool isVisible;

    //Draw Clouds on screen
    public void DrawCloud()
    {
            Draw.LineSize = 1;
            Draw.LineColor = Color.Black;
            Draw.FillColor = Color.White;
            Draw.Ellipse(position, size);
    }

    //Move clouds across screen
    public void MoveCloud()
    {
        Vector2 velocity = new Vector2(0, 2);
        position += velocity;
    }

    //Wrap Clouds back to top of screen
    public bool KeepCloudOnScreen()
    {
        bool doWrap = position.Y > Window.Height;
        if (doWrap)
        {
            position.X = Random.Float(0, Window.Width - size.X);
            position.Y = Random.Float(-200, -size.Y);
        }
        return doWrap;
    }
}
