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
            itemToDynamicObjects();

        }

        protected void itemToDynamicObjects()
        {
            List<IBlock> blockList = blockSpace.BlockList();
            List<IEnemy> enemyList = enemySpace.EnemyList();
            foreach (IBlock block in blockList)
            {
                for (int i = 0; i < enemyList.Count; i++)
                {
                    IEnemy enemy = enemyList[i];
                    Boolean require1 = block.getX2() >= enemy.GetX1();
                    Boolean require2 = block.getX1() <= enemy.GetX2();
                    Boolean require3 = block.getY2() >= enemy.GetY1();
                    Boolean require4 = block.getY1() <= enemy.GetY2();


                    if (require1 && require2 && require3 && require4)
                    {

                        //need to handle touch here
                        enemy.GetDamaged();
                    }

                    //no touch in other conditions
                    if (block.getX2() < enemy.GetX1())
                    {
                        break;
                        //no need to do extra test
                    }


                }

            }
        }






        protected void blockToDynamicObjects()
        {

        }

        protected void linkToEnemies()
        { }



    }

}
