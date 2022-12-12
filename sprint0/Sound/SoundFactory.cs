using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;
namespace sprint0
{

    public class SoundFactory
    {
        private SoundEffect enemyHit;
        private SoundEffect linkDie;
        private SoundEffect enemyDie;
        private SoundEffect shootArrow;
        private SoundEffect shootBoomerang;
        private SoundEffect swordSlash;
        private SoundEffect dropBomb;
        private SoundEffect shootFireBall;
        private SoundEffect linkHurt;
        private SoundEffect linkFinalHurt;
        private SoundEffect rockCrush;
        private SoundEffect blast;
        private SoundEffect shieldBall;
        private SoundEffect pickup;
        private SoundEffect pickupStaff;


        private Song backgroundMusic;
        private Song TauntSong;
        private bool muteSoundEffect;


        private static SoundFactory instance = new SoundFactory();

        public static SoundFactory Instance
        {
            get
            {
                return instance;
            }
        }

        private SoundFactory()
        {
        }

        public void LoadAllContent(Game1 game)
        {
            enemyHit = game.Content.Load<SoundEffect>("sound/LOZ_Enemy_Hit");
            linkDie = game.Content.Load<SoundEffect>("sound/LOZ_Link_Die");
            enemyDie = game.Content.Load<SoundEffect>("sound/LOZ_Enemy_Die");
            shootArrow = game.Content.Load<SoundEffect>("sound/arrow");
            shootBoomerang = game.Content.Load<SoundEffect>("sound/LOZ_Arrow_Boomerang");
            swordSlash = game.Content.Load<SoundEffect>("sound/LOZ_Sword_Slash");
            dropBomb = game.Content.Load<SoundEffect>("sound/LOZ_Bomb_Drop");
            shootFireBall = game.Content.Load<SoundEffect>("sound/ShootFireBall");
            linkHurt = game.Content.Load<SoundEffect>("sound/LOZ_Link_Hurt");
            linkFinalHurt = game.Content.Load<SoundEffect>("sound/Link_Final_Hurt");
            blast = game.Content.Load<SoundEffect>("sound/blast");
            rockCrush = game.Content.Load<SoundEffect>("sound/rock_crush");
            shieldBall = game.Content.Load<SoundEffect>("sound/shieldBall");
            pickup = game.Content.Load<SoundEffect>("sound/pickup");
            pickupStaff = game.Content.Load<SoundEffect>("sound/pickupStaff");

            TauntSong = game.Content.Load<Song>("sound/Taunt-Song");
            backgroundMusic = game.Content.Load<Song>("sound/backgroundMusic");

            muteSoundEffect = false;

        }

        public bool MuteSoundEffect()
        {
            return this.muteSoundEffect;
        }

        public void SetMuteSoundEffect()
        {
            this.muteSoundEffect = true;
        }

        public void TurnOnSoundEffect()
        {
            this.muteSoundEffect = false;
        }









        public void PlaySoundEnemyHit()
        {
            if (muteSoundEffect == false)
            {
                ISound sound = new Sound(enemyHit);
                sound.Play((float)0.5);
            }
        }

        public void PlaySoundLinkDie()
        {
            if (muteSoundEffect == false)
            {
                ISound sound = new Sound(linkDie);
                sound.Play();
            }
        }

        public void PlaySoundEnemyDie()
        {
            if (muteSoundEffect == false)
            {
                ISound sound = new Sound(enemyDie);
                sound.Play();
            }
        }

        public void PlaySoundShootArrow()
        {

            if (muteSoundEffect == false)
            {
                ISound sound = new Sound(shootArrow);
                sound.Play((float)0.1);
            }
        }

        public void PlaySoundShootBoomerang()
        {
            if (muteSoundEffect == false)
            {
                ISound sound = new Sound(shootBoomerang);
                sound.Play();
            }
        }

        public void PlaySoundSwordSlash()
        {
            if (muteSoundEffect == false)
            {
                ISound sound = new Sound(swordSlash);
                sound.Play();
            }
        }

        public void PlaySoundDropBomb()
        {
            if (muteSoundEffect == false)
            {
                ISound sound = new Sound(dropBomb);
                sound.Play((float)0.2);
            }
        }

        public void PlaySoundShootFireBall()
        {
            if (muteSoundEffect == false)
            {
                ISound sound = new Sound(shootFireBall);
                sound.Play((float)0.1);
            }
        }
        public void PlaySoundLinkHurt()
        {

            if (muteSoundEffect == false)
            {
                ISound sound = new Sound(linkHurt);
                sound.Play();
            }
            // not use for now
        }


        public void PlayBackgroundMusic()
        {

            MediaPlayer.Play(backgroundMusic);
            MediaPlayer.Volume = (float)0.09;
            MediaPlayer.IsRepeating = true;

        }

        public void PlaySoundRockCrush()
        {
            if (muteSoundEffect == false)
            {
                ISound sound = new Sound(rockCrush);
                sound.Play();
            }
        }
        public void PlaySoundPickup()
        {
            if (muteSoundEffect == false)
            {
                ISound sound = new Sound(pickup);
                sound.Play((float)0.5);
            }
        }
        public void PlaySoundPickupStaff()
        {
            if (muteSoundEffect == false)
            {
                ISound sound = new Sound(pickupStaff);
                sound.Play();
            }
        }



        public void PlaySoundShieldBall()
        {
            if (muteSoundEffect == false)
            {
                ISound sound = new Sound(shieldBall);
                sound.Play((float)0.7);
            }
        }

        public void PlaySoundBlast()
        {
            if (muteSoundEffect == false)
            {
                ISound sound = new Sound(blast);
                sound.Play();
            }
        }

        public void PlaySoundLinkFinalHurt()
        {
            if (muteSoundEffect == false)
            {
                ISound sound = new Sound(linkFinalHurt);
                sound.Play();
            }
        }

        public void PlayTauntSong()
        {
            MediaPlayer.Play(TauntSong);
            MediaPlayer.Volume = (float)0.2;
            MediaPlayer.IsRepeating = true;
        }

        // More public ISprite returning methods follow
        // ...
    }
}

