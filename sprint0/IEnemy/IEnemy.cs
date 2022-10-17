
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
    public interface IEnemy
    {
        void EnemyUpdate(Game1 game);
        void EnemyDraw(SpriteBatch _spriteBatch);


        int getX1();
        int getX2();
        int getY1();
        int getY2();

    }

    public abstract class Enemy : IEnemy
    {

        protected Rectangle positionRectangle;
        protected Texture2D EnemyTextureSheet;
        public Direction direction;


        public abstract void EnemyUpdate(Game1 game);
        public abstract void EnemyDraw(SpriteBatch _spriteBatch);

        public int getX1() { return positionRectangle.X; }
        public int getX2() { return positionRectangle.X + positionRectangle.Width; }
        public int getY1() { return positionRectangle.Y; }
        public int getY2() { return positionRectangle.Y + positionRectangle.Height; }


    }


    //non-moving,non-animated sprite
    public class Enemy1 : Enemy
    {

        protected Rectangle rangeInSheet;


        public Enemy1(Texture2D textureSheet, Rectangle positionRectangle)
        {
            EnemyTextureSheet = textureSheet;
            this.positionRectangle = positionRectangle;
            this.rangeInSheet = new Rectangle(0, 0, textureSheet.Width, textureSheet.Height);


        }



        public Enemy1(Texture2D textureSheet, Rectangle positionRectangle, Rectangle rangeInSheet)
        {
            EnemyTextureSheet = textureSheet;
            this.rangeInSheet = rangeInSheet;
            this.positionRectangle = positionRectangle;

        }

        public override void EnemyUpdate(Game1 game)
        {
            //TODO: IMPLEMENT UPDATE METHODS? MAYBE
        }


        public override void EnemyDraw(SpriteBatch _spriteBatch)
        {

            _spriteBatch.Draw(
            EnemyTextureSheet,
            positionRectangle,
            rangeInSheet,
            Color.White
            );


        }
    }

}
