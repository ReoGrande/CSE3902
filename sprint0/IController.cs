using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace sprint0
{
    interface IController
    {
        ISprite Update(ISprite input);
        /*After any click on keyboard or mouse,
         *the appropriate controller will be utilized.*/
        //TODO: IMPLEMENT UPDATE METHODS
    }

    public class IKeyboard : IController
    {
        KeyboardState press;

        public ISprite Update(ISprite input)
        {
            press = Keyboard.GetState();
            if (press.GetPressedKeyCount() > 0)
            {
                Keys[] allKeys = press.GetPressedKeys();


                if (allKeys[0].Equals(Keys.D1))
                {
                    Console.WriteLine("1 Pressed");
                    return new nnSprite();

                }
                else if (allKeys[0].Equals(Keys.D2))
                {
                    Console.WriteLine("2 Pressed");
                    return new naSprite();

                }
                else if (allKeys[0].Equals(Keys.D3))
                {
                    Console.WriteLine("3 Pressed");
                    return new anSprite();
                }
                else if (allKeys[0].Equals(Keys.D4))
                {
                    Console.WriteLine("4 Pressed");
                    return new aaSprite();
                }
            }
            return input;
            
        }

        

    }

    public class IMouse : IController
    {
        MouseState press;

        public ISprite Update(ISprite input)
        {
            press = Mouse.GetState();
            
            if (press.LeftButton == ButtonState.Pressed
                || press.RightButton == ButtonState.Pressed)
            {
                if (press.LeftButton == ButtonState.Pressed)
                {
                    Point spot = press.Position;
                    if (spot.X <= 400 && spot.Y < 200)
                    {
                        Console.WriteLine("TOP LEFT");
                        return new nnSprite();
                    }
                    else if (spot.X <= 400 && spot.Y >= 200)
                    {
                        Console.WriteLine("BOTTOM LEFT");
                        return new anSprite();
                    }
                    else if (spot.X > 400 && spot.Y < 200)
                    {
                        Console.WriteLine("TOP RIGHT");
                        return new naSprite();
                    }
                    else if (spot.X > 400 && spot.Y >= 200)
                    {
                        Console.WriteLine("BOTTOM RIGHT");
                        return new aaSprite();
                    }
                }
                return new nnSprite();
            }
            return input;
        }

        
    }
}

