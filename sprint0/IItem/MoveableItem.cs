

using System;
using System.Security.Cryptography.X509Certificates;
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
        void Update(Rectangle position);
        // Draw() might also be included here
    }



    public class MoveableItem : StaticItem
    {
        public IMovingItemState state;
        public double speed;





        public MoveableItem(Texture2D textureSheet, Rectangle positionRectangle) : base(textureSheet, positionRectangle)
        {

            state = new StaticItemState(this);
            speed = 4;
            damage =-2;
            direction = Direction.Left;
            this.throwable = true;
            this.moveable = true;

        }

        public override IItem Clone()
        {
            IItem itemClone = new MoveableItem(this.ItemTextureSheet, this.positionRectangle);
            return itemClone;

        }

        public override void Update(Game1 game, Rectangle position)
        {
            CheckOutOfBound(game);
            if (this.attribute == ItemAttribute.WaitForPick)
            {
                state.Update(this.GetPosition());
            }
            else
            {
                state.Update(position);
            }
        }

        public override void ToMoving()
        {
            state.ToMoving();
        }

        public void ToStatic()
        {

            state.ToStatic();
        }
        public Direction OppositeDirection(Direction direction)
        {
            Direction result = Direction.Up;
            if (direction == Direction.Up) { result = Direction.Down; }
            else if (direction == Direction.Left) { result = Direction.Right; }
            else if (direction == Direction.Right) { result = Direction.Left; }
            return result;


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

        public void Update(Rectangle position)
        {
            switch (moveableItem.direction)
            {
                case Direction.Up:
                    moveableItem.positionRectangle.Y -= (int)moveableItem.speed;
                    break;

                case Direction.Down:
                    moveableItem.positionRectangle.Y += (int)moveableItem.speed;
                    break;

                case Direction.Left:
                    moveableItem.positionRectangle.X -= (int)moveableItem.speed;
                    break;

                case Direction.Right:
                    moveableItem.positionRectangle.X += (int)moveableItem.speed;
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




        public void Update(Rectangle position)
        {
            moveableItem.positionRectangle.X = position.X;
            moveableItem.positionRectangle.Y = position.Y;

        }

    }



}