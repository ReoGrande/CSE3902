using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sprint0
{

    public class CollisionController
    {
        OutItemSpace outItemSpace;
        BlockSpace blockSpace;
        EnemySpace enemySpace;
        ILinkState link;

        public CollisionController(Game1 game)
        {
            this.outItemSpace = game.outItemSpace;
            this.blockSpace = game.blockSpace;
            this.enemySpace = game.enemySpace;
            this.link = game.character;
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
            linkToBlocks();
            linkToEnemies();
            linkToItems();


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
                    Boolean require1 = block.GetX2() >= enemy.GetX1();
                    Boolean require2 = block.GetX1() <= enemy.GetX2();
                    Boolean require3 = block.GetY2() >= enemy.GetY1();
                    Boolean require4 = block.GetY1() <= enemy.GetY2();


                    if (require1 && require2 && require3 && require4)
                    {

                        //need to handle touch here
                        enemy.GetDamaged();
                    }

                    //no touch in other conditions
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
                        enemy.GetDamaged();
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

        protected void linkToEnemies()
        {
            int linkX = link.GetPosition().X;
            int linkY = link.GetPosition().Y;
            int linkW = link.GetPosition().Width;
            int linkH = link.GetPosition().Height;

            List<IEnemy> enemyList = enemySpace.EnemyList();
            foreach (IEnemy enemy in enemyList)
            {
                // FOR LARGE OBJECTS; small objects will pass through (Should be good for all enemies?)
                if ((linkX < enemy.GetX2() && linkX > enemy.GetX1()) && (linkY < enemy.GetY2() && linkY > enemy.GetY1()))
                {
                    //link's top-left side is colliding
                    link.ToThrowing();
                }
                else if ((linkX < enemy.GetX2() && linkX > enemy.GetX1()) && ((linkY + linkH) < enemy.GetY2() && (linkY + linkH) > enemy.GetY1()))
                {
                    link.ToThrowing();
                    //link's bottom-left side is colliding
                }
                else if (((linkX + linkW) < enemy.GetX2() && (linkX + linkW) > enemy.GetX1()) && (linkY < enemy.GetY2() && linkY > enemy.GetY1()))
                {
                    link.ToThrowing();
                    //link's top-right side is colliding
                }
                else if (((linkX + linkW) < enemy.GetX2() && (linkX + linkW) > enemy.GetX1()) && ((linkY + linkH) < enemy.GetY2() && (linkY + linkH) > enemy.GetY1()))
                {
                    link.ToThrowing();
                    //link's bottom right side is colliding
                }

            }

        }


        protected void linkToBlocks()
        {

            int linkX = link.GetPosition().X;
            int linkY = link.GetPosition().Y;
            int linkW = link.GetPosition().Width;
            int linkH = link.GetPosition().Height;
            List<IBlock> blockList = blockSpace.BlockList();
            foreach (IBlock block in blockList)
            {
                if ((linkX < block.GetX2() && linkX > block.GetX1()) && (linkY < block.GetY2() && linkY > block.GetY1()))
                {
                    //link's top-left side is colliding
                    link.ToThrowing();
                }
                else if ((linkX < block.GetX2() && linkX > block.GetX1()) && ((linkY + linkH) < block.GetY2() && (linkY + linkH) > block.GetY1()))
                {
                    link.ToThrowing();
                    //link's bottom-left side is colliding
                }
                else if (((linkX + linkW) < block.GetX2() && (linkX + linkW) > block.GetX1()) && (linkY < block.GetY2() && linkY > block.GetY1()))
                {
                    link.ToThrowing();
                    //link's top-right side is colliding
                }
                else if (((linkX + linkW) < block.GetX2() && (linkX + linkW) > block.GetX1()) && ((linkY + linkH) < block.GetY2() && (linkY + linkH) > block.GetY1()))
                {
                    link.ToThrowing();
                    //link's bottom right side is colliding
                }


            }
        }



        protected void linkToItems()
        {

            int linkX1 = link.GetPosition().X;
            int linkY1 = link.GetPosition().Y;
            int linkX2 = link.GetPosition().X + link.GetPosition().Width;
            int linkY2 = link.GetPosition().Y + link.GetPosition().Height;
            List<IItem> itemList = outItemSpace.OutItemList();
            foreach (IItem item in itemList)
            {
                if (linkX1 <= item.GetX2() && linkX2 >= item.GetX2() && linkY1 <= item.GetY2() &&
                linkY2 >= item.GetY1())
                {
                    link.ToThrowing();
                    //handle the collision between link and item
                }


            }


        }

    }






}
