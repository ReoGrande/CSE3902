using System;
using System.Reflection;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using sprint0;
using static System.Formats.Asn1.AsnWriter;
using static sprint0.Arrow;
using static sprint0.Link;


/*
public class Blast : StaticItem
{
    private int timer;
    private int index;
    public List<Rectangle> sourceRectangleList;

    public Blast() : base()
    {

        attribute = ItemAttribute.NotHandle;
        sourceRectangleList = new List<Rectangle>();
        Rectangle rectangle1 = new Rectangle(0, 0, textureSheet.Width, textureSheet.Height);
        timer = 0;
        index = 0;
        //read rectangles
        int y = 0;
        for (i = 0; i < 4; i++)
        {
            int x = 0;

            for (j = 0; j < 3; j++)
            {
                sourceRectangleList.Add(new Rectangle(x, y, 62, 65));
                x += 62;
            }
            y += 65;
        }


    }



    public Blast(Texture2D textureSheet, Rectangle positionRectangle) : this()
    {
        ItemTextureSheet = textureSheet;
        index = 0;
        this.positionRectangle = positionRectangle;
        this.rangeInSheet = new Rectangle(0, 0, textureSheet.Width, textureSheet.Height);
    }

    public Blast(Texture2D textureSheet, Rectangle positionRectangle, Rectangle rangeInSheet) : this(textureSheet, positionRectangle)
    {
        this.positionRectangle = positionRectangle;
    }



    public override void Update(Game1 game, int x, int y)
    {

        if (timer >= 12)
        {
            {
                if (index < 12)
                { index++; }
                else
                {
                    index = 0;
                    Damage();
                }

                rangeInSheet = sourceRectangleList[i];

                timer = 0;
            }
        }
        else { timer++; }


    }
}


*/


