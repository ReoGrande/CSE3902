using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using static sprint0.Link;


namespace sprint0
{
    public interface INPC
    {
        void NPCUpdate(Game1 game);
        void NPCDraw(SpriteBatch _spriteBatch);



        Rectangle GetPosition();






    }


    public class NPC : INPC
    {


        protected Rectangle positionRectangle;
        protected Texture2D NPCTextureSheet;
        protected Rectangle rangeInSheet;

        protected Color color;

        public NPC(Texture2D textureSheet, Rectangle positionRectangle)
        {
            NPCTextureSheet = textureSheet;
            this.positionRectangle = positionRectangle;
            this.rangeInSheet = new Rectangle(0, 0, textureSheet.Width, textureSheet.Height);
            color = Color.White;
        }



        public void NPCUpdate(Game1 game)
        { }




        public Rectangle GetPosition() { return positionRectangle; }

        public Rectangle ChangePosition(Rectangle newPosition)
        {
            this.positionRectangle = newPosition;
            return this.positionRectangle;
        }


        public void NPCDraw(SpriteBatch _spriteBatch)
        {

            _spriteBatch.Draw(
            NPCTextureSheet,
            positionRectangle,
            rangeInSheet,
            color
            );


        }
    }





}


