﻿using System.Diagnostics;
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
}






