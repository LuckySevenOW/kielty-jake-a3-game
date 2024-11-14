using System;
using System.Numerics;

namespace Game10003;

public class Cloud
{
    public Vector2 position;
    public Vector2 size;
    public Vector2 velocity;
    public bool isVisible;

    //Cloud Texture
    Texture2D textureCloud = Graphics.LoadTexture("../../../assets/CloudsV3.png");

    //Draw the clouds on the screen (drawing the texture)
    public void DrawCloud()
    {
            Draw.LineSize = 0;
            Draw.FillColor = Color.Clear;
            Draw.Ellipse(position, size);
            Graphics.Draw(textureCloud, position.X - 75, position.Y - 43);    
    }

    //Move the clouds across the screen.
    public void MoveCloud()
    {
        Vector2 velocity = new Vector2(0, 2);
        position += velocity;
    }

    //Wrap the clouds back to the top of the screen when they reach the bottom.
    public bool KeepCloudOnScreen()
    {
        bool doWrap = position.Y > Window.Height + 43;
        if (doWrap)
        {
            position.X = Random.Float(0, Window.Width - size.X);
            position.Y = Random.Float(-200, -size.Y);
        }
        return doWrap;
    }
}
