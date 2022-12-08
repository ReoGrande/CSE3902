using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sprint0
{
    public class Link : ILinkState
    {
        public Game1 game;
        private SpriteBatch spriteBatch;    // SpriteBatch to Draw Link
        public Texture2D texture;          // Texture to load Link
        public ILinkState state;            // The current State of Link
        public Rectangle position;          // The current Position of Link
        public int animationTimer;                   // timer to keep track of Link's walk speed
        public enum Direction { Up, Down, Left, Right };    // Directions in which Link can face
        public Direction direction;                         // The current Direction Link is facing
        public Rectangle[] spriteAtlas;                     // Array of Link's Sprite sheet
        public int directionScalar;

        public Rectangle currentFrame;  // The currentFrame
        public SpriteEffects flipped;   // Flips the sprite
        public Color color;             // Link Sprite color tint


        public int hp;                  //Link's hp
        public int maxHp;               //Link's max hp

        public int speed;               // Link's movement speed
        public int xVel;                // Converts Link's horizontal scalar speed to a vector
        public int yVel;                // Converts Link's vertical scalar speed to a vector

        public bool isAttacking;               // Stores whether Link is attacking

        public double scale;

        public Link(Game1 game)
        {
            // Create SpriteBatch and load textures
            this.game = game;
            spriteBatch = new SpriteBatch(game.GraphicsDevice);
            texture = game.Content.Load<Texture2D>("Zelda_Sheet");
            flipped = SpriteEffects.None;
            color = Color.White;
            hp = 9;
            maxHp = 9;
            scale = 0.8;

            // Initial Position and Speed of Link
            position = new Rectangle(game._playerScreen.X + 350, game._playerScreen.Y + 150, 40, 40);
            animationTimer = 1;

            // Create Array of Link's Movements
            directionScalar = 4;
            spriteAtlas = new Rectangle[16];
            spriteAtlas[0] = new Rectangle(86, 11, 15, 16);     // Walk Up Frame 1
            spriteAtlas[1] = new Rectangle(69, 11, 15, 16);     // Walk Up Frame 2
            spriteAtlas[2] = new Rectangle(141, 11, 17, 17);    // Up Use Item
            spriteAtlas[3] = new Rectangle(18, 97, 15, 30);     // Up Use Sword (18, 95, 15, 30) (18, 109, 15, 16)
            spriteAtlas[4] = new Rectangle(0, 11, 15, 16);      // Walk Down Frame 1
            spriteAtlas[5] = new Rectangle(17, 11, 15, 16);     // Walk Down Frame 2
            spriteAtlas[6] = new Rectangle(106, 11, 15, 16);    // Down Use Item
            spriteAtlas[7] = new Rectangle(18, 47, 15, 28);     // Down Use Sword (18, 45, 15, 30) (18, 47, 15, 16)
            spriteAtlas[8] = new Rectangle(34, 11, 17, 17);     // Walk Left 1
            spriteAtlas[9] = new Rectangle(52, 11, 15, 16);     // Walk Left 2
            spriteAtlas[10] = new Rectangle(123, 11, 17, 17);	// Left Use Item
            spriteAtlas[11] = new Rectangle(18, 78, 27, 15);    // Left Use Sword (18, 78, 27, 15) (18, 78, 16, 15)
            spriteAtlas[12] = new Rectangle(34, 11, 16, 16);    // Walk Right Frame 1
            spriteAtlas[13] = new Rectangle(52, 11, 16, 16);    // Walk Right frame 2
            spriteAtlas[14] = new Rectangle(123, 11, 17, 17);	// Right Use Item
            spriteAtlas[15] = new Rectangle(18, 78, 27, 15);    // Right Use Sword (18, 78, 27, 15) (18, 78, 16, 15)

            // Initial State and Direction of Link
            direction = Direction.Down;
            state = new StandingLinkState(this);
            isAttacking = false;

            speed = 4;
            xVel = 0;
            yVel = 0;
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
         public void ChangeHP(int value)
        {
            state.ChangeHP(value);
        }
        public int HP()
        {
            return state.HP();
        }
        public int MaxHP()
        {
            return state.MaxHP();
        }

        public void Draw()
        {

            spriteBatch.Begin();
            
            spriteBatch.Draw(texture, position, currentFrame, color, 0, new Vector2(), flipped, 1);


            

            spriteBatch.End();
        }

        public void TakeDamage(int val)
        {
            state.TakeDamage(val);
        }

        public void Taunt()
        {
            state.Taunt();
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

        public bool IsAttacking()
        {
            return isAttacking;
        }
    }
}
