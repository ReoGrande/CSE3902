﻿using System.Diagnostics;
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

    public class MoveLeft:ICommand{
        private Game1 myGame;
        private Rectangle[] Frame;
        private int countFrame;
        private int countTime;
        private int speed;

        private Rectangle position;
        private SpriteEffects left;

        public MoveLeft(Game1 game){
            myGame = game;
            Frame = new Rectangle[2];
            Frame[0] = new Rectangle(35, 11, 15, 15);//WalkLeft Frame 1
            Frame[1] = new Rectangle(52, 11, 15, 15);//WalkLeft frame 2
            countFrame = 1;
            position =new Rectangle(350,150,150,150);
            speed = 30;
            left = SpriteEffects.FlipHorizontally;
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
            sprites.Draw(mar,position,Frame[countFrame-1],Color.White, 0, new Vector2(), left, 1);
            sprites.End();
        }
    }

    public class MoveRight:ICommand{
        private Game1 myGame;
        private Rectangle[] Frame;
        private int countFrame;
        private int countTime;
        private int speed;

        private Rectangle position;

        public MoveRight(Game1 game){
            myGame = game;
            Frame = new Rectangle[2];
            Frame[0] = new Rectangle(35, 11, 15, 15);//WalkLeft Frame 1
            Frame[1] = new Rectangle(52, 11, 15, 15);//WalkLeft frame 2
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
                if(position.X <speed*(-20)){
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
    public class MoveUp:ICommand{
        private Game1 myGame;
        private Rectangle[] Frame;
        private int countFrame;
        private int countTime;
        private int speed;

        private Rectangle position;

        public MoveUp(Game1 game){
            myGame = game;
            Frame = new Rectangle[2];
            Frame[0] = new Rectangle(86, 11, 15, 15);//Stand Frame 1
            Frame[1] = new Rectangle(69, 11, 15, 15);//Stand Frame 2
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
                if(position.Y > speed*4){
                    position.Y = position.Y + speed;
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

    public class MoveDown:ICommand{
        private Game1 myGame;
        private Rectangle[] Frame;
        private int countFrame;
        private int countTime;
        private int speed;

        private Rectangle position;

        public MoveDown(Game1 game){
            myGame = game;
            Frame = new Rectangle[2];
            Frame[0] = new Rectangle(1, 11, 15, 15);//Stand Frame 1
            Frame[1] = new Rectangle(18, 11, 15, 15);//Stand Frame 2
            countFrame = 1;
            position =new Rectangle(350,150,150,150);
            speed = -30;
        }

        public void Execute(){
            // SpriteBatch sprites = new SpriteBatch(myGame.GraphicsDevice);
            // Texture2D mar = myGame.Content.Load<Texture2D>("Zelda_Sheet");
            // if(countTime >10){
            //     if(countFrame % Frame.Length ==0){
            //         countFrame = 1;
            //     }else{
            //         countFrame++;
                    
            //     }
            //     if(position.Y < 150 *2){
            //         position.Y = position.Y - speed;
            //         }else{
            //             position.Y = 150;
            //         }
            //     countTime = 0;
            // }
            // countTime++;
            // sprites.Begin();
            // sprites.Draw(mar,position,Frame[countFrame-1],Color.White);
            // sprites.End();
        }
    }

    
    
}

