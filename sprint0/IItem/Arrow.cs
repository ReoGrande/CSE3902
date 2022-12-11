

using System;
using System.Reflection;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using sprint0;
using static System.Formats.Asn1.AsnWriter;
using static sprint0.Arrow;
using static sprint0.Link;



namespace sprint0
{

    public class Arrow : MoveableItem
    {



        public Arrow(Texture2D textureSheet, Rectangle positionRectangle) : base(textureSheet, positionRectangle)
        {
            state = new StaticArrowState(this);
            damage = -2;
            this.infinite = true;
            number = 50;
            speed = 8;
        }

        public override IItem Clone()
        {
            IItem itemClone = ItemFactory.Instance.CreateArrow(this.positionRectangle);

            return itemClone;

        }




        public void FrameUpdate()

        {
            int number = textureSheetList.Count;


        }

        public override void CollisionWithEnemy(IEnemy enemy)
        {
            if (this.attribute == ItemAttribute.FriendlyAttack && enemy.Touchable())
            {
                enemy.GetDamaged();
                enemy.ChangeHP(this.damage);
                SoundFactory.Instance.PlaySoundEnemyHit();
                Damage();
            }
        }
        public override void Use1(Game1 game)
        {
            if (number > 0)
            {
                IItem newItem = this.Clone();
                newItem.ChangeDirection(game.character.GetDirection());
                newItem.ToMoving();
                game.outItemSpace.Add(newItem);
                game.character.ToThrowing();
                SoundFactory.Instance.PlaySoundShootArrow();
                number--;
            }

        }
    }





    public class MovingArrowState : IMovingItemState
    {
        private Arrow arrow;


        public MovingArrowState(Arrow arrow)
        {
            this.arrow = arrow;


        }

        public void ToMoving()
        {
        }

        public void ToStatic()
        {
            arrow.state = new StaticArrowState(arrow);
        }

        public void Update(Rectangle position)
        {
            arrow.FrameUpdate();
            switch (arrow.direction)
            {
                case Direction.Up:
                    arrow.positionRectangle.Y -= (int)arrow.speed;
                    arrow.ItemTextureSheet = arrow.textureSheetList[0];
                    break;

                case Direction.Down:
                    arrow.positionRectangle.Y += (int)arrow.speed;
                    arrow.ItemTextureSheet = arrow.textureSheetList[1];
                    break;

                case Direction.Left:
                    arrow.positionRectangle.X -= (int)arrow.speed;
                    arrow.ItemTextureSheet = arrow.textureSheetList[2];
                    break;

                case Direction.Right:
                    arrow.positionRectangle.X += (int)arrow.speed;
                    arrow.ItemTextureSheet = arrow.textureSheetList[3];
                    break;
                default:
                    Console.WriteLine("Error: Incorrect command to change Link State.");
                    return;
            }
            arrow.rangeInSheet = new Rectangle(0, 0, arrow.ItemTextureSheet.Width, arrow.ItemTextureSheet.Height);

        }


    }


    public class StaticArrowState : IMovingItemState
    {
        private Arrow arrow;


        public StaticArrowState(Arrow arrow)
        {
            this.arrow = arrow;

        }
        public void ToMoving()
        {
            arrow.state = new MovingArrowState(arrow);
        }

        public void ToStatic()
        {

        }

        public void Update(Rectangle position)
        {
            arrow.positionRectangle.X = position.X;
            arrow.positionRectangle.Y = position.Y;

        }

    }
}



