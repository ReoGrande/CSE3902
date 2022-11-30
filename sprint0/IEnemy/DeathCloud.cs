

using System;
using System.Security.Cryptography.X509Certificates;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using static System.Formats.Asn1.AsnWriter;
using static sprint0.Link;



namespace sprint0
{


    //non-moving,non-animated sprite
    public class DeathCloud : MovingAnimatedEnemy
    {

        int movingTimer;

        public DeathCloud(Texture2D textureSheet, Rectangle positionRectangle) : base(textureSheet, positionRectangle)
        {
            movingTimer = 0;
            touchable = false;
            attackable = false;
            isDeathCloud = true;
            
        }




        private void FrameUpdate(int startIndex, int endIndex)
        {

            if (timer >= 12)
            {
                {

                    if (index < endIndex - 1)
                    { index++; }
                    else
                    {
                        index = startIndex;
                        this.needTObeRemoved = true;
                    }

                    EnemyTextureSheet = textureSheetList[index];
                    rangeInSheet = new Rectangle(0, 0, EnemyTextureSheet.Width, EnemyTextureSheet.Height);
                    timer = 0;
                }
            }
            else { timer++; }
        }




        public override void EnemyUpdate(Game1 game)
        {
            state.Update();
            FrameUpdate(0, 3);


        }
    }
}


