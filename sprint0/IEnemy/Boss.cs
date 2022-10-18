

using System;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using static System.Formats.Asn1.AsnWriter;




namespace sprint0
{



    public class Boss : AnimatedEnemy
    {


        int attackTimer;
        public Boss(Texture2D textureSheet, Rectangle positionRectangle) : base(textureSheet, positionRectangle)
        {
            attackTimer = 0;
        }




        public void attack(Game1 game)
        {
            IItem fireBall = ItemFactory.Instance.CreateFireBall(new Rectangle(this.positionRectangle.X-30, this.positionRectangle.Y, 25, 25));
            fireBall.ToMoving();
            game.outItemSpace.Add(fireBall);

        }

        public override void EnemyUpdate(Game1 game)
        {
            base.EnemyUpdate(game);
            if (attackTimer >= 40)
            {

                attack(game);
                attackTimer = 0;

            }
            else { attackTimer++; }
        }

    }



}


