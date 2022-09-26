

using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections;
using System.ComponentModel;
using System.Reflection.Metadata;
using Microsoft.Xna.Framework.Input;
using System.Runtime.InteropServices;
using static System.Formats.Asn1.AsnWriter;
using static sprint0.MoveableItem;



namespace sprint0
{
    public interface IMovingItemState
    {
        void ToMoving();
        void ToStatic();
        void Update();
        // Draw() might also be included here
    }



    public class MoveableItem : Item1
    {
        public IMovingItemState state;
        protected Rectangle rangeInSheet;
        public enum Direction { Up, Down, Left, Right };    // Directions in which the Item is moving
        public Direction direction;
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


        public void AddFrames()
        {

            //TODO:If the item is animated, it needs more frames.
        }

        public override void Update()
        {

            state.Update();
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

        public void Update()
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

        public void Update()
        {
            //As this is a static item, nothing needs to be updated
        }

    }



}