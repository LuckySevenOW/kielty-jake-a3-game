using Raylib_cs;
using System;
using System.Numerics;

namespace Game10003;

public class Player
{
    public Vector2 position;
    public Vector2 size;
    public Vector2 bulletPosition;
    public Vector2 bulletSize = new Vector2(4, 6);
    public float speed;
    public int health;
    public int score;

    public bool isShooting = false;

    //Used for setting up class in Game.cs
    public Player()
    {

    }

    //Draw the player on the screen (drawing the texture)
    public void DrawPlayer()
    {
        Draw.LineSize = 0;
        Draw.FillColor = Color.Clear;
        Draw.Rectangle(position, size);

        //Player Texture
        Texture2D texturePlayer = Graphics.LoadTexture("../../../assets/Playerv2.png");
        Graphics.Draw(texturePlayer, position - (Vector2.One * 10));
    }

    //Collision - If your plane collides with the enemy, return true. This will remove 1 HP per hit.
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

    //Player Movement - If the arrow keys are pressed, move the plane in the corresponding direction across the screen.
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

    //Shoot - Fire a projectile when you press the spacebar
    public void Shoot()
    {
        if (Input.IsKeyboardKeyPressed(KeyboardInput.Space))
        {
            bulletPosition = position;
            isShooting = true;
        }
    }

    //Spawn Bullet - This draws and moves a bullet when isShooting == true. This basically allows it to continue traveling beyond the frame or two that the button is pressed. 
    public void SpawnBullet()
    {
        if (isShooting == true)
        {
            Vector2 bulletVelocity = new Vector2(0, -3);
            bulletPosition += bulletVelocity;

            Draw.LineSize = 0;
            Draw.FillColor = Color.Red;
            Draw.Rectangle(bulletPosition.X + 38, bulletPosition.Y + 37, bulletSize.X, bulletSize.Y);
        }
    }

    //Bullet Collision - This checks if the bullet has hit an enemy, and then returns a true or false depending on whether it did or not. 
    public bool DoesBulletHitEnemy(Enemy enemy)
    {
        //If the enemy isn't even visible, they should not count for points. 
        if (enemy.isVisible == false)
            return false;

        float bulletLeft = bulletPosition.X;
        float bulletRight = bulletPosition.X + bulletSize.X;
        float bulletTop = bulletPosition.Y;
        float bulletBottom = bulletPosition.Y + bulletSize.Y;

        float enemyLeft = enemy.position.X;
        float enemyRight = enemy.position.X + size.X;
        float enemyTop = enemy.position.Y;
        float enemyBottom = enemy.position.Y + size.Y;

        bool isHittingEnemyLeftEdge = bulletRight > enemyLeft;
        bool isHittingEnemyRightEdge = bulletLeft < enemyRight;
        bool isHittingEnemyTopEdge = bulletBottom > enemyTop;
        bool isHittingEnemyBottomEdge = bulletTop < enemyBottom;
        bool isColliding = isHittingEnemyLeftEdge && isHittingEnemyRightEdge && isHittingEnemyTopEdge && isHittingEnemyBottomEdge;

        return isColliding;
    }

}

