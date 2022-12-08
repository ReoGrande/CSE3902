using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace sprint0
{
    public interface ICommand
    {
        void Execute();
    }

    public abstract class SingleClickCommand : ICommand
    {

        public int endTime = System.Environment.TickCount;
        public int startTime = System.Environment.TickCount;

        public abstract void SingleExecute();

        public void Execute()
        {
            endTime = System.Environment.TickCount;
            int runTime = endTime - startTime;
            if (runTime > 200)
            {
                SingleExecute();
                startTime = endTime;
            }


        }
    }



    public class NextRoom : ICommand
    {
        private Game1 myGame;
        public NextRoom(Game1 game)
        {
            myGame = game;
        }
        public void Execute()
        {
            myGame._currentMap.MapControl.NextRoom();
        }
    }
    public class PreviousRoom : ICommand
    {
        private Game1 myGame;
        public PreviousRoom(Game1 game)
        {
            myGame = game;
        }
        public void Execute()
        {
            myGame._currentMap.MapControl.PreviousRoom();
        }
    }

    public class LoadItems : ICommand
    {
        private Game1 myGame;
        private Rectangle characterPos;
        public LoadItems(Game1 game)
        {
            myGame = game;
            characterPos = myGame.character.GetPosition();
        }
        public void Execute()
        {


            ItemFactory.Instance.LoadAllTextures(myGame);
            myGame.itemSpace.Add(ItemFactory.Instance.CreateCompass(new Rectangle(characterPos.X, characterPos.Y, 50, 50)));
            myGame.itemSpace.Add(ItemFactory.Instance.CreateMap(new Rectangle(characterPos.X, characterPos.Y, 50, 50)));
            myGame.itemSpace.Add(ItemFactory.Instance.CreateKey(new Rectangle(characterPos.X, characterPos.Y, 50, 50)));
            myGame.itemSpace.Add(ItemFactory.Instance.CreateHeartContainer(new Rectangle(characterPos.X, characterPos.Y, 50, 50)));
            myGame.itemSpace.Add(ItemFactory.Instance.CreateTriforcePiece(new Rectangle(characterPos.X, characterPos.Y, 50, 50)));
            myGame.itemSpace.Add(ItemFactory.Instance.CreateWoodenBoomerang(new Rectangle(characterPos.X, characterPos.Y, 50, 50)));
            myGame.itemSpace.Add(ItemFactory.Instance.CreateBow(new Rectangle(characterPos.X, characterPos.Y, 50, 50)));
            myGame.itemSpace.Add(ItemFactory.Instance.Createrupee(new Rectangle(characterPos.X, characterPos.Y, 50, 50)));
            myGame.itemSpace.Add(ItemFactory.Instance.CreateArrow(new Rectangle(characterPos.X, characterPos.Y, 50, 50)));
            myGame.itemSpace.Add(ItemFactory.Instance.CreateBomb(new Rectangle(characterPos.X, characterPos.Y, 50, 50)));
            myGame.itemSpace.Add(ItemFactory.Instance.CreateFairy(new Rectangle(characterPos.X, characterPos.Y, 50, 50)));
            myGame.itemSpace.Add(ItemFactory.Instance.CreateClock(new Rectangle(characterPos.X, characterPos.Y, 50, 50)));
            myGame.itemSpace.Add(ItemFactory.Instance.CreateBlueCandle(new Rectangle(characterPos.X, characterPos.Y, 50, 50)));
            myGame.itemSpace.Add(ItemFactory.Instance.CreateBluePotion(new Rectangle(characterPos.X, characterPos.Y, 50, 50)));
        }
    }

    public class SetControllers : ICommand
    {
        private Game1 myGame;
        public SetControllers(Game1 game)
        {
            myGame = game;
        }
        public void Execute()
        {
            myGame._controllers.RegisterCommand(Keys.A, new Move(myGame));
            myGame._controllers.RegisterCommand(Keys.D, new Move(myGame));
            myGame._controllers.RegisterCommand(Keys.W, new Move(myGame));
            myGame._controllers.RegisterCommand(Keys.S, new Move(myGame));
            myGame._controllers.RegisterCommand(Keys.T, new PreviousBlock(myGame));
            myGame._controllers.RegisterCommand(Keys.Y, new NextBlock(myGame));
            myGame._controllers.RegisterCommand(Keys.U, new PreviousItem(myGame));
            myGame._controllers.RegisterCommand(Keys.I, new NextItem(myGame));
            myGame._controllers.RegisterCommand(Keys.O, new PreviousEnemy(myGame));
            myGame._controllers.RegisterCommand(Keys.P, new NextEnemy(myGame));
            myGame._controllers.RegisterCommand(Keys.Z, new Attack(myGame));
            myGame._controllers.RegisterCommand(Keys.N, new Attack(myGame));
            myGame._controllers.RegisterCommand(Keys.D1, new UseFirstItem(myGame));
            myGame._controllers.RegisterCommand(Keys.D2, new UseSecondItem(myGame));
            myGame._controllers.RegisterCommand(Keys.D3, new UseThirdItem(myGame));
            myGame._controllers.RegisterCommand(Keys.E, new TakeDamageOn(myGame));
        }
    }
    public class Reset : SingleClickCommand
    {
        private Game1 myGame;

        public Reset(Game1 game)
        {
            myGame = game;
        }

        public override void SingleExecute()
        {
            MediaPlayer.Stop();
            myGame.gameState.Play();
            myGame._currentMap = new IMap(myGame, myGame.currentLevel);
            myGame.character.ChangeHP(myGame.character.MaxHP() - myGame.character.HP());
            myGame.character.ToStanding();
            myGame._currentMap.MapControl.LoadContent();
            myGame.itemSpace.Clear();
            myGame.itemSpace.Add(ItemFactory.Instance.CreateWoodenBoomerang(new Rectangle(myGame.character.GetPosition().X, myGame.character.GetPosition().Y, 25, 25)));
            myGame.itemSpace.Add(ItemFactory.Instance.CreateArrow(new Rectangle(myGame.character.GetPosition().X, myGame.character.GetPosition().Y, 25, 25)));
            myGame.itemSpace.Add(ItemFactory.Instance.CreateBomb(new Rectangle(myGame.character.GetPosition().X, myGame.character.GetPosition().Y, 25, 25)));
            myGame.itemSpace.Add(ItemFactory.Instance.CreateFairy(new Rectangle(myGame.character.GetPosition().X, myGame.character.GetPosition().Y, 25, 25)));
            myGame.character.ChangePosition(new Link(myGame).GetPosition());


            SoundFactory.Instance.PlayBackgroundMusic();
        }
    }

    public class Move : ICommand
    {
        private ILinkState link;

        public Move(Game1 game)
        {
            link = game.character;
        }

        public void Execute()
        {
            link.ToMoving();
        }
    }

    public class Attack : SingleClickCommand
    {
        private Game1 myGame;
        private ILinkState link;

        public Attack(Game1 game)
        {
            myGame = game;
            link = myGame.character;
        }

        public override void SingleExecute()
        {
            link = myGame.character;
            link.ToAttacking();
            SoundFactory.Instance.PlaySoundShootBoomerang();
        }
    }

    public class TakeDamageOn : SingleClickCommand
    {
        private Game1 myGame;
        ILinkState link;

        public TakeDamageOn(Game1 game)
        {
            myGame = game;
            link = myGame.character;
        }

        public override void SingleExecute()
        {
            link = myGame.character;
            link.TakeDamage(-1);
        }
    }
    public class Pause : SingleClickCommand
    {
        private Game1 game;
        IGameState gameState;


        public Pause(Game1 game)
        {
            this.game = game;
            gameState = game.gameState;


        }

        public override void SingleExecute()
        {
            gameState = game.gameState;
            if (!game.isPaused)
            {
                gameState.Pause();
                game.isPaused = true;

                MediaPlayer.Pause();
            }
            else
            {

                gameState.Play();
                game.isPaused = false;
                game.nightmareMode = false;
                MediaPlayer.Resume();
            }
        }
    }
    public class Pause2 : SingleClickCommand
    {
        private Game1 game;
        IGameState gameState;


        public Pause2(Game1 game)
        {
            this.game = game;
            gameState = game.gameState;


        }

        public override void SingleExecute()
        {
            gameState = game.gameState;
            if (!game.isPaused)
            {
                gameState.Pause();
                game.isPaused = true;

                MediaPlayer.Pause();
            }
            else
            {
                gameState.Play();
                game.isPaused = false;
                game.nightmareMode = true;
                MediaPlayer.Resume();
            }
        }
    }
    public class Win : SingleClickCommand
    {
        private Game1 game;
        IGameState gameState;
        Boolean won;

        public Win(Game1 game)
        {
            this.game = game;
            gameState = game.gameState;
            won = false;

        }

        public override void SingleExecute()
        {
            gameState = game.gameState;
            if (!won)
            {
                gameState.Win();
                won = true;
                MediaPlayer.Pause();
            }
            else
            {
                gameState.Play();
                won = false;
                MediaPlayer.Resume();
            }
        }
    }

    public class Lose : SingleClickCommand
    {
        private Game1 game;
        IGameState gameState;
        Boolean lost;

        public Lose(Game1 game)
        {
            this.game = game;
            gameState = game.gameState;
            lost = false;

        }

        public override void SingleExecute()
        {
            gameState = game.gameState;
            if (!lost)
            {
                gameState.Lose();
                lost = true;

                MediaPlayer.Pause();
            }
            else
            {
                gameState.Play();
                lost = false;
                MediaPlayer.Resume();
            }
        }
    }

    public class TestMode : SingleClickCommand
    {
        private Game1 game;

        public TestMode(Game1 game)
        {
            this.game = game;

        }

        public override void SingleExecute()
        {
            game._testMode = !game._testMode;
        }
    }

    public class SwitchLevel : SingleClickCommand
    {
        private Game1 myGame;
        public SwitchLevel(Game1 game)
        {
            myGame = game;
        }

        public override void SingleExecute()
        {
            myGame.currentLevel += 1;
            if (myGame.currentLevel > myGame.totalLevels)
            {
                myGame.currentLevel = 1;
            }
            myGame._currentMap = new IMap(myGame, myGame.currentLevel);
            myGame._currentMap.MapControl.LoadContent();
            myGame.character.ChangePosition(new Link(myGame).GetPosition());
        }
    }

    public class Quit : SingleClickCommand
    {
        private Game1 game;

        public Quit(Game1 game)
        {
            this.game = game;

        }

        public override void SingleExecute()
        {
            game.Exit();
        }
    }

    public class Taunt : SingleClickCommand
    {
        private Game1 myGame;
        Link link;

        public Taunt(Game1 game)
        {
            myGame = game;
            link = (Link)myGame.character;
        }

        public override void SingleExecute()
        {
            link = (Link)myGame.character;
            link.Taunt();
        }
    }
}

