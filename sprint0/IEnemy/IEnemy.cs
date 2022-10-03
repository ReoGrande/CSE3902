
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
        void EnemyUpdate();
        void EnemyDraw(SpriteBatch _spriteBatch);
    }

    public abstract class Enemy : IEnemy
    {

        protected Rectangle positionRectangle;
        protected Texture2D EnemyTextureSheet;
        public Direction direction;


        public abstract void EnemyUpdate();
        public abstract void EnemyDraw(SpriteBatch _spriteBatch);
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

        public override void EnemyUpdate()
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
