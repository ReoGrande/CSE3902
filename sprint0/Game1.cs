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
        private IController _controllers;
        List<IBlock> blockList;
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        public BlockSpace blockSpace;
        public ItemSpace itemSpace;

        private GameTime gameTime;
        public Link character;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);

            Content.RootDirectory = "Content";
            IsMouseVisible = true;

        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            _controllers = new IKeyboard();//Creates default valued controller mappings;

            character = new Link(this);
            _controllers.RegisterCommand(Keys.A, new MoveLeft(this));
            _controllers.RegisterCommand(Keys.D, new MoveRight(this));
            _controllers.RegisterCommand(Keys.W, new MoveUp(this));
            _controllers.RegisterCommand(Keys.S, new MoveDown(this));
            _controllers.RegisterCommand(Keys.T, new PreviousBlock(this));
            _controllers.RegisterCommand(Keys.Y, new NextBlock(this));
            _controllers.RegisterCommand(Keys.U, new PreviousItem(this));
            _controllers.RegisterCommand(Keys.I, new NextItem(this));
            _controllers.RegisterCommand(Keys.Z, new Shoot(this));


            //block and item part
            blockSpace = new BlockSpace();
            itemSpace = new ItemSpace();

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            //block
            BlockFactory.Instance.LoadAllTextures(this);
            blockSpace.Add(BlockFactory.Instance.CreateSquareBlock(new Rectangle(100, 100, 50, 50)));
            blockSpace.Add(BlockFactory.Instance.CreatePushAbleBlock(new Rectangle(150, 100, 50, 50)));
            blockSpace.Add(BlockFactory.Instance.CreateFire(new Rectangle(200, 100, 50, 50)));
            blockSpace.Add(BlockFactory.Instance.CreateBlueGap(new Rectangle(250, 100, 50, 50)));
            blockSpace.Add(BlockFactory.Instance.CreateStairs(new Rectangle(100, 150, 50, 50)));
            blockSpace.Add(BlockFactory.Instance.CreateWhiteBrick(new Rectangle(150, 150, 50, 50)));
            blockSpace.Add(BlockFactory.Instance.CreateLadder(new Rectangle(200, 150, 50, 50)));
            blockSpace.Add(BlockFactory.Instance.CreateBlueFloor(new Rectangle(250, 150, 50, 50)));
            blockSpace.Add(BlockFactory.Instance.CreateBlueSand(new Rectangle(100, 200, 50, 50)));
            blockSpace.Add(BlockFactory.Instance.CreateBlueSand(new Rectangle(150, 200, 50, 50)));

            //item
            ItemFactory.Instance.LoadAllTextures(this);
            itemSpace.Add(ItemFactory.Instance.CreateCompass(new Rectangle(character.position.X, character.position.Y, 50, 50)));
            itemSpace.Add(ItemFactory.Instance.CreateMap(new Rectangle(character.position.X, character.position.Y, 50, 50)));
            itemSpace.Add(ItemFactory.Instance.CreateKey(new Rectangle(character.position.X, character.position.Y, 50, 50)));
            itemSpace.Add(ItemFactory.Instance.CreateHeartContainer(new Rectangle(character.position.X, character.position.Y, 50, 50)));
            itemSpace.Add(ItemFactory.Instance.CreateTriforcePiece(new Rectangle(character.position.X, character.position.Y, 50, 50)));
            itemSpace.Add(ItemFactory.Instance.CreateWoodenBoomerang(new Rectangle(character.position.X, character.position.Y, 50, 50)));
            itemSpace.Add(ItemFactory.Instance.CreateBow(new Rectangle(character.position.X, character.position.Y, 50, 50)));
            itemSpace.Add(ItemFactory.Instance.Createrupee(new Rectangle(character.position.X, character.position.Y, 50, 50)));
            itemSpace.Add(ItemFactory.Instance.CreateArrow(new Rectangle(character.position.X, character.position.Y, 50, 50)));
            itemSpace.Add(ItemFactory.Instance.CreateBomb(new Rectangle(character.position.X, character.position.Y, 50, 50)));
            itemSpace.Add(ItemFactory.Instance.CreateFairy(new Rectangle(character.position.X, character.position.Y, 50, 50)));
            itemSpace.Add(ItemFactory.Instance.CreateClock(new Rectangle(character.position.X, character.position.Y, 50, 50)));
            itemSpace.Add(ItemFactory.Instance.CreateBlueCandle(new Rectangle(character.position.X, character.position.Y, 50, 50)));
            itemSpace.Add(ItemFactory.Instance.CreateBluePotion(new Rectangle(character.position.X, character.position.Y, 50, 50)));

            // TODO: use this.Content to load your game content here        

        }

        protected override void Update(GameTime gameTime)
        {
            // TODO: Add your update logic here
            // TODO: IMPLEMENT MORE ROBUST UPDATE METHODS FOR CONTROLLER AND SPRITE
            //SIMPKY CALL UPDATE WITHIN CONTROLLER AND SPRITE

            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed
                || Keyboard.GetState().IsKeyDown(Keys.Escape)
                || Keyboard.GetState().IsKeyDown(Keys.D0) || Mouse.GetState().RightButton == ButtonState.Pressed)
            {
                Exit();
            }

            character.Update();
            itemSpace.Update(character.position.X, character.position.Y);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            // TODO: Add your drawing code here
            GraphicsDevice.Clear(Color.CornflowerBlue);
            //_spriteBatch.Begin();
            _controllers.Update();

            _spriteBatch.Begin();
            character.Draw();
            blockSpace.Draw(_spriteBatch);
            itemSpace.Draw(_spriteBatch);
            _spriteBatch.End();

            //_commander.Execute();

            // _spriteBatch.End();
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
    }
}
