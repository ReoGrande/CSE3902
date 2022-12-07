

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
using Microsoft.Xna.Framework.Media;

namespace sprint0
{
    public class ManuButton : Button
    {





        public ManuButton(Rectangle positionRectangle, Rectangle rangeInSheet)
        {
            this.positionRectangle = positionRectangle;
            this.rangeInSheet = rangeInSheet;

            timeCount = 0;

        }
        public override void LoadContent(Game1 game)
        {

            currentTexture = game.Content.Load<Texture2D>("button/Manu"); ;
        }
        public override void Process(Game1 game)
        {


            IGameState gameState = game.gameState;




            gameState = game.gameState;
            if (!game.isPaused)
            {
                gameState.Pause();
                game.isPaused = true;
                MediaPlayer.Pause();
            }
            else
            {
                gameState.Play();
                game.isPaused = false;
                MediaPlayer.Resume();

            }


        }


    }
}
