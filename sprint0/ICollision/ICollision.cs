using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static sprint0.Link;

namespace sprint0
{

    public class CollisionController
    {
        OutItemSpace outItemSpace;
        ItemSpace itemSpace;
        BlockSpace blockSpace;
        EnemySpace enemySpace;
        ILinkState link;

        // Variables used for Link Damage
        Game1 game;

        public CollisionController(Game1 game)
        {
            this.game = game;
            this.outItemSpace = game.outItemSpace;
            this.blockSpace = game.blockSpace;
            this.enemySpace = game.enemySpace;
            this.link = game.character;
            this.itemSpace = game.itemSpace;
        }

        protected void sortEnemy()
        {
            //use selection sort
            List<IEnemy> originalList = enemySpace.EnemyList();
            int length = originalList.Count;
            for (int i = 0; i < length; i++)
            {

                int min = originalList[i].GetX1();
                int minIndex = i;
                for (int j = i; j < length; j++)
                {
                    if (originalList[j].GetX1() < originalList[minIndex].GetX1())
                    {
                        min = originalList[j].GetX1();
                        minIndex = j;
                    }
                }

                if (originalList[i].GetX1() != originalList[minIndex].GetX1())
                {
                    IEnemy tempEnemy = originalList[minIndex];
                    originalList[minIndex] = originalList[i];
                    originalList[i] = tempEnemy;
                }
            }
        }

        public void collisionDetection()
        {
            sortEnemy();
            blockToEnemies();
            itemToEnemies();
            itemToBlocks();
            itemToBounds();
            linkToBlocks();
            linkToEnemies();
            linkToItems();
            linkToBounds();
            EnemyToBounds();


        }

        protected void blockToEnemies()
        {
            List<IBlock> blockList = blockSpace.BlockList();
            List<IEnemy> enemyList = enemySpace.EnemyList();
            foreach (IBlock block in blockList)
            {
                for (int i = 0; i < enemyList.Count; i++)
                {
                    IEnemy enemy = enemyList[i];
                    Rectangle enemyPos = enemy.GetPosition();

                    block.CollisionWithEnemy(enemy);
                    if (block.GetX2() < enemy.GetX1())
                    {
                        break;
                        //no need to do extra test
                    }

                }


            }

        }

        protected void itemToEnemies()
        {

            List<IItem> itemList = outItemSpace.OutItemList();

            List<IEnemy> enemyList = enemySpace.EnemyList();
            foreach (IItem item in itemList)
            {
                for (int i = 0; i < enemyList.Count; i++)
                {
                    IEnemy enemy = enemyList[i];
                    Boolean require1 = item.GetX2() >= enemy.GetX1();
                    Boolean require2 = item.GetX1() <= enemy.GetX2();
                    Boolean require3 = item.GetY2() >= enemy.GetY1();
                    Boolean require4 = item.GetY1() <= enemy.GetY2();


                    if (require1 && require2 && require3 && require4)
                    {

                        //need to handle touch here
                        item.CollisionWithEnemy(enemy);
                    }

                    //no touch in other conditions
                    if (item.GetX2() < enemy.GetX1())
                    {
                        break;
                        //no need to do extra test
                    }

                }

            }
        }

        protected void itemToBlocks()
        {

            List<IItem> itemList = outItemSpace.OutItemList();
            List<IBlock> blockList = blockSpace.BlockList();

            foreach (IItem item in itemList)
            {
                foreach (IBlock block in blockList)
                {

                    Boolean require1 = item.GetX2() >= block.GetX1();
                    Boolean require2 = item.GetX1() <= block.GetX2();
                    Boolean require3 = item.GetY2() >= block.GetY1();
                    Boolean require4 = item.GetY1() <= block.GetY2();


                    if (require1 && require2 && require3 && require4)
                    {

                        //need to handle touch here
                        block.CollisionWithItem(item);
                    }
                    //no touch in other conditions


                }

            }
        }


        protected void itemToBounds()
        {

            List<IItem> itemList = outItemSpace.OutItemList();


            Rectangle[] bounding = game._currentMap.MapControl.getRoomBounds();

            foreach (Rectangle bound in bounding)
            {
                for (int i = 0; i < itemList.Count; i++)
                {

                    IItem item = itemList[i];
                    if (item.GetPosition().Intersects(bound))
                    {
                        //need to handle touch here
                        item.CollisionWithBound(bound);
                    }
                    //no touch in other conditions


                }

            }
        }





        protected void linkToEnemies()
        {
            link = game.character;
            Rectangle linkPos = link.GetPosition();
            Rectangle swordPos = new Rectangle();

            List<IEnemy> enemyList = enemySpace.EnemyList();
            foreach (IEnemy enemy in enemyList)
            {
                if (linkPos.Intersects(enemy.GetPosition()))
                {
                    if (!link.IsAttacking() && enemy.Attackable())
                    {
                        link.TakeDamage(enemy.Power() * (-1));
                    }
                    else
                    {
                        switch (link.GetDirection())
                        {
                            case Link.Direction.Up:
                                swordPos = new Rectangle(linkPos.X, linkPos.Y, linkPos.Width, linkPos.Height / 2);
                                break;
                            case Link.Direction.Down:
                                swordPos = new Rectangle(linkPos.X, linkPos.Bottom - linkPos.Height / 2, linkPos.Width, linkPos.Height / 2);
                                break;
                            case Link.Direction.Left:
                                swordPos = new Rectangle(linkPos.X, linkPos.Y, linkPos.Width / 2, linkPos.Height);
                                break;
                            case Link.Direction.Right:
                                swordPos = new Rectangle(linkPos.Right - linkPos.Width / 2, linkPos.Y, linkPos.Width / 2, linkPos.Height);
                                break;
                            default:
                                break;
                        }
                        if (swordPos.Intersects(enemy.GetPosition()) && enemy.Touchable())
                        {
                            enemy.GetDamaged();
                            enemy.ChangeHP(-1);
                        }
                        else
                        {
                            link.TakeDamage(enemy.Power() * (-1));

                        }
                    }



                    /* In the future, if you want to see what direction link is facing, use:
                     * if (link.GetDirection() == Link.Direction.Left)...ect...
                    */
                }
            }
        }


