
using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
namespace sprint0
{

    public class EnemySpace
    {
        private List<IEnemy> enemyList;
        private int currentIndex;

        public EnemySpace()
        {
            enemyList = new List<IEnemy>();
            currentIndex = 0;
        }

        public void Add(IEnemy iEnemy)
        {
            this.enemyList.Add(iEnemy);
        }

        public void Update(Game1 game)
        {
            enemyList[currentIndex].EnemyUpdate(game);

        }

        public void Draw(SpriteBatch _spriteBatch)
        {
            enemyList[currentIndex].EnemyDraw(_spriteBatch);

        }

        public void PreviousEnemy()
        {
            if (currentIndex > 0)
            {
                currentIndex--;
            }
            else
            {
                currentIndex = enemyList.Count - 1;
            }
        }

        public void NextEnemy()
        {
            if (currentIndex < enemyList.Count - 1)
            {
                currentIndex++;
            }
            else
            {
                currentIndex = 0;
            }
        }
    }
}
