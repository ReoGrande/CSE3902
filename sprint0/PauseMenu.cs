using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using System.Reflection.Metadata;

namespace sprint0
{
    public class PauseMenu
    {
        private Game1 game;
        private Texture2D texture;
        private SpriteFont font;
        public PauseMenu(Game1 game)
        {
            this.game = game;
            texture = game.Content.Load<Texture2D>("grey");
            font = game.Content.Load<SpriteFont>("Paused");
        }
        public void Draw()
        {
            game._spriteBatch.Begin();
            game._spriteBatch.DrawString(font, "PAUSED", new Vector2(game._playerScreen.Right / 2 - font.MeasureString("PAUSED").X / 2, game._playerScreen.Bottom / 2), Color.Black);
            game._spriteBatch.Draw(texture, game._playerScreen, Color.Gray * 0.1f);
            game._spriteBatch.End();
        }
    }
}
