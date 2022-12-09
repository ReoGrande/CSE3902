
using System;
using System.Reflection;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using sprint0;
using static System.Formats.Asn1.AsnWriter;
using static sprint0.Bomb;
using static sprint0.Link;



namespace sprint0
{

    public class Bomb : MoveableItem
    {


        public int timer;
        private int index;//which frame is shown
        private bool blast;
        private int blastTimer;
        private int blastIndex;
        private List<Rectangle> sourceRectangleList;
        private Texture2D blastSheet;


        public Bomb(Texture2D textureSheet, Rectangle positionRectangle) : base(textureSheet, positionRectangle)
        {
            state = new StaticBombState(this);
            timer = 0;
            index = 0;
            damage = -5;
            blastTimer = 0;
            blastIndex = 0;
            number = 20;
            blast = false;
            //read rectangles
            sourceRectangleList = new List<Rectangle>();
            int y = 0;
            for (int i = 0; i < 4; i++)
            {
                int x = 0;

                for (int j = 0; j < 3; j++)
                {
                    sourceRectangleList.Add(new Rectangle(x, y, 62, 65));
                    x += 62;
                }
                y += 65;
            }


        }

        public override IItem Clone()
        {
            IItem itemClone = ItemFactory.Instance.CreateBomb(this.positionRectangle);

            return itemClone;

        }

        public void AddBlastSheet(Texture2D textureSheet)
        {
            this.blastSheet = textureSheet;
        }

        public override void Damage()
        {
            if (!blast)
            {
                blast = true;
                pickable = false;
                attribute = ItemAttribute.NotHandle;
                ItemTextureSheet = blastSheet;
                int x = positionRectangle.X;
                int y = positionRectangle.Y;
                int width = positionRectangle.Width;
                int height = positionRectangle.Height;
                this.positionRectangle = new Rectangle(x - width, y - width, 3 * width, 3 * height);
                this.specialType = SpecialType.Blast;
                SoundFactory.Instance.PlaySoundBlast();
            }

        }


        private void blastUpdate()
        {
            if (blastTimer >= 4)
            {
                {
                    if (blastIndex < 11)
                    { blastIndex++; }
                    else
                    {
                        blastIndex = 0;
                        this.damaged = true;
                    }

                    rangeInSheet = sourceRectangleList[blastIndex];

                    blastTimer = 0;
                }
            }
            else { blastTimer++; }

        }
        public override void Update(Game1 game, Rectangle position)
        {
            if (!blast)
            {
                CheckOutOfBound(game);
                if (this.attribute == ItemAttribute.WaitForPick)
                {
                    state.Update(this.GetPosition());
                }
                else
                {
                    state.Update(position);
                }
            }
            else { blastUpdate(); }
        }



        private void FrameUpdate()

        {
            int number = textureSheetList.Count;


            if (timer >= 5)
            {


                if (index < number - 1)
                { index++; }
                else
                { index = 0; }

                ItemTextureSheet = textureSheetList[index];
                rangeInSheet = new Rectangle(0, 0, ItemTextureSheet.Width, ItemTextureSheet.Height);
                timer = 0;

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
                SoundFactory.Instance.PlaySoundDropBomb();
                number--;
            }
        }




        public class MovingBombState : IMovingItemState
        {
            private Bomb bomb;


            public MovingBombState(Bomb Bomb)
            {
                this.bomb = Bomb;


            }

            public void ToMoving()
            {
            }

            public void ToStatic()
            {
                bomb.state = new StaticItemState(bomb);
            }

            public void Update(Rectangle position)
            {
                bomb.FrameUpdate();
                switch (bomb.direction)
                {
                    case Direction.Up:
                        bomb.positionRectangle.Y -= (int)bomb.speed;
                        break;

                    case Direction.Down:
                        bomb.positionRectangle.Y += (int)bomb.speed;
                        break;

                    case Direction.Left:
                        bomb.positionRectangle.X -= (int)bomb.speed;
                        break;

                    case Direction.Right:
                        bomb.positionRectangle.X += (int)bomb.speed;
                        break;
                    default:
                        Console.WriteLine("Error: Incorrect command to change Link State.");
                        return;
                }

            }


        }
    }

    public class StaticBombState : IMovingItemState
    {
        private Bomb Bomb;


        public StaticBombState(Bomb Bomb)
        {
            this.Bomb = Bomb;

        }
        public void ToMoving()
        {
            Bomb.state = new MovingBombState(Bomb);
        }

        public void ToStatic()
        {

        }

        public void Update(Rectangle position)
        {
            Bomb.positionRectangle.X = position.X;
            Bomb.positionRectangle.Y = position.Y;

        }

    }
}


