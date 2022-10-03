using System.Diagnostics;
using Microsoft.VisualBasic.FileIO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using static sprint0.Link;

namespace sprint0
{
    public interface ILinkState
    {
        void ChangeFrame();
        void ToStanding();
        void ToMoving();
        void ToAttacking();
        void ToThrowing();
        void Update();
        // Draw() might also be included here
    }


    public class Link//could be template for ISprite
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

        public Link(Game1 game)
        {
            // Create SpriteBatch and load textures
            spriteBatch = new SpriteBatch(game.GraphicsDevice);
            texture = game.Content.Load<Texture2D>("Zelda_Sheet");
            flipped = SpriteEffects.None;

            // Initial Position and Speed of Link
            position = new Rectangle(350, 150, 100, 100);

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
            spriteAtlas[11] = new Rectangle(141, 11, 17, 17);    // Left Use Sword
            spriteAtlas[12] = new Rectangle(34, 11, 16, 16); // Walk Right Frame 1
            spriteAtlas[13] = new Rectangle(52, 11, 16, 16); // Walk Right frame 2
            spriteAtlas[14] = new Rectangle(123, 11, 17, 17);	// Right Use Item
            spriteAtlas[15] = new Rectangle(141, 11, 17, 17);    // Right Use Sword

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

        public void ToMoving()
        {
            state.ToMoving();
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
        }

        public void Draw()
        {
            spriteBatch.Begin();
            spriteBatch.Draw(texture, position, currentFrame, Color.White, 0, new Vector2(), flipped, 1);
            spriteBatch.End();
        }

        public void MoveDown()
        {

        }
    }

    public class StandingLinkState : ILinkState
    {
        private Link link;

        public StandingLinkState(Link link)
        {
            this.link = link;
            this.link.currentFrame = this.link.spriteAtlas[(int)this.link.direction * this.link.directionScalar];
            // construct link's sprite here too
        }
        public void ChangeFrame()
        {
            //purposely empty
        }
        public void ToStanding()
        {
            //purposely empty
        }

        public void ToMoving()
        {
            link.state = new MovingLinkState(link);
        }

        public void ToAttacking()
        {
            link.state = new AttackingLinkState(link);
        }

        public void ToThrowing()
        {
            link.state = new ThrowingLinkState(link);
        }

        public void Update()
        {

        }
    }

    public class MovingLinkState : ILinkState
    {
        private Link link;
        int frame;

        public MovingLinkState(Link link)
        {
            this.link = link;
            frame = 0;
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
        public void ToStanding()
        {
            link.state = new StandingLinkState(link);
        }

        public void ToMoving()
        {
            // Already moving
        }

        public void ToAttacking()
        {
            link.state = new AttackingLinkState(link);
        }

        public void ToThrowing()
        {
            link.state = new ThrowingLinkState(link);
        }

        public void Update()
        {
            if (Keyboard.GetState().IsKeyDown(Keys.W) || Keyboard.GetState().IsKeyDown(Keys.S) || Keyboard.GetState().IsKeyDown(Keys.A) || Keyboard.GetState().IsKeyDown(Keys.D))
            {
                switch (link.direction)
                {
                    case Direction.Up:
                        link.position.Y -= link.speed;
                        link.flipped = SpriteEffects.None;
                        break;
                    case Direction.Down:
                        link.position.Y += link.speed;
                        link.flipped = SpriteEffects.None;
                        break;
                    case Direction.Left:
                        link.position.X -= link.speed;
                        link.flipped = SpriteEffects.FlipHorizontally;
                        break;
                    case Direction.Right:
                        link.position.X += link.speed;
                        link.flipped = SpriteEffects.None;
                        break;
                    default:
                        Console.WriteLine("Error: Incorrect command to change Link State.");
                        return;
                }
            }
            else
            {
                link.ToStanding();
            }
            this.ChangeFrame();
        }
    }

    public class AttackingLinkState : ILinkState
    {
        private Link link;
        private int count;
        private Rectangle initPos;

        public AttackingLinkState(Link link)
        {
            this.link = link;
            initPos = this.link.position;
            this.link.currentFrame = this.link.spriteAtlas[(int)this.link.direction * this.link.directionScalar + 3];
            count = 0;

            if (link.direction == Direction.Up || link.direction == Direction.Down)
            {
                this.link.position.Height *= 2;
            }
            else
            {
                this.link.position.Width *= 2;
            }

        }
        public void ChangeFrame()
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
        public void ToStanding()
        {
            link.state = new StandingLinkState(link);
        }

        public void ToMoving()
        {
            // cannot move while attacking
        }

        public void ToAttacking()
        {
            // Already attacking
        }

        public void ToThrowing()
        {
            // cannot throw while Attacking
        }

        public void Update()
        {
            ChangeFrame();
        }
    }
    public class ThrowingLinkState : ILinkState
    {
        private Link link;
        private int count;

        public ThrowingLinkState(Link link)
        {
            this.link = link;
            this.link.currentFrame = this.link.spriteAtlas[(int)this.link.direction * this.link.directionScalar + 2];
            count = 0;
        }
        public void ChangeFrame()
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
        public void ToStanding()
        {
            link.state = new StandingLinkState(link);
        }

        public void ToMoving()
        {
            // cannot move while throwing
        }

        public void ToAttacking()
        {
            // cannot attack while throwing
        }

        public void ToThrowing()
        {
            // Already Throwing
        }

        public void Update()
        {
            ChangeFrame();
        }
    }

}