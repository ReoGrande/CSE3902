

using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections;
using System.ComponentModel;
using System.Reflection.Metadata;
using Microsoft.Xna.Framework.Input;
using System.Runtime.InteropServices;
using Microsoft.Xna.Framework.Audio;
using static System.Formats.Asn1.AsnWriter;
using static sprint0.Link;




namespace sprint0
{
    public interface ISound
    {
        void Play();
        void Play(float volume);
    }
    public class Sound : ISound
    {
        private SoundEffect soundEffect;
        public Sound(SoundEffect soundEffect)
        {
            this.soundEffect = soundEffect;
        }

        public void Play()
        {

            soundEffect.Play();

        }
        public void Play(float volume)
        {
            soundEffect.Play(volume, 0, 0);
        }

    }







}