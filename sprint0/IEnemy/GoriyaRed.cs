

using System;
using System.Security.Cryptography.X509Certificates;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using static System.Formats.Asn1.AsnWriter;
using static sprint0.Link;



namespace sprint0
{


    //non-moving,non-animated sprite
    public class GoriyaRed : MovingAnimatedEnemy
    {



        public GoriyaRed(Texture2D textureSheet, Rectangle positionRectangle) : base(textureSheet, positionRectangle)
        {

            speed = 2;
            originalSpeed = 2;
        }




        protected void FrameUpdate(int startIndex, int endIndex)
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





        public override void EnemyUpdate(Game1 game)
        {
            base.EnemyUpdate(game);
            MovementChoice();
            if (this.direction == Direction.Left) { FrameUpdate(0, 2); }
            else if (this.direction == Direction.Right) { FrameUpdate(2, 4); }
            else if (this.direction == Direction.Down) { FrameUpdate(4, 6); }
            else if (this.direction == Direction.Up) { FrameUpdate(6, 8); }
            PositionUpdate();



        }
    }
}



