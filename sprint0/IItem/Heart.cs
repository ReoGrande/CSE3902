using System;
using System.Reflection;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using sprint0;
using static System.Formats.Asn1.AsnWriter;
using static sprint0.Arrow;
using static sprint0.Link;


public class Heart : StaticItem
{
    public Heart() : base()
    {
        pickable = true;
        attribute = ItemAttribute.FriendlyCure;
        specialType = SpecialType.Heart;
    }

    public Heart(Texture2D textureSheet, Rectangle positionRectangle) : this()
    {
        ItemTextureSheet = textureSheet;
        this.positionRectangle = positionRectangle;
        this.rangeInSheet = new Rectangle(0, 0, textureSheet.Width, textureSheet.Height);
    }

    public Heart(Texture2D textureSheet, Rectangle positionRectangle, Rectangle rangeInSheet) : this(textureSheet, positionRectangle)
    {
        this.positionRectangle = positionRectangle;
    }
    public override void CollisionWithLink(ILinkState link, ItemSpace itemSpace)
    {

        //if(link.hp<=maxhp)
        //{link.hp+=1;
        //Damage;}
        Damage();
    }


}






