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
        private Texture2D woodenBoomerangSheet;
        private Texture2D bowSheet;
        private Texture2D heartSheet;
        private Texture2D rupeeSheet;
        private Texture2D arrowSheet;
        private Texture2D bombSheet;
        private Texture2D fairySheet;
        private Texture2D clockSheet;
        private Texture2D blueCandleSheet;
        private Texture2D bluePotionSheet;



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
            bowSheet = game.Content.Load<Texture2D>("item/ZeldaSpriteBow");
            heartSheet = game.Content.Load<Texture2D>("item/ZeldaSpriteHeart");
            rupeeSheet = game.Content.Load<Texture2D>("item/ZeldaSpriteRupy");
            arrowSheet = game.Content.Load<Texture2D>("item/ZeldaSpriteArrow");
            bombSheet = game.Content.Load<Texture2D>("item/ZeldaSpriteBomb");
            fairySheet = game.Content.Load<Texture2D>("item/ZeldaSpriteFairy");
            clockSheet = game.Content.Load<Texture2D>("item/ZeldaSpriteClock");
            blueCandleSheet = game.Content.Load<Texture2D>("item/ZeldaSpriteBlueCandle");
            bluePotionSheet = game.Content.Load<Texture2D>("item/ZeldaSpriteLifePotion");

            // More Content.Load calls follow
            //...
        }


        public IItem CreateCompass(Rectangle positionRectangle)
        {
            return new Item1(compassSheet, positionRectangle);
        }
        public IItem CreateMap(Rectangle positionRectangle)
        {
            return new Item1(mapSheet, positionRectangle);
        }
        public IItem CreateKey(Rectangle positionRectangle)
        {
            return new Item1(keySheet, positionRectangle);
        }
        public IItem CreateHeartContainer(Rectangle positionRectangle)
        {
            return new Item1(heartContainerSheet, positionRectangle);
        }
        public IItem CreateTriforcePiece(Rectangle positionRectangle)
        {
            return new Item1(triforcePieceSheet, positionRectangle);
        }
        public IItem CreateWoodenBoomerang(Rectangle positionRectangle)
        {
            return new MoveableItem(woodenBoomerangSheet, positionRectangle);
        }
        public IItem CreateBow(Rectangle positionRectangle)
        {
            return new Item1(bowSheet, positionRectangle);
        }
        public IItem CreateHeart(Rectangle positionRectangle)
        {
            return new Item1(heartSheet, positionRectangle);
        }
        public IItem Createrupee(Rectangle positionRectangle)
        {
            return new Item1(rupeeSheet, positionRectangle);
        }
        public IItem CreateArrow(Rectangle positionRectangle)
        {
            return new MoveableItem(arrowSheet, positionRectangle);
        }
        public IItem CreateBomb(Rectangle positionRectangle)
        {
            return new Item1(bombSheet, positionRectangle);
        }
        public IItem CreateFairy(Rectangle positionRectangle)
        {
            return new Item1(fairySheet, positionRectangle);
        }

        public IItem CreateClock(Rectangle positionRectangle)
        {
            return new Item1(clockSheet, positionRectangle);
        }
        public IItem CreateBlueCandle(Rectangle positionRectangle)
        {
            return new Item1(blueCandleSheet, positionRectangle);
        }
        public IItem CreateBluePotion(Rectangle positionRectangle)
        {
            return new Item1(bluePotionSheet, positionRectangle);
        }

        // More public ISprite returning methods follow
        // ...
    }
}

// Client code in main game class' LoadContent method:

//BlockFactory.Instance.LoadAllTextures(Content);

// Client code in Goomba class:

//IBlock myBlock = BlockFactory.Instance.CreateSquareBlock();