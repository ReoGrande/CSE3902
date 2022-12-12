using System;
using System.Reflection;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;
using sprint0;
using static System.Formats.Asn1.AsnWriter;
using static sprint0.Arrow;
using static sprint0.Link;



public class TriForcePiece : StaticItem
{




    public TriForcePiece() : base()
    {

        pickable = true;
        attribute = ItemAttribute.NotHandle;
        specialType = SpecialType.TriForcePiece;

    }



    public TriForcePiece(Texture2D textureSheet, Rectangle positionRectangle) : this()
    {
        ItemTextureSheet = textureSheet;
        this.positionRectangle = positionRectangle;
        this.rangeInSheet = new Rectangle(0, 0, textureSheet.Width, textureSheet.Height);
    }

    public TriForcePiece(Texture2D textureSheet, Rectangle positionRectangle, Rectangle rangeInSheet) : this(textureSheet, positionRectangle)
    {
        this.positionRectangle = positionRectangle;
    }

    public override void CollisionWithLink(ILinkState link, ItemSpace itemSpace)
    {
        //TODO: Make gamestate win here
        MediaPlayer.Stop();
        Link Llink = (Link)link;
        Llink.game.gameState.Win();
    }


}






