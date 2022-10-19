﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using static sprint0.Link;


namespace sprint0
{


    //non-moving,non-animated sprite
    public class StaticEnemy : Enemy
    {

        protected Rectangle rangeInSheet;


        public StaticEnemy(Texture2D textureSheet, Rectangle positionRectangle)
        {
            EnemyTextureSheet = textureSheet;
            this.positionRectangle = positionRectangle;
            this.rangeInSheet = new Rectangle(0, 0, textureSheet.Width, textureSheet.Height);
            color = Color.White;
            state = new NomalState(this);
            timer = 0;
            damageTimer = 0;


        }


        public StaticEnemy(Texture2D textureSheet, Rectangle positionRectangle, Rectangle rangeInSheet)
        {
            EnemyTextureSheet = textureSheet;
            this.rangeInSheet = rangeInSheet;
            this.positionRectangle = positionRectangle;
            color = Color.White;

        }


        public override void EnemyUpdate(Game1 game)
        {

            state.Update();

        }
        public override void EnemyDraw(SpriteBatch _spriteBatch)
        {

            _spriteBatch.Draw(
            EnemyTextureSheet,
            positionRectangle,
            rangeInSheet,
            color
            );


        }

        public override void BlockDirection()
        {

        }




    }

}