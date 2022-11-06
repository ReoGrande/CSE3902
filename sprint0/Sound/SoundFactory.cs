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

        private Song backgroundMusic;
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

        public void LoadAllTextures(Game1 game)
        {
            enemyHit = game.Content.Load<SoundEffect>("sound/LOZ_Enemy_Hit");
            linkDie = game.Content.Load<SoundEffect>("sound/LOZ_Link_Die");
            enemyDie = game.Content.Load<SoundEffect>("sound/LOZ_Enemy_Die");
            shootArrow = game.Content.Load<SoundEffect>("sound/arrow");
            shootBoomerang = game.Content.Load<SoundEffect>("sound/LOZ_Arrow_Boomerang");
            swordSlash = game.Content.Load<SoundEffect>("sound/LOZ_Sword_Slash");
            dropBomb = game.Content.Load<SoundEffect>("sound/LOZ_Bomb_Drop");
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
                sound.Play((float)0.3);
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

        public void PlayBackgroundMusic()
        {

            MediaPlayer.Play(backgroundMusic);
            MediaPlayer.Volume = (float)0.2;
            MediaPlayer.IsRepeating = true;

        }


        // More public ISprite returning methods follow
        // ...
    }
}