        protected void linkToBlocks()
        {
            Rectangle linkPos = link.GetPosition();

            List<IBlock> blockList = blockSpace.BlockList();
            foreach (IBlock block in blockList)
            {
                if (linkPos.Intersects(block.GetPosition()))
                {
                    switch (link.GetDirection())
                    {
                        case Link.Direction.Up:
                            link.ChangePosition(new Rectangle(linkPos.X, block.GetPosition().Bottom, linkPos.Width, linkPos.Height));
                            break;
                        case Link.Direction.Down:
                            link.ChangePosition(new Rectangle(linkPos.X, block.GetPosition().Top - linkPos.Height, linkPos.Width, linkPos.Height));
                            break;
                        case Link.Direction.Left:
                            link.ChangePosition(new Rectangle(block.GetPosition().Right, linkPos.Y, linkPos.Width, linkPos.Height));
                            break;
                        case Link.Direction.Right:
                            link.ChangePosition(new Rectangle(block.GetPosition().Left - linkPos.Width, linkPos.Y, linkPos.Width, linkPos.Height));
                            break;
                        default:
                            break;
                    }
                }
            }
        }



        protected void linkToItems()
        {

            Rectangle linkPos = link.GetPosition();

            List<IItem> itemList = outItemSpace.OutItemList();
            foreach (IItem item in itemList)
            {
                if (linkPos.Intersects(item.GetPosition()))
                {
                    item.CollisionWithLink(link, itemSpace);
                }
            }
        }

        protected void linkToBounds()
        {
            Rectangle linkPos = link.GetPosition();
            Rectangle[] bounding = game._currentMap.MapControl.getRoomBounds();
            Rectangle[] locked = game._currentMap.MapControl.getLockedRoomDoors();
            Rectangle[] doors = game._currentMap.MapControl.getRoomDoors();
            Boolean inDoor = false;
            foreach (Rectangle door in locked)
            {
                if (linkPos.Intersects(door))
                {
                    //TODO: UNLOCK DOOR ONLY IF LINK HAS A KEY, REMOVE KEY FROM INVENTORY
                    game._currentMap.MapControl.keyEnableDoor(door, itemSpace);
                }
            }
            foreach (Rectangle bound in bounding)
            {
                foreach (Rectangle door in doors)
                {
                    if (linkPos.Intersects(door))
                    {
                        inDoor = true;
                    }
                }

                if (inDoor != true && linkPos.Intersects(bound))
                {


                    Rectangle intersectRegion = Rectangle.Intersect(bound, linkPos);
                    if (intersectRegion.Width > intersectRegion.Height)
                    {//up and down
                        if (bound.Top < linkPos.Top)
                        {
                            //up
                            link.ChangePosition(new Rectangle(linkPos.X, bound.Bottom, linkPos.Width, linkPos.Height));
                        }
                        else
                        {
                            //down
                            link.ChangePosition(new Rectangle(linkPos.X, bound.Top - linkPos.Height, linkPos.Width, linkPos.Height));

                        }
                    }
                    else
                    {//left and right
                        if (bound.Left < linkPos.Left)
                        { //left
                            link.ChangePosition(new Rectangle(bound.Right, linkPos.Y, linkPos.Width, linkPos.Height));
                        }
                        else
                        {
                            //right
                            link.ChangePosition(new Rectangle(bound.Left - linkPos.Width, linkPos.Y, linkPos.Width, linkPos.Height));
                        }

                    }




                }
            }
        }

        protected void EnemyToBounds()
        {


            Rectangle[] bounding = game._currentMap.MapControl.getRoomBounds();
            List<IEnemy> enemyList = enemySpace.EnemyList();
            foreach (Rectangle bound in bounding)
            {
                for (int i = 0; i < enemyList.Count; i++)
                {
                    IEnemy enemy = enemyList[i];
                    Rectangle enemyPos = enemy.GetPosition();
                    if (enemyPos.Intersects(bound))
                    {
                        Rectangle intersectRegion = Rectangle.Intersect(bound, enemyPos);
                        if (intersectRegion.Width > intersectRegion.Height)
                        {//up and down
                            if (bound.Top < enemyPos.Top)
                            {
                                //up
                                enemy.ChangePosition(new Rectangle(enemyPos.X, bound.Bottom, enemyPos.Width, enemyPos.Height));
                            }
                            else
                            {
                                //down
                                enemy.ChangePosition(new Rectangle(enemyPos.X, bound.Top - enemyPos.Height, enemyPos.Width, enemyPos.Height));

                            }
                        }
                        else
                        {//left and right
                            if (bound.Left < enemyPos.Left)
                            { //left
                                enemy.ChangePosition(new Rectangle(bound.Right, enemyPos.Y, enemyPos.Width, enemyPos.Height));
                            }
                            else
                            {
                                //right
                                enemy.ChangePosition(new Rectangle(bound.Left - enemyPos.Width, enemyPos.Y, enemyPos.Width, enemyPos.Height));
                            }

                        }



                    }





                }




            }

        }
    }
}
