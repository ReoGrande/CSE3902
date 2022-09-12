using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace sprint0;

public class Game1 : Game
{
    //List <object> constollerList;// could be defined as List <IController>
    //Allows multiple controllers to exist
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;
    private Texture2D _mario;
    private Rectangle _marioPosition;
    private Rectangle[] _marioSourceRectangle = new Rectangle[2];
    private IKeyboard _controlK;
    private IMouse _controlM;
    private ISprite _currentSprite;
    private SpriteFont _font;
    private ISprite _text;
    private Vector2 _textPosition;
    private int _frameMario = 0;
    private float _timer = 0;
    private bool _marioLeft = true;

    



    public Game1()
    {
        _graphics = new GraphicsDeviceManager(this);
        
        Content.RootDirectory = "Content";
        IsMouseVisible = true;

    }

    protected override void Initialize()
    {
        // TODO: Add your initialization logic here
        _controlK = new IKeyboard();
        _controlM = new IMouse();
        _currentSprite = new nnSprite();
        _marioPosition = new Rectangle(350, 150, 150, 150);
        //_marioSourceRectangle[0] = new Rectangle(115, 0, 25, 25); STANDING MARIO
        _marioSourceRectangle[0] = new Rectangle(85, 0, 25, 25);
        _marioSourceRectangle[1] = new Rectangle(145, 0, 25, 25);
        _textPosition = new Vector2(50, 300);
        _text = new textSprite();
        _font = this.Content.Load<SpriteFont>("File");

        base.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);
        // TODO: use this.Content to load your game content here

        _mario = this.Content.Load<Texture2D>("smb_mario_sheet");

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
        else if (Keyboard.GetState().GetPressedKeyCount() > 0) { _currentSprite = _controlK.Update(_currentSprite);}
        else {_currentSprite = _controlM.Update(_currentSprite); }

        if (_timer > 25)
        {
            _frameMario = _currentSprite.checkTime(_frameMario, _marioSourceRectangle.Length);
            if(_marioPosition.X >= _graphics.PreferredBackBufferWidth - 120 || _marioPosition.Y >= _graphics.PreferredBackBufferHeight - 100)
            {
                _marioLeft = true;
            }
            else if(_marioPosition.X <= 15 || _marioPosition.Y <= 30)
            {
                _marioLeft = false;
            }
            _marioPosition = _currentSprite.position(_marioPosition, _marioLeft);
            _timer = 0;
        }
        else { _timer += 1; }
        


        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        // TODO: Add your drawing code here
        GraphicsDevice.Clear(Color.CornflowerBlue);
        _spriteBatch.Begin();

        _currentSprite.Draw(_spriteBatch, _mario, _marioPosition, _marioSourceRectangle, _frameMario);
        _text.Draw(_spriteBatch, _font, _textPosition, _marioSourceRectangle, _frameMario);


        _spriteBatch.End();
        base.Draw(gameTime);
    }
}

