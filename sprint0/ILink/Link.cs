using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sprint0
{
    public class Link : ILinkState
    {
        private SpriteBatch spriteBatch;    // SpriteBatch to Draw Link
        private Texture2D texture;          // Texture to load Link
        public ILinkState state;            // The current State of Link
        public Rectangle position;          // The current Position of Link
        public int animationTimer;                   // timer to keep track of Link's walk speed
        public enum Direction { Up, Down, Left, Right };    // Directions in which Link can face
        public Direction direction;                         // The current Direction Link is facing
        public Rectangle[] spriteAtlas;                     // Array of Link's Sprite sheet
        public int directionScalar;

        public Rectangle currentFrame;  // The currentFrame
        public int speed;               // Link's movement speed
        public SpriteEffects flipped;   // Flips the sprite
        public Color color;             // Link Sprite color tint

        public Link(Game1 game)
        {
            // Create SpriteBatch and load textures
            spriteBatch = new SpriteBatch(game.GraphicsDevice);
            texture = game.Content.Load<Texture2D>("Zelda_Sheet");
            flipped = SpriteEffects.None;
            color = Color.White;

            // Initial Position and Speed of Link
            position = new Rectangle(350, 150, 60, 60);
            animationTimer = 1;
            speed = 4;

            // Create Array of Link's Movements
            directionScalar = 4;
            spriteAtlas = new Rectangle[16];
            spriteAtlas[0] = new Rectangle(86, 11, 15, 16);     // Walk Up Frame 1
            spriteAtlas[1] = new Rectangle(69, 11, 15, 16);     // Walk Up Frame 2
            spriteAtlas[2] = new Rectangle(141, 11, 17, 17);    // Up Use Item
            spriteAtlas[3] = new Rectangle(18, 95, 15, 30);     // Up Use Sword
            spriteAtlas[4] = new Rectangle(0, 11, 15, 16);      // Walk Down Frame 1
            spriteAtlas[5] = new Rectangle(17, 11, 15, 16);     // Walk Down Frame 2
            spriteAtlas[6] = new Rectangle(106, 11, 15, 16);    // Down Use Item
            spriteAtlas[7] = new Rectangle(18, 45, 15, 30);     // Down Use Sword
            spriteAtlas[8] = new Rectangle(34, 11, 17, 17);     // Walk Left 1
            spriteAtlas[9] = new Rectangle(52, 11, 15, 16);     // Walk Left 2
            spriteAtlas[10] = new Rectangle(123, 11, 17, 17);	// Left Use Item
            spriteAtlas[11] = new Rectangle(18, 78, 27, 15);    // Left Use Sword
            spriteAtlas[12] = new Rectangle(34, 11, 16, 16);    // Walk Right Frame 1
            spriteAtlas[13] = new Rectangle(52, 11, 16, 16);    // Walk Right frame 2
            spriteAtlas[14] = new Rectangle(123, 11, 17, 17);	// Right Use Item
            spriteAtlas[15] = new Rectangle(18, 78, 27, 15);    // Right Use Sword

            // Initial State and Direction of Link
            direction = Direction.Down;
            state = new StandingLinkState(this);
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
        public Rectangle ChangePosition(Rectangle newPosition)
        {
            this.position = newPosition;
            return this.position;
        }


        public Direction GetDirection()
        {
            return this.direction;
        }
    }
}
