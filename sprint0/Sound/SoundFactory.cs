using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;


namespace sprint0
{

    public class SoundFactory
    {
        private SoundEffect enemyHit;

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
            enemyHit = game.Content.Load<SoundEffect>("sound\LOZ_Enemy_Hit");

        }


        public ISound SoundEnemyHit()
        {
            return new StaticItem();
        }



        // More public ISprite returning methods follow
        // ...
    }
}

