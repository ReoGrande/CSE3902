


namespace sprint0
{

    public class BlockFactory
    {
        private Texture2D SquareBlockSheet;
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
            SquareBlockSheet = content.Load<Texture2D>("smb_mario_sheet");
            // More Content.Load calls follow
            //...
        }

        public IBlock CreateSquareBlock()
        {
            return new Block1(SquareBlockSheet);
        }





        // More public ISprite returning methods follow
        // ...
    }
}

// Client code in main game class' LoadContent method:

//BlockFactory.Instance.LoadAllTextures(Content);

// Client code in Goomba class:

//IBlock myBlock = BlockFactory.Instance.CreateSquareBlock();