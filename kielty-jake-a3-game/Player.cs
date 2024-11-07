using System;
using System.Numerics;

namespace Game10003;

public class Player
{
    public Vector2 position;
    public Vector2 size;
    public float speed;
    public int health;
    public int score;

    public Player()
    {

    }


    //Draw player
    public void DrawPlayer()
    {
        Draw.LineSize = 0;
        Draw.FillColor = Color.Gray;
        Draw.Rectangle(position, size);
    }

    //Collision with enemy
    public bool DoesCollideWithEnemy(Enemy enemy)
    {
        if (enemy.isVisible == false)
            return false;

        float playerLeft = position.X;
        float playerRight = position.X + size.X;
        float playerTop = position.Y;
        float playerBottom = position.Y + size.Y;

        float enemyLeft = enemy.position.X;
        float enemyRight = enemy.position.X + size.X;
        float enemyTop = enemy.position.Y;
        float enemyBottom = enemy.position.Y + size.Y;

        bool isWithinEnemyLeftEdge = playerRight > enemyLeft;
        bool isWithinEnemyRightEdge = playerLeft < enemyRight;
        bool isWithinEnemyTopEdge = playerBottom > enemyTop;
        bool isWithinEnemyBottomEdge = playerTop < enemyBottom;
        bool isColliding = isWithinEnemyLeftEdge && isWithinEnemyRightEdge && isWithinEnemyTopEdge && isWithinEnemyBottomEdge;

        return isColliding;
    }

    //Player movement
    public void Move()
    {
        //Move Left
        if (Input.IsKeyboardKeyDown(KeyboardInput.Left))
        {
            position.X -= speed * Time.DeltaTime;
        }

        //Move Right
        if (Input.IsKeyboardKeyDown(KeyboardInput.Right))
        {
            position.X += speed * Time.DeltaTime;
        }

        //Move Up
        if (Input.IsKeyboardKeyDown(KeyboardInput.Up))
        {
            position.Y -= speed * Time.DeltaTime;
        }

        //Move Down
        if (Input.IsKeyboardKeyDown(KeyboardInput.Down))
        {
            position.Y += speed * Time.DeltaTime;
        }

        //Constrain left side
        if (position.X < 0)
        {
            position.X = 0;
        }

        //Constrain right side
        if (position.X + size.X > Window.Width)
        {
            position.X = Window.Width - size.X;
        }

        //Constrain Top 
        if (position.Y < 0)
        {
            position.Y = 0;
        }

        //Constrain Bottom
        if (position.Y + size.Y > Window.Height)
        {
            position.Y = Window.Height - size.Y;
        }

    }
}

