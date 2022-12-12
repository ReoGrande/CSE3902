

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

namespace sprint0
{
    public interface IItem
    {


        void Update(Game1 game, Rectangle position);// update accourding to input position 
        void ToMoving();
        void ItemDraw(SpriteBatch _spriteBatch);
        void ChangeDirection(Direction direction);
        void ChangeAttribute(ItemAttribute attribute);
        SpecialType ReturnSpecialType();
        IItem Clone();
        bool IsInfinite();//return whether this item is infinite 
        bool IsThrowable();//return whether this item is throwable
        bool IsDamaged();//return whether this item is damaged
        void ChangeIsUsingState(bool reuslt);

        int GetX1();
        int GetX2();
        int GetY1();
        int GetY2();
        Rectangle GetPosition();
        void NumberChange(int changeValue);
        void ChangeSheet(int sheetNumber);
        void SetPickable(bool result);

        int Number();
        int TriggerTime();
        void SetNumber(int value);
        void CollisionWithNormalBlock();
        void CollisionWithEnemy(IEnemy enemy);
        void CollisionWithBound(Rectangle bound);
        void CollisionWithLink(ILinkState link, ItemSpace itemSpace);
        void Damage();
        void Draw(SpriteBatch _spriteBatch, Rectangle position);//draw this item in specific position
        void Use1(Game1 game);

    }

    public abstract class Item : IItem
    {
        public bool moveable;
        public bool infinite;
        public bool throwable;
        public bool pickable;
        public bool damaged;
        public bool isUsing;
        public ItemAttribute attribute;
        public SpecialType specialType;

        public Rectangle positionRectangle;
        public Texture2D ItemTextureSheet;
        public Rectangle rangeInSheet;
        public int number;
        public int triggerTime;

        // Directions in which the Item is moving
        public Direction direction;
        public abstract void ToMoving();
        public abstract void Update(Game1 game, Rectangle position);
        public abstract void ItemDraw(SpriteBatch _spriteBatch);
        public abstract void ChangeSheet(int sheetNumber);
        public void ChangeDirection(Direction direction)
        {
            this.direction = direction;
        }
        public void ChangeAttribute(ItemAttribute attribute)
        {
            this.attribute = attribute;
        }

        public void ChangeIsUsingState(bool result)
        {
            this.isUsing = result;
        }

        public void NumberChange(int changeValue)
        {
            number += changeValue;

        }
        public int Number()
        {
            return this.number;

        }

        public int TriggerTime()
        {
            return this.triggerTime;

        }


        public void SetNumber(int value)
        {

            this.number = value;
        }
        public SpecialType ReturnSpecialType()
        {
            return this.specialType;
        }

        public bool IsInfinite()
        {
            return this.infinite;
        }
        public bool IsThrowable()
        {
            return this.throwable;
        }
        public bool IsDamaged()
        {
            return this.damaged;
        }

        public abstract IItem Clone();
        public abstract void Damage();
        public abstract void CollisionWithNormalBlock();
        public abstract void CollisionWithEnemy(IEnemy enemy);
        public abstract void CollisionWithLink(ILinkState link, ItemSpace itemSpace);
        public abstract void CollisionWithBound(Rectangle bound);
        public int GetX1() { return positionRectangle.X; }
        public int GetX2() { return positionRectangle.X + positionRectangle.Width; }
        public int GetY1() { return positionRectangle.Y; }
        public int GetY2() { return positionRectangle.Y + positionRectangle.Height; }
        public Rectangle GetPosition() { return positionRectangle; }





        public void SetPickable(bool result)
        {

            this.pickable = result;
        }
        public void Draw(SpriteBatch _spriteBatch, Rectangle position)
        {
            _spriteBatch.Draw(
               ItemTextureSheet,
               position,
               rangeInSheet,
               Color.White
               );


        }
        public abstract void Use1(Game1 game);

    }



    public class StaticItem : Item
    {

