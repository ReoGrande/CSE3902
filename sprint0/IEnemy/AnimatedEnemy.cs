﻿

using System;
using System.Security.Cryptography.X509Certificates;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using static System.Formats.Asn1.AsnWriter;




namespace sprint0
{



    public class AnimatedEnemy : Enemy1
    {

        public List<Texture2D> textureSheetList;
        public int timer;
        private int index;//which frame is shown

        public AnimatedEnemy(Texture2D textureSheet, Rectangle positionRectangle) : base(textureSheet, positionRectangle)
        {

            textureSheetList = new List<Texture2D>();
            textureSheetList.Add(EnemyTextureSheet);
            timer = 0;
            index = 0;
        }


        public void AddFrames(Texture2D textureSheet)
        {

            textureSheetList.Add(textureSheet);
        }



        public override void EnemyUpdate()
        {
            int number = textureSheetList.Count;


            if (timer >= 9)
            {
                {

                    if (index < number - 1)
                    { index++; }
                    else
                    { index = 0; }

                    EnemyTextureSheet = textureSheetList[index];
                    rangeInSheet = new Rectangle(0, 0, EnemyTextureSheet.Width, EnemyTextureSheet.Height);
                    timer = 0;
                }
            }
            else { timer++; }
        }



    }

}
