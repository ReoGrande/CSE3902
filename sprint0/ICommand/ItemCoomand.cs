using System.Diagnostics;
using System.Reflection.PortableExecutable;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
namespace sprint0
{
    public class PreviousItem : SingleClickCommand
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
    public class NextItem : SingleClickCommand
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

    /*
    public class Shoot : SingleClickCommand
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
            //myGame.character.direction;

            
            IItem item = myGame.itemSpace.CurrentItem();
            if (item.IsThrowable()) { 
            // if the items is infinite
            if (item.IsInfinite())
            {
                IItem itemClone = item.Clone();
                myGame.itemSpace.Exchange(item.Clone());
            }
            else
            {
                //if the item is not infinite
                myGame.itemSpace.Remove(item);
            }

            item.ChangeDirection(myGame.character.direction);
            item.ToMoving();
            myGame.outItemSpace.Add(item);

                }
            myGame.character.ToThrowing();

        }
    }
    */
    public class ShootBoomerang : SingleClickCommand
    {
        private Game1 myGame;
        public ShootBoomerang(Game1 game)
        {
            myGame = game;
            startTime = System.Environment.TickCount;
            endTime = System.Environment.TickCount;

        }

        public override void SingleExecute()
        {
            //myGame.character.direction;


            List<IItem> itemList = myGame.itemSpace.ItemList();
            IItem item = itemList[5].Clone(); //Boomerang Position in ItemList
            if (item.IsThrowable())
            {
                item.ChangeDirection(myGame.character.GetDirection());
                item.ToMoving();
                myGame.outItemSpace.Add(item);

            }
            myGame.character.ToThrowing();

        }

    }

    public class ShootArrow : SingleClickCommand
    {
        private Game1 myGame;
        public ShootArrow(Game1 game)
        {
            myGame = game;
            startTime = System.Environment.TickCount;
            endTime = System.Environment.TickCount;

        }

        public override void SingleExecute()
        {
            //myGame.character.direction;


            List<IItem> itemList = myGame.itemSpace.ItemList();
            IItem item = itemList[8].Clone(); //Arrow Position in ItemList
            item.ChangeDirection(myGame.character.GetDirection());
            item.ToMoving();
            myGame.outItemSpace.Add(item);
            myGame.character.ToThrowing();

        }

    }

    public class ShootBomb : SingleClickCommand
    {
        private Game1 myGame;
        public ShootBomb(Game1 game)
        {
            myGame = game;
            startTime = System.Environment.TickCount;
            endTime = System.Environment.TickCount;

        }

        public override void SingleExecute()
        {
            //myGame.character.direction;


            List<IItem> itemList = myGame.itemSpace.ItemList();
            IItem item = itemList[9].Clone(); //Bomb Position in ItemList
            item.ChangeDirection(myGame.character.GetDirection());
            item.ToMoving();
            myGame.outItemSpace.Add(item);
            myGame.character.ToThrowing();

        }

    }
}






