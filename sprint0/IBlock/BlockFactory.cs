


namespace sprint0
{

    public class BlockFactory
    {
        private Texture2D tileSheet;
        private Texture2D fireSheet;
        private Texture2D ladderSheet;


        // More private Texture2Ds follow
        // ...

        private static BlockFactory instance = new BlockFactory();

        public static BlockFactory Instance
        {
            get
            {
                return instance;
            }
        }

        private BlockFactory()
        {
        }

        public void LoadAllTextures(ContentManager content)
        {
            tileSheet = content.Load<Texture2D>("environment/tiles1");
            fireSheet = content.Load<Texture2D>("environment/ZeldaSpriteFire");
            ladderSheet = content.Load<Texture2D>("environment/ZeldaSpriteStepladder");
            // More Content.Load calls follow
            //...
        }

        public IBlock CreateSquareBlock(Rectangle positionRectangle)
        {
            return new Block1(tileSheet, positionRectangle, new Rectangle(62, 32, 346, 346));
        }

        public IBlock CreatePushAbleBlock(Rectangle positionRectangle)
        {
            return new Block1(tileSheet, positionRectangle, new Rectangle(432, 32, 346, 346));
        }

        public IBlock CreateFire(Rectangle positionRectangle)
        {
            return new Block1(fireSheet, positionRectangle);
        }

        public IBlock CreateLadder(Rectangle positionRectangle)
        {
            return new Block1(ladderSheet, positionRectangle);
        }


        public IBlock CreateBlueGap(Rectangle positionRectangle)
        {
            return new Block1(tileSheet, positionRectangle, new Rectangle(67, 403, 346, 346));
        }


        public IBlock CreateStairs(Rectangle positionRectangle)
        {
            return new Block1(tileSheet, positionRectangle, new Rectangle(1116, 403, 346, 346));
        }
        public IBlock CreateWhiteBrick(Rectangle positionRectangle)
        {
            return new Block1(tileSheet, positionRectangle, new Rectangle(62, 770, 346, 346));
        }

        public IBlock CreateBlueFloor(Rectangle positionRectangle)
        {
            return new Block1(tileSheet, positionRectangle, new Rectangle(804, 403, 346, 346));
        }

        public IBlock CreateBlueSand(Rectangle positionRectangle)
        {
            return new Block1(tileSheet, positionRectangle, new Rectangle(424, 403, 346, 346));
        }


        // More public ISprite returning methods follow
        // ...
    }
}

// Client code in main game class' LoadContent method:

//BlockFactory.Instance.LoadAllTextures(Content);

// Client code in Goomba class:

//IBlock myBlock = BlockFactory.Instance.CreateSquareBlock();