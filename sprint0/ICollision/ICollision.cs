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

                int min = originalList[i].getX1();
                int minIndex = i;
                for (int j = i; j < length; j++)
                {
                    if (originalList[j].getX1() < originalList[minIndex].getX1())
                    {
                        min = originalList[j].getX1();
                        minIndex = j;
                    }
                }

                if (originalList[i].getX1() != originalList[minIndex].getX1())
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
        }

        protected void itemToDynamicObjects()
        {

        }
        protected void blockToDynamicObjects()
        {

        }

        protected void linkToEnemies()
        { }



    }

}
