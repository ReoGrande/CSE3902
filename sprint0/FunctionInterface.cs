



using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections;
using System.ComponentModel;
using System.Reflection.Metadata;
using Microsoft.Xna.Framework.Input;
using System.Threading;
using Microsoft.Xna.Framework.Media;

namespace sprint0
{

    public class FunctionInterface
    {


        private Texture2D soundOn;
        private Texture2D soundOff;
        private Texture2D musicOn;
        private Texture2D musicOff;

        private Texture2D sound;
        private Texture2D music;

        private Rectangle button1;
        private Rectangle button2;

        private bool soundState;
        private bool musicState;
        private Color soundButtonColor;
        private Color musicButtonColor;

        int timeCount;

        public FunctionInterface(Game1 game)
        {

            button1 = new Rectangle(50, 200, 40, 40);
            button2 = new Rectangle(100, 200, 40, 40);
            soundState = true;
            musicState = true;
            soundButtonColor = Color.White;
            musicButtonColor = Color.White;
            timeCount = 0;
        }

        public void LoadContent(Game1 game)
        {


            soundOn = game.Content.Load<Texture2D>("button/SoundOn");
            soundOff = game.Content.Load<Texture2D>("button/SoundOff");
            musicOn = game.Content.Load<Texture2D>("button/MusicOn");
            musicOff = game.Content.Load<Texture2D>("button/MusicOff");
            sound = soundOn;
            music = musicOn;
        }
        private void DrawSoundButton(Texture2D texturesheet, SpriteBatch _spriteBatch)
        {
            _spriteBatch.Draw(
            texturesheet,
            button1,
            new Rectangle(4, 4, 96, 96),
            soundButtonColor
            );
        }
        private void DrawMusicButton(Texture2D texturesheet, SpriteBatch _spriteBatch)
        {
            _spriteBatch.Draw(
            texturesheet,
            button2,
            new Rectangle(10, 12, 50, 49),
            musicButtonColor
            );
        }


        public void Draw(Game1 game, SpriteBatch _spriteBatch)
        {

            DrawMusicButton(music, _spriteBatch);
            DrawSoundButton(sound, _spriteBatch);

        }





        public void Update(Game1 game)
        {
            MouseState mouseState = Mouse.GetState();

            //button1
            if (mouseState.LeftButton == ButtonState.Pressed)
            {
                if (button1.Contains(mouseState.X, mouseState.Y) && timeCount > 6)
                {
                    if (soundState)
                    {
                        sound = soundOff;
                        soundState = false;
                        SoundFactory.Instance.SetMuteSoundEffect();
                    }
                    else
                    {
                        sound = soundOn;

                        SoundFactory.Instance.TurnOnSoundEffect();
                        soundState = true;

                    }
                    timeCount = 0;
                }
            }
            else
            {
                if (button1.Contains(mouseState.X, mouseState.Y))
                {
                    soundButtonColor = Color.Gray;
                }
                else
                {
                    soundButtonColor = Color.White;

                }

            }

            //button2 update
            if (mouseState.LeftButton == ButtonState.Pressed)
            {
                if (button2.Contains(mouseState.X, mouseState.Y) && timeCount > 6)
                {
                    if (musicState)
                    {
                        music = musicOff;
                        musicState = false;
                        MediaPlayer.Pause();
                    }
                    else
                    {
                        music = musicOn;

                        MediaPlayer.Resume();
                        musicState = true;

                    }
                    timeCount = 0;
                }
            }
            else
            {
                if (button2.Contains(mouseState.X, mouseState.Y))
                {
                    musicButtonColor = Color.Gray;
                }
                else
                {
                    musicButtonColor = Color.White;

                }

            }



            timeCount++;
        }


    }
}
