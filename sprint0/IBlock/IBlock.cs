

using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections;
using System.ComponentModel;
using System.Reflection.Metadata;
using Microsoft.Xna.Framework.Input;
using System.Runtime.InteropServices;
using static System.Formats.Asn1.AsnWriter;

public interface ISprite
{

    void SpriteUpdate(GraphicsDeviceManager _graphics, GameTime gameTime);
    void SpriteInitialize(GraphicsDeviceManager _graphics);
    void SpriteLoadContent(Object content);
    void SpriteDraw(SpriteBatch _spriteBatch);


}

public abstract class IBlock : ISprite
{

    protected Vector2 spritePosition;
    public abstract void SpriteUpdate(GraphicsDeviceManager _graphics, GameTime gameTime);
    public abstract void SpriteInitialize(GraphicsDeviceManager _graphics);
    public abstract void SpriteLoadContent(Object content);
    public abstract void SpriteDraw(SpriteBatch _spriteBatch);
}



//non-moving,non-animated sprite
public class NMNASprite : IBlock
{
    Texture2D textureSheet;


    public override void SpriteUpdate(GraphicsDeviceManager _graphics, GameTime gameTime)
    {
        Console.WriteLine("non-moving and non-animated");
    }
    public override void SpriteInitialize(GraphicsDeviceManager _graphics)
    {
        spritePosition = new Vector2(_graphics.PreferredBackBufferWidth / 2,
                _graphics.PreferredBackBufferHeight / 2);
    }
    public override void SpriteLoadContent(Object content)
    {
        textureSheet = (Texture2D)content;
    }

    public override void SpriteDraw(SpriteBatch _spriteBatch)
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





//non-moving,animated sprite
public class NMASprite : IBlock
{
    Texture2D textureSheet;
    int currentState;
    double lastTime;
    ArrayList sourceRectangleArray;




    public override void SpriteInitialize(GraphicsDeviceManager _graphics)
    {
        spritePosition = new Vector2(_graphics.PreferredBackBufferWidth / 2,
                _graphics.PreferredBackBufferHeight / 2);
        currentState = 0;
        lastTime = 0;
        sourceRectangleArray = new ArrayList();
        sourceRectangleArray.Add(new Rectangle(85, 50, 25, 35));
        sourceRectangleArray.Add(new Rectangle(115, 50, 25, 35));
        sourceRectangleArray.Add(new Rectangle(145, 50, 25, 35));
        sourceRectangleArray.Add(new Rectangle(175, 50, 25, 35));
        sourceRectangleArray.Add(new Rectangle(205, 50, 25, 35));
        sourceRectangleArray.Add(new Rectangle(235, 50, 25, 35));
        sourceRectangleArray.Add(new Rectangle(265, 50, 25, 35));
        sourceRectangleArray.Add(new Rectangle(295, 50, 25, 35));

    }

    public override void SpriteLoadContent(Object content)
    {
        textureSheet = (Texture2D)content;
    }

    public override void SpriteUpdate(GraphicsDeviceManager _graphics, GameTime gameTime)
    {
        Console.WriteLine("non-moving and animated");
        int numberOfArray = sourceRectangleArray.Count;

        double currentTime = gameTime.TotalGameTime.TotalSeconds;
        double timeDifference = 0.2;//the time difference set between different frames
        if (currentTime - lastTime >= timeDifference)
        {
            lastTime = currentTime;
            if (currentState < numberOfArray - 1)
            { currentState++; }
            else
            { currentState = 0; }
        }
    }

    public override void SpriteDraw(SpriteBatch _spriteBatch)
    {

        _spriteBatch.Draw(
        textureSheet,
        spritePosition,
        (Rectangle)sourceRectangleArray[currentState],
        Color.White,
        0f,
        new Vector2(25 / 2, 35 / 2),
        new Vector2(2, 2),
        SpriteEffects.None,
        0f
        ); ;
    }
}

//moving,non-animated sprite
public class MNASprite : IBlock
{
    Texture2D spriteTexture;
    Vector2 scale;
    float speed;
    int direction;//0 means up, 1 means down
    public override void SpriteUpdate(GraphicsDeviceManager _graphics, GameTime gameTime)
    {
        Console.WriteLine("moving and non-animated");
        if (direction == 0)
        {
            spritePosition.Y -= speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
        }
        else if (direction == 1)
        {
            spritePosition.Y += speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
        }
        if (spritePosition.Y > _graphics.PreferredBackBufferHeight - 35 * scale.Y / 2)
        {
            direction = 0;
        }
        else if (spritePosition.Y < 35 * scale.Y / 2)
        { direction = 1; }
    }
    public override void SpriteInitialize(GraphicsDeviceManager _graphics)
    {
        spritePosition = new Vector2(_graphics.PreferredBackBufferWidth / 2,
                _graphics.PreferredBackBufferHeight / 2);
        speed = 150f;
        direction = 0;
        scale = new Vector2(2, 2);
    }

    public override void SpriteLoadContent(Object content)
    {
        spriteTexture = (Texture2D)content;
    }

    public override void SpriteDraw(SpriteBatch _spriteBatch)
    {

        _spriteBatch.Draw(
        spriteTexture,
        spritePosition,
        new Rectangle(175, 50, 25, 35),
        Color.White,
        0f,
        new Vector2(25 / 2, 35 / 2),
        scale,
        SpriteEffects.None,
        0f
        );
    }
}

//moving,animated sprite
public class MASprite : IBlock
{
    Texture2D textureSheet;
    ArrayList sourceRectangleArray;
    int currentState;
    double lastTime;
    float speed;
    int numberOfArray;
    int frameGourp1;//the number of frame used for left moving
    int direction;//0 means left, 1 means right

    Vector2 scale;


    public override void SpriteInitialize(GraphicsDeviceManager _graphics)
    {
        spritePosition = new Vector2(_graphics.PreferredBackBufferWidth / 2,
                _graphics.PreferredBackBufferHeight / 2);
        currentState = 0;
        lastTime = 0;
        speed = 150f;
        direction = 0;
        scale = new Vector2(2, 2);

        sourceRectangleArray = new ArrayList();
        sourceRectangleArray.Add(new Rectangle(85, 50, 25, 35));
        sourceRectangleArray.Add(new Rectangle(115, 50, 25, 35));
        sourceRectangleArray.Add(new Rectangle(145, 50, 25, 35));
        sourceRectangleArray.Add(new Rectangle(175, 50, 25, 35));
        sourceRectangleArray.Add(new Rectangle(205, 50, 25, 35));
        sourceRectangleArray.Add(new Rectangle(235, 50, 25, 35));
        sourceRectangleArray.Add(new Rectangle(265, 50, 25, 35));
        sourceRectangleArray.Add(new Rectangle(295, 50, 25, 35));

    }

    public override void SpriteLoadContent(Object content)
    {
        textureSheet = (Texture2D)content;
        numberOfArray = sourceRectangleArray.Count;
        frameGourp1 = numberOfArray / 2;
    }

    public override void SpriteUpdate(GraphicsDeviceManager _graphics, GameTime gameTime)
    {
        Console.WriteLine("non-moving and animated");
        frameUpdate(gameTime);
        positionUpdate(_graphics, gameTime);

    }

    protected void frameUpdate(GameTime gameTime)
    {

        double currentTime = gameTime.TotalGameTime.TotalSeconds;
        double timeDifference = 0.2;//the time difference set between different frames
        if (currentTime - lastTime >= timeDifference)
        {
            lastTime = currentTime;
            //left
            if (direction == 0)
            {
                if (currentState < frameGourp1 - 1)
                { currentState++; }
                else
                { currentState = 0; }
            }
            //right
            else
            {
                if ((currentState < numberOfArray - 1) && (currentState > frameGourp1 - 1))
                { currentState++; }
                else
                { currentState = frameGourp1; }
            }

        }

    }
    protected void positionUpdate(GraphicsDeviceManager _graphics, GameTime gameTime)
    {


        if (direction == 0)
        {
            spritePosition.X -= speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
        }
        else
        {
            spritePosition.X += speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
        }
        if (spritePosition.X > _graphics.PreferredBackBufferWidth - 25 * scale.X / 2)
        { direction = 0; }
        else if (spritePosition.X < 35 * scale.X / 2)
        { direction = 1; }

    }



    public override void SpriteDraw(SpriteBatch _spriteBatch)
    {

        _spriteBatch.Draw(
        textureSheet,
        spritePosition,
        (Rectangle)sourceRectangleArray[currentState],
        Color.White,
        0f,
        new Vector2(25 / 2, 35 / 2),
        scale, SpriteEffects.None,
        0f
        );
    }
}


//text Sprite
public class TextSprite : IBlock
{

    SpriteFont font;

    float maxX;
    float maxY;


    public override void SpriteUpdate(GraphicsDeviceManager _graphics, GameTime gameTime)
    {
        Console.WriteLine("TextSprite.");
    }
    public override void SpriteInitialize(GraphicsDeviceManager _graphics)
    {
        maxX = _graphics.PreferredBackBufferWidth;
        maxY = _graphics.PreferredBackBufferHeight;

    }

    public override void SpriteLoadContent(Object content)
    {
        font = (SpriteFont)content;

    }

    public override void SpriteDraw(SpriteBatch _spriteBatch)
    {


        drawSingleText(new Vector2(maxX * 0.25f, maxY * (0.55f)), "Credits", _spriteBatch);
        drawSingleText(new Vector2(maxX * 0.25f, maxY * (0.62f)), "Program Made By: MICHAEL YANG", _spriteBatch);
        drawSingleText(new Vector2(maxX * 0.25f, maxY * (0.69f)), "Sprites from: https://www.mariomayhem.com/downloads/", _spriteBatch);
        drawSingleText(new Vector2(maxX * 0.25f, maxY * (0.76f)), "sprites//smb1/smb_luigi_sheet.png", _spriteBatch);
    }

    protected void drawSingleText(Vector2 position, String content, SpriteBatch _spriteBatch)
    {


        //position = new Vector2(100, 100);
        _spriteBatch.DrawString(font, content, position, Color.Black);
    }

}

