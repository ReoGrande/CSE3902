using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;


namespace sprint0{

public class Game1 : Game
{
    //List <object> constollerList;// could be defined as List <IController>
    //Allows multiple controllers to exist
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;

    private ICommand _commander;
    private IController _controllers;

    
     
      
      IBlock myBlock;

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
        _commander = new DrawMario(this);
        _controllers.RegisterCommand(Keys.A, _commander);
        _commander = new MoveMarioLeft(this);
        _controllers.RegisterCommand(Keys.B, _commander);

        //block part
        
      
       


        base.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);
        
         

          
          BlockFactory.Instance.LoadAllTextures(Content);
            myBlock = BlockFactory.Instance.CreateSquareBlock();
           
            // TODO: use this.Content to load your game content here



    }

    protected override void Update(GameTime gameTime)
    {
        // TODO: Add your update logic here
        // TODO: IMPLEMENT MORE ROBUST UPDATE METHODS FOR CONTROLLER AND SPRITE
        //SIMPKY CALL UPDATE WITHIN CONTROLLER AND SPRITE

        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed
            || Keyboard.GetState().IsKeyDown(Keys.Escape)
            || Keyboard.GetState().IsKeyDown(Keys.D0) || Mouse.GetState().RightButton == ButtonState.Pressed) {
            Exit();
        }
        

        
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

            myBlock.BlockDraw(_spriteBatch);

            _spriteBatch.End();

        // _spriteBatch.End();
        base.Draw(gameTime);
    }
}
}

