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
            characterPos = myGame.character.GetPosition();
        }
    public void Execute(){
            

            ItemFactory.Instance.LoadAllTextures(myGame);
            myGame.itemSpace.Add(ItemFactory.Instance.CreateCompass(new Rectangle(characterPos.X,characterPos.Y, 50, 50)));
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
            myGame._controllers.RegisterCommand(Keys.A, new Move(myGame));
            myGame._controllers.RegisterCommand(Keys.D, new Move(myGame));
            myGame._controllers.RegisterCommand(Keys.W, new Move(myGame));
            myGame._controllers.RegisterCommand(Keys.S, new Move(myGame));
            myGame._controllers.RegisterCommand(Keys.T, new PreviousBlock(myGame));
            myGame._controllers.RegisterCommand(Keys.Y, new NextBlock(myGame));
            myGame._controllers.RegisterCommand(Keys.U, new PreviousItem(myGame));
            myGame._controllers.RegisterCommand(Keys.I, new NextItem(myGame));
            myGame._controllers.RegisterCommand(Keys.O, new PreviousEnemy(myGame));
            myGame._controllers.RegisterCommand(Keys.P, new NextEnemy(myGame));
            myGame._controllers.RegisterCommand(Keys.Z, new Attack(myGame));
            myGame._controllers.RegisterCommand(Keys.N, new Attack(myGame));
            myGame._controllers.RegisterCommand(Keys.D1, new UseFirstItem(myGame));
            myGame._controllers.RegisterCommand(Keys.D2, new UseSecondItem(myGame));
            myGame._controllers.RegisterCommand(Keys.D3, new UseThirdItem(myGame));
            myGame._controllers.RegisterCommand(Keys.E, new TakeDamageOn(myGame));
        }
    }

    public class Reset : ICommand
    {
        private Game1 myGame;

        public Reset(Game1 game)
        {
            myGame = game;
        }

        public void Execute()
        {
            myGame._currentMap = new IMap(myGame);
            myGame.character.ChangePosition(new Link(myGame).GetPosition());
        }
    }

    public class Move:ICommand{
        private ILinkState link;

        public Move(Game1 game){
            link = game.character;
        }

        public void Execute(){
            link.ToMoving();
        }
    }

    public class Attack : SingleClickCommand
    {
        private Game1 myGame;
        private ILinkState link;

        public Attack(Game1 game)
        {
            myGame = game;
            link = myGame.character;
        }

        public override void SingleExecute()
        {
            link.ToAttacking();
        }
    }

    public class TakeDamageOn : SingleClickCommand
    {
        private Game1 myGame;
        private LinkDamagedDecorator dLink;
        Link link;

        public TakeDamageOn(Game1 game)
        {
            myGame = game;
            link = (Link)myGame.character;
            dLink = new LinkDamagedDecorator(link);
        }

        public override void SingleExecute()
        {
            //dLink.Update();
            if(myGame.character.GetType().Equals((new LinkDamagedDecorator(link)).GetType())){
                
                myGame.character = (ILinkState)link;
            }else{
                myGame.character = dLink;
            }
            //some wait time;

        }
    }
    
}

