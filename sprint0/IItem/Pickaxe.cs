using System;
using System.Reflection;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using sprint0;
using static System.Formats.Asn1.AsnWriter;
using static sprint0.Arrow;
using static sprint0.Link;



public class Pickaxe : StaticItem
{




    public Pickaxe() : base()
    {

        pickable = true;
        specialType = SpecialType.Pickaxe;
        attribute = ItemAttribute.NotHandle;
        number = 50;
    }


    public Pickaxe(Texture2D textureSheet, Rectangle positionRectangle) : this()
    {
        ItemTextureSheet = textureSheet;
        this.positionRectangle = positionRectangle;
        this.rangeInSheet = new Rectangle(0, 0, textureSheet.Width, textureSheet.Height);
    }

    public Pickaxe(Texture2D textureSheet, Rectangle positionRectangle, Rectangle rangeInSheet) : this(textureSheet, positionRectangle)
    {
        this.positionRectangle = positionRectangle;
    }





    public override IItem Clone()
    {
        IItem itemClone = ItemFactory.Instance.CreatePickaxe(this.positionRectangle);

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
            number--;
        }
    }


}


