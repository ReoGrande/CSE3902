

using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using static System.Formats.Asn1.AsnWriter;
using static sprint0.Link;



namespace sprint0
{
    public interface IMovingItemState
    {
        void ToMoving();
        void ToStatic();
        void Update(int x, int y);
        // Draw() might also be included here
    }



    public class MoveableItem : StaticItem
    {
        public IMovingItemState state;
        public int speed;




        public MoveableItem(Texture2D textureSheet, Rectangle positionRectangle)
        {
            ItemTextureSheet = textureSheet;
            state = new StaticItemState(this);
            this.positionRectangle = positionRectangle;
            this.rangeInSheet = new Rectangle(0, 0, textureSheet.Width, textureSheet.Height);
            speed = 4;
            direction = Direction.Up;
            this.moveable = true;

        }




        public override void Update(int x, int y)
        {

            state.Update(x, y);
        }

        public override void ToMoving()
        {
            state.ToMoving();
        }

        public void ToStatic()
        {

            state.ToStatic();
        }

        public override void ItemDraw(SpriteBatch _spriteBatch)
        {

            _spriteBatch.Draw(
            ItemTextureSheet,
            positionRectangle,
            rangeInSheet,
            Color.White
            );
        }
    }





    public class MovingItemState : IMovingItemState
    {
        private MoveableItem moveableItem;

        public MovingItemState(MoveableItem moveableItem)
        {
            this.moveableItem = moveableItem;
        }

        public void ToMoving()
        {
        }

        public void ToStatic()
        {
            moveableItem.state = new StaticItemState(moveableItem);
        }

        public void Update(int x, int y)
        {
            switch (moveableItem.direction)
            {
                case Direction.Up:
                    moveableItem.positionRectangle.Y -= moveableItem.speed;
                    break;

                case Direction.Down:
                    moveableItem.positionRectangle.Y += moveableItem.speed;
                    break;

                case Direction.Left:
                    moveableItem.positionRectangle.X -= moveableItem.speed;
                    break;

                case Direction.Right:
                    moveableItem.positionRectangle.X += moveableItem.speed;
                    break;
                default:
                    Console.WriteLine("Error: Incorrect command to change Link State.");
                    return;
            }
        }
    }

    public class StaticItemState : IMovingItemState
    {
        private MoveableItem moveableItem;


        public StaticItemState(MoveableItem moveableItem)
        {
            this.moveableItem = moveableItem;

        }
        public void ToMoving()
        {
            moveableItem.state = new MovingItemState(moveableItem);
        }

        public void ToStatic()
        {

        }

        public void Update(int x, int y)
        {
            moveableItem.positionRectangle.X = x;
            moveableItem.positionRectangle.Y = y;

        }

    }



}