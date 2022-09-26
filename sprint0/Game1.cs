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
        _commander = new DrawMario(this);

        character = new Link(this);
        _controllers.RegisterCommand(Keys.Q, _commander);
        _controllers.RegisterCommand(Keys.A,new MoveLeft(this));
        _controllers.RegisterCommand(Keys.D,new MoveRight(this));
        _controllers.RegisterCommand(Keys.W,new MoveUp(this));
        _controllers.RegisterCommand(Keys.S,new MoveDown(this));
        _controllers.RegisterCommand(Keys.Space, new Idle(this));


        base.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);
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
        character.Draw();
        // _spriteBatch.End();
        base.Draw(gameTime);
    }
}
}

