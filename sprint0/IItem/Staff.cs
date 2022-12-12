using System;
using System.Reflection;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using sprint0;
using static System.Formats.Asn1.AsnWriter;
using static sprint0.Arrow;
using static sprint0.Link;



public class Staff : StaticItem
{




    public Staff() : base()
    {

        pickable = true;
        specialType = SpecialType.Staff;
        attribute = ItemAttribute.NotHandle;
        number = 50;


    }


    public Staff(Texture2D textureSheet, Rectangle positionRectangle) : this()
    {
        ItemTextureSheet = textureSheet;
        this.positionRectangle = positionRectangle;
        this.rangeInSheet = new Rectangle(0, 0, textureSheet.Width, textureSheet.Height);
    }

    public Staff(Texture2D textureSheet, Rectangle positionRectangle, Rectangle rangeInSheet) : this(textureSheet, positionRectangle)
    {
        this.positionRectangle = positionRectangle;
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
                newItem.SetNumber(50);
                itemSpace.Add(newItem);
            }
            else
            {
                itemSpace.ItemList()[itemLocation].NumberChange(50);
            }
            SoundFactory.Instance.PlaySoundPickupStaff();
            Damage();

        }
    }



    public override IItem Clone()
    {
        IItem itemClone = ItemFactory.Instance.CreateStaff(this.positionRectangle);

        return itemClone;
    }

    public override void Use1(Game1 game)
    {
        if (number > 0)
        {
            IItem shieldBall = ItemFactory.Instance.CreateShieldBall(this.positionRectangle); //ShieldBall Position in ItemList
            shieldBall.ToMoving();
            game.outItemSpace.Add(shieldBall);
            game.character.ToThrowing();
            SoundFactory.Instance.PlaySoundShieldBall();
            number--;
        }
    }


}






