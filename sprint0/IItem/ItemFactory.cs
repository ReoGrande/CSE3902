using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;


namespace sprint0
{

    public class ItemFactory
    {
        private Texture2D compassSheet;
        private Texture2D mapSheet;
        private Texture2D keySheet;
        private Texture2D heartContainerSheet;
        private Texture2D triforcePieceSheet;
        //boomerang
        private Texture2D woodenBoomerangSheet;
        private Texture2D woodenBoomerangSheet2;
        private Texture2D woodenBoomerangSheet3;
        private Texture2D woodenBoomerangSheet4;
        private Texture2D bowSheet;
        private Texture2D heartSheet;
        private Texture2D rupeeSheet;
        //arrow
        private Texture2D arrowSheet;
        private Texture2D arrowSheet2;
        private Texture2D arrowSheet3;
        private Texture2D arrowSheet4;

        //bomb
        private Texture2D bombSheet;
        private Texture2D bombSheet2;
        private Texture2D bombSheet3;
        private Texture2D bombSheet4;
        private Texture2D blastSheet;


        //shieldBall
        private Texture2D shieldBallSheet1;
        private Texture2D shieldBallSheet2;
        private Texture2D shieldBallSheet3;
        private Texture2D shieldBallSheet4;




        private Texture2D fairySheet;
        private Texture2D clockSheet;
        private Texture2D blueCandleSheet;
        private Texture2D bluePotionSheet;


        private Texture2D fireBallSheet;
        private Texture2D staffSheet;
        //pickaxe
        private Texture2D pickaxeSheet;
        private Texture2D pickaxeSheet2;
        private Texture2D pickaxeSheet3;
        private Texture2D pickaxeSheet4;

        // More private Texture2Ds follow
        // ...

        private static ItemFactory instance = new ItemFactory();

        public static ItemFactory Instance
        {
            get
            {
                return instance;
            }
        }

        private ItemFactory()
        {
        }

        public void LoadAllTextures(Game1 game)
        {
            compassSheet = game.Content.Load<Texture2D>("item/ZeldaSpriteCompass");
            mapSheet = game.Content.Load<Texture2D>("item/ZeldaSpriteMap");
            keySheet = game.Content.Load<Texture2D>("item/ZeldaSpriteKey");
            heartContainerSheet = game.Content.Load<Texture2D>("item/ZeldaSpriteHeartContainer");
            triforcePieceSheet = game.Content.Load<Texture2D>("item/ZeldaSpriteTriforce");
            woodenBoomerangSheet = game.Content.Load<Texture2D>("item/ZeldaSpriteBoomerang");
            woodenBoomerangSheet2 = game.Content.Load<Texture2D>("item/ZeldaSpriteBoomerang2");
            woodenBoomerangSheet3 = game.Content.Load<Texture2D>("item/ZeldaSpriteBoomerang3");
            woodenBoomerangSheet4 = game.Content.Load<Texture2D>("item/ZeldaSpriteBoomerang4");

            bowSheet = game.Content.Load<Texture2D>("item/ZeldaSpriteBow");
            heartSheet = game.Content.Load<Texture2D>("item/ZeldaSpriteHeart");
            rupeeSheet = game.Content.Load<Texture2D>("item/ZeldaSpriteRupy");
            arrowSheet = game.Content.Load<Texture2D>("item/ZeldaSpriteArrow");
            arrowSheet2 = game.Content.Load<Texture2D>("item/ZeldaSpriteArrow2");
            arrowSheet3 = game.Content.Load<Texture2D>("item/ZeldaSpriteArrow3");
            arrowSheet4 = game.Content.Load<Texture2D>("item/ZeldaSpriteArrow4");


            bombSheet = game.Content.Load<Texture2D>("item/LOZ1_Bomb1");
            bombSheet2 = game.Content.Load<Texture2D>("item/LOZ1_Bomb2");
            bombSheet3 = game.Content.Load<Texture2D>("item/LOZ1_Bomb3");
            bombSheet4 = game.Content.Load<Texture2D>("item/LOZ1_Bomb4");
            blastSheet = game.Content.Load<Texture2D>("item/Blast");


            shieldBallSheet1 = game.Content.Load<Texture2D>("item/ShieldBall/1");
            shieldBallSheet2 = game.Content.Load<Texture2D>("item/ShieldBall/2");
            shieldBallSheet3 = game.Content.Load<Texture2D>("item/ShieldBall/3");
            shieldBallSheet4 = game.Content.Load<Texture2D>("item/ShieldBall/4");

            fairySheet = game.Content.Load<Texture2D>("item/ZeldaSpriteFairy");
            clockSheet = game.Content.Load<Texture2D>("item/ZeldaSpriteClock");
            blueCandleSheet = game.Content.Load<Texture2D>("item/ZeldaSpriteBlueCandle");
            bluePotionSheet = game.Content.Load<Texture2D>("item/ZeldaSpriteLifePotion");

            fireBallSheet = game.Content.Load<Texture2D>("enemy/fireball");
            staffSheet = game.Content.Load<Texture2D>("item/Staff");
            pickaxeSheet = game.Content.Load<Texture2D>("item/Pickaxe");
            pickaxeSheet2 = game.Content.Load<Texture2D>("item/Pickaxe2");
            pickaxeSheet3 = game.Content.Load<Texture2D>("item/Pickaxe3");
            pickaxeSheet4 = game.Content.Load<Texture2D>("item/Pickaxe4");
            // More Content.Load calls follow
            //...
        }


