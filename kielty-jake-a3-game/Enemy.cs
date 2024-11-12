
using System;
using System.Numerics;

namespace Game10003;

public class Enemy
{
    public Vector2 position;
    public Vector2 size;
    public Vector2 velocity;
    public bool isVisible;

    Texture2D textureEnemy = Graphics.LoadTexture("../../../assets/EnemyV1.png");

    //Draw enemy on screen
    public void DrawEnemy()
    {
        if (isVisible == true)
        {
            //Draw Enemy Plane
            Draw.LineSize = 0;
            Draw.FillColor = Color.Clear;
            Draw.Rectangle(position, size);
            Graphics.Draw(textureEnemy, position - (Vector2.One * 10));
        }
    }

    //Move enemy across screen
    public void EnemyMove()
    {
        Vector2 movement = new Vector2(0, 6);
        position += movement;
    }

    //Wrap enemy back to top of screen after they reach the bottom
    public bool KeepEnemyOnScreen()
    {
        bool doWrap = position.Y > Window.Height;
        if (doWrap)
        {
            position.X = Random.Float(0, Window.Width - size.X);
            position.Y = Random.Float(-200, -size.Y);

            velocity = Vector2.Zero;
            isVisible = true;
        }
        return doWrap;
    }
}

