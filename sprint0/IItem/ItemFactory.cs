


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

        public void LoadAllTextures(ContentManager content)
        {
            compassSheet = content.Load<Texture2D>("item/ZeldaSpriteCompass");
            mapSheet = content.Load<Texture2D>("item/ZeldaSpriteMap");
            keySheet = content.Load<Texture2D>("item/ZeldaSpriteKey");
            heartContainerSheet = content.Load<Texture2D>("item/ZeldaSpriteHeartContainer");
            triforcePieceSheet = content.Load<Texture2D>("item/ZeldaSpriteTriforce");
            woodenBoomerangSheet = content.Load<Texture2D>("item/ZeldaSpriteBoomerang");
            bowSheet = content.Load<Texture2D>("item/ZeldaSpriteBow");
            heartSheet = content.Load<Texture2D>("item/ZeldaSpriteHeart");
            rupeeSheet = content.Load<Texture2D>("item/ZeldaSpriteRupy");
            arrowSheet = content.Load<Texture2D>("item/ZeldaSpriteArrow");
            bombSheet = content.Load<Texture2D>("item/ZeldaSpriteBomb");
            fairySheet = content.Load<Texture2D>("item/ZeldaSpriteFairy");
            clockSheet = content.Load<Texture2D>("item/ZeldaSpriteClock");
            blueCandleSheet = content.Load<Texture2D>("item/ZeldaSpriteBlueCandle");
            bluePotionSheet = content.Load<Texture2D>("item/ZeldaSpriteLifePotion");

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
            return new Item1(woodenBoomerangSheet, positionRectangle);
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
            return new Item1(arrowSheet, positionRectangle);
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