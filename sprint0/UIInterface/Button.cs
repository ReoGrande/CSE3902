

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
using System.Reflection.Metadata.Ecma335;

namespace sprint0
{
    public interface IButton
    {
        void ButtonUpdate();
        void ButtonDraw(SpriteBatch _spriteBatch);
        void LoadContent(Game1 game);


    }

    public abstract class Button : IButton
    {

        protected Rectangle positionRectangle;
        protected Texture2D currentTexture;
        protected Color color;
        public List<Texture2D> textureSheetList;
        protected int timeCount;

        public void ButtonUpdate()

        {
            MouseState mouseState = Mouse.GetState();

            if (mouseState.LeftButton == ButtonState.Pressed)
            {
                if (positionRectangle.Contains(mouseState.X, mouseState.Y) && timeCount > 6)
                {
                    //process when clicking button
                    Process();
                    //
                    timeCount = 0;
                }
            }
            else
            {
                if (positionRectangle.Contains(mouseState.X, mouseState.Y))
                {
                    color = Color.Gray;
                }
                else
                {
                    color = Color.White;

                }

            }
            timeCount++;
        }

        public abstract void Process();
        public abstract void LoadContent(Game1 game);

        public void ButtonDraw(SpriteBatch _spriteBatch)
        {

            _spriteBatch.Draw(
            currentTexture,
            positionRectangle,
            Color.White
            );
        }




    }




}

