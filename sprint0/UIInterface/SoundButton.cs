

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
    public class SoundButton : Button
    {


        private bool soundState;
        private Texture2D soundOn;
        private Texture2D soundOff;

        public SoundButton(Rectangle positionRectangle, Rectangle rangeInSheet)
        {
            this.positionRectangle = positionRectangle;
            this.rangeInSheet = rangeInSheet;
            soundState = true;
            timeCount = 0;

        }
        public override void LoadContent(Game1 game)
        {
            soundOn = game.Content.Load<Texture2D>("button/SoundOn");
            soundOff = game.Content.Load<Texture2D>("button/SoundOff");
            currentTexture = soundOn;
        }
        public override void Process(Game1 game)
        {
            if (soundState)
            {
                currentTexture = soundOff;
                soundState = false;
                SoundFactory.Instance.SetMuteSoundEffect();
            }
            else
            {
                currentTexture = soundOn;

                SoundFactory.Instance.TurnOnSoundEffect();
                soundState = true;

            }


        }


    }
}