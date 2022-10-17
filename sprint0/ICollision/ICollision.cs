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
            List<IEnemy> originalList = enemySpace.EnemyList();
            int length = originalList.Count;
            for (int i = 0; i < length; i++)
            {
                IEnemy enemy = originalList[i];
                int min = originalList[i].;     //设定第i位为最小值
                int minIndex = i;       //最小值下标
                for (int j = i; j < array.Length; j++)  //从第i为开始找出最小的数
                {
                    if (array[j] < array[minIndex])     //重新存储最小值和下标
                    {
                        min = array[j];
                        minIndex = j;
                    }
                }

                if (array[i] != array[minIndex])        //如果到比第i为更小的数，则发生交换。找不到则不改变
                {
                    array[minIndex] = array[i];
                    array[i] = min;
                }
            }




        }



    }

    public void collisionDetection()
    {

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
