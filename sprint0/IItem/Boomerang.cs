

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

        public List<Texture2D> textureSheetList;
        public int timer;
        private int index;//which frame is shown
        public Boomerang(Texture2D textureSheet, Rectangle positionRectangle) : base(textureSheet, positionRectangle)
        {
            state = new StaticBoomerangState(this);
            textureSheetList = new List<Texture2D>();
            textureSheetList.Add(textureSheet);
            timer = 0;
            index = 0;
        }


        public override IItem Clone()
        {
            IItem itemClone = ItemFactory.Instance.CreateWoodenBoomerang(this.positionRectangle);

            return itemClone;
        }


        public override void CollisionWithNormalBlock()
        {
           this.direction = OppositeDirection(this.direction);
            
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
             SoundFactory.Instance.PlaySoundShootBoomerang();
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

            public void Update(int x, int y)
            {
                boomerang.FrameUpdate();
                switch (boomerang.direction)
                {
                    case Direction.Up:
                        boomerang.positionRectangle.Y -= boomerang.speed;
                        break;

                    case Direction.Down:
                        boomerang.positionRectangle.Y += boomerang.speed;
                        break;

                    case Direction.Left:
                        boomerang.positionRectangle.X -= boomerang.speed;
                        break;

                    case Direction.Right:
                        boomerang.positionRectangle.X += boomerang.speed;
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

        public void Update(int x, int y)
        {
            boomerang.positionRectangle.X = x;
            boomerang.positionRectangle.Y = y;

        }

    }
}



