using System.Diagnostics;
using System.Drawing;
using System.Runtime.CompilerServices;
using Microsoft.VisualBasic.FileIO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
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
        void Taunt();
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

        public virtual void TakeDamage(int val)
        {
            ChangeHP(val);

            link.game.character = new LinkDamagedDecorator(link);
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

        public virtual void Taunt()
        {
            link.state = new TauntingLinkState(link);
        }

        public virtual void ChangeHP(int value)
        {
            if (value < 0)
            {
                SoundFactory.Instance.PlaySoundLinkHurt();
            }
            if (link.hp + value <= 0)
            {
                link.state = new DeadLinkState(link);
            }
            else
            {
                link.hp += value;
            }

        }
        public int HP()
        {
            return link.hp;

        }
        public int MaxHP()
        {
            return link.maxHp;

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
            MediaPlayer.Pause();
            SoundFactory.Instance.PlaySoundLinkFinalHurt();
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

        public override void ChangeHP(int value)
        {
            // Do not change HP anymore
        }

        public override void Update()
        {
            
            this.link.color = Color.Red;
            if(i > 100)
            {
                SoundFactory.Instance.PlaySoundLinkDie();
                link.game.gameState.Lose();
                this.link.color = Color.White;
                link.flipped = SpriteEffects.None;
            }
            i++;
        }
    }

    public class TauntingLinkState : LinkState
    {
        private Link link;
        Rectangle[] originalSprAt;
        Rectangle[] tauntSpriteAt;
        Texture2D oldTexture;
        int frame;

        public TauntingLinkState(Link link) : base(link)
        {
            oldTexture = link.texture;
            originalSprAt = link.spriteAtlas;
            tauntSpriteAt = new Rectangle[29];
            tauntSpriteAt[0] = new Rectangle(100, 0, 300, 300);     // Walk Up Frame 1
            tauntSpriteAt[1] = new Rectangle(500, 0, 300, 300);     // Walk Up Frame 2
            tauntSpriteAt[2] = new Rectangle(900, 0, 300, 300);    // Up Use Item
            tauntSpriteAt[3] = new Rectangle(1350, 0, 300, 300);     // Up Use Sword (18, 95, 15, 30) (18, 109, 15, 16)
            tauntSpriteAt[4] = new Rectangle(1800, 0, 300, 300);      // Walk Down Frame 1
            tauntSpriteAt[5] = new Rectangle(100, 315, 300, 300);     // Walk Down Frame 2
            tauntSpriteAt[6] = new Rectangle(500, 315, 300, 300);    // Down Use Item
            tauntSpriteAt[7] = new Rectangle(900, 315, 300, 300);     // Down Use Sword (18, 45, 15, 30) (18, 47, 15, 16)
            tauntSpriteAt[8] = new Rectangle(1350, 315, 300, 300);     // Walk Left 1
            tauntSpriteAt[9] = new Rectangle(1800, 315, 300, 300);     // Walk Left 2
            tauntSpriteAt[10] = new Rectangle(100, 630, 300, 300);	// Left Use Item
            tauntSpriteAt[11] = new Rectangle(500, 630, 300, 300);    // Left Use Sword (18, 78, 27, 15) (18, 78, 16, 15)
            tauntSpriteAt[12] = new Rectangle(900, 630, 300, 300);    // Walk Right Frame 1
            tauntSpriteAt[13] = new Rectangle(1350, 630, 300, 300);    // Walk Right frame 2
            tauntSpriteAt[14] = new Rectangle(1800, 630, 300, 300);	// Right Use Item
            tauntSpriteAt[15] = new Rectangle(100, 945, 300, 300);
            tauntSpriteAt[16] = new Rectangle(500, 945, 300, 300);
            tauntSpriteAt[17] = new Rectangle(900, 945, 300, 300);
            tauntSpriteAt[18] = new Rectangle(1350, 945, 300, 300);
            tauntSpriteAt[19] = new Rectangle(1800, 945, 300, 300);
            tauntSpriteAt[20] = new Rectangle(100, 1260, 300, 300);
            tauntSpriteAt[21] = new Rectangle(500, 1260, 300, 300);
            tauntSpriteAt[22] = new Rectangle(900, 1260, 300, 300);
            tauntSpriteAt[23] = new Rectangle(1350, 1260, 300, 300);
            tauntSpriteAt[24] = new Rectangle(1800, 1260, 300, 300);
            tauntSpriteAt[25] = new Rectangle(100, 1575, 300, 300);
            tauntSpriteAt[26] = new Rectangle(500, 1575, 300, 300);
            tauntSpriteAt[27] = new Rectangle(900, 1575, 300, 300);
            tauntSpriteAt[28] = new Rectangle(1350, 1575, 300, 300);

            link.spriteAtlas = tauntSpriteAt;
            link.texture = link.game.Content.Load<Texture2D>("Zelda_Sheet_2");

            this.link = link;
            link.position.Height *=2;
            this.link.currentFrame = this.link.spriteAtlas[0];

            SoundFactory.Instance.PlayTauntSong(); ;
        }

        public void Revert()
        {
            link.currentFrame = link.spriteAtlas[0];
            link.spriteAtlas = originalSprAt;
            link.position.Height /= 2;
            link.texture = oldTexture;
            MediaPlayer.Stop();
            SoundFactory.Instance.PlayBackgroundMusic();
            
        }

        public override void ToMoving()
        {
            Revert();
            link.state = new MovingLinkState(link);
        }

        public override void ToAttacking()
        {
            Revert();
            link.state = new AttackingLinkState(link);
        }

        public override void ToThrowing()
        {
            Revert();
            link.state = new ThrowingLinkState(link);
        }

        public override void Taunt()
        {
            // Already Taunting
        }

        public override void TakeDamage(int val)
        {
            Revert();
            link.state = new StandingLinkState(link);
            ChangeHP(val);
            link.game.character = new LinkDamagedDecorator(link);
            
        }

        public override void Update()
        {
            if (frame > 28 * 8)
            {
                frame = 0;
            }
            link.currentFrame = link.spriteAtlas[frame/8];
            frame++;
        }
    }
}