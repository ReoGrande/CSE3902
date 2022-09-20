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
            SpriteBatch sprites = new SpriteBatch(myGame.GraphicsDevice);
            Texture2D mar = myGame.Content.Load<Texture2D>("smb_mario_sheet");
            sprites.Begin();
            sprites.Draw(mar,new Rectangle(350,150,150,150),new Rectangle(115,0,25,25),Color.White);
            sprites.End();

        }
    }

    public class MoveMarioLeft:ICommand{
        private Game1 myGame;
        private Rectangle[] Frame;
        private int countFrame;
        public MoveMarioLeft(Game1 game){
            myGame = game;
            Frame = new Rectangle[2];
            Frame[0] = new Rectangle(85, 0, 25, 25);//WalkLeft Frame 1
            Frame[1] = new Rectangle(145, 0, 25, 25);//WalkLeft frame 2
            countFrame = 0;
        }

        public void Execute(){
            SpriteBatch sprites = new SpriteBatch(myGame.GraphicsDevice);
            Texture2D mar = myGame.Content.Load<Texture2D>("smb_mario_sheet");
            if(countFrame % Frame.Length ==0){
                countFrame = 0;
            }else{
                countFrame++;
            }
            sprites.Begin();
            sprites.Draw(mar,new Rectangle(350,150,150,150),Frame[countFrame],Color.White);
            sprites.End();
        }
    }
}

