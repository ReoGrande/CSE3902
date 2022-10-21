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
        CollisionHandlerLink lnkColHand;

        // Variables used for Link Damage
        bool damaged;

        public CollisionController(Game1 game)
        {
            this.outItemSpace = game.outItemSpace;
            this.blockSpace = game.blockSpace;
            this.enemySpace = game.enemySpace;
            this.link = game.character;
            this.lnkColHand = new CollisionHandlerLink(game);
            this.damaged = false;
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
                        block.CollisionWithEnemy(enemy);
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


        protected void linkToEnemies()
        {
            Rectangle linkPos = link.GetPosition();

            List<IEnemy> enemyList = enemySpace.EnemyList();
            foreach (IEnemy enemy in enemyList)
            {
                if (linkPos.Intersects(enemy.GetPosition()))
                {
                    damaged = true;
                }
            }

            damaged = lnkColHand.TakeDamage(damaged);
        }


        protected void linkToBlocks()
        {
            Rectangle linkPos = link.GetPosition();

            List<IBlock> blockList = blockSpace.BlockList();
            foreach (IBlock block in blockList)
            {
                if (linkPos.Intersects(block.GetPosition()))
                {
                    damaged = true;
                }
            }

            damaged = lnkColHand.TakeDamage(damaged);
        }



        protected void linkToItems()
        {

            Rectangle linkPos = link.GetPosition();

            List<IItem> itemList = outItemSpace.OutItemList();
            foreach (IItem item in itemList)
            {
                if (linkPos.Intersects(item.GetPosition()))
                {
                    damaged = true;
                }
            }
            damaged = lnkColHand.TakeDamage(damaged);
        }

    }






}
