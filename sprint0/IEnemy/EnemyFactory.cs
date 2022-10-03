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
        private Texture2D skeletonSheet1;
        private Texture2D skeletonSheet2;
        private Texture2D bossLeftSheet1;
        private Texture2D bossLeftSheet2;


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
            skeletonSheet1 = game.Content.Load<Texture2D>("enemy/ZeldaSpriteStalfos/ZeldaSpriteStalfos1");
            skeletonSheet2 = game.Content.Load<Texture2D>("enemy/ZeldaSpriteStalfos/ZeldaSpriteStalfos2");
            bossLeftSheet1 = game.Content.Load<Texture2D>("enemy/ZeldaSpriteAquamentus/ZeldaSpriteAquamentusLeft1");
            bossLeftSheet2 = game.Content.Load<Texture2D>("enemy/ZeldaSpriteAquamentus/ZeldaSpriteAquamentusLeft2");
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
            AnimatedEnemy skeleton = new AnimatedEnemy(skeletonSheet1, positionRectangle);
            skeleton.AddFrames(skeletonSheet2);
            return skeleton;
        }
        public IEnemy CreateBoss(Rectangle positionRectangle)
        {
            AnimatedEnemy boss = new AnimatedEnemy(bossLeftSheet1, positionRectangle);
            boss.AddFrames(bossLeftSheet2);
            return boss;
        }

    }


    // More public ISprite returning methods follow
    // ...
}
