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
            Texture2D mar = myGame.Content.Load<Texture2D>("Zelda_Sheet");
            sprites.Begin();
            sprites.Draw(mar,new Rectangle(350,150,150,150),new Rectangle(115,0,25,25),Color.White);
            sprites.End();

        }
    }

    public class MoveMarioLeft:ICommand{
        private Game1 myGame;
        private Rectangle[] Frame;
        private int countFrame;
        private int countTime;
        private int speed;

        private Rectangle position;

        public MoveMarioLeft(Game1 game){
            myGame = game;
            Frame = new Rectangle[2];
            Frame[0] = new Rectangle(85, 0, 25, 25);//WalkLeft Frame 1
            Frame[1] = new Rectangle(145, 0, 25, 25);//WalkLeft frame 2
            countFrame = 1;
            position =new Rectangle(350,150,150,150);
            speed = 30;
        }

        public void Execute(){
            SpriteBatch sprites = new SpriteBatch(myGame.GraphicsDevice);
            Texture2D mar = myGame.Content.Load<Texture2D>("Zelda_Sheet");
            if(countTime >10){
                if(countFrame % Frame.Length ==0){
                    countFrame = 1;
                }else{
                    countFrame++;
                    
                }
                if(position.X >speed){
                    position.X = position.X - speed;
                    }else{
                        position.X = 350;
                    }
                countTime = 0;
            }
            countTime++;
            sprites.Begin();
            sprites.Draw(mar,position,Frame[countFrame-1],Color.White);
            sprites.End();
        }
    }

    public class MoveMarioRight:ICommand{
        private Game1 myGame;
        private Rectangle[] Frame;
        private int countFrame;
        private int countTime;
        private int speed;

        private Rectangle position;

        public MoveMarioRight(Game1 game){
            myGame = game;
            Frame = new Rectangle[2];
            Frame[0] = new Rectangle(240, 0, 25, 25);//WalkLeft Frame 1
            Frame[1] = new Rectangle(300, 0, 25, 25);//WalkLeft frame 2
            countFrame = 1;
            position =new Rectangle(350,150,150,150);
            speed = -30;
        }

        public void Execute(){
            SpriteBatch sprites = new SpriteBatch(myGame.GraphicsDevice);
            Texture2D mar = myGame.Content.Load<Texture2D>("Zelda_Sheet");
            if(countTime >10){
                if(countFrame % Frame.Length ==0){
                    countFrame = 1;
                }else{
                    countFrame++;
                    
                }
                if(position.X >speed){
                    position.X = position.X - speed;
                    }else{
                        position.X = 350;
                    }
                countTime = 0;
            }
            countTime++;
            sprites.Begin();
            sprites.Draw(mar,position,Frame[countFrame-1],Color.White);
            sprites.End();
        }
    }
    public class MoveMarioUp:ICommand{
        private Game1 myGame;
        private Rectangle[] Frame;
        private int countFrame;
        private int countTime;
        private int speed;

        private Rectangle position;

        public MoveMarioUp(Game1 game){
            myGame = game;
            Frame = new Rectangle[1];
            Frame[0] = new Rectangle(115, 0, 25, 25);//Stand Frame 1
            countFrame = 1;
            position =new Rectangle(350,150,150,150);
            speed = -30;
        }

        public void Execute(){
            SpriteBatch sprites = new SpriteBatch(myGame.GraphicsDevice);
            Texture2D mar = myGame.Content.Load<Texture2D>("Zelda_Sheet");
            if(countTime >10){
                if(countFrame % Frame.Length ==0){
                    countFrame = 1;
                }else{
                    countFrame++;
                    
                }
                if(position.Y < speed){
                    position.Y = position.Y - speed;
                    }else{
                        position.Y = 150;
                    }
                countTime = 0;
            }
            countTime++;
            sprites.Begin();
            sprites.Draw(mar,position,Frame[countFrame-1],Color.White);
            sprites.End();
        }
    }

    
    
}

