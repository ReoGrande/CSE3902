using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
namespace sprint0
{
    public class PreviousItem : ICommand
    {
        private Game1 myGame;
        public PreviousItem(Game1 game)
        {
            myGame = game;
        }

        public void Execute()
        {
            myGame.ChangetoPreviousItem();
        }
    }
    public class NextItem : ICommand
    {
        private Game1 myGame;
        public NextItem(Game1 game)
        {
            myGame = game;
        }

        public void Execute()
        {
            myGame.ChangetoNextItem();
        }
    }




}

