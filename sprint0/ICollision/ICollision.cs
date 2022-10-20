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

        // Variables used for Link Damage
        Game1 game;
        Link lLink;
        LinkDamagedDecorator dLink;
        int timer;
        bool damaged;


        public CollisionController(Game1 game)
        {
            this.outItemSpace = game.outItemSpace;
            this.blockSpace = game.blockSpace;
            this.enemySpace = game.enemySpace;
            this.link = game.character;
            this.game = game;
            this.lLink = (Link)game.character;
            this.dLink = new LinkDamagedDecorator(lLink);
            this.timer = 0;
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






        private void TakeDamageLink(CollisionController icollision)
        {
        

        }


        

        protected void linkToEnemies()
        {
            Rectangle linkPos = link.GetPosition();

            List<IEnemy> enemyList = enemySpace.EnemyList();
            foreach (IEnemy enemy in enemyList)
            {
                // FOR LARGE OBJECTS; small objects will pass through (Should be good for all enemies?)
                if (linkPos.Left < enemy.GetX2() && linkPos.Left > enemy.GetX1() && linkPos.Top < enemy.GetY2() && linkPos.Bottom > enemy.GetY1())
                {
                    //link's top-left side is colliding
                    damaged = true;
                }
                else if (linkPos.Left < enemy.GetX2() && linkPos.Left > enemy.GetX1() && linkPos.Bottom < enemy.GetY2() && linkPos.Bottom > enemy.GetY1())
                {
                    //link's bottom-left side is colliding
                    damaged = true;
                }
                else if (linkPos.Right < enemy.GetX2() && linkPos.Right > enemy.GetX1() && linkPos.Top < enemy.GetY2() && linkPos.Top > enemy.GetY1())
                {
                    //link's top-right side is colliding
                    damaged = true;
                }
                else if (linkPos.Right < enemy.GetX2() && linkPos.Right > enemy.GetX1() && linkPos.Bottom < enemy.GetY2() && linkPos.Bottom > enemy.GetY1())
                {
                    //link's bottom right side is colliding
                    damaged = true;
                }

            }

            if (damaged)
            {
                if (timer == 0)
                {
                    game.character = dLink;
                    timer++;
                }
                else if (timer < 50)
                {
                    timer++;
                }
                else if (timer >= 50)
                {
                    damaged = false;
                    lLink.color = Color.White;
                    game.character = (ILinkState)lLink;
                    timer = 0;

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
