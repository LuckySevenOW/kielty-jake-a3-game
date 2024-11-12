using System;
using System.Numerics;

namespace Game10003;

public class UserInterface
{ 
    Player player = new Player();
    

   

    //Draw Game Over Screen
    public void DrawGameOver()
    {
        Window.ClearBackground(Color.Black);
        Text.Size = 80;
        Text.Color = Color.Red;
        Text.Draw("MISSION FAILED.", Window.Size - new Vector2(710, 350));
    }
}
