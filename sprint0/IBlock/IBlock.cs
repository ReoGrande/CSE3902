

using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections;
using System.ComponentModel;
using System.Reflection.Metadata;
using Microsoft.Xna.Framework.Input;
using System.Runtime.InteropServices;
using static System.Formats.Asn1.AsnWriter;
using static sprint0.Link;
using System.Reflection.Metadata.Ecma335;

namespace sprint0
{
    public interface IBlock
    {
        void BlockUpdate(GraphicsDeviceManager _graphics, GameTime gameTime);
        void BlockDraw(SpriteBatch _spriteBatch);

        int GetX1();
        int GetX2();
        int GetY1();
        int GetY2();
        Rectangle GetPosition();
        bool NeedRemove();
        void CollisionWithItem(IItem item);
        void CollisionWithEnemy(IEnemy enemy);
    }

    public abstract class Block : IBlock
    {

        protected Rectangle positionRectangle;
        protected Texture2D BlockTextureSheet;
        protected bool needRemove;


        public abstract void BlockUpdate(GraphicsDeviceManager _graphics, GameTime gameTime);
        public abstract void BlockDraw(SpriteBatch _spriteBatch);
        public int GetX1() { return positionRectangle.X; }
        public int GetX2() { return positionRectangle.X + positionRectangle.Width; }
        public int GetY1() { return positionRectangle.Y; }
        public int GetY2() { return positionRectangle.Y + positionRectangle.Height; }
        public Rectangle GetPosition() { return positionRectangle; }

        public abstract void CollisionWithItem(IItem item);
        public bool NeedRemove() { return needRemove; }
        public void CollisionWithEnemy(IEnemy enemy)
        {
            Rectangle enemyPos = enemy.GetPosition();
            if (enemyPos.Intersects(this.GetPosition()))
            {
                switch (enemy.GetDirection())
                {
                    case Direction.Up:
                        enemy.ChangePosition(new Rectangle(enemyPos.X, this.GetPosition().Bottom, enemyPos.Width, enemyPos.Height));
                        break;
                    case Direction.Down:
                        enemy.ChangePosition(new Rectangle(enemyPos.X, this.GetPosition().Top - enemyPos.Height, enemyPos.Width, enemyPos.Height));
                        break;
                    case Direction.Left:
                        enemy.ChangePosition(new Rectangle(this.GetPosition().Right, enemyPos.Y, enemyPos.Width, enemyPos.Height));
                        break;
                    case Direction.Right:
                        enemy.ChangePosition(new Rectangle(this.GetPosition().Left - enemyPos.Width, enemyPos.Y, enemyPos.Width, enemyPos.Height));
                        break;
                    default:
                        break;
                }
            }

        }

    }
    //non-moving,non-animated sprite
    public class StaticBlock : Block
    {

        protected Rectangle rangeInSheet;


        public StaticBlock(Texture2D textureSheet, Rectangle positionRectangle)
        {
            BlockTextureSheet = textureSheet;
            this.positionRectangle = positionRectangle;
            this.rangeInSheet = new Rectangle(0, 0, textureSheet.Width, textureSheet.Height);
            this.needRemove = false;

        }



        public StaticBlock(Texture2D textureSheet, Rectangle positionRectangle, Rectangle rangeInSheet) : this(textureSheet, positionRectangle)
        {
            this.rangeInSheet = rangeInSheet;


        }

        public override void BlockUpdate(GraphicsDeviceManager _graphics, GameTime gameTime)
        {
            //TODO: IMPLEMENT UPDATE METHODS? MAYBE
        }
        public override void CollisionWithItem(IItem item)
        {

            if (item.ReturnSpecialType() == SpecialType.Pickaxe && item.TriggerTime() > 12)
            {
                this.needRemove = true;
                SoundFactory.Instance.PlaySoundRockCrush();

            }
            item.CollisionWithNormalBlock();
        }

        public override void BlockDraw(SpriteBatch _spriteBatch)
        {

            _spriteBatch.Draw(
            BlockTextureSheet,
            positionRectangle,
            rangeInSheet,
            Color.White
            );


        }
    }


}
