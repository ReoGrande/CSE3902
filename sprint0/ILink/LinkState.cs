using System.Diagnostics;
using System.Drawing;
using System.Runtime.CompilerServices;
using Microsoft.VisualBasic.FileIO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using static sprint0.Link;
using Color = Microsoft.Xna.Framework.Color;
using Rectangle = Microsoft.Xna.Framework.Rectangle;

namespace sprint0
{
    public interface ILinkState
    {
        void ToStanding();
        void ToMovingUp();
        void ToMovingDown();
        void ToMovingLeft();
        void ToMovingRight();
        void ToAttacking();
        void ToThrowing();
        void Update();
        void Draw();
        Rectangle GetPosition();
        Rectangle ChangePosition(Rectangle position);
        Direction GetDirection();
    }

    public abstract class LinkState : ILinkState
    {
        private Link link;
        private int frame;
        public LinkState(Link link)
        {
            this.link = link;
            frame = 0;
        }
        public void Draw()
        {
            link.Draw();
        }

        public Rectangle GetPosition()
        {
            return link.position;
        }

         public Rectangle ChangePosition(Rectangle newPosition){
            link.position = newPosition;
            return link.position;
        }

        public Direction GetDirection()
        {
            return link.direction;
        }

        public void ToStanding()
        {
            link.state = new StandingLinkState(link);
        }
        public void ChangeFrame()
        {
            if (link.animationTimer == 6)
            {
                if (frame == 1)
                {
                    frame = 0;
                }
                else
                {
                    frame++;
                }
                link.currentFrame = link.spriteAtlas[(int)link.direction * link.directionScalar + frame];
                link.animationTimer = 1;
            }
            else
            {
                link.animationTimer += 1;
            }
        }
        public abstract void ToAttacking();
        public abstract void ToMovingDown();
        public abstract void ToMovingLeft();
        public abstract void ToMovingRight();
        public abstract void ToMovingUp();
        public abstract void ToThrowing();
        public abstract void Update();
    }

    public class StandingLinkState : LinkState
    {
        private Link link;

        public StandingLinkState(Link link) : base(link)
        {
            this.link = link;
            this.link.currentFrame = this.link.spriteAtlas[(int)this.link.direction * this.link.directionScalar];
        }

        public override void ToMovingUp()
        {
            link.state = new MovingUpLinkState(link);
        }

        public override void ToMovingDown()
        {
            link.state = new MovingDownLinkState(link);
        }

        public override void ToMovingLeft()
        {
            link.state = new MovingLeftLinkState(link);
        }

        public override void ToMovingRight()
        {
            link.state = new MovingRightLinkState(link);
        }

        public override void ToAttacking()
        {
            link.state = new AttackingLinkState(link);
        }

        public override void ToThrowing()
        {
            link.state = new ThrowingLinkState(link);
        }

        public override void Update()
        {

        }
    }

    public class MovingUpLinkState : LinkState
    {
        private Link link;

        public MovingUpLinkState(Link link) : base(link)
        {
            this.link = link;
            this.link.flipped = SpriteEffects.None;
            this.link.direction = Direction.Up;
        }

        public override void ToMovingUp()
        {
            // Already moving up
        }

        public override void ToMovingDown()
        {
            link.state = new MovingDownLinkState(link);
        }

        public override void ToMovingLeft()
        {
            link.state = new MovingLeftLinkState(link);
        }

        public override void ToMovingRight()
        {
            link.state = new MovingRightLinkState(link);
        }

        public override void ToAttacking()
        {
            link.state = new AttackingLinkState(link);
        }

        public override void ToThrowing()
        {
            link.state = new ThrowingLinkState(link);
        }

        public override void Update()
        {
            if (Keyboard.GetState().IsKeyDown(Keys.W)||Keyboard.GetState().IsKeyDown(Keys.Up))
            {
                this.ChangeFrame();
                link.position.Y -= link.speed;
            }
            else
            {
                link.ToStanding();
            }
        }

    }

    public class MovingDownLinkState : LinkState
    {
        private Link link;

        public MovingDownLinkState(Link link) : base(link)
        {
            this.link = link;
            this.link.flipped = SpriteEffects.None;
            this.link.direction = Direction.Down;
        }

        public override void ToMovingUp()
        {
            link.state = new MovingDownLinkState(link);
        }

        public override void ToMovingDown()
        {
            // Already moving down
        }

        public override void ToMovingLeft()
        {
            link.state = new MovingLeftLinkState(link);
        }

        public override void ToMovingRight()
        {
            link.state = new MovingRightLinkState(link);
        }

        public override void ToAttacking()
        {
            link.state = new AttackingLinkState(link);
        }

        public override void ToThrowing()
        {
            link.state = new ThrowingLinkState(link);
        }

