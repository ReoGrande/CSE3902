using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace sprint0
{
    public interface IGameState
    {
        void Pause();
        void Play();
        void Win();
        void Update();
        void Draw();
    }

    public class GameState : IGameState
    {
        Game1 game;
        SpriteBatch spriteBatch;
        IGameState state;

        public GameState(Game1 game)
        {
            this.game = game;
            spriteBatch = new SpriteBatch(game.GraphicsDevice);
        }
        public void Play()
        {
            state.Play();
        }

        public void Pause()
        {
            state.Pause();
        }

        public void Win()
        {
            state.Win();
        }

        public void Update()
        {
            state.Update();
        }

        public void Draw()
        {
            spriteBatch.Begin();
            //spriteBatch.Draw(texture, position, currentFrame, color, 0, new Vector2(), flipped, 1);
            state.Draw();
            spriteBatch.End();
            
        }
    }

    public class PausedGameState : GameState
    {

    }
    public class PlayGameState : GameState
    {

    }
    public class WinGameState : GameState
    {

    }
}
