// Include code libraries you need below (use the namespace).
using Raylib_cs;
using System;
using System.Numerics;

// The namespace your code is in.
namespace Game10003;

/// <summary>
///     Your game code goes inside this class!
/// </summary>
public class Game
{
    // Place your variables here:
    Player player = new Player();
    Enemy[] enemies = new Enemy[4];
    Cloud[] clouds = new Cloud[10];

    int activeEnemyCount = 1;

    Vector2 bgPosition = new Vector2(0, 0);
    Vector2 bgMovement = new Vector2(0, 1);

    /// <summary>
    ///     Setup runs once before the game loop begins.
    /// </summary>
    public void Setup()
    {
        Window.SetSize(800, 600);
        Window.SetTitle("Air Defense");

        player.position.X = Window.Width / 2;
        player.position.Y = Window.Height - 100;
        player.size = Vector2.One * 80;
        player.speed = 450;
        player.health = 3;
        player.score = 0;

        //Place clouds in random spots
        for (int i = 0; i < clouds.Length; i++)
        {
            Cloud cloud = new Cloud();
            cloud.size = new Vector2(60, 30);
            cloud.position.X = Random.Float(0, Window.Width - cloud.size.X);
            cloud.position.Y = Random.Float(0, Window.Height * 2);

            clouds[i] = cloud;
        }

        //Place enemies in random spots
        for (int i = 0; i < enemies.Length; i++)
        {
            Enemy enemy = new Enemy();
            enemy.size = new Vector2(80, 80);
            enemy.position.X = Random.Float(0, Window.Width - enemy.size.X);
            enemy.position.Y = -enemy.size.Y;

            enemies[i] = enemy;
        }
    }

    /// <summary>
    ///     Update runs every frame.
    /// </summary>
    public void Update()
    {
        Window.ClearBackground(Color.White);

        player.Move();

        //Cloud movement
        for (int i = 0; i < clouds.Length; i++)
        {
            clouds[i].MoveCloud();
            bool didCloudWrap = clouds[i].KeepCloudOnScreen();
        }

        //Enemy Collision
        for (int i = 0; i < activeEnemyCount; i++)
        {
            enemies[i].EnemyMove();

            bool doesEnemyHitPlayer = player.DoesCollideWithEnemy(enemies[i]);
            if (doesEnemyHitPlayer == true)
            {
                player.health -= 1;
                enemies[i].isVisible = false;
            }

            bool didEnemyWrap = enemies[i].KeepEnemyOnScreen();
            if (didEnemyWrap && activeEnemyCount < enemies.Length)
            {
                activeEnemyCount ++;
            }
        }

        //If the player dies, draw the game over screen
        if (player.health <= 0)
        {
            DrawGameOver(); 
        }
        else
        {
            DrawGame();
        }
        
        //Draw the game. Enemies, health, score, etc. 
        void DrawGame()
        {
            DrawBackground();

            for (int i = 0; i < clouds.Length; i++)
            {
                clouds[i].DrawCloud();
            }

            for (int i = 0; i < enemies.Length;i++)
            {
                enemies[i].DrawEnemy();
            }

            player.DrawPlayer();

            DrawUI();
        }

        //Draws the UI
        void DrawUI()
        {
            //UI Elements
            Texture2D textureInterface = Graphics.LoadTexture("../../../assets/UI.png");
            Graphics.Draw(textureInterface, 0, 0);

            //Text
            Text.Color = Color.Cyan;
            Text.Size = 50;
            Font uiText = Text.LoadFont("../../../assets/PixelSplitter-Bold.ttf");
            Text.Draw($"{player.health}", Window.Size - new Vector2(715, 57), uiText);
        }

        //Draws the background
        void DrawBackground()
        {
            bgPosition += bgMovement;

            Draw.FillColor = Color.Blue;
            Draw.Rectangle(bgPosition.X, bgPosition.Y, bgPosition.X + 800, bgPosition.Y + 600);

            //Background Texture
            Texture2D textureBackground = Graphics.LoadTexture("../../../assets/OceanBG.png");
            Graphics.Draw(textureBackground, bgPosition.X, bgPosition.Y - 600);

            //If the background image reaches the top, reset back to starting position and keep moving (to produce pseudo-scrolling effect)
            if(bgPosition.Y == 600)
            {
                bgPosition.Y = 0;
            }
        }

        //Draw Game Over Screen
        void DrawGameOver()
        {
            Window.ClearBackground(Color.Black);
            Text.Size = 80;
            Text.Color = Color.Red;
            Font uiText = Text.LoadFont("../../../assets/PixelSplitter-Bold.ttf");
            Text.Draw("MISSION FAILED.", Window.Size - new Vector2(735, 350), uiText);
        }
    }
}
