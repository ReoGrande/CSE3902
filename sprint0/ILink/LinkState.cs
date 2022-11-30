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
        void ToMoving();
        void ToAttacking();
        void ToThrowing();
        public abstract void ChangeHP(int value);
        public abstract int HP();
        public abstract int MaxHP();
        void Update();
        void Draw();
        void TakeDamage(int val);
        Rectangle GetPosition();
        Rectangle ChangePosition(Rectangle position);
        Direction GetDirection();
        bool IsAttacking();
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

        public void TakeDamage(int val)
        {
            link.TakeDamage(val);
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

        public bool IsAttacking()
        {
            return link.IsAttacking();
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

        public void ChangeHP(int value)
        {
            link.ChangeHP(value);
        }

        public int HP()
        {
            return link.HP();
        }

        public int MaxHP()
        {
            return link.MaxHP();
        }

        public abstract void ToAttacking();
        public abstract void ToMoving();
        public abstract void ToThrowing();
        public abstract void Update();
    }

    public class MovingLinkState : LinkState
    {
        private Link link;

        public MovingLinkState(Link link) : base(link)
        {
            this.link = link;
            this.link.flipped = SpriteEffects.None;
            this.link.direction = Direction.Up;
        }

        public override void ToMoving()
        {
            // Already moving
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
            if (Keyboard.GetState().IsKeyDown(Keys.W))
            {
                link.flipped = SpriteEffects.None;
                link.direction = Direction.Up;
                this.ChangeFrame();
                link.yVel = -link.speed;
                link.position.Y += link.yVel;
            } else if (Keyboard.GetState().IsKeyDown(Keys.A))
            {
                link.direction = Direction.Left;
                link.flipped = SpriteEffects.FlipHorizontally;
                this.ChangeFrame();
                link.xVel = -link.speed;
                link.position.X += link.xVel;
            } else if (Keyboard.GetState().IsKeyDown(Keys.S))
            {
                link.flipped = SpriteEffects.None;
                link.direction = Direction.Down;
                this.ChangeFrame();
                link.yVel = link.speed;
                link.position.Y += link.yVel;
            } else if (Keyboard.GetState().IsKeyDown(Keys.D))
            {
                link.flipped = SpriteEffects.None;
                link.direction = Direction.Right;
                this.ChangeFrame();
                link.xVel = link.speed;
                link.position.X += link.xVel;
            }
            else
            {
                link.ToStanding();
            }
        }
    }

    public class StandingLinkState : LinkState
    {
        private Link link;

        public StandingLinkState(Link link) : base(link)
        {
            this.link = link;
            this.link.currentFrame = this.link.spriteAtlas[(int)this.link.direction * this.link.directionScalar];
        }

        public override void ToMoving()
        {
            link.state = new MovingLinkState(link);
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

    public class AttackingLinkState : LinkState
    {
        private Link link;
        private int count;
        private Rectangle initPos;

        public AttackingLinkState(Link link) : base(link)
        {
            this.link = link;
            this.link.isAttacking = true;
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

        public override void ToMoving()
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
            if (count < 15)
            {
                count++;
            }
            else
            {
                link.position = initPos;
                link.state.ToStanding();
                link.isAttacking = false;
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

        public override void ToMoving()
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

    public class DeadLinkState : LinkState
    {
        private Link link;
        int i;

        public DeadLinkState(Link link) : base(link)
        {
            this.link = link;
            this.link.currentFrame = this.link.spriteAtlas[0];
            this.link.flipped = SpriteEffects.FlipVertically;
            this.link.color = Color.Red;
            this.link.hp = 0;
            i = 0;
        }

        public override void ToMoving()
        {
            // Can't move when dead
        }

        public override void ToAttacking()
        {
            // Can't attack when dead
        }

        public override void ToThrowing()
        {
            // Can't throw when dead
        }

        public override void Update()
        {
            this.link.color = Color.Red;
            if(i > 50)
            {
                link.game.gameState.Lose();
            }
            i++;
        }
    }
}