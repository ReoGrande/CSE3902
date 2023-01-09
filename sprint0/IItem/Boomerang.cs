

using System;
using System.Reflection;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using sprint0;
using static System.Formats.Asn1.AsnWriter;
using static sprint0.Boomerang;
using static sprint0.Link;



namespace sprint0
{

    public class Boomerang : MoveableItem
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
        public Boomerang(Texture2D textureSheet, Rectangle positionRectangle) : base(textureSheet, positionRectangle)
        {
            state = new StaticBoomerangState(this);
            damage = -2;
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
            specialType = SpecialType.Boomerang;
        }


        public override IItem Clone()
        {
            IItem itemClone = ItemFactory.Instance.CreateWoodenBoomerang(this.positionRectangle);

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
                enemy.ChangeHP(this.damage);
                this.direction = OppositeDirection(this.direction);
                firstBounce = false;
                flyTime = System.Environment.TickCount - startTime;
                SoundFactory.Instance.PlaySoundEnemyHit();
            }
        }


        public override void CollisionWithBound(Rectangle bound)
        {
            if (firstBounce)
            {
                this.direction = OppositeDirection(this.direction);
                firstBounce = false;
                flyTime = System.Environment.TickCount - startTime;

            }
            else
            {

                this.attribute = ItemAttribute.NotHandle;
                this.pickable = true;
                this.speed = 0;
                textureSheetList[0] = ItemTextureSheet;

                int count = textureSheetList.Count;
                int i = count - 1;
                while (i > 0)
                {
                    textureSheetList.RemoveAt(i);
                    i--;
                }

            }

        }




        public override void CollisionWithLink(ILinkState link, ItemSpace itemSpace)
        {

            endTime = System.Environment.TickCount;
            int runTime = endTime - startTime;

            if (attribute == ItemAttribute.AdverseAttack)
            {
                link.TakeDamage(0);

                Damage();
            }
            if (pickable && runTime > 400)
            {

                int itemLocation = existInSpace(itemSpace);
                if (itemLocation < 0)
                {
                    IItem newItem = this.Clone();
                    newItem.SetNumber(1);
                    itemSpace.Add(newItem);
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
                IItem item = this.Clone(); //Boomerang Position in ItemList
                item.ChangeDirection(game.character.GetDirection());
                item.ToMoving();
                game.outItemSpace.Add(item);
                game.character.ToThrowing();
                SoundFactory.Instance.PlaySoundShootBoomerang();
                number--;
            }
        }





        public class MovingBoomerangState : IMovingItemState
        {
            private Boomerang boomerang;



            public MovingBoomerangState(Boomerang boomerang)
            {
                this.boomerang = boomerang;



            }

            public void ToMoving()
            {
            }

            public void ToStatic()
            {
                boomerang.state = new StaticItemState(boomerang);
            }

            public void Update(Rectangle position)
            {
                boomerang.FrameUpdate();
                int totalRunTime = System.Environment.TickCount - boomerang.startTime;
                int intervalTime = System.Environment.TickCount - boomerang.timeCount;
                if (totalRunTime >= boomerang.flyTime && boomerang.firstBounce)
                {
                    boomerang.ChangeDirection(boomerang.OppositeDirection(boomerang.direction));
                    boomerang.firstBounce = false;
                }

                if (boomerang.firstBounce) { boomerang.speed -= intervalTime * boomerang.acceleration; }
                else { boomerang.speed += intervalTime * boomerang.acceleration; }
                boomerang.timeCount = System.Environment.TickCount;
                switch (boomerang.direction)
                {

                    case Direction.Up:
                        boomerang.positionRectangle.Y -= (int)boomerang.speed;
                        break;

                    case Direction.Down:
                        boomerang.positionRectangle.Y += (int)boomerang.speed;
                        break;

                    case Direction.Left:
                        boomerang.positionRectangle.X -= (int)boomerang.speed;
                        break;

                    case Direction.Right:
                        boomerang.positionRectangle.X += (int)boomerang.speed;
                        break;
                    default:
                        Console.WriteLine("Error: Incorrect command to change Link State.");
                        return;
                }

            }


        }
    }

    public class StaticBoomerangState : IMovingItemState
    {
        private Boomerang boomerang;


        public StaticBoomerangState(Boomerang boomerang)
        {
            this.boomerang = boomerang;

        }
        public void ToMoving()
        {
            boomerang.state = new MovingBoomerangState(boomerang);
        }

        public void ToStatic()
        {

        }

        public void Update(Rectangle position)
        {
            boomerang.positionRectangle.X = position.X;
            boomerang.positionRectangle.Y = position.Y;

        }

    }
}



