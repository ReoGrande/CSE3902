

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
        void Update();
        void ToMoving();
        void ItemDraw(SpriteBatch _spriteBatch);
    }

    public abstract class Item : IItem
    {
        public bool moveable;
        public Rectangle positionRectangle;
        protected Texture2D ItemTextureSheet;
        protected Rectangle rangeInSheet;

        public abstract void ToMoving();
        public abstract void Update();
        public abstract void ItemDraw(SpriteBatch _spriteBatch);
    }



    public class Item1 : Item
    {

        
        

         public Item1()
        {
            moveable=false;
        }



        public Item1(Texture2D textureSheet, Rectangle positionRectangle)
        {
            ItemTextureSheet = textureSheet;
            moveable=false;
            this.positionRectangle = positionRectangle;
            this.rangeInSheet = new Rectangle(0, 0, textureSheet.Width, textureSheet.Height);
        }

        public Item1(Texture2D textureSheet, Rectangle positionRectangle, Rectangle rangeInSheet)
        {
            moveable=false;
            ItemTextureSheet = textureSheet;
            this.rangeInSheet = rangeInSheet;
            this.positionRectangle = positionRectangle;
        }

        public override void Update()
        {
            
            //TODO: IMPLEMENT UPDATE METHODS? MAYBE
        }

        public override void ToMoving()
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