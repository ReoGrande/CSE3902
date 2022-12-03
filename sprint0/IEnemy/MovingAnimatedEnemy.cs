

using System;
using System.Security.Cryptography.X509Certificates;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using static System.Formats.Asn1.AsnWriter;
using static sprint0.Link;




namespace sprint0
{


    //non-moving,non-animated sprite
    public class MovingAnimatedEnemy : StaticEnemy
    {

        public List<Texture2D> textureSheetList;

        protected int index;//which frame is shown
        protected int speed;
        protected int maxSpeed;
        protected int movingTimer;
        protected int movePattern;

        protected Direction originalDirection;
        protected int originalSpeed;
        protected int movementPatternNum;

        public MovingAnimatedEnemy(Texture2D textureSheet, Rectangle positionRectangle) : base(textureSheet, positionRectangle)
        {
            movementPatternNum = 4;
            movingTimer = 0;
            textureSheetList = new List<Texture2D>();
            textureSheetList.Add(EnemyTextureSheet);
            index = 0;
            maxSpeed = 3;
            speed = 1;
            movePattern = 0;
            originalSpeed = 4;
            this.direction = Direction.Left;
            originalDirection = Direction.Left;
        



        }


        public void AddFrames(Texture2D textureSheet)
        {

            textureSheetList.Add(textureSheet);
        }







        protected void FrameUpdate(int startIndex, int endIndex)
        {

            if (timer >= 6)
            {
                {

                    if (index < endIndex - 1)
                    { index++; }
                    else
                    { index = startIndex; }

                    EnemyTextureSheet = textureSheetList[index];
                    rangeInSheet = new Rectangle(0, 0, EnemyTextureSheet.Width, EnemyTextureSheet.Height);
                    timer = 0;
                }
            }
            else { timer++; }
        }

        public void PositionUpdate()
        {
            if (this.speed < this.maxSpeed)
            {
                this.speed += 1;
            }
            switch (this.direction)
            {
                case Direction.Up:
                    this.positionRectangle.Y -= this.speed;
                    break;

                case Direction.Down:
                    this.positionRectangle.Y += this.speed;
                    break;

                case Direction.Left:
                    this.positionRectangle.X -= this.speed;
                    break;

                case Direction.Right:
                    this.positionRectangle.X += this.speed;
                    break;
                default:
                    Console.WriteLine("Error: Incorrect command to change Link State.");
                    return;
            }

        }
        public Direction OppositeDirection(Direction direction)
        {
            this.speed = 0;
            Direction result = Direction.Up;
            if (direction == Direction.Up) { result = Direction.Down; }
            else if (direction == Direction.Left) { result = Direction.Right; }
            else if (direction == Direction.Right) { result = Direction.Left; }
            return result;
        }



        public override void SetMovePattern(int i)
        {
            this.movePattern = i;
        }

        public override void SetSpeed(int v)
        {
            this.speed = v;

        }

        protected void MovementChoice()
        {

            switch (movePattern)
            {
                case 0://Static
                    SetSpeed(0);
                    break;
                case 1://Move Only Left and Right
                    LeftAndRightMove((new Random().Next() % 200) + 1);
                    break;
                case 2://Move Only Up and Down
                    UpAndDownMove((new Random().Next() % 200) + 1);
                    break;
                case 3://Move Only Up and Down
                    CircleMove1((new Random().Next() % 200) + 1);
                    break;
                case 4://Move Only Up and Down
                    CircleMove2((new Random().Next() % 200) + 1);
                    break;
                //more move pattern latter
                default:
                    Console.WriteLine("Invalid Choice For Movement");
                    break;
            }
        }

        protected void LeftAndRightMove(int timeInterval)
        {
            if (direction != Direction.Left && direction != Direction.Right)
            {
                this.speed = 0;
                this.direction = Direction.Left;
            }
            if (movingTimer >= timeInterval)
            {
                this.direction = OppositeDirection(this.direction);
                movingTimer = 0;
            }
            else
            {
                if (this.speed < this.maxSpeed)
                {
                    this.speed += 1;
                }
                movingTimer++;
            }
        }

        protected void UpAndDownMove(int timeInterval)
        {
            if (direction != Direction.Up && direction != Direction.Down) { this.direction = Direction.Up; }
            if (movingTimer >= timeInterval)
            {
                this.direction = OppositeDirection(this.direction);
                movingTimer = 0;
            }
            else
            {
                movingTimer++;
            }
        }

        protected void CircleMove1(int timeInterval)
        {

            if (movingTimer >= timeInterval)
            {
                if (direction == Direction.Up) { direction = Direction.Left; }
                else if (direction == Direction.Left) { direction = Direction.Down; }
                else if (direction == Direction.Right) { direction = Direction.Up; }
                else if (direction == Direction.Down) { direction = Direction.Right; }
                movingTimer = 0;
            }
            else
            {
                movingTimer++;
            }
        }

        protected void CircleMove2(int timeInterval)
        {

            if (movingTimer >= timeInterval)
            {
                if (direction == Direction.Up) { direction = Direction.Right; }
                else if (direction == Direction.Left) { direction = Direction.Up; }
                else if (direction == Direction.Right) { direction = Direction.Down; }
                else if (direction == Direction.Down) { direction = Direction.Left; }
                movingTimer = 0;
            }
            else
            {
                movingTimer++;
            }
        }




        public override void EnemyUpdate(Game1 game)
        {
            base.EnemyUpdate(game);
            MovementChoice();
            int number = textureSheetList.Count;
            FrameUpdate(0, number);
            PositionUpdate();

        }
    }


}
