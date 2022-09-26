

using System;


namespace sprint0
{
    public interface IBlock
    {
        void BlockUpdate(GraphicsDeviceManager _graphics, GameTime gameTime);
        void BlockDraw(SpriteBatch _spriteBatch);
    }

    public abstract class Block : IBlock
    {

        protected Rectangle positionRectangle;
        protected Texture2D BlockTextureSheet;


        public abstract void BlockUpdate(GraphicsDeviceManager _graphics, GameTime gameTime);
        public abstract void BlockDraw(SpriteBatch _spriteBatch);
    }


    //non-moving,non-animated sprite
    public class Block1 : Block
    {

        protected Rectangle rangeInSheet;


        public Block1(Texture2D textureSheet, Rectangle positionRectangle)
        {
            BlockTextureSheet = textureSheet;
            this.positionRectangle = positionRectangle;
            this.rangeInSheet = new Rectangle(0, 0, textureSheet.Width, textureSheet.Height);


        }



        public Block1(Texture2D textureSheet, Rectangle positionRectangle, Rectangle rangeInSheet)
        {
            BlockTextureSheet = textureSheet;
            this.rangeInSheet = rangeInSheet;
            this.positionRectangle = positionRectangle;

        }

        public override void BlockUpdate(GraphicsDeviceManager _graphics, GameTime gameTime)
        {
            //TODO: IMPLEMENT UPDATE METHODS? MAYBE
        }


        public override void BlockDraw(SpriteBatch _spriteBatch)
        {

            _spriteBatch.Draw(
            BlockTextureSheet,
            positionRectangle,
            rangeInSheet,
            Color.White
            );


        }
    }

}

