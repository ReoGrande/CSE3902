

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
    public interface IItem
    {
        void ItemUpdate(GraphicsDeviceManager _graphics, GameTime gameTime);
        void ItemDraw(SpriteBatch _spriteBatch);
    }

    public abstract class Item : IItem
    {

        protected Rectangle positionRectangle;
        protected Texture2D ItemTextureSheet;


        public abstract void ItemUpdate(GraphicsDeviceManager _graphics, GameTime gameTime);
        public abstract void ItemDraw(SpriteBatch _spriteBatch);
    }


    //non-moving,non-animated sprite
    public class Item1 : Item
    {

        protected Rectangle rangeInSheet;


        public Item1(Texture2D textureSheet, Rectangle positionRectangle)
        {
            ItemTextureSheet = textureSheet;
            this.positionRectangle = positionRectangle;
            this.rangeInSheet = new Rectangle(0, 0, textureSheet.Width, textureSheet.Height);


        }



        public Item1(Texture2D textureSheet, Rectangle positionRectangle, Rectangle rangeInSheet)
        {
            ItemTextureSheet = textureSheet;
            this.rangeInSheet = rangeInSheet;
            this.positionRectangle = positionRectangle;

        }

        public override void ItemUpdate(GraphicsDeviceManager _graphics, GameTime gameTime)
        {
            //TODO: IMPLEMENT UPDATE METHODS? MAYBE
        }


        public override void ItemDraw(SpriteBatch _spriteBatch)
        {

            _spriteBatch.Draw(
            ItemTextureSheet,
            positionRectangle,
            rangeInSheet,
            Color.White
            );


        }
    }

}