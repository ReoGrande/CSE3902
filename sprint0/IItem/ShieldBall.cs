

using System;
using System.Reflection;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using sprint0;
using static System.Formats.Asn1.AsnWriter;
using static sprint0.ShieldBall;
using static sprint0.Link;



namespace sprint0
{

    public class ShieldBall : MoveableItem
    {
        int timer;
        public int circleRadius;
        bool forward;
        public int degree;


        private int index;//which frame is shown
        public ShieldBall(Texture2D textureSheet, Rectangle positionRectangle) : base(textureSheet, positionRectangle)
        {
            state = new StaticShieldBallState(this);
            this.infinite = true;
            damage = -2;
            speed = 2;
            timer = 0;
            circleRadius = 100;
            degree = 0;
            forward = true;

        }


        public override IItem Clone()
        {
            IItem itemClone = ItemFactory.Instance.CreateShieldBall(this.positionRectangle);
            return itemClone;
        }


        public override void CollisionWithNormalBlock()
        {


        }
        public override void CollisionWithBound(Rectangle bound)
        {

            //nothing need to do

        }



        public override void CollisionWithEnemy(IEnemy enemy)
        {
            if (enemy.Touchable() && enemy.CanBeAttactedByShieldBall())

            {
                enemy.SetCannotBeAttactedByShieldBall();
                enemy.GetDamaged();
                enemy.ChangeHP(this.damage);
                SoundFactory.Instance.PlaySoundEnemyHit();
            }
        }


        public void FrameUpdate()

        {
            int number = textureSheetList.Count;


            if (timer >= 5)
            {


                if (index < number - 1 && forward == true)
                { index++; }
                else if (index > 0 && forward == false)
                {
                    index--;
                }
                else if (forward)
                {
                    forward = false;
                    index--;

                }
                else if (!forward)
                {
                    forward = true;
                    index++;

                }

                ItemTextureSheet = textureSheetList[index];
                rangeInSheet = new Rectangle(0, 0, ItemTextureSheet.Width, ItemTextureSheet.Height);
                timer = 0;

            }
            else { timer++; }

        }


    }



    public class MovingShieldBallState : IMovingItemState
    {
        private ShieldBall shieldBall;



        public MovingShieldBallState(ShieldBall shieldBall)
        {
            this.shieldBall = shieldBall;



        }

        public void ToMoving()
        {
        }

        public void ToStatic()
        {
            shieldBall.state = new StaticItemState(shieldBall);
        }

        public void Update(Rectangle position)
        {

            int linkCenterX = position.X + position.Width / 2;
            int linkCenterY = position.Y + position.Height / 2;
            shieldBall.degree += (int)shieldBall.speed;
            if (shieldBall.degree > 360)
            {
                shieldBall.degree -= 360;
            }

            double angle = Math.PI * shieldBall.degree / 180.0;
            double sinAngle = Math.Sin(angle);
            double cosAngle = Math.Cos(angle);
            int ballcenterX = (int)(linkCenterX + shieldBall.circleRadius * cosAngle);
            int ballcenterY = (int)(linkCenterY + shieldBall.circleRadius * sinAngle);
            shieldBall.positionRectangle.X = ballcenterX - shieldBall.positionRectangle.Width / 2;
            shieldBall.positionRectangle.Y = ballcenterY - shieldBall.positionRectangle.Height / 2;

            shieldBall.FrameUpdate();

        }







    }

    public class StaticShieldBallState : IMovingItemState
    {
        private ShieldBall shieldBall;


        public StaticShieldBallState(ShieldBall shieldBall)
        {
            this.shieldBall = shieldBall;

        }
        public void ToMoving()
        {
            shieldBall.state = new MovingShieldBallState(shieldBall);
        }

        public void ToStatic()
        {

        }

        public void Update(Rectangle position)
        {
            shieldBall.positionRectangle.X = position.X;
            shieldBall.positionRectangle.Y = position.Y;

        }

    }
}




