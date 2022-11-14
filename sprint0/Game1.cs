using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace sprint0
{

    public class Game1 : Game
    {
        //List <object> constollerList;// could be defined as List <IController>
        //Allows multiple controllers to exist
        private ICommand _commander;
        public IController _controllers;
        public GraphicsDeviceManager _graphics;
        public SpriteBatch _spriteBatch;
        public BlockSpace blockSpace;
        public ItemSpace itemSpace;
        public OutItemSpace outItemSpace;
        public EnemySpace enemySpace;
        public NPCSpace nPCSpace;
        //private GameTime gameTime;
        public ILinkState character;
        public IMap _currentMap;
        public SpriteFont font;
        public CollisionController collisionController;
        public Boolean _testMode;
        public Rectangle _playerScreen;

        public Boolean isPaused;
        private PauseMenu pMen;

        public int _globalTime;
        int _previousTime;
        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            

            Content.RootDirectory = "Content";
            IsMouseVisible = true;

        }

        protected override void Initialize()
        {
            _playerScreen = _graphics.GraphicsDevice.PresentationParameters.Bounds;
            _graphics.PreferredBackBufferWidth = 900;
            _graphics.PreferredBackBufferHeight = 720;
            _graphics.ApplyChanges();
            _playerScreen.X = (int)Math.Ceiling(_graphics.PreferredBackBufferWidth*0.05);
            _playerScreen.Y = (int)Math.Ceiling(_graphics.PreferredBackBufferHeight*0.25);
            Console.WriteLine(_playerScreen);
            _controllers = new IKeyboard();//Creates default valued controller mappings;
            _currentMap = new IMap(this);
            character = new Link(this);
            _testMode = false;
            _globalTime = 0;
            _previousTime = 0;



            _controllers.RegisterCommand(Keys.A, new Move(this));
            _controllers.RegisterCommand(Keys.D, new Move(this));
            _controllers.RegisterCommand(Keys.W, new Move(this));
            _controllers.RegisterCommand(Keys.S, new Move(this));
            _controllers.RegisterCommand(Keys.T, new PreviousBlock(this));
            _controllers.RegisterCommand(Keys.Y, new NextBlock(this));
            _controllers.RegisterCommand(Keys.U, new PreviousItem(this));
            _controllers.RegisterCommand(Keys.I, new NextItem(this));
            _controllers.RegisterCommand(Keys.O, new PreviousEnemy(this));
            _controllers.RegisterCommand(Keys.P, new NextEnemy(this));
            _controllers.RegisterCommand(Keys.Z, new Attack(this));
            _controllers.RegisterCommand(Keys.N, new Attack(this));
            _controllers.RegisterCommand(Keys.D1, new UseFirstItem(this));
            _controllers.RegisterCommand(Keys.D2, new UseSecondItem(this));
            _controllers.RegisterCommand(Keys.D3, new UseThirdItem(this));
            _controllers.RegisterCommand(Keys.E, new TakeDamageOn(this));
            _controllers.RegisterCommand(Keys.R, new Reset(this));
            _controllers.RegisterCommand(Keys.D9, new MuteSoundEffect(this));
            _controllers.RegisterCommand(Keys.D8, new MuteBackgroudMusic(this));
            _controllers.RegisterCommand(Keys.G, new Pause(this));


            //block and item part
            blockSpace = new BlockSpace();
            itemSpace = new ItemSpace();
            outItemSpace = new OutItemSpace();
            enemySpace = new EnemySpace();
            nPCSpace =new NPCSpace();
            collisionController = new CollisionController(this);
            base.Initialize();

            isPaused = false;
            pMen = new PauseMenu(this);
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            font = Content.Load<SpriteFont>("File");

            //block
            BlockFactory.Instance.LoadAllTextures(this);
            //blockSpace.Add(BlockFactory.Instance.CreatePushAbleBlock(new Rectangle(100, 300, 50, 50)));
            


            //item
            ItemFactory.Instance.LoadAllTextures(this);
            itemSpace.LoadBox(this);
            
            itemSpace.Add(ItemFactory.Instance.CreateWoodenBoomerang(new Rectangle(character.GetPosition().X, character.GetPosition().Y, 25, 25)));
            itemSpace.Add(ItemFactory.Instance.CreateArrow(new Rectangle(character.GetPosition().X, character.GetPosition().Y, 25, 25)));
            itemSpace.Add(ItemFactory.Instance.CreateBomb(new Rectangle(character.GetPosition().X, character.GetPosition().Y, 25, 25)));
            itemSpace.Add(ItemFactory.Instance.CreateFairy(new Rectangle(character.GetPosition().X, character.GetPosition().Y, 25, 25)));
            

            //enemy
            EnemyFactory.Instance.LoadAllTextures(this);
            // enemySpace.Add(EnemyFactory.Instance.CreateBoss(new Rectangle(500, 400, 70, 70)));
            // enemySpace.Add(EnemyFactory.Instance.CreateBat(new Rectangle(100, 300, 70, 70)));
            // enemySpace.Add(EnemyFactory.Instance.CreateSkeleton(new Rectangle(200, 400, 70, 70)));
            // enemySpace.Add(EnemyFactory.Instance.CreateRope(new Rectangle(400, 300, 70, 70)));
            // enemySpace.Add(EnemyFactory.Instance.CreateTrap(new Rectangle(600, 50, 70, 70)));
            // enemySpace.Add(EnemyFactory.Instance.CreateWallMaster(new Rectangle(500, 100, 70, 70)));
            // enemySpace.Add(EnemyFactory.Instance.CreateGoriyaBlue(new Rectangle(600, 100, 70, 70)));

            //Sound
            SoundFactory.Instance.LoadAllContent(this);
            SoundFactory.Instance.PlayBackgroundMusic();
            
            //NPC
            NPCFactory.Instance.LoadAllTextures(this);

            _currentMap.MapControl.LoadContent();


        }

        protected override void Update(GameTime gameTime)
        {
            _controllers.Update();
            if (!isPaused)
            {
                _globalTime = (_globalTime + 1) % 100;
                if (_globalTime % 25 == 0)
                {
                    _previousTime = 0;
                }
                if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed
                    || Keyboard.GetState().IsKeyDown(Keys.Escape)
                    || Keyboard.GetState().IsKeyDown(Keys.D0)
                    || Keyboard.GetState().IsKeyDown(Keys.Q))
                {
                    Exit();
                }
                if (Keyboard.GetState().IsKeyDown(Keys.Tab))
                {
                    _testMode = !_testMode;
                }
                if (Mouse.GetState().LeftButton == ButtonState.Pressed && _previousTime == 0 && _testMode)
                {
                    _previousTime = 1;
                    _commander = new NextRoom(this);
                    _commander.Execute();
                }
                if (Mouse.GetState().RightButton == ButtonState.Pressed && _previousTime == 0 && _testMode)
                {
                    _previousTime = 1;
                    _commander = new PreviousRoom(this);
                    _commander.Execute();
                }
                _currentMap.Update();
                //_currentMap.MapControl.translate(_playerScreen);

                character.Update();
                itemSpace.Update(this, character.GetPosition().X, character.GetPosition().Y);
                outItemSpace.Update(this);
                enemySpace.Update(this);
                nPCSpace.Update(this);

                collisionController.collisionDetection();


                base.Update(gameTime);
            } else
            {
                if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed
                    || Keyboard.GetState().IsKeyDown(Keys.Escape)
                    || Keyboard.GetState().IsKeyDown(Keys.D0)
                    || Keyboard.GetState().IsKeyDown(Keys.Q))
                {
                    Exit();
                }
                // pause menu updates
            }
        }

        protected override void Draw(GameTime gameTime)
        {
            

            if (!isPaused)
            {
                GraphicsDevice.Clear(Color.Black);
                
                _spriteBatch.Begin();
                _currentMap.Draw();
                blockSpace.Draw(_spriteBatch);
                itemSpace.Draw(this, _spriteBatch);
                enemySpace.Draw(_spriteBatch);
                nPCSpace.Draw(_spriteBatch);
                enemySpace.DrawNumber(_spriteBatch, this);
                outItemSpace.Draw(_spriteBatch);
                character.Draw();
                _spriteBatch.End();
            } else
            {
                // pause menu drawing
                pMen.Draw();
            }

            base.Draw(gameTime);
        }

        public void ChangetoPreviousBlock()
        {
            this.blockSpace.PreviousBlock();
        }
        public void ChangetoNextBlock()
        {
            this.blockSpace.NextBlock();
        }
        public void ChangetoPreviousItem()
        {
            this.itemSpace.PreviousItem();
        }
        public void ChangetoNextItem()
        {
            this.itemSpace.NextItem();
        }

        public void ChangetoPreviousEnemy()
        {
            this.enemySpace.PreviousEnemy();
        }

        public void ChangetoNextEnemy()
        {
            this.enemySpace.NextEnemy();
        }

        public void ItemIntoOut()
        {
            IItem item = itemSpace.CurrentItem();

            outItemSpace.Add(item);
            if (item.IsInfinite())
            {

                itemSpace.Exchange(ItemFactory.Instance.CreateArrow(new Rectangle(character.GetPosition().X, character.GetPosition().Y, 50, 50)));

            }
            else
            {
                itemSpace.Remove(item);

            }
        }


    }
}
