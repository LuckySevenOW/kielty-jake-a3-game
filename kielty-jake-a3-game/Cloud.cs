using System;
using System.Numerics;

namespace Game10003;

public class Cloud
{
    public Vector2 position;
    public Vector2 size;
    public Vector2 velocity;
    public bool isVisible;

    Texture2D textureCloud = Graphics.LoadTexture("../../../assets/Clouds.png");

    //Draw Clouds on screen
    public void DrawCloud()
    {
            Draw.LineSize = 0;
            Draw.FillColor = Color.Clear;
            Draw.Ellipse(position, size);
            Graphics.Draw(textureCloud, position.X - 35, position.Y - 20);    
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