        public override void Update()
        {
            if (Keyboard.GetState().IsKeyDown(Keys.S)||Keyboard.GetState().IsKeyDown(Keys.Down))
            {
                this.ChangeFrame();
                link.position.Y += link.speed;
            }
            else
            {
                link.ToStanding();
            }
        }
    }

    public class MovingLeftLinkState : LinkState
    {
        private Link link;

        public MovingLeftLinkState(Link link) : base(link)
        {
            this.link = link;
            this.link.flipped = SpriteEffects.FlipHorizontally;
            this.link.direction = Direction.Left;
        }

        public override void ToMovingUp()
        {
            link.state = new MovingDownLinkState(link);
        }

        public override void ToMovingDown()
        {
            link.state = new MovingLeftLinkState(link);
        }

        public override void ToMovingLeft()
        {
            // Already moving left
        }

        public override void ToMovingRight()
        {
            link.state = new MovingRightLinkState(link);
        }

        public override void ToAttacking()
        {
            link.state = new AttackingLinkState(link);
        }

        public override void ToThrowing()
        {
            link.state = new ThrowingLinkState(link);
        }

        public override void Update()
        {
            if (Keyboard.GetState().IsKeyDown(Keys.A)|| Keyboard.GetState().IsKeyDown(Keys.Left))
            {
                this.ChangeFrame();
                link.position.X -= link.speed;
            }
            else
            {
                link.ToStanding();
            }
        }
    }

    public class MovingRightLinkState : LinkState
    {
        private Link link;

        public MovingRightLinkState(Link link) : base(link)
        {
            this.link = link;
            link.flipped = SpriteEffects.None;
            this.link.direction = Direction.Right;
        }

        public override void ToMovingUp()
        {
            link.state = new MovingDownLinkState(link);
        }

        public override void ToMovingDown()
        {
            link.state = new MovingLeftLinkState(link);
        }

        public override void ToMovingLeft()
        {
            link.state = new MovingLeftLinkState(link);
        }

        public override void ToMovingRight()
        {
            // Already moving right
        }

        public override void ToAttacking()
        {
            link.state = new AttackingLinkState(link);
        }

        public override void ToThrowing()
        {
            link.state = new ThrowingLinkState(link);
        }

        public override void Update()
        {
            if (Keyboard.GetState().IsKeyDown(Keys.D)|| Keyboard.GetState().IsKeyDown(Keys.Right))
            {
                this.ChangeFrame();
                link.position.X += link.speed;
            }
            else
            {
                link.ToStanding();
            }
        }
    }

    public class AttackingLinkState : LinkState
    {
        private Link link;
        private int count;
        private Rectangle initPos;

        public AttackingLinkState(Link link) : base(link)
        {
            this.link = link;
            initPos = this.link.position;
            this.link.currentFrame = this.link.spriteAtlas[(int)this.link.direction * this.link.directionScalar + 3];
            count = 0;

            switch (this.link.direction)
            {
                case Direction.Up:
                    this.link.position.Y -= this.link.position.Height;
                    this.link.position.Height *= 2;
                    break;
                case Direction.Down:
                    this.link.position.Height *= 2;
                    break;
                case Direction.Left:
                    this.link.position.X -= this.link.position.Width;
                    this.link.position.Width *= 2;
                    link.flipped = SpriteEffects.FlipHorizontally;
                    break;
                case Direction.Right:
                    this.link.position.Width *= 2;
                    break;
                default:
                    break;
            }

        }

        public override void ToMovingUp()
        {
            // cannot move while attacking
        }

        public override void ToMovingDown() {
            // cannot move while attacking
        }

        public override void ToMovingLeft()
        {
            // cannot move while attacking
        }

        public override void ToMovingRight()
        {
            // cannot move while attacking
        }

        public override void ToAttacking()
        {
            // Already attacking
        }

        public override void ToThrowing()
        {
            // cannot throw while Attacking
        }

        public override void Update()
        {
            if (count > 20)
            {
                link.position = initPos;
                link.state.ToStanding();
            }
            else
            {
                count++;
            }
        }
    }
    public class ThrowingLinkState : LinkState
    {
        private Link link;
        private int count;

        public ThrowingLinkState(Link link) : base(link)
        {
            this.link = link;
            this.link.currentFrame = this.link.spriteAtlas[(int)this.link.direction * this.link.directionScalar + 2];
            count = 0;
        }

        public override void ToMovingUp()
        {
            // cannot move while throwing
        }

        public override void ToMovingDown()
        {
            // cannot move while throwing
        }

        public override void ToMovingLeft()
        {
            // cannot move while throwing
        }

        public override void ToMovingRight()
        {
            // cannot move while throwing
        }

        public override void ToAttacking()
        {
            // cannot attack while throwing
        }

        public override void ToThrowing()
        {
            // Already Throwing
        }

        public override void Update()
        {
            if (count > 30)
            {
                link.state.ToStanding();
            }
            else
            {
                count++;
            }
        }
    }

}