using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;


namespace sprint0
{

    public class Game1 : Game
    {
    private ICommand _commander;
    private IController _controllers;
    List<IBlock> blockList;
    private GraphicsDeviceManager _graphics;
     private SpriteBatch _spriteBatch;
    BlockSpace blockSpace;
    private GameTime gameTime;

    ItemSpace itemSpace;
    
     
      
     

    public Link character;

    // public Game1()
    // {
    //     //List <object> constollerList;// could be defined as List <IController>
    //     //Allows multiple controllers to exist
    //     private GraphicsDeviceManager _graphics;
    //     private SpriteBatch _spriteBatch;

    //     private ICommand _commander;
    //     private IController _controllers;
    //     BlockSpace blockSpace;
    //     ItemSpace itemSpace;
    


    






        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
        
        character.Update();
        
        base.Update(gameTime);
        

            _controllers = new IKeyboard();//Creates default valued controller mappings;
            _commander = new DrawMario(this);

            character = new Link(this);
            character.ToMoving(0);
            _controllers.RegisterCommand(Keys.Q, _commander);
            _controllers.RegisterCommand(Keys.A, new MoveLeft(this));
            _controllers.RegisterCommand(Keys.D, new MoveRight(this));
            _controllers.RegisterCommand(Keys.W, new MoveUp(this));
            _controllers.RegisterCommand(Keys.S, new MoveDown(this));
            _controllers.RegisterCommand(Keys.T, new PreviousBlock(this));
            _controllers.RegisterCommand(Keys.Y, new NextBlock(this));
            _controllers.RegisterCommand(Keys.U, new PreviousItem(this));
            _controllers.RegisterCommand(Keys.I, new NextItem(this));

            //block and item part
            blockSpace = new BlockSpace();
            itemSpace = new ItemSpace();




            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            //block
            BlockFactory.Instance.LoadAllTextures(Content);
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
            itemSpace.Add(ItemFactory.Instance.CreateCompass(new Rectangle(400, 100, 50, 50)));
            itemSpace.Add(ItemFactory.Instance.CreateMap(new Rectangle(450, 100, 50, 50)));
            itemSpace.Add(ItemFactory.Instance.CreateKey(new Rectangle(500, 100, 50, 50)));
            itemSpace.Add(ItemFactory.Instance.CreateHeartContainer(new Rectangle(550, 100, 50, 50)));
            itemSpace.Add(ItemFactory.Instance.CreateTriforcePiece(new Rectangle(400, 150, 50, 50)));
            itemSpace.Add(ItemFactory.Instance.CreateWoodenBoomerang(new Rectangle(450, 150, 50, 50)));
            itemSpace.Add(ItemFactory.Instance.CreateBow(new Rectangle(500, 150, 50, 50)));
            itemSpace.Add(ItemFactory.Instance.Createrupee(new Rectangle(550, 150, 50, 50)));
            itemSpace.Add(ItemFactory.Instance.CreateArrow(new Rectangle(400, 200, 50, 50)));
            itemSpace.Add(ItemFactory.Instance.CreateBomb(new Rectangle(450, 200, 50, 50)));
            itemSpace.Add(ItemFactory.Instance.CreateFairy(new Rectangle(500, 200, 50, 50)));
            itemSpace.Add(ItemFactory.Instance.CreateClock(new Rectangle(550, 200, 50, 50)));
            itemSpace.Add(ItemFactory.Instance.CreateBlueCandle(new Rectangle(400, 250, 50, 50)));
            itemSpace.Add(ItemFactory.Instance.CreateBluePotion(new Rectangle(450, 250, 50, 50)));

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

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            // TODO: Add your drawing code here
            GraphicsDevice.Clear(Color.CornflowerBlue);
            //_spriteBatch.Begin();
            _controllers.Update();

            //_commander.Execute();
             
            _spriteBatch.Begin();
            blockSpace.Draw(_spriteBatch);
            itemSpace.Draw(_spriteBatch);

            

            character.Draw();
            _spriteBatch.End();

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

