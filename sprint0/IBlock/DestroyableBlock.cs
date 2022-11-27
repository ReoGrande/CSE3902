

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


    public class DestroyableBlock : StaticBlock
    {



        protected Color color;
        public DestroyableBlock(Texture2D textureSheet, Rectangle positionRectangle) : base(textureSheet, positionRectangle)
        {
            color = Color.Gray;
        }



        public DestroyableBlock(Texture2D textureSheet, Rectangle positionRectangle, Rectangle rangeInSheet) : base(textureSheet, positionRectangle, rangeInSheet)
        {
            color = Color.Gray;
        }

        public override void CollisionWithItem(IItem item)
        {
            if (item.ReturnSpecialType() == SpecialType.Blast)
            {
                this.needRemove = true;
                SoundFactory.Instance.PlaySoundRockCrush();
            }
            else
            {
                item.CollisionWithNormalBlock();
            }
        }


        public override void BlockUpdate(GraphicsDeviceManager _graphics, GameTime gameTime)
        {
            //TODO: IMPLEMENT UPDATE METHODS? MAYBE
        }


        public override void BlockDraw(SpriteBatch _spriteBatch)
        {

            _spriteBatch.Draw(
            BlockTextureSheet,
            positionRectangle,
            rangeInSheet,
            color
            );


        }
    }


}
