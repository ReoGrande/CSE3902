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
        void TakeDamage();
    }

    public class Link : ILinkState
    {
        public ILinkState state;        // The current State of Link
        public Rectangle position;      // The current Position of Link
        public SpriteBatch spriteBatch; // SpriteBatch to Draw Link
        public Texture2D texture;       // Texture to load Link
        public int timer;               // timer to keep track of Link's walk speed
        public enum Direction { Up, Down, Left, Right };    // Directions in which Link can face
        public Direction direction;                         // The current Direction Link is facing
        public Rectangle[] spriteAtlas;                     // Array of Link's Sprite sheet
        public int directionScalar;

        public Rectangle currentFrame;  // The currentFrame
        public int speed;               // Link's movement speed
        public SpriteEffects flipped;   // Flips the sprite

        // For Sprint2; will be implemented in a decorator class later
        Color color;
        public Color[] damagedColors;
        public bool isDamaged;
        int i;                          // Loop iterator
        int j;                          // Loop iterator
        

        public Link(Game1 game)
        {
            // Create SpriteBatch and load textures
            spriteBatch = new SpriteBatch(game.GraphicsDevice);
            texture = game.Content.Load<Texture2D>("Zelda_Sheet");
            flipped = SpriteEffects.None;
            color = Color.White;

            // Initial Position and Speed of Link
            position = new Rectangle(350, 150, 100, 100);

            // For Sprint2; will be implemented in a decorator class later
            isDamaged = false;
            i = 0;
            j = 0;
            damagedColors = new Color[4];
            damagedColors[0] = Color.Red;
            damagedColors[1] = Color.Blue;
            damagedColors[2] = Color.Green;
            damagedColors[3] = Color.Yellow;

            // Create Array of Link's Movements
            directionScalar = 4;
            spriteAtlas = new Rectangle[16];
            spriteAtlas[0] = new Rectangle(86, 11, 15, 16); // Walk Up Frame 1
            spriteAtlas[1] = new Rectangle(69, 11, 15, 16); // Walk Up Frame 2
            spriteAtlas[2] = new Rectangle(141, 11, 17, 17);    // Up Use Item
            spriteAtlas[3] = new Rectangle(18, 95, 15, 30);    // Up Use Sword
            spriteAtlas[4] = new Rectangle(0, 11, 15, 16);  // Walk Down Frame 1
            spriteAtlas[5] = new Rectangle(17, 11, 15, 16); // Walk Down Frame 2
            spriteAtlas[6] = new Rectangle(106, 11, 15, 16);	// Down Use Item
            spriteAtlas[7] = new Rectangle(18, 45, 15, 30);    // Down Use Sword
            spriteAtlas[8] = new Rectangle(34, 11, 17, 17); // Walk Left 1
            spriteAtlas[9] = new Rectangle(52, 11, 15, 16); // Walk Left 2
            spriteAtlas[10] = new Rectangle(123, 11, 17, 17);	// Left Use Item
            spriteAtlas[11] = new Rectangle(18, 78, 27, 15);    // Left Use Sword
            spriteAtlas[12] = new Rectangle(34, 11, 16, 16); // Walk Right Frame 1
            spriteAtlas[13] = new Rectangle(52, 11, 16, 16); // Walk Right frame 2
            spriteAtlas[14] = new Rectangle(123, 11, 17, 17);	// Right Use Item
            spriteAtlas[15] = new Rectangle(18, 78, 27, 15);    // Right Use Sword

            // Initial State and Direction of Link
            direction = Direction.Down;
            state = new StandingLinkState(this);



            timer = 1;
            speed = 4;
        }

        public void ToStanding()
        {
            state.ToStanding();
        }

        public void ToMovingUp()
        {
            state.ToMovingUp();
        }

        public void ToMovingDown()
        {
            state.ToMovingDown();
        }

        public void ToMovingLeft()
        {
            state.ToMovingLeft();
        }

        public void ToMovingRight()
        {
            state.ToMovingRight();
        }

        public void ToAttacking()
        {
            state.ToAttacking();
        }

        public void ToThrowing()
        {
            state.ToThrowing();
        }

        public void Update()
        {
            state.Update();
            if (isDamaged)
            {
                if (j < 30)
                {
                    if (i > damagedColors.Length - 1)
                    {
                        i = 0;
                    }
                    color = damagedColors[i];
                    i++;
                    j++;
                } else
                {
                    isDamaged = false;
                    j = 0;
                }
            } else
            {
                color = Color.White;
            }
        }

        public void Draw()
        {
            spriteBatch.Begin();
            spriteBatch.Draw(texture, position, currentFrame, color, 0, new Vector2(), flipped, 1);
            spriteBatch.End();
        }

        public Rectangle GetPosition()
        {
            return this.position;
        }
        public Rectangle ChangePosition(Rectangle newPosition){
            this.position = newPosition;
            return this.position;
        }


        public Direction GetDirection()
        {
            return this.direction;
        }

        public void TakeDamage()
        {
            isDamaged = true;
        }
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
            if (link.timer == 8)
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
                link.timer = 1;
            }
            else
            {
                link.timer += 1;
            }
        }
        public void TakeDamage()
        {
            link.TakeDamage();
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
            // construct link's sprite here too
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
        int frame;

        public MovingUpLinkState(Link link) : base(link)
        {
            this.link = link;
            frame = 0;
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
        int frame;

        public MovingDownLinkState(Link link) : base(link)
        {
            this.link = link;
            frame = 0;
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
        int frame;

        public MovingLeftLinkState(Link link) : base(link)
        {
            this.link = link;
            frame = 0;
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
        int frame;

        public MovingRightLinkState(Link link) : base(link)
        {
            this.link = link;
            frame = 0;
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