using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
namespace sprint0
{
    public class MuteSoundEffect : SingleClickCommand
    {
        private Game1 myGame;
        public MuteSoundEffect(Game1 game)
        {
            myGame = game;
            startTime = System.Environment.TickCount;
            endTime = System.Environment.TickCount;
        }

        public override void SingleExecute()
        {
            if (SoundFactory.Instance.MuteSoundEffect())
            {
                SoundFactory.Instance.TurnOnSoundEffect();
            }
            else
            {

                SoundFactory.Instance.SetMuteSoundEffect();

            }
        }
    }


    public class MuteBackgroudMusic : SingleClickCommand
    {
        private Game1 myGame;
        public MuteBackgroudMusic(Game1 game)
        {
            myGame = game;
            startTime = System.Environment.TickCount;
            endTime = System.Environment.TickCount;
        }

        public override void SingleExecute()
        {
            if (MediaPlayer.State == MediaState.Paused)
            {
                MediaPlayer.Resume();
            }
            else
            {

                MediaPlayer.Pause();

            }
        }



    }
}
