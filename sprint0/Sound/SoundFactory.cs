using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;

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
        }


        public void PlaySoundEnemyHit()
        {
            ISound sound = new Sound(enemyHit);
            sound.Play();
        }

        public void PlaySoundLinkDie()
        {
            ISound sound = new Sound(linkDie);
            sound.Play();
        }

        public void PlaySoundEnemyDie()
        {
            ISound sound = new Sound(enemyDie);
            sound.Play();
        }

        public void PlaySoundShootArrow()
        {
            ISound sound = new Sound(shootArrow);
            sound.Play();
        }

        public void PlaySoundShootBoomerang()
        {
            ISound sound = new Sound(shootBoomerang);
            sound.Play();
        }

        public void PlaySoundSwordSlash()
        {
            ISound sound = new Sound(swordSlash);
            sound.Play();
        }

        public void PlaySoundDropBomb()
        {
            ISound sound = new Sound(dropBomb);
            sound.Play();
        }


        // More public ISprite returning methods follow
        // ...
    }
}

