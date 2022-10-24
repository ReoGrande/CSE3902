using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
namespace sprint0
{
    public class PreviousEnemy : SingleClickCommand
    {
        private Game1 myGame;
        public PreviousEnemy(Game1 game)
        {
            myGame = game;
            startTime = System.Environment.TickCount;
            endTime = System.Environment.TickCount;
        }

        public override void SingleExecute()
        {
            myGame.ChangetoPreviousEnemy();
        }
    }
    public class NextEnemy : SingleClickCommand
    {
        private Game1 myGame;
        public NextEnemy(Game1 game)
        {
            myGame = game;
            startTime = System.Environment.TickCount;
            endTime = System.Environment.TickCount;
        }

        public override void SingleExecute()
        {
            myGame.ChangetoNextEnemy();
        }
    }




}
