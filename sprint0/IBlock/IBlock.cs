

using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections;
using System.ComponentModel;
using System.Reflection.Metadata;
using Microsoft.Xna.Framework.Input;
using System.Runtime.InteropServices;
using static System.Formats.Asn1.AsnWriter;

public interface IBlock
{
    void BlockUpdate(GraphicsDeviceManager _graphics, GameTime gameTime);
    void BlockLoadContent(Object content);
    void BlockDraw(SpriteBatch _spriteBatch);
}

public abstract class Block : IBlock
{

    protected Vector2 spritePosition;
    protected Texture2D textureSheet;


    public abstract void BlockUpdate(GraphicsDeviceManager _graphics, GameTime gameTime);
    public abstract void BlockLoadContent(Object content);
    public abstract void BlockDraw(SpriteBatch _spriteBatch);
}


//non-moving,non-animated sprite
public class Block1 : Block
{
    
        public Block1(GraphicsDeviceManager _graphics)
	{
		spritePosition = new Vector2(_graphics.PreferredBackBufferWidth / 2,
                _graphics.PreferredBackBufferHeight / 2);
	}

    public override void  BlockUpdate(GraphicsDeviceManager _graphics, GameTime gameTime)
    {
        //TODO: IMPLEMENT UPDATE METHODS? MAYBE
    }


    public override void BlockLoadContent(Object content)
    {
        textureSheet = (Texture2D)content;
    }

    public override void BlockDraw(SpriteBatch _spriteBatch)
    {

        _spriteBatch.Draw(
        textureSheet,
        spritePosition,
        new Rectangle(175, 50, 25, 35),
        Color.White,
        0f,
        new Vector2(25 / 2, 35 / 2),
        new Vector2(2, 2), SpriteEffects.None,
        0f
        );
    }
}


