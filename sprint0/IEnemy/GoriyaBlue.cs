

using System;
using System.Security.Cryptography.X509Certificates;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using static System.Formats.Asn1.AsnWriter;
using static sprint0.Link;



namespace sprint0
{


    //non-moving,non-animated sprite
    public class GoriyaBlue : MovingAnimatedEnemy
    {

        int movingTimer;

        public GoriyaBlue(Texture2D textureSheet, Rectangle positionRectangle) : base(textureSheet, positionRectangle)
        {
            movingTimer = 0;
            speed = 2;
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


        private Direction oppositeDirection(Direction direction)
        {
            Direction result = Direction.Left;
            if (direction == Direction.Left)
            {
                result = Direction.Right;
            }
            return result;
        }


        public override void EnemyUpdate(Game1 game)
        {
            base.EnemyUpdate(game);
            if (movingTimer >= 100)
            {
                this.direction = oppositeDirection(this.direction);
                movingTimer = 0;
            }
            else
            {
                movingTimer++;
            }
            if (this.direction == Direction.Left) { FrameUpdate(0, 2); }
            else if (this.direction == Direction.Right) { FrameUpdate(2, 4); }
            PositionUpdate();



        }
    }
}


