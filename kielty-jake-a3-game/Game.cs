// Include code libraries you need below (use the namespace).
using Raylib_cs;
using System;
using System.Numerics;
using System.Threading.Tasks.Sources;

// The namespace your code is in.
namespace Game10003;

/// <summary>
///     Your game code goes inside this class!
/// </summary>
public class Game
{
    // Place your variables here:
    Player player = new Player();
    Enemy[] enemies = new Enemy[6];
    Cloud[] clouds = new Cloud[10];
    Player[] bullets = new Player[8];

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
        player.speed = 475;
        player.health = 3;
        player.score = 0;

        //Initializing the bullet's position
        player.bulletPosition.X = player.position.X;
        player.bulletPosition.Y = player.position.Y;

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
            enemy.size = new Vector2(100, 100);
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

        //Cloud Movement - If the clouds hit the edge of the screen, wrap them back around to the top of the screen. 
        for (int i = 0; i < clouds.Length; i++)
        {
            clouds[i].MoveCloud();
            bool didCloudWrap = clouds[i].KeepCloudOnScreen();
        }

        //Enemy Collision - If the player hits an enemy, toggle visibility off for that enemy, and remove 1 HP. Also, if the enemy hits the edge of
        //                  the screen, or gets destroyed by the player, increase enemy count by one per enemy wrapped/destroyed. 
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
                activeEnemyCount++;
            }
        }

        //Bullet Collision - If the bullet hits an enemy, grant +100 score, toggle the enemy visibility off for that enemy, and despawn the projectile. 
        for (int i = 0; i < activeEnemyCount; i++)
        {
            enemies[i].EnemyMove();

            bool didBulletHit = player.DoesBulletHitEnemy(enemies[i]);
            if (didBulletHit == true)
            {
                player.score += 100;
                enemies[i].isVisible = false;
                player.isShooting = false;
            }
        }
        

        //If the player dies, draw the game over screen. Otherwise, display the game. 
        if (player.health <= 0)
        {
            DrawGameOver();
        }
        else
        {
            DrawGame();
        }

        //Draw the game. Enemies, UI, Projectiles, Clouds, etc. 
        void DrawGame()
        {
            DrawBackground();

            for (int i = 0; i < clouds.Length; i++)
            {
                clouds[i].DrawCloud();
            }

            for (int i = 0; i < enemies.Length; i++)
            {
                enemies[i].DrawEnemy();
            }

            player.Shoot();

            for (int i = 0; i <= bullets.Length; i++)
            {
                player.SpawnBullet();
            }
            
            player.DrawPlayer();
            DrawUI();
        }

        //Draws the UI. Also, if the player's score is above above 9999, then change up the positioning and size in order to automatically fit it in the UI. 
        void DrawUI()
        {
            //UI Elements
            Texture2D textureInterface = Graphics.LoadTexture("../../../assets/UI.png");
            Graphics.Draw(textureInterface, 0, 0);

            //Health Indicator
            Text.Color = Color.Cyan;
            Text.Size = 50;
            Font uiText = Text.LoadFont("../../../assets/PixelSplitter-Bold.ttf");
            Text.Draw($"{player.health}", Window.Size - new Vector2(715, 57), uiText);

            //Score Indicator
            if (player.score < 10000)
            {
                Text.Size = 35;
                Text.Draw($"{player.score}", Window.Size - new Vector2(115, 54), uiText);
            }
            else
            {
                Text.Size = 30;
                Text.Draw($"{player.score}", Window.Size - new Vector2(115, 52), uiText);
            }
        }

        //Draws the background. It uses a tiled texture and loops its position in order to create the illusion of scrolling indefinitely. 
        void DrawBackground()
        {
            bgPosition += bgMovement;

            Draw.FillColor = Color.Blue;
            Draw.Rectangle(bgPosition.X, bgPosition.Y, bgPosition.X + 800, bgPosition.Y + 600);

            //Background Texture
            Texture2D textureBackground = Graphics.LoadTexture("../../../assets/OceanBGV2.png");
            Graphics.Draw(textureBackground, bgPosition.X, bgPosition.Y - 600);

            //If the background image reaches the top, reset back to starting position and keep moving (to produce pseudo-scrolling effect)
            if (bgPosition.Y == 600)
            {
                bgPosition.Y = 0;
            }
        }

        //Draw Game Over Screen. Displays your score, and if you got 5000 points or more, it also displays a "Well Done!" message. 
        void DrawGameOver()
        {
            Window.ClearBackground(Color.Black);
            Text.Size = 70; 
            Text.Color = Color.Cyan;
            Font uiText = Text.LoadFont("../../../assets/PixelSplitter-Bold.ttf");
            Text.Draw("MISSION COMPLETE.", Window.Size - new Vector2(750, 400), uiText);

            Text.Size = 40;
            Text.Draw($"Your final score is: {player.score}", Window.Size - new Vector2(690, 300), uiText);

            if (player.score >= 5000)
            {
                Text.Draw("Well done!", Window.Size - new Vector2(515, 225), uiText);
            }
        }
    }
}
