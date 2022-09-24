

using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections;
using System.ComponentModel;
using System.Reflection.Metadata;
using Microsoft.Xna.Framework.Input;
using System.Runtime.InteropServices;
using static System.Formats.Asn1.AsnWriter;



namespace sprint0
{
    public interface IBlock
    {
        void BlockUpdate(GraphicsDeviceManager _graphics, GameTime gameTime);
        void BlockDraw(SpriteBatch _spriteBatch);
    }

    public abstract class Block : IBlock
    {

        protected Vector2 spritePosition;
        protected Texture2D BlockTextureSheet;


        public abstract void BlockUpdate(GraphicsDeviceManager _graphics, GameTime gameTime);
        public abstract void BlockDraw(SpriteBatch _spriteBatch);
    }


    //non-moving,non-animated sprite
    public class Block1 : Block
    {

        public Block1(Texture2D textureSheet)
        {
            spritePosition = new Vector2(100, 100);
            BlockTextureSheet = textureSheet;
        }

        public override void BlockUpdate(GraphicsDeviceManager _graphics, GameTime gameTime)
        {
            //TODO: IMPLEMENT UPDATE METHODS? MAYBE
        }


        public override void BlockDraw(SpriteBatch _spriteBatch)
        {

            _spriteBatch.Draw(
            BlockTextureSheet,
            spritePosition,
            new Rectangle(175, 50, 25, 35),
            Color.White,
            0f,
            new Vector2(25 / 2, 35 / 2),
            new Vector2(2, 2), SpriteEffects.None,
            0f
            );
        }
    }

}

