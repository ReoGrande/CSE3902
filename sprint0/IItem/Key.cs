using System;
using System.Reflection;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using sprint0;
using static System.Formats.Asn1.AsnWriter;
using static sprint0.Arrow;
using static sprint0.Link;



public class Key : StaticItem
{




    public Key() : base()
    {

        pickable = true;
        attribute = ItemAttribute.NotHandle;
        specialType = SpecialType.Key;

    }


    public Key(Texture2D textureSheet, Rectangle positionRectangle) : this()
    {
        ItemTextureSheet = textureSheet;
        this.positionRectangle = positionRectangle;
        this.rangeInSheet = new Rectangle(0, 0, textureSheet.Width, textureSheet.Height);
    }

    public Key(Texture2D textureSheet, Rectangle positionRectangle, Rectangle rangeInSheet) : this(textureSheet, positionRectangle)
    {
        this.positionRectangle = positionRectangle;
    }


    public override IItem Clone()
    {
        IItem itemClone = ItemFactory.Instance.CreateKey(this.positionRectangle);

        return itemClone;
    }




}





