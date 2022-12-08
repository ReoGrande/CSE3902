using System.Diagnostics;
using System.Runtime.CompilerServices;
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

        private Texture2D ropeLeftSheet1;
        private Texture2D ropeLeftSheet2;
        private Texture2D ropeRightSheet1;
        private Texture2D ropeRightSheet2;

        private Texture2D wallMasterURSheet1;
        private Texture2D wallMasterURSheet2;


        private Texture2D trapSheet1;

        private Texture2D deathSheet1;
        private Texture2D deathSheet2;
        private Texture2D deathSheet3;



        private Texture2D goriyaBlueLeftSheet1;
        private Texture2D goriyaBlueLeftSheet2;
        private Texture2D goriyaBlueRightSheet1;
        private Texture2D goriyaBlueRightSheet2;
        private Texture2D goriyaBlueFrontSheet1;
        private Texture2D goriyaBlueFrontSheet2;
        private Texture2D goriyaBlueBackSheet1;
        private Texture2D goriyaBlueBackSheet2;




        private Texture2D goriyaRedLeftSheet1;
        private Texture2D goriyaRedLeftSheet2;
        private Texture2D goriyaRedRightSheet1;
        private Texture2D goriyaRedRightSheet2;
        private Texture2D goriyaRedFrontSheet1;
        private Texture2D goriyaRedFrontSheet2;
        private Texture2D goriyaRedBackSheet1;
        private Texture2D goriyaRedBackSheet2;



        private Texture2D ghostSheet1;
        private Texture2D ghostSheet2;


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

            ropeLeftSheet1 = game.Content.Load<Texture2D>("enemy/ZeldaSpriteRope/ZeldaSpriteRopeLeft1");
            ropeLeftSheet2 = game.Content.Load<Texture2D>("enemy/ZeldaSpriteRope/ZeldaSpriteRopeLeft2");
            ropeRightSheet1 = game.Content.Load<Texture2D>("enemy/ZeldaSpriteRope/ZeldaSpriteRopeRight1");
            ropeRightSheet2 = game.Content.Load<Texture2D>("enemy/ZeldaSpriteRope/ZeldaSpriteRopeRight2");

            wallMasterURSheet1 = game.Content.Load<Texture2D>("enemy/ZeldaSpriteWallmaster/ZeldaSpriteWallMasterUR1");
            wallMasterURSheet2 = game.Content.Load<Texture2D>("enemy/ZeldaSpriteWallmaster/ZeldaSpriteWallMasterUR2");

            trapSheet1 = game.Content.Load<Texture2D>("enemy/ZeldaSpriteTrap");

            deathSheet1 = game.Content.Load<Texture2D>("enemy/EnemyDeath/EnemyDeath1");
            deathSheet2 = game.Content.Load<Texture2D>("enemy/EnemyDeath/EnemyDeath2");
            deathSheet3 = game.Content.Load<Texture2D>("enemy/EnemyDeath/EnemyDeath3");


            goriyaBlueLeftSheet1 = game.Content.Load<Texture2D>("enemy/ZeldaSpriteGoriyaBlue/ZeldaSpriteGoriyaBlueLeft1");
            goriyaBlueLeftSheet2 = game.Content.Load<Texture2D>("enemy/ZeldaSpriteGoriyaBlue/ZeldaSpriteGoriyaBlueLeft2");
            goriyaBlueRightSheet1 = game.Content.Load<Texture2D>("enemy/ZeldaSpriteGoriyaBlue/ZeldaSpriteGoriyaBlueRight1");
            goriyaBlueRightSheet2 = game.Content.Load<Texture2D>("enemy/ZeldaSpriteGoriyaBlue/ZeldaSpriteGoriyaBlueRight2");
            goriyaBlueFrontSheet1 = game.Content.Load<Texture2D>("enemy/ZeldaSpriteGoriyaBlue/ZeldaSpriteGoriyaBlueFront1");
            goriyaBlueFrontSheet2 = game.Content.Load<Texture2D>("enemy/ZeldaSpriteGoriyaBlue/ZeldaSpriteGoriyaBlueFront2");
            goriyaBlueBackSheet1 = game.Content.Load<Texture2D>("enemy/ZeldaSpriteGoriyaBlue/ZeldaSpriteGoriyaBlueBack1");
            goriyaBlueBackSheet2 = game.Content.Load<Texture2D>("enemy/ZeldaSpriteGoriyaBlue/ZeldaSpriteGoriyaBlueBack2");

            goriyaRedLeftSheet1 = game.Content.Load<Texture2D>("enemy/GoriyaRed/ZeldaSpriteGoriyaRedLeft1");
            goriyaRedLeftSheet2 = game.Content.Load<Texture2D>("enemy/GoriyaRed/ZeldaSpriteGoriyaRedLeft2");
            goriyaRedRightSheet1 = game.Content.Load<Texture2D>("enemy/GoriyaRed/ZeldaSpriteGoriyaRedRight1");
            goriyaRedRightSheet2 = game.Content.Load<Texture2D>("enemy/GoriyaRed/ZeldaSpriteGoriyaRedRight2");
            goriyaRedFrontSheet1 = game.Content.Load<Texture2D>("enemy/GoriyaRed/ZeldaSpriteGoriyaRedFront1");
            goriyaRedFrontSheet2 = game.Content.Load<Texture2D>("enemy/GoriyaRed/ZeldaSpriteGoriyaRedFront2");
            goriyaRedBackSheet1 = game.Content.Load<Texture2D>("enemy/GoriyaRed/ZeldaSpriteGoriyaRedBack1");
            goriyaRedBackSheet2 = game.Content.Load<Texture2D>("enemy/GoriyaRed/ZeldaSpriteGoriyaRedBack2");





            ghostSheet1 = game.Content.Load<Texture2D>("enemy/Ghost/TLoZ_Gibdo_Sprite1"); ;
            ghostSheet2 = game.Content.Load<Texture2D>("enemy/Ghost/TLoZ_Gibdo_Sprite2"); ;
            // More Content.Load calls follow
            //...
        }


        public IEnemy CreateBat(Rectangle positionRectangle)
        {
            MovingAnimatedEnemy bat = new MovingAnimatedEnemy(batSheet1, positionRectangle);
            bat.AddFrames(batSheet2);
            return bat;
        }

        public IEnemy CreateSkeleton(Rectangle positionRectangle)
        {
            MovingAnimatedEnemy skeleton = new MovingAnimatedEnemy(skeletonSheet1, positionRectangle);
            skeleton.AddFrames(skeletonSheet2);
            return skeleton;
        }
        public IEnemy CreateBoss(Rectangle positionRectangle)
        {
            Boss boss = new Boss(bossLeftSheet1, positionRectangle);
            boss.AddFrames(bossLeftSheet2);
            return boss;
        }

        public IEnemy CreateRope(Rectangle positionRectangle)
        {
            Rope rope = new Rope(ropeLeftSheet1, positionRectangle);

            rope.AddFrames(ropeLeftSheet2);
            rope.AddFrames(ropeRightSheet1);
            rope.AddFrames(ropeRightSheet2);
            return rope;
        }
        public IEnemy CreateWallMaster(Rectangle positionRectangle)
        {
            AnimatedEnemy wallMaster = new AnimatedEnemy(wallMasterURSheet1, positionRectangle);
            wallMaster.AddFrames(wallMasterURSheet2);

            return wallMaster;
        }

        public IEnemy CreateTrap(Rectangle positionRectangle)
        {

            return new AnimatedEnemy(trapSheet1, positionRectangle);
        }

        public IEnemy CreateDeathCloud(Rectangle positionRectangle)
        {

            DeathCloud deathCloud = new DeathCloud(deathSheet1, positionRectangle);
            deathCloud.AddFrames(deathSheet2);
            deathCloud.AddFrames(deathSheet3);

            return deathCloud;
        }


        public IEnemy CreateGoriyaBlue(Rectangle positionRectangle)
        {
            GoriyaBlue goriyaBlue = new GoriyaBlue(goriyaBlueLeftSheet1, positionRectangle);
            goriyaBlue.AddFrames(goriyaBlueLeftSheet2);
            goriyaBlue.AddFrames(goriyaBlueRightSheet1);
            goriyaBlue.AddFrames(goriyaBlueRightSheet2);
            goriyaBlue.AddFrames(goriyaBlueFrontSheet1);
            goriyaBlue.AddFrames(goriyaBlueFrontSheet2);
            goriyaBlue.AddFrames(goriyaBlueBackSheet1);
            goriyaBlue.AddFrames(goriyaBlueBackSheet2);
            return goriyaBlue;
        }

        public IEnemy CreateGoriyaRed(Rectangle positionRectangle)
        {
            GoriyaRed goriyaRed = new GoriyaRed(goriyaRedLeftSheet1, positionRectangle);
            goriyaRed.AddFrames(goriyaRedLeftSheet2);
            goriyaRed.AddFrames(goriyaRedRightSheet1);
            goriyaRed.AddFrames(goriyaRedRightSheet2);
            goriyaRed.AddFrames(goriyaRedFrontSheet1);
            goriyaRed.AddFrames(goriyaRedFrontSheet2);
            goriyaRed.AddFrames(goriyaRedBackSheet1);
            goriyaRed.AddFrames(goriyaRedBackSheet2);
            return goriyaRed;
        }



        public IEnemy CreateGhost(Rectangle positionRectangle)
        {
            Ghost ghost = new Ghost(ghostSheet1, positionRectangle);
            ghost.AddFrames(ghostSheet2);
            return ghost;
        }

    }


    // More public ISprite returning methods follow
    // ...
}
