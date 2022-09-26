﻿using System.Diagnostics;
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
		void Update();
		// Draw() might also be included here
	}


	public class Link//could be template for ISprite
	{
		public ILinkState state;		// The current State of Link
		public Rectangle position;		// The current Position of Link
		public SpriteBatch spriteBatch;	// SpriteBatch to Draw Link
		public Texture2D texture;		// Texture to load Link
		public int timer;				// timer to keep track of Link's walk speed
		public enum Direction { Up, Down, Left, Right };	// Directions in which Link can face
		public Direction direction;							// The current Direction Link is facing
		public Rectangle[] spriteAtlas;						// Array of Link's Sprite sheet

		public Rectangle currentFrame;	// The currentFrame
		public int speed;				// Link's movement speed
		public SpriteEffects flipped;	// Flips the sprite

		public Link(Game1 game)
		{
			// Create SpriteBatch and load textures
            spriteBatch = new SpriteBatch(game.GraphicsDevice);
            texture = game.Content.Load<Texture2D>("Zelda_Sheet");
			flipped = SpriteEffects.None;

			// Initial Position and Speed of Link
            position = new Rectangle(350, 150, 150, 150);

            // Create Array of Link's Movements
            spriteAtlas = new Rectangle[8];
            spriteAtlas[0] = new Rectangle(86, 11, 15, 15); // Walk Up Frame 1
            spriteAtlas[1] = new Rectangle(69, 11, 15, 15); // Walk Up Frame 2
			spriteAtlas[2] = new Rectangle(1, 11, 15, 15);  // Walk Down Frame 1
            spriteAtlas[3] = new Rectangle(18, 11, 15, 15); // Walk Down Frame 2
            spriteAtlas[4] = new Rectangle(35, 11, 15, 15); // Walk Left 1
            spriteAtlas[5] = new Rectangle(52, 11, 15, 15); // Walk Left 2
			spriteAtlas[6] = new Rectangle(35, 11, 15, 15); // Walk Right Frame 1
            spriteAtlas[7] = new Rectangle(52, 11, 15, 15); // Walk Right frame 2

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

		public void Update()
		{
			state.Update();
		}

		public void Draw()
		{
			spriteBatch.Begin();
			spriteBatch.Draw(texture, position, currentFrame, Color.White, 0 , new Vector2(), flipped, 1);
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
			this.link.currentFrame = this.link.spriteAtlas[(int) this.link.direction * 2];
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
                link.currentFrame = link.spriteAtlas[(int) link.direction * 2 + frame];
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

        }

		public void ToAttacking()
		{
			link.state = new AttackingLinkState(link);
		}

		public void Update()
		{
			if (Keyboard.GetState().GetPressedKeyCount() > 0)
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
            } else
			{
				link.state = new StandingLinkState(link);
			}
            this.ChangeFrame();
        }
	}

    // Not yet implemented
    public class AttackingLinkState : ILinkState
    {
        private Link link;

        public AttackingLinkState(Link link)
        {
            this.link = link;
            // construct link's sprite here too
        }

        public void ToStanding()
        {

        }
        public void ChangeFrame()
        {
            //TODO:STUFF
        }
        public void ToMoving()
        {
            //link.state = new LeftMovingStompedLinkState(link);
        }

        public void ToAttacking()
        {

        }

        public void Update()
        {
            // call something like goomba.MoveLeft() or goomba.Move(-x,0);
        }
    }
}