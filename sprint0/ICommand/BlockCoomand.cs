using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
namespace sprint0
{
    public class PreviousBlock : ICommand
    {
        private Game1 myGame;
        public PreviousBlock(Game1 game)
        {
            myGame = game;
        }

        public void Execute()
        {
            myGame.ChangetoPreviousBlock();
        }
    }
    public class NextBlock : ICommand
    {
        private Game1 myGame;
        public NextBlock(Game1 game)
        {
            myGame = game;
        }

        public void Execute()
        {
            myGame.ChangetoNextBlock();
        }
    }




}