        public int damage;
        public List<Texture2D> textureSheetList;

        public StaticItem()
        {
            damage = 0;
            moveable = false;
            infinite = false;
            throwable = false;
            damaged = false;
            pickable = false;
            isUsing = false;
            number = 1;
            attribute = ItemAttribute.FriendlyAttack;
            specialType = SpecialType.Default;
            textureSheetList = new List<Texture2D>();
            triggerTime = 0;


        }





        public StaticItem(Texture2D textureSheet, Rectangle positionRectangle) : this()
        {
            ItemTextureSheet = textureSheet;
            this.positionRectangle = positionRectangle;
            this.rangeInSheet = new Rectangle(0, 0, textureSheet.Width, textureSheet.Height);
            textureSheetList.Add(textureSheet);
        }

        public StaticItem(Texture2D textureSheet, Rectangle positionRectangle, Rectangle rangeInSheet) : this(textureSheet, positionRectangle)
        {
            this.positionRectangle = positionRectangle;
        }


        public override void ChangeSheet(int sheetNumber)
        {
            ItemTextureSheet = textureSheetList[sheetNumber];

        }

        public override IItem Clone()
        {
            IItem itemClone = new StaticItem(this.ItemTextureSheet, this.positionRectangle, this.rangeInSheet);
            return itemClone;

        }

        public void AddFrames(Texture2D textureSheet)
        {
            textureSheetList.Add(textureSheet);
        }

        public override void Damage()
        {
            this.damaged = true;
        }

        protected void CheckOutOfBound(Game1 game)
        {


            int rightBound = game.GraphicsDevice.PresentationParameters.BackBufferWidth;
            int downBound = game.GraphicsDevice.PresentationParameters.BackBufferHeight;
            if (GetX1() < -300 || GetX2() > rightBound + 300 || GetY1() < -300 || GetY2() > downBound + 300)
            { Damage(); }
        }

        public override void CollisionWithLink(ILinkState link, ItemSpace itemSpace)
        {
            if (attribute == ItemAttribute.AdverseAttack)
            {
                link.TakeDamage(0);

                Damage();
            }
            if (pickable)
            {
                int itemLocation = existInSpace(itemSpace);
                if (itemLocation < 0)
                {
                    IItem newItem = this.Clone();
                    newItem.SetNumber(1);
                    itemSpace.Add(newItem);
                }
                else
                {
                    itemSpace.ItemList()[itemLocation].NumberChange(1);
                }
                SoundFactory.Instance.PlaySoundPickup();
                Damage();

            }
        }

        protected int existInSpace(ItemSpace itemSpace)
        {
            List<IItem> list = itemSpace.ItemList();
            int lenth = list.Count;
            for (int i = 0; i < lenth; i++)
            {
                if (list[i].ReturnSpecialType() == this.specialType)
                { return i; }
            }
            return -1;

        }


        public override void CollisionWithEnemy(IEnemy enemy)
        {
            if (this.attribute == ItemAttribute.FriendlyAttack && enemy.Touchable())
            {
                Damage();
                enemy.GetDamaged();
                if (this.damage.GetType() != null)
                {
                    enemy.ChangeHP(this.damage);
                }
                SoundFactory.Instance.PlaySoundEnemyHit();
            }
        }
        public override void CollisionWithBound(Rectangle bound)
        {

            Damage();

        }



        public override void Update(Game1 game, Rectangle position)
        {

            CheckOutOfBound(game);

        }

        public override void ToMoving()
        {
            //not move for this kind of item
            //TODO: IMPLEMENT UPDATE METHODS? MAYBE
        }


        public override void ItemDraw(SpriteBatch _spriteBatch)
        {
            _spriteBatch.Draw(
            ItemTextureSheet,
            positionRectangle,
            rangeInSheet,
            Color.White
            );
        }

        public override void CollisionWithNormalBlock()
        {
            this.Damage();
        }



        public override void Use1(Game1 game)
        {

        }


    }





}