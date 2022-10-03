using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;


namespace sprint0
{

    public class EnemyFactory
    {
        private Texture2D batSheet1;
        private Texture2D batSheet2;
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
            batSheet1 = game.Content.Load<Texture2D>("enemy/ZeldaSpriteKeeseBlue/ZeldaSpriteKeeseBlue1");
            batSheet2 = game.Content.Load<Texture2D>("enemy/ZeldaSpriteKeeseBlue/ZeldaSpriteKeeseBlue2");
            skeletonSheet = game.Content.Load<Texture2D>("enemy/ZeldaSpriteStalfos/ZeldaSpriteStalfos1");
            bossLeftSheet = game.Content.Load<Texture2D>("enemy/ZeldaSpriteAquamentus/ZeldaSpriteAquamentusLeft1");
            // More Content.Load calls follow
            //...
        }


        public IEnemy CreateBat(Rectangle positionRectangle)
        {
            AnimatedEnemy bat = new AnimatedEnemy(batSheet1, positionRectangle);
            bat.AddFrames(batSheet2);
            return bat;
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
