

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
        int circleRadius;



        private int index;//which frame is shown
        public ShieldBall(Texture2D textureSheet, Rectangle positionRectangle) : base(textureSheet, positionRectangle)
        {
            state = new StaticShieldBallState(this);
            this.infinite = true;
            number = 50;
            speed = 8;
            timer = 0;
            circleRadius = 70;

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

            enemy.GetDamaged();
            enemy.ChangeHP(-1);

        }


        public void FrameUpdate()

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

        public void Update(int x, int y)
        {
            shieldBall.FrameUpdate();
            switch (shieldBall.direction)
            {
                case Direction.Up:
                    shieldBall.positionRectangle.Y -= (int)shieldBall.speed;
                    shieldBall.ItemTextureSheet = shieldBall.textureSheetList[0];
                    break;

                case Direction.Down:
                    shieldBall.positionRectangle.Y += (int)shieldBall.speed;
                    shieldBall.ItemTextureSheet = shieldBall.textureSheetList[1];
                    break;

                case Direction.Left:
                    shieldBall.positionRectangle.X -= (int)shieldBall.speed;
                    shieldBall.ItemTextureSheet = shieldBall.textureSheetList[2];
                    break;

                case Direction.Right:
                    shieldBall.positionRectangle.X += (int)shieldBall.speed;
                    shieldBall.ItemTextureSheet = shieldBall.textureSheetList[3];
                    break;
                default:
                    Console.WriteLine("Error: Incorrect command to change Link State.");
                    return;
            }
            shieldBall.rangeInSheet = new Rectangle(0, 0, shieldBall.ItemTextureSheet.Width, shieldBall.ItemTextureSheet.Height);


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

        public void Update(int x, int y)
        {
            shieldBall.positionRectangle.X = x;
            shieldBall.positionRectangle.Y = y;

        }

    }
}




