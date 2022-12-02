

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
using Microsoft.Xna.Framework.Media;

namespace sprint0
{
    public class MusicButton : Button
    {


        private bool musicState;
        private Texture2D musicOn;
        private Texture2D musicOff;

        public MusicButton(Rectangle positionRectangle, Rectangle rangeInSheet)
        {
            this.positionRectangle = positionRectangle;
            this.rangeInSheet = rangeInSheet;
            musicState = true;
            timeCount = 0;

        }
        public override void LoadContent(Game1 game)
        {
            musicOn = game.Content.Load<Texture2D>("button/MusicOn");
            musicOff = game.Content.Load<Texture2D>("button/MusicOff");
            currentTexture = musicOn;
        }
        public override void Process(Game1 game)
        {
            if (musicState)
            {
                currentTexture = musicOff;
                musicState = false;
                MediaPlayer.Pause();
            }
            else
            {
                currentTexture = musicOn;
                MediaPlayer.Resume();
                musicState = true;

            }


        }



    }
}
