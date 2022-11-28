

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


        protected int timer;
        protected int endTime;
        public int startTime;
        protected int flyTimeWithoutCollision;
        public int flyTime;
        public int timeCount;
        protected bool firstBounce;
        protected int maxLength;
        protected double acceleration;


        private int index;//which frame is shown
        public ShieldBall(Texture2D textureSheet, Rectangle positionRectangle) : base(textureSheet, positionRectangle)
        {
            state = new StaticShieldBallState(this);

            timer = 0;
            index = 0;
            speed = 10;
            number = 10;
            flyTimeWithoutCollision = 1200;
            flyTime = flyTimeWithoutCollision;
            pickable = true;
            firstBounce = true;
            endTime = System.Environment.TickCount;
            startTime = System.Environment.TickCount;
            timeCount = System.Environment.TickCount;
            maxLength = 200;
            acceleration = speed / flyTimeWithoutCollision;

        }


        public override IItem Clone()
        {
            IItem itemClone = ItemFactory.Instance.CreateShieldBall(this.positionRectangle);

            return itemClone;
        }


        public override void CollisionWithNormalBlock()
        {


        }


        public override void CollisionWithEnemy(IEnemy enemy)
        {
            if (this.attribute == ItemAttribute.FriendlyAttack && enemy.Touchable() && firstBounce)
            {
                enemy.GetDamaged();
                enemy.ChangeHP(-1);
                this.direction = OppositeDirection(this.direction);
                firstBounce = false;
                flyTime = System.Environment.TickCount - startTime;
                SoundFactory.Instance.PlaySoundEnemyHit();
            }
        }

        public override void CollisionWithLink(ILinkState link, ItemSpace itemSpace)
        {

            endTime = System.Environment.TickCount;
            int runTime = endTime - startTime;

            if (attribute == ItemAttribute.AdverseAttack)
            {
                link.TakeDamage();

                Damage();
            }
            if (pickable && runTime > 400)
            {

                int itemLocation = existInSpace(itemSpace);
                if (itemLocation < 0)
                {
                    itemSpace.Add(this.Clone());
                }
                else
                {
                    itemSpace.ItemList()[itemLocation].NumberChange(1);
                }

                Damage();

            }
        }







        private void FrameUpdate()

        {
            int number = textureSheetList.Count;


            if (timer >= 5)
            {
                {

                    if (index < number - 1)
                    { index++; }
                    else
                    { index = 0; }

                    ItemTextureSheet = textureSheetList[index];
                    rangeInSheet = new Rectangle(0, 0, ItemTextureSheet.Width, ItemTextureSheet.Height);
                    timer = 0;
                }
            }
            else { timer++; }

        }
        public override void Use1(Game1 game)
        {
            if (number > 0)
            {
                IItem item = this.Clone(); //ShieldBall Position in ItemList
                item.ChangeDirection(game.character.GetDirection());
                item.ToMoving();
                game.outItemSpace.Add(item);
                game.character.ToThrowing();
                number--;
            }
        }





        public class MovingShieldBallState : IMovingItemState
        {
            private ShieldBall ShieldBall;



            public MovingShieldBallState(ShieldBall ShieldBall)
            {
                this.ShieldBall = ShieldBall;



            }

            public void ToMoving()
            {
            }

            public void ToStatic()
            {
                ShieldBall.state = new StaticItemState(ShieldBall);
            }

            public void Update(int x, int y)
            {
                ShieldBall.FrameUpdate();
                int totalRunTime = System.Environment.TickCount - ShieldBall.startTime;
                int intervalTime = System.Environment.TickCount - ShieldBall.timeCount;
                if (totalRunTime >= ShieldBall.flyTime && ShieldBall.firstBounce)
                {
                    ShieldBall.ChangeDirection(ShieldBall.OppositeDirection(ShieldBall.direction));
                    ShieldBall.firstBounce = false;
                }

                if (ShieldBall.firstBounce) { ShieldBall.speed -= intervalTime * ShieldBall.acceleration; }
                else { ShieldBall.speed += intervalTime * ShieldBall.acceleration; }
                ShieldBall.timeCount = System.Environment.TickCount;
                switch (ShieldBall.direction)
                {

                    case Direction.Up:
                        ShieldBall.positionRectangle.Y -= (int)ShieldBall.speed;
                        break;

                    case Direction.Down:
                        ShieldBall.positionRectangle.Y += (int)ShieldBall.speed;
                        break;

                    case Direction.Left:
                        ShieldBall.positionRectangle.X -= (int)ShieldBall.speed;
                        break;

                    case Direction.Right:
                        ShieldBall.positionRectangle.X += (int)ShieldBall.speed;
                        break;
                    default:
                        Console.WriteLine("Error: Incorrect command to change Link State.");
                        return;
                }

            }


        }
    }

    public class StaticShieldBallState : IMovingItemState
    {
        private ShieldBall ShieldBall;


        public StaticShieldBallState(ShieldBall ShieldBall)
        {
            this.ShieldBall = ShieldBall;

        }
        public void ToMoving()
        {
            ShieldBall.state = new MovingShieldBallState(ShieldBall);
        }

        public void ToStatic()
        {

        }

        public void Update(int x, int y)
        {
            ShieldBall.positionRectangle.X = x;
            ShieldBall.positionRectangle.Y = y;

        }

    }
}




