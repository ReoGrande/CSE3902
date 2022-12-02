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
        textureSheetList.Add(textureSheet);
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
            int sheetNumber = 0;
            Rectangle position = game.character.GetPosition();

            switch (game.character.GetDirection())
            {
                case Direction.Up:
                    sheetNumber = 3;
                    break;

                case Direction.Down:
                    sheetNumber = 2;
                    break;

                case Direction.Left:
                    sheetNumber = 1;
                    break;

                case Direction.Right:
                    //direction
                    break;
                default:
                    Console.WriteLine("Error: Incorrect command.");
                    return;
            }

            IItem newItem = ItemFactory.Instance.CreatePickaxe(position);
            newItem.ChangeSheet(sheetNumber);
            newItem.SetPickable(false);
            game.outItemSpace.Add(newItem);
            game.character.ToThrowing();
            //sound of use pickaxe
            //SoundFactory.Instance.PlaySoundShootArrow();
            number--;
        }
    }




}





