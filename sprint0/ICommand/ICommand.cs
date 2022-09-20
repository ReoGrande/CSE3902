using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
namespace sprint0
{
    public interface ICommand
    {
        void Execute();
    }

    public class DrawMario:ICommand{
        private Game1 myGame;
        public DrawMario(Game1 game){
            myGame = game;
        }

        public void Execute(){
            //private SpriteBatch sprites = new SpriteBatch(myGame.GraphicsDevice);
            Texture2D mar = myGame.Content.Load<Texture2D>("smb_mario_sheet");

        }
    }
}