        public IItem CreateCompass(Rectangle positionRectangle)
        {
            return new Compass(compassSheet, positionRectangle);
        }
        public IItem CreateMap(Rectangle positionRectangle)
        {
            return new Map(mapSheet, positionRectangle);
        }
        public IItem CreateKey(Rectangle positionRectangle)
        {
            return new Key(keySheet, positionRectangle);
        }
        public IItem CreateHeartContainer(Rectangle positionRectangle)
        {
            return new StaticItem(heartContainerSheet, positionRectangle);
        }
        public IItem CreateTriforcePiece(Rectangle positionRectangle)
        {
            return new TriForcePiece(triforcePieceSheet, positionRectangle);
        }
        public IItem CreateWoodenBoomerang(Rectangle positionRectangle)
        {
            Boomerang boomerang = new Boomerang(woodenBoomerangSheet, positionRectangle);
            boomerang.AddFrames(woodenBoomerangSheet2);
            boomerang.AddFrames(woodenBoomerangSheet3);
            boomerang.AddFrames(woodenBoomerangSheet4);
            return boomerang;
        }
        public IItem CreateBow(Rectangle positionRectangle)
        {
            return new StaticItem(bowSheet, positionRectangle);
        }
        public IItem CreateHeart(Rectangle positionRectangle)
        {
            return new Heart(heartSheet, positionRectangle);
        }
        public IItem Createrupee(Rectangle positionRectangle)
        {
            return new StaticItem(rupeeSheet, positionRectangle);
        }
        public IItem CreateArrow(Rectangle positionRectangle)
        {
            Arrow arrow = new Arrow(arrowSheet, positionRectangle);
            arrow.AddFrames(arrowSheet2);
            arrow.AddFrames(arrowSheet3);
            arrow.AddFrames(arrowSheet4);
            return arrow;

        }
        public IItem CreateBomb(Rectangle positionRectangle)
        {
            Bomb Bomb = new Bomb(bombSheet, positionRectangle);
            Bomb.AddFrames(bombSheet2);
            Bomb.AddFrames(bombSheet3);
            Bomb.AddFrames(bombSheet4);
            Bomb.AddBlastSheet(blastSheet);
            return Bomb;
        }
        public IItem CreateShieldBall(Rectangle positionRectangle)
        {
            ShieldBall sheildBall = new ShieldBall(shieldBallSheet1, positionRectangle);
            sheildBall.AddFrames(shieldBallSheet2);
            sheildBall.AddFrames(shieldBallSheet3);
            sheildBall.AddFrames(shieldBallSheet4);

            return sheildBall;
        }



        public IItem CreateFairy(Rectangle positionRectangle)
        {
            return new Fairy(fairySheet, positionRectangle);
        }

        public IItem CreateClock(Rectangle positionRectangle)
        {
            return new Clock(clockSheet, positionRectangle);
        }
        public IItem CreateBlueCandle(Rectangle positionRectangle)
        {
            return new StaticItem(blueCandleSheet, positionRectangle);
        }
        public IItem CreateBluePotion(Rectangle positionRectangle)
        {
            return new StaticItem(bluePotionSheet, positionRectangle);
        }

        public IItem CreateFireBall(Rectangle positionRectangle)
        {
            return new MoveableItem(fireBallSheet, positionRectangle);
        }

        public IItem CreateBlast(Rectangle positionRectangle)
        {
            return new StaticItem(blastSheet, positionRectangle);
        }
        public IItem CreateStaff(Rectangle positionRectangle)
        {
            return new Staff(staffSheet, positionRectangle);
        }
        public IItem CreatePickaxe(Rectangle positionRectangle)
        {
            Pickaxe pickaxe = new Pickaxe(pickaxeSheet, positionRectangle);

            pickaxe.AddFrames(pickaxeSheet2);
            pickaxe.AddFrames(pickaxeSheet3);
            pickaxe.AddFrames(pickaxeSheet4);
            return pickaxe;
        }
        // More public ISprite returning methods follow
        // ...
    }
}

// Client code in main game class' LoadContent method:

//BlockFactory.Instance.LoadAllTextures(Content);

// Client code in Goomba class:

//IBlock myBlock = BlockFactory.Instance.CreateSquareBlock();