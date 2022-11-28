using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Color = Microsoft.Xna.Framework.Color;

namespace sprint0
{
    public interface IGameState
    {
        void Pause();
        void Play();
        void Win();
        IGameState getState();
        void Update();
        void Draw();
    }

    public class GameState : IGameState
    {
        public Game1 game;
        public SpriteBatch spriteBatch;
        public IGameState state;



        public GameState(Game1 game)
        {
            this.game = game;
            spriteBatch = new SpriteBatch(game.GraphicsDevice);
            state = new PlayGameState(this);

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
        public IGameState getState()
        {
            return state;
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

    public class PausedGameState : IGameState
    {
        private GameState gameState;
        Texture2D texture;

        public PausedGameState(GameState gameState)
        {
            this.gameState = gameState;
            texture = gameState.game.Content.Load<Texture2D>("maps/Pause-Screen-Empty");
        }

        public void Play()
        {
            gameState.state = new PlayGameState(gameState);
        }

        public void Pause()
        {
            // Already Paused
        }

        public void Win()
        {
            // Can't Win while Paused
        }
        public IGameState getState()
        {
            return gameState.state;
        }

        public void Update()
        {
        }

        public void Draw()
        {
            gameState.spriteBatch.Draw(texture, gameState.game.GraphicsDevice.ScissorRectangle, Color.White);
        }
    }
    public class PlayGameState : IGameState
    {
        private GameState gameState;

        public PlayGameState(GameState gameState)
        {
            this.gameState = gameState;
        }

        public void Play()
        {
            // Already Playing
        }

        public void Pause()
        {
            gameState.state = new PausedGameState(gameState);
        }

        public void Win()
        {
            gameState.state = new WinGameState(gameState);
        }
        public IGameState getState()
        {
            return gameState.state;
        }

        public void Update()
        {

            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed
                || Keyboard.GetState().IsKeyDown(Keys.Escape)
                || Keyboard.GetState().IsKeyDown(Keys.D0)
                || Keyboard.GetState().IsKeyDown(Keys.Q))
            {
                gameState.game.Exit();
            }


            gameState.game._currentMap.Update();
            //_currentMap.MapControl.translate(_playerScreen);

            gameState.game.character.Update();
            gameState.game.itemSpace.Update(gameState.game, gameState.game.character.GetPosition().X, gameState.game.character.GetPosition().Y);
            gameState.game.outItemSpace.Update(gameState.game);
            gameState.game.enemySpace.Update(gameState.game);
            gameState.game.blockSpace.Update();
            gameState.game.nPCSpace.Update(gameState.game);

            gameState.game.collisionController.collisionDetection();
        }

        public void Draw()
        {
            gameState.game._currentMap.Draw();
            gameState.game.blockSpace.Draw(gameState.spriteBatch);
            gameState.game.itemSpace.Draw(gameState.game, gameState.spriteBatch);
            gameState.game.enemySpace.Draw(gameState.spriteBatch);
            gameState.game.nPCSpace.Draw(gameState.spriteBatch);
            //gameState.game.enemySpace.DrawNumber(gameState.spriteBatch, gameState.game);
            gameState.game.outItemSpace.Draw(gameState.spriteBatch);
            gameState.game.functionInterface.Draw(gameState.game, gameState.spriteBatch);

            gameState.game.character.Draw();
        }
    }
    public class WinGameState : IGameState
    {
        private GameState gameState;
        Texture2D texture;

        public WinGameState(GameState gameState)
        {
            this.gameState = gameState;
            texture = gameState.game.Content.Load<Texture2D>("maps/ending");
        }

        public void Play()
        {
            gameState.state = new PlayGameState(gameState);
        }

        public void Pause()
        {
            gameState.state = new PausedGameState(gameState);
        }

        public void Win()
        {
            // Already Winning   B)
        }
        public IGameState getState()
        {
            return gameState.state;
        }

        public void Update()
        {
        }

        public void Draw()
        {
            gameState.spriteBatch.Draw(texture, gameState.game.GraphicsDevice.ScissorRectangle, Color.White);
        }
    }
}
