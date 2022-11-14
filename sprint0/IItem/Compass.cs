using System;
using System.Reflection;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using sprint0;
using static System.Formats.Asn1.AsnWriter;
using static sprint0.Arrow;
using static sprint0.Link;



public class Compass : StaticItem
{




    public Compass() : base()
    {

        pickable = true;
        attribute = ItemAttribute.NotHandle;
        specialType = SpecialType.Compass;

    }



    public Compass(Texture2D textureSheet, Rectangle positionRectangle) : this()
    {
        ItemTextureSheet = textureSheet;
        this.positionRectangle = positionRectangle;
        this.rangeInSheet = new Rectangle(0, 0, textureSheet.Width, textureSheet.Height);
    }

    public Compass(Texture2D textureSheet, Rectangle positionRectangle, Rectangle rangeInSheet) : this(textureSheet, positionRectangle)
    {
        this.positionRectangle = positionRectangle;
    }


}






