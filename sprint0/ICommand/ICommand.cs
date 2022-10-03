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

    public abstract class SingleClickCommand:ICommand{

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


        }
    }

    public class LoadBlocks:ICommand{
      private Game1 myGame;
        public LoadBlocks(Game1 game){
            myGame = game;
        }
        public void Execute(){
            BlockFactory.Instance.LoadAllTextures(myGame);
            myGame.blockSpace.Add(BlockFactory.Instance.CreateSquareBlock(new Rectangle(100, 100, 50, 50)));
            myGame.blockSpace.Add(BlockFactory.Instance.CreatePushAbleBlock(new Rectangle(100, 100, 50, 50)));
            myGame.blockSpace.Add(BlockFactory.Instance.CreateFire(new Rectangle(100, 100, 50, 50)));
            myGame.blockSpace.Add(BlockFactory.Instance.CreateBlueGap(new Rectangle(100, 100, 50, 50)));
            myGame.blockSpace.Add(BlockFactory.Instance.CreateStairs(new Rectangle(100, 100, 50, 50)));
            myGame.blockSpace.Add(BlockFactory.Instance.CreateWhiteBrick(new Rectangle(100, 100, 50, 50)));
            myGame.blockSpace.Add(BlockFactory.Instance.CreateLadder(new Rectangle(100, 100, 50, 50)));
            myGame.blockSpace.Add(BlockFactory.Instance.CreateBlueFloor(new Rectangle(100, 100, 50, 50)));
            myGame.blockSpace.Add(BlockFactory.Instance.CreateBlueSand(new Rectangle(100, 100, 50, 50)));
            myGame.blockSpace.Add(BlockFactory.Instance.CreateBlueSand(new Rectangle(100, 100, 50, 50)));
        }
    }

    public class LoadItems:ICommand{
      private Game1 myGame;
      private Rectangle characterPos;
        public LoadItems(Game1 game){
            myGame = game;
            characterPos = myGame.character.position;
        }
    public void Execute(){
            

            ItemFactory.Instance.LoadAllTextures(myGame);
            myGame.itemSpace.Add(ItemFactory.Instance.CreateCompass(new Rectangle(characterPos.X, characterPos.Y, 50, 50)));
            myGame.itemSpace.Add(ItemFactory.Instance.CreateMap(new Rectangle(characterPos.X, characterPos.Y, 50, 50)));
            myGame.itemSpace.Add(ItemFactory.Instance.CreateKey(new Rectangle(characterPos.X, characterPos.Y, 50, 50)));
            myGame.itemSpace.Add(ItemFactory.Instance.CreateHeartContainer(new Rectangle(characterPos.X, characterPos.Y, 50, 50)));
            myGame.itemSpace.Add(ItemFactory.Instance.CreateTriforcePiece(new Rectangle(characterPos.X, characterPos.Y, 50, 50)));
            myGame.itemSpace.Add(ItemFactory.Instance.CreateWoodenBoomerang(new Rectangle(characterPos.X, characterPos.Y, 50, 50)));
            myGame.itemSpace.Add(ItemFactory.Instance.CreateBow(new Rectangle(characterPos.X, characterPos.Y, 50, 50)));
            myGame.itemSpace.Add(ItemFactory.Instance.Createrupee(new Rectangle(characterPos.X, characterPos.Y, 50, 50)));
            myGame.itemSpace.Add(ItemFactory.Instance.CreateArrow(new Rectangle(characterPos.X, characterPos.Y, 50, 50)));
            myGame.itemSpace.Add(ItemFactory.Instance.CreateBomb(new Rectangle(characterPos.X, characterPos.Y, 50, 50)));
            myGame.itemSpace.Add(ItemFactory.Instance.CreateFairy(new Rectangle(characterPos.X, characterPos.Y, 50, 50)));
            myGame.itemSpace.Add(ItemFactory.Instance.CreateClock(new Rectangle(characterPos.X, characterPos.Y, 50, 50)));
            myGame.itemSpace.Add(ItemFactory.Instance.CreateBlueCandle(new Rectangle(characterPos.X, characterPos.Y, 50, 50)));
            myGame.itemSpace.Add(ItemFactory.Instance.CreateBluePotion(new Rectangle(characterPos.X, characterPos.Y, 50, 50)));
        }
    }

    public class SetControllers:ICommand{
        private Game1 myGame;
        public SetControllers(Game1 game){
            myGame = game;
        }
        public void Execute(){
            myGame._controllers.RegisterCommand(Keys.A, new MoveLeft(myGame));
            myGame._controllers.RegisterCommand(Keys.D, new MoveRight(myGame));
            myGame._controllers.RegisterCommand(Keys.W, new MoveUp(myGame));
            myGame._controllers.RegisterCommand(Keys.S, new MoveDown(myGame));
            myGame._controllers.RegisterCommand(Keys.T, new PreviousBlock(myGame));
            myGame._controllers.RegisterCommand(Keys.Y, new NextBlock(myGame));
            myGame._controllers.RegisterCommand(Keys.U, new PreviousItem(myGame));
            myGame._controllers.RegisterCommand(Keys.I, new NextItem(myGame));
            myGame._controllers.RegisterCommand(Keys.O, new PreviousEnemy(myGame));
            myGame._controllers.RegisterCommand(Keys.P, new NextEnemy(myGame));
            myGame._controllers.RegisterCommand(Keys.Z, new Shoot(myGame));
        }
    }
    // public class DrawLink:ICommand{
    //     private Game1 myGame;
    //     private Link link;
    //     public DrawLink(Game1 game){
    //         myGame = game;
            
    //     }
    //     public void Execute(){
    //     }
    // }

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

