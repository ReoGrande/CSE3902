using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace sprint0
{
    public interface ISprite
    {
        void Draw(SpriteBatch source, Object mario, Object position, Rectangle[] marioSourceRectangle, int frameMario);
        int checkTime(int frame, int length);
        Rectangle position(Rectangle current, bool goingLeft);
        //TODO: IMPLEMENT UPDATE METHODS? MAYBE

    }

    //non-moving, non-animated sprite 
    public class nnSprite:ISprite
    {

        public void Draw(SpriteBatch source,  Object mario, Object position, Rectangle[] marioSourceRectangle, int frameMario)
        {
            source.Draw((Texture2D)mario, (Rectangle)position, marioSourceRectangle[frameMario], Color.White);
        }

        public int checkTime(int frame, int length)
        {
            return frame;
        }

        public Rectangle position(Rectangle current, bool goingLeft)
        {
            return current;
        }
    }
    //non-moving but animated sprite 
    public class naSprite : ISprite
    {
        public void Draw(SpriteBatch source, Object mario, Object position, Rectangle[] marioSourceRectangle, int frameMario)
        {
            if(frameMario > marioSourceRectangle.Length)
            {
                frameMario = 0;
            }
            source.Draw((Texture2D)mario, (Rectangle)position, marioSourceRectangle[frameMario], Color.White);
            

        }
        public int checkTime(int frame, int length)
        {
           
            return (frame+1)%length;
        }

        public Rectangle position(Rectangle current, bool goingLeft)
        {
            return current;
        }
    }

    //moving but not animated sprite
    public class anSprite : ISprite
    {
        private int _speed = 60;
        public void Draw(SpriteBatch source, Object mario, Object position, Rectangle[] marioSourceRectangle, int frameMario)
        {
            
            source.Draw((Texture2D)mario, (Rectangle)position, marioSourceRectangle[frameMario], Color.White);
        }
        public int checkTime(int frame, int length)
        {
            //NEEDS TO CHANGE POSITION OF SPRITE INSTEAD OF ANIMATION
            
            return frame;
        }
        public Rectangle position(Rectangle current, bool goingUp)
        {
            if (goingUp)
            {
                current.Y -= _speed;
            }
            else
            {
                current.Y += _speed;
            }

            return current;
        }
    }
    //moving and animated sprite
    public class aaSprite : ISprite
    {
        private int _speed = 60;

        public void Draw(SpriteBatch source, Object mario, Object position, Rectangle[] marioSourceRectangle, int frameMario)
        {
            if (frameMario > marioSourceRectangle.Length)
            {
                frameMario = 0;
            }
            source.Draw((Texture2D)mario, (Rectangle)position, marioSourceRectangle[frameMario], Color.White);
        }
        public int checkTime(int frame, int length)
        {

            return (frame + 1) % length;
        }

        public Rectangle position(Rectangle current, bool goingLeft)
        {
            if (goingLeft)
            {
                current.X -= _speed;
            }
            else
            {
                current.X += _speed;
            }

            return current;
        }
    }

    //text sprite
    public class textSprite : ISprite
    {

        public void Draw(SpriteBatch source, Object text, Object position, Rectangle[] sourceRectangle, int frame)
        {
            Vector2 newPosition = ((Vector2)position);
            source.DrawString((SpriteFont)text, "Credits:", newPosition, Color.Black);
            newPosition.Y = newPosition.Y + 20;
            source.DrawString((SpriteFont)text, "Program Made By: Rehoboth (Reo) Ogundare", newPosition, Color.Black);
            newPosition.Y = newPosition.Y + 20;
            source.DrawString((SpriteFont)text, "Sprites From: https://www.mariomayhem.com/downloads/sprites/super_mario_bros_sprites.php", newPosition, Color.Black);

        }
        public int checkTime(int frame, int length)
        {
            return 0;
        }
        public Rectangle position(Rectangle current, bool goingLeft)
        {
            return new Rectangle();
        }
    }
}

