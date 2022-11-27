
using System;
using System.Security.Cryptography.X509Certificates;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using static System.Formats.Asn1.AsnWriter;
using static sprint0.Link;




namespace sprint0
{


    //non-moving,non-animated sprite
    public class Ghost : MovingAnimatedEnemy
    {



        public Ghost(Texture2D textureSheet, Rectangle positionRectangle) : base(textureSheet, positionRectangle)
        {

        }


        public override void EnemyUpdate(Game1 game)
        {
            base.EnemyUpdate(game);
            MovementChoice();
            int number = textureSheetList.Count;
            FrameUpdate(0, number);
            PositionUpdate();

        }
    }


}

