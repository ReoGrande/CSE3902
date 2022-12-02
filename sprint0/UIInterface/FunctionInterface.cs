



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


        private IButton soundButton;
        private IButton musicButton;
        private IButton manuButton;





        public FunctionInterface(Game1 game)
        {

            soundButton = new SoundButton(new Rectangle(50, 200, 40, 40), new Rectangle(4, 4, 96, 96));
            musicButton = new MusicButton(new Rectangle(100, 200, 40, 40), new Rectangle(14, 14, 45, 48));
            manuButton = new ManuButton(new Rectangle(150, 200, 40, 40), new Rectangle(0, 0, 68, 68));

        }

        public void LoadContent(Game1 game)
        {

            soundButton.LoadContent(game);
            musicButton.LoadContent(game);
            manuButton.LoadContent(game);
        }


        public void Draw(Game1 game, SpriteBatch _spriteBatch)
        {

            soundButton.ButtonDraw(_spriteBatch);
            musicButton.ButtonDraw(_spriteBatch);
            manuButton.ButtonDraw(_spriteBatch);

        }





        public void Update(Game1 game)
        {
            soundButton.ButtonUpdate(game);
            musicButton.ButtonUpdate(game);
            manuButton.ButtonUpdate(game);
        }


    }
}
