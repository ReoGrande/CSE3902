
using System;
using System.Reflection.Metadata;
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

        public List<IEnemy> EnemyList()
        {

            return this.enemyList;
        }

        public void Add(IEnemy iEnemy)
        {
            this.enemyList.Add(iEnemy);
        }
        public void Remove(IEnemy iEnemy)
        {
            this.enemyList.Remove(iEnemy);
        }
        public void Clear()
        {
            this.enemyList = new List<IEnemy>();
        }

        public void Update(Game1 game)
        {

            for (int i = 0; i < enemyList.Count; i++)
            {
                IEnemy enemy = enemyList[i];
                enemy.EnemyUpdate(game);
                 if (enemy.NeedToBeRemoved())
                {
                    if (!enemy.IsDeathCloud())
                    {
                        
                        this.Add(EnemyFactory.Instance.CreateDeathCloud(enemy.GetPosition()));
                    }
                   enemyList.RemoveAt(i);
                }}

            }
        

        public void Draw(SpriteBatch _spriteBatch)
        {
            /*foreach (IEnemy enemy in this.enemyList)
            { enemy.EnemyDraw(_spriteBatch); }
            */
            for (int i = 0; i < this.enemyList.Count; i++)
            {
                enemyList[i].EnemyDraw(_spriteBatch);

            }


        }

        //This method now used to draw hp of enemy
        public void DrawNumber(SpriteBatch _spriteBatch, Game1 game)
        {
            /*foreach (IEnemy enemy in this.enemyList)
            { enemy.EnemyDraw(_spriteBatch); }
            */
            for (int i = 0; i < this.enemyList.Count; i++)
            {
                _spriteBatch.DrawString(game.font, enemyList[i].HP().ToString(), new Vector2(enemyList[i].GetX1(), enemyList[i].GetY1()), Color.Black);
            }

        }




        public void ReplaceList(List<IEnemy> enemyList)
        {
            this.enemyList = enemyList;


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
