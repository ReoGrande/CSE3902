using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
namespace sprint0
{
    public class PreviousItem :  SingleClickCommand
    {
        private Game1 myGame;
        public PreviousItem(Game1 game)
        {
            myGame = game;
            startTime = System.Environment.TickCount;
            endTime = System.Environment.TickCount;
        }

        public override void SingleExecute()
        {
            myGame.ChangetoPreviousItem();
        }
    }
    public class NextItem :  SingleClickCommand
    {
        private Game1 myGame;
        public NextItem(Game1 game)
        {
            myGame = game;
            startTime = System.Environment.TickCount;
            endTime = System.Environment.TickCount;
        }

        public override void SingleExecute()
        {
            myGame.ChangetoNextItem();
        }
    }

       public class Shoot :  SingleClickCommand
    {
        private Game1 myGame;
        public Shoot(Game1 game)
        {
            myGame = game;
            startTime = System.Environment.TickCount;
            endTime = System.Environment.TickCount;
            
        }

        public override void SingleExecute()
        {
            myGame.itemSpace.CurrentItem().ToMoving();
            myGame.character.ToThrowing();

        }
    }






}