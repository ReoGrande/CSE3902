

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


        void Update(Game1 game, int x, int y);// update accourding to input position 

        void ToMoving();
        void ItemDraw(SpriteBatch _spriteBatch);
        void ChangeDirection(Direction direction);
        void ChangeAttribute(ItemAttribute attribute);
        SpecialType ReturnSpecialType();

        IItem Clone();

        bool IsInfinite();//return whether this item is infinite 
        bool IsThrowable();//return whether this item is throwable
        bool IsDamaged();//return whether this item is damaged


        int GetX1();
        int GetX2();
        int GetY1();
        int GetY2();
        Rectangle GetPosition();

        void NumberChange(int changeValue);
        int Number();
        void CollisionWithNormalBlock();
        void CollisionWithEnemy(IEnemy enemy);
        void CollisionWithLink(ILinkState link,ItemSpace itemSpace);
        void Damage();
        void Draw(SpriteBatch _spriteBatch,Rectangle position);//draw this item in specific position
        void Use1(Game1 game);

    }

    public abstract class Item : IItem
    {
        public bool moveable;
        public bool infinite;
        public bool throwable;
        public bool pickable;
        public bool damaged;
        public ItemAttribute attribute;
        public SpecialType specialType;

        public Rectangle positionRectangle;
        public Texture2D ItemTextureSheet;
        public Rectangle rangeInSheet;
        public int number;


        // Directions in which the Item is moving
        public Direction direction;
        public abstract void ToMoving();
        public abstract void Update(Game1 game, int x, int y);
        public abstract void ItemDraw(SpriteBatch _spriteBatch);
        public void ChangeDirection(Direction direction)
        {
            this.direction = direction;
        }
         public void ChangeAttribute(ItemAttribute attribute)
        {
            this.attribute=attribute;
        }

        public void NumberChange(int changeValue)
        {
            number+=changeValue;

        }
         public int Number()
        {
            return this.number;

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
        public int GetX1() { return positionRectangle.X; }
        public int GetX2() { return positionRectangle.X + positionRectangle.Width; }
        public int GetY1() { return positionRectangle.Y; }
        public int GetY2() { return positionRectangle.Y + positionRectangle.Height; }
        public Rectangle GetPosition() { return positionRectangle; }

 

     


        public void Draw(SpriteBatch _spriteBatch,Rectangle position) 
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


        public List<Texture2D> textureSheetList;

        public StaticItem()
        {
            moveable = false;
            infinite = false;
            throwable = false;
            damaged = false;
            pickable=false;
            number=1;
            attribute=ItemAttribute.FriendlyAttack;
            specialType=SpecialType.Default;
            

        }



        public StaticItem(Texture2D textureSheet, Rectangle positionRectangle) : this()
        {
            ItemTextureSheet = textureSheet;
            this.positionRectangle = positionRectangle;
            this.rangeInSheet = new Rectangle(0, 0, textureSheet.Width, textureSheet.Height);
            textureSheetList = new List<Texture2D>();
            textureSheetList.Add(textureSheet);
        }

        public StaticItem(Texture2D textureSheet, Rectangle positionRectangle, Rectangle rangeInSheet) : this(textureSheet, positionRectangle)
        {
            this.positionRectangle = positionRectangle;
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
            if (GetX1() < -300 || GetX2() > rightBound+300 || GetY1() < -300 || GetY2() > downBound+300)
            { Damage(); }
        }

            public override void CollisionWithLink(ILinkState link, ItemSpace itemSpace){
            if (attribute == ItemAttribute.AdverseAttack) { 
            link.TakeDamage();

            Damage();
            }
            if (pickable) 
            {   int itemLocation=existInSpace(itemSpace);
                if ( itemLocation< 0) { 
                itemSpace.Add(this.Clone());}
                else {
                    itemSpace.ItemList()[itemLocation].NumberChange(1);
                }

                Damage();

            }
        }

        protected int existInSpace(ItemSpace itemSpace)
        {
            List<IItem> list=itemSpace.ItemList();
            int lenth=list.Count;
            for (int i=0; i<lenth; i++)
            {
                if (list[i].ReturnSpecialType() == this.specialType) 
                {return i;}
            }
            return -1;

        }


        public override void CollisionWithEnemy(IEnemy enemy)
        {
            if (this.attribute == ItemAttribute.FriendlyAttack && enemy.Touchable()) { 
            Damage();
            enemy.GetDamaged();
            enemy.ChangeHP(-1);
            SoundFactory.Instance.PlaySoundEnemyHit();}
        }




        public override void Update(Game1 game, int x, int y)
        {
            positionRectangle.X = x;
            positionRectangle.Y = y;
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