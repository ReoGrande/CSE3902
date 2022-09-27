

using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections;
using System.ComponentModel;
using System.Reflection.Metadata;
using Microsoft.Xna.Framework.Input;
using System.Runtime.InteropServices;
using static System.Formats.Asn1.AsnWriter;
using static sprint0.Link;

namespace sprint0
{
    public interface IItem
    {


        void Update(int x, int y);// update accourding to input position 

        void ToMoving();
        void ItemDraw(SpriteBatch _spriteBatch);
        void ChangeDirection(Direction direction);
    }

    public abstract class Item : IItem
    {
        public bool moveable;
        public Rectangle positionRectangle;
        public Texture2D ItemTextureSheet;
        protected Rectangle rangeInSheet;

        // Directions in which the Item is moving
        public Direction direction;



        public abstract void ToMoving();
        public abstract void Update(int x, int y);
        public abstract void ItemDraw(SpriteBatch _spriteBatch);
        public void ChangeDirection(Direction direction)
        {
            this.direction = direction;
        }

    }



    public class StaticItem : Item
    {




        public StaticItem()
        {
            moveable = false;
        }



        public StaticItem(Texture2D textureSheet, Rectangle positionRectangle)
        {
            ItemTextureSheet = textureSheet;
            moveable = false;
            this.positionRectangle = positionRectangle;
            this.rangeInSheet = new Rectangle(0, 0, textureSheet.Width, textureSheet.Height);
        }

        public StaticItem(Texture2D textureSheet, Rectangle positionRectangle, Rectangle rangeInSheet)
        {
            moveable = false;
            ItemTextureSheet = textureSheet;
            this.rangeInSheet = rangeInSheet;
            this.positionRectangle = positionRectangle;
        }

        public override void Update(int x, int y)
        {
            positionRectangle.X = x;
            positionRectangle.Y = y;


        }

        public override void ToMoving()
        {
            //not move for this kind of item
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