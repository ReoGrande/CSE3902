

using System;
using System.Reflection;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using sprint0;
using static System.Formats.Asn1.AsnWriter;
using static sprint0.Arrow;
using static sprint0.Link;



namespace sprint0
{

    public class Arrow : MoveableItem
    {

        public List<Texture2D> textureSheetList;

        public Arrow(Texture2D textureSheet, Rectangle positionRectangle) : base(textureSheet, positionRectangle)
        {
            state = new StaticArrowState(this);
            textureSheetList = new List<Texture2D>();
            textureSheetList.Add(textureSheet);
            this.infinite = true;
        }

        public override IItem Clone()
        {
            IItem itemClone = ItemFactory.Instance.CreateArrow(this.positionRectangle);

            return itemClone;

        }


        public void AddFrames(Texture2D textureSheet)
        {

            textureSheetList.Add(textureSheet);
        }

        private void FrameUpdate()

        {
            int number = textureSheetList.Count;


        }






        public class MovingArrowState : IMovingItemState
        {
            private Arrow arrow;


            public MovingArrowState(Arrow arrow)
            {
                this.arrow = arrow;


            }

            public void ToMoving()
            {
            }

            public void ToStatic()
            {
                arrow.state = new StaticArrowState(arrow);
            }

            public void Update(int x, int y)
            {
                arrow.FrameUpdate();
                switch (arrow.direction)
                {
                    case Direction.Up:
                        arrow.positionRectangle.Y -= arrow.speed;
                        arrow.ItemTextureSheet = arrow.textureSheetList[0];
                        break;

                    case Direction.Down:
                        arrow.positionRectangle.Y += arrow.speed;
                        arrow.ItemTextureSheet = arrow.textureSheetList[1];
                        break;

                    case Direction.Left:
                        arrow.positionRectangle.X -= arrow.speed;
                        arrow.ItemTextureSheet = arrow.textureSheetList[2];
                        break;

                    case Direction.Right:
                        arrow.positionRectangle.X += arrow.speed;
                        arrow.ItemTextureSheet = arrow.textureSheetList[3];
                        break;
                    default:
                        Console.WriteLine("Error: Incorrect command to change Link State.");
                        return;
                }
                arrow.rangeInSheet = new Rectangle(0, 0, arrow.ItemTextureSheet.Width, arrow.ItemTextureSheet.Height);

            }


        }
    }

    public class StaticArrowState : IMovingItemState
    {
        private Arrow arrow;


        public StaticArrowState(Arrow arrow)
        {
            this.arrow = arrow;

        }
        public void ToMoving()
        {
            arrow.state = new MovingArrowState(arrow);
        }

        public void ToStatic()
        {

        }

        public void Update(int x, int y)
        {
            arrow.positionRectangle.X = x;
            arrow.positionRectangle.Y = y;

        }

    }
}



