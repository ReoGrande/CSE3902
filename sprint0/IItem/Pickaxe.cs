using System;
using System.Reflection;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using sprint0;
using static System.Formats.Asn1.AsnWriter;
using static sprint0.Arrow;
using static sprint0.Link;


namespace sprint0
{
    public class Pickaxe : StaticItem
    {




        public Pickaxe() : base()
        {

            pickable = true;
            specialType = SpecialType.Pickaxe;
            attribute = ItemAttribute.NotHandle;
            number = 1;
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
                int locationShift = 30;
                int sheetNumber = 0;
                Rectangle position = game.character.GetPosition();

                switch (game.character.GetDirection())
                {
                    case Direction.Up:
                        sheetNumber = 3;
                        position.Y -= locationShift;
                        break;

                    case Direction.Down:
                        sheetNumber = 2;
                        position.Y += locationShift;
                        break;

                    case Direction.Left:
                        sheetNumber = 1;
                        position.X -= locationShift;
                        break;

                    case Direction.Right:
                        position.X += locationShift;
                        //direction
                        break;
                    default:
                        Console.WriteLine("Error: Incorrect command.");
                        return;
                }

                IItem newItem = ItemFactory.Instance.CreatePickaxe(position);
                newItem.ChangeSheet(sheetNumber);
                newItem.SetPickable(false);
                newItem.ChangeIsUsingState(true);
                game.outItemSpace.Add(newItem);
                game.character.ToThrowing();
                //sound of use pickaxe
                //SoundFactory.Instance.PlaySoundShootArrow();
                number--;
            }
        }

        public override void CollisionWithNormalBlock()
        {
            if (triggerTime > 12)
            {
                this.Damage();
            }
        }
        public override void Update(Game1 game, Rectangle position)
        {

            triggerTime++;
            if (triggerTime > 16 && isUsing)
            {
                game.itemSpace.Add(this.Clone());
                this.Damage();

            }

        }


    }




}





