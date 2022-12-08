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
        void Lose();
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
            state = new PausedGameState(this);

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

        public void Lose()
        {
            state.Lose();
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
        private Microsoft.Xna.Framework.Rectangle bottomUI;
        private Microsoft.Xna.Framework.Rectangle equipBox;

        SpriteFont font;



        public PausedGameState(GameState gameState)
        {
            this.gameState = gameState;
            // texture = gameState.game.Content.Load<Texture2D>("maps/Pause-Screen-Empty");
            font = gameState.game.Content.Load<SpriteFont>("File");
            GraphicsDevice tempDevice = gameState.game.GraphicsDevice;
            texture = new Texture2D(tempDevice, 1, 1);
            texture.SetData<Color>(new Color[] { Color.Black });
            Microsoft.Xna.Framework.Rectangle tempPosition = gameState.game._currentMap.Map.getMiniMap().Bounds;
            bottomUI = new Microsoft.Xna.Framework.Rectangle(100, 550, tempPosition.Width * 3, tempPosition.Height * 3);
            equipBox = bottomUI;
            equipBox.X = bottomUI.X + 300;
            equipBox.Height = 80;
            equipBox.Width = 50;

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

        public void Lose()
        {
            gameState.state = new LoseGameState(gameState);
        }

        public IGameState getState()
        {
            return gameState.state;
        }

        public void Update()
        {
        }
        private void drawHelp()
        {
            //font.Texture.Height() //can make bigger TODO: COMPLETE PAUSE MENU
            String keys = "Press W to move up, A to move left, S to move down, and D to move right.\n" +
            "Press I to move inventory to next item, U to previous item.\n" +
            "Press N to attack.\n" +
            "Press 1,2,or 3 to use item in inventory position.\n" +
            "Press G to toggle pause menu.\n" +
            "Press Tab to toggle test mode.\n" +
            "Press J to taunt.\n" +
            "Press Y to switch levels.\n" +
            "Press R to reset level.\n" +
            "Press Q or escape to quit.\n\n" +
            "Press G to start\n" +
            "Press F to start nightmare mode";
            gameState.spriteBatch.DrawString(font, keys, new Vector2(bottomUI.X, bottomUI.Y - 330), Color.White);
            gameState.spriteBatch.DrawString(font, "Pause Menu", new Vector2(bottomUI.X, bottomUI.Y - 360), Color.Red);

        }
        public void Draw()
        {
            gameState.spriteBatch.Draw(texture, gameState.game.GraphicsDevice.ScissorRectangle, Color.White);
            gameState.game._currentMap.drawMiniMapUI(gameState.spriteBatch, bottomUI);
            gameState.game.itemSpace.Draw(gameState.game, gameState.spriteBatch, equipBox);
            drawHelp();

        }
    }
    public class PlayGameState : IGameState
    {
        private GameState gameState;
        private Microsoft.Xna.Framework.Rectangle UI;

        public PlayGameState(GameState gameState)
        {
            this.gameState = gameState;
            UI = new Microsoft.Xna.Framework.Rectangle(410, 100, 50, 70);
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

        public void Lose()
        {
            gameState.state = new LoseGameState(gameState);
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

            gameState.game.collisionController.collisionDetection();
            gameState.game.character.Update();
            gameState.game.itemSpace.Update(gameState.game, gameState.game.character.GetPosition());
            gameState.game.outItemSpace.Update(gameState.game);
            gameState.game.enemySpace.Update(gameState.game);
            gameState.game.blockSpace.Update();
            gameState.game.nPCSpace.Update(gameState.game);


        }

        public void Draw()
        {
            gameState.game._currentMap.Draw();
            gameState.game.blockSpace.Draw(gameState.spriteBatch);
            gameState.game.itemSpace.Draw(gameState.game, gameState.spriteBatch, UI);
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

        public void Lose()
        {
            // Can't Lose B)
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
    public class LoseGameState : IGameState
    {
        private GameState gameState;
        Texture2D texture;

        public LoseGameState(GameState gameState)
        {
            this.gameState = gameState;
            texture = gameState.game.Content.Load<Texture2D>("maps/zelda-gameover");
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
            // Can't win   :(
        }

        public void Lose()
        {
            // Already lost :(
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
