
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

        public List<Texture2D> textureSheetList;
        public int timer;
        private int index;//which frame is shown
        public Bomb(Texture2D textureSheet, Rectangle positionRectangle) : base(textureSheet, positionRectangle)
        {
            state = new StaticBombState(this);
            textureSheetList = new List<Texture2D>();
            textureSheetList.Add(textureSheet);
            timer = 0;
            index = 0;
        }

        public override IItem Clone()
        {
            IItem itemClone = ItemFactory.Instance.CreateBomb(this.positionRectangle);

            return itemClone;

        }


        public void AddFrames(Texture2D textureSheet)
        {

            textureSheetList.Add(textureSheet);
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

         public override void Use(Game1 game)
        {
             IItem item =this.Clone(); //Boomerang Position in ItemList
             item.ChangeDirection(game.character.GetDirection());
             item.ToMoving();
             game.outItemSpace.Add(item);   
             game.character.ToThrowing();

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

            public void Update(int x, int y)
            {
                bomb.FrameUpdate();
                switch (bomb.direction)
                {
                    case Direction.Up:
                        bomb.positionRectangle.Y -= bomb.speed;
                        break;

                    case Direction.Down:
                        bomb.positionRectangle.Y += bomb.speed;
                        break;

                    case Direction.Left:
                        bomb.positionRectangle.X -= bomb.speed;
                        break;

                    case Direction.Right:
                        bomb.positionRectangle.X += bomb.speed;
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

        public void Update(int x, int y)
        {
            Bomb.positionRectangle.X = x;
            Bomb.positionRectangle.Y = y;

        }

    }
}


