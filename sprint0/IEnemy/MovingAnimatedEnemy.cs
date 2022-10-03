

using System;
using System.Security.Cryptography.X509Certificates;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using static System.Formats.Asn1.AsnWriter;
using static sprint0.Link;




namespace sprint0
{


    //non-moving,non-animated sprite
    public class MovingAnimatedEnemy : Enemy1
    {

        public List<Texture2D> textureSheetList;
        public int timer;
        protected int index;//which frame is shown
        protected int speed;

        public MovingAnimatedEnemy(Texture2D textureSheet, Rectangle positionRectangle) : base(textureSheet, positionRectangle)
        {

            textureSheetList = new List<Texture2D>();
            textureSheetList.Add(EnemyTextureSheet);
            timer = 0;
            index = 0;
            speed = 4;
            this.direction = Direction.Left;
        }


        public void AddFrames(Texture2D textureSheet)
        {

            textureSheetList.Add(textureSheet);
        }

        private void FrameUpdate(int startIndex, int endIndex)
        {

            if (timer >= 6)
            {
                {

                    if (index < endIndex - 1)
                    { index++; }
                    else
                    { index = startIndex; }

                    EnemyTextureSheet = textureSheetList[index];
                    rangeInSheet = new Rectangle(0, 0, EnemyTextureSheet.Width, EnemyTextureSheet.Height);
                    timer = 0;
                }
            }
            else { timer++; }
        }


        public void PositionUpdate()
        {
            switch (this.direction)
            {
                case Direction.Up:
                    this.positionRectangle.Y -= this.speed;
                    break;

                case Direction.Down:
                    this.positionRectangle.Y += this.speed;
                    break;

                case Direction.Left:
                    this.positionRectangle.X -= this.speed;
                    break;

                case Direction.Right:
                    this.positionRectangle.X += this.speed;
                    break;
                default:
                    Console.WriteLine("Error: Incorrect command to change Link State.");
                    return;
            }

        }
        public override void EnemyUpdate(Game1 game)
        {
            int number = textureSheetList.Count;
            FrameUpdate(0, number);

        }
    }
}
