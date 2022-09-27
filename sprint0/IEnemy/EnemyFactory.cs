using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;


namespace sprint0
{

    public class EnemyFactory
    {
        private Texture2D batSheet;
        private Texture2D skeletonSheet;
        private Texture2D bossLeftSheet;


        // More private Texture2Ds follow
        // ...

        private static EnemyFactory instance = new EnemyFactory();

        public static EnemyFactory Instance
        {
            get
            {
                return instance;
            }
        }

        private EnemyFactory()
        {
        }

        public void LoadAllTextures(Game1 game)
        {
            batSheet = game.Content.Load<Texture2D>("enemy/ZeldaSpriteKeeseBlue");
            skeletonSheet = game.Content.Load<Texture2D>("enemy/ZeldaSpriteStalfos");
            bossLeftSheet = game.Content.Load<Texture2D>("enemy/ZeldaSpriteAquamentusLeft");
            // More Content.Load calls follow
            //...
        }


        public IEnemy CreateBat(Rectangle positionRectangle)
        {
            return new Enemy1(batSheet, positionRectangle);
        }

        public IEnemy CreateSkeleton(Rectangle positionRectangle)
        {
            return new Enemy1(skeletonSheet, positionRectangle);
        }
        public IEnemy CreateBoss(Rectangle positionRectangle)
        {
            return new Enemy1(bossLeftSheet, positionRectangle);
        }

    }


    // More public ISprite returning methods follow
    // ...
}
