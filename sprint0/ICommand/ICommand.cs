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

    public abstract class SingleClickCommand:ICommand
    {

        public int startTime;
        public int endTime;
                      
        public abstract void SingleExecute();

        public void Execute()
        {
            endTime = System.Environment.TickCount;
            int runTime = endTime - startTime;
            if (runTime > 200)
            {
                SingleExecute();
                startTime = endTime;
        }


        }}


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
        private Link link;

        public MoveLeft(Game1 game){
            myGame = game;
            link = myGame.character;
        }

        public void Execute(){
            link.direction = Link.Direction.Left;
            link.ToMoving();
        }
    }

    public class MoveRight:ICommand{
        private Game1 myGame;
        private Link link;

        public MoveRight(Game1 game){
            myGame = game;
            link = myGame.character;
        }

        public void Execute(){
            link.direction = Link.Direction.Right;
            link.ToMoving();
        }
    }
    public class MoveUp:ICommand{
        private Game1 myGame;
        private Link link;

        public MoveUp(Game1 game){
            myGame = game;
            link = myGame.character;
        }

        public void Execute(){
            link.direction = Link.Direction.Up;
            link.ToMoving();
        }
    }

    public class MoveDown:ICommand{
        private Game1 myGame;
        private Link link;

        public MoveDown(Game1 game){
            myGame = game;
            link = myGame.character;
        }

        public void Execute(){
            link.direction = Link.Direction.Down;
            link.ToMoving();
        }
    }

    public class Idle : ICommand
    {
        private Game1 myGame;
        private Link link;

        public Idle(Game1 game)
        {
            myGame = game;
            link = myGame.character;
        }

        public void Execute()
        {
            link.ToStanding();
        }
    }



}

