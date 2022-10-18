

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

        int GetX1();
        int GetX2();
        int GetY1();
        int GetY2();

        void CollisionWithItem(IItem item);
        void CollisionWithEnemy(IEnemy enemy);
    }

    public abstract class Block : IBlock
    {

        protected Rectangle positionRectangle;
        protected Texture2D BlockTextureSheet;



        public abstract void BlockUpdate(GraphicsDeviceManager _graphics, GameTime gameTime);
        public abstract void BlockDraw(SpriteBatch _spriteBatch);
        public int GetX1() { return positionRectangle.X; }
        public int GetX2() { return positionRectangle.X + positionRectangle.Width; }
        public int GetY1() { return positionRectangle.Y; }
        public int GetY2() { return positionRectangle.Y + positionRectangle.Height; }

        public void CollisionWithItem(IItem item) { item.Damage(); }

        public void CollisionWithEnemy(IEnemy enemy)
        { //TBD:May change latter
            enemy.GetDamaged();

        }

    }
    //non-moving,non-animated sprite
    public class StaticBlock : Block
    {

        protected Rectangle rangeInSheet;


        public StaticBlock(Texture2D textureSheet, Rectangle positionRectangle)
        {
            BlockTextureSheet = textureSheet;
            this.positionRectangle = positionRectangle;
            this.rangeInSheet = new Rectangle(0, 0, textureSheet.Width, textureSheet.Height);


        }



        public StaticBlock(Texture2D textureSheet, Rectangle positionRectangle, Rectangle rangeInSheet)
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
