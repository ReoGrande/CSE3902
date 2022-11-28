
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
            touchable = false;
            movePattern = (new Random().Next() % 4) + 1;
            speed = 3;
            //movePattern = (new Random().Next() % 4) + 1;
        }



    }


}

