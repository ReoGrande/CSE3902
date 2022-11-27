using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;


namespace sprint0
{

    public class BlockFactory
    {
        private Texture2D tileSheet;
        private Texture2D fireSheet;
        private Texture2D ladderSheet;
        private Texture2D riverSheet;


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

        public void LoadAllTextures(Game1 game)
        {
            tileSheet = game.Content.Load<Texture2D>("environment/tiles1");
            fireSheet = game.Content.Load<Texture2D>("environment/ZeldaSpriteFire");
            ladderSheet = game.Content.Load<Texture2D>("environment/ZeldaSpriteStepladder");
            riverSheet = game.Content.Load<Texture2D>("environment/River");
            // More Content.Load calls follow
            //...
        }

        public IBlock CreateSquareBlock(Rectangle positionRectangle)
        {
            return new StaticBlock(tileSheet, positionRectangle, new Rectangle(62, 32, 346, 346));
        }

        public IBlock CreatePushAbleBlock(Rectangle positionRectangle)
        {
            return new StaticBlock(tileSheet, positionRectangle, new Rectangle(432, 32, 346, 346));
        }

        public IBlock CreateFire(Rectangle positionRectangle)
        {
            return new StaticBlock(fireSheet, positionRectangle);
        }

        public IBlock CreateLadder(Rectangle positionRectangle)
        {
            return new StaticBlock(ladderSheet, positionRectangle);
        }

        public IBlock CreateRiver(Rectangle positionRectangle)
        {
            return new StaticBlock(riverSheet, positionRectangle);
        }



        public IBlock CreateBlueGap(Rectangle positionRectangle)
        {
            return new StaticBlock(tileSheet, positionRectangle, new Rectangle(67, 403, 346, 346));
        }


        public IBlock CreateStairs(Rectangle positionRectangle)
        {
            return new StaticBlock(tileSheet, positionRectangle, new Rectangle(1116, 403, 346, 346));
        }
        public IBlock CreateWhiteBrick(Rectangle positionRectangle)
        {
            return new StaticBlock(tileSheet, positionRectangle, new Rectangle(62, 770, 346, 346));
        }

        public IBlock CreateBlueFloor(Rectangle positionRectangle)
        {
            return new StaticBlock(tileSheet, positionRectangle, new Rectangle(804, 403, 346, 346));
        }

        public IBlock CreateBlueSand(Rectangle positionRectangle)
        {
            return new StaticBlock(tileSheet, positionRectangle, new Rectangle(424, 403, 346, 346));
        }

        public IBlock CreateDestroyableBlock(Rectangle positionRectangle)
        {
            return new DestroyableBlock(tileSheet, positionRectangle, new Rectangle(432, 32, 346, 346));
        }



        // More public ISprite returning methods follow
        // ...
    }
}

// Client code in main game class' LoadContent method:

//BlockFactory.Instance.LoadAllTextures(Content);

// Client code in Goomba class:

//IBlock myBlock = BlockFactory.Instance.CreateSquareBlock();