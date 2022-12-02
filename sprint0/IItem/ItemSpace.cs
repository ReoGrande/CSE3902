



using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections;
using System.ComponentModel;
using System.Reflection.Metadata;
using Microsoft.Xna.Framework.Input;


namespace sprint0
{

    public class ItemSpace
    {
        private List<IItem> itemList;
        private int currentIndex;
        private Texture2D equipmentBoxSheet;


        public ItemSpace()
        {
            itemList = new List<IItem>();
            currentIndex = 0;
        }

        public void LoadBox(Game1 game)
        {

            equipmentBoxSheet = game.Content.Load<Texture2D>("Ornament/EquipmentBox");
        }

        public void DrawEquipmentBox(SpriteBatch _spriteBatch, Rectangle position)
        {
            _spriteBatch.Draw(
            equipmentBoxSheet,
            position,
            new Rectangle(3, 8, 210, 210),
            Color.White
            );

        }


        public List<IItem> ItemList()
        { return itemList; }

        public IItem CurrentItem()
        {
            return itemList[currentIndex];
        }



        public void Add(IItem iItem)
        {
            this.itemList.Add(iItem);
        }


        public void Remove(IItem iItem)
        {
            this.itemList.Remove(iItem);
        }

        public void Clear()
        {
            this.itemList = new List<IItem>();
        }


        public void Exchange(IItem iItem)
        {
            this.itemList[currentIndex] = iItem;
        }


        public void Draw(Game1 game, SpriteBatch _spriteBatch, Rectangle spot)
        {
            Rectangle tempSpot = spot;
            DrawEquipmentBox(_spriteBatch, spot);
            tempSpot.X = tempSpot.X+50;
            DrawEquipmentBox(_spriteBatch, tempSpot);
            tempSpot.X = tempSpot.X+50;
            DrawEquipmentBox(_spriteBatch, tempSpot);
            tempSpot.X = spot.X +5;
            tempSpot.Y = spot.Y+45;
            _spriteBatch.DrawString(game.font, "1", new Vector2(tempSpot.X,tempSpot.Y), Color.White);
            tempSpot.X = tempSpot.X+50;
            _spriteBatch.DrawString(game.font, "2", new Vector2(tempSpot.X,tempSpot.Y), Color.White);
            tempSpot.X = tempSpot.X+50;
            _spriteBatch.DrawString(game.font, "3", new Vector2(tempSpot.X,tempSpot.Y), Color.White);

            tempSpot.X = spot.X +12;
            tempSpot.Y = spot.Y +23;
            tempSpot.Width = spot.Width - 25;
            tempSpot.Height = spot.Height - 35;

            if (this.itemList.ToArray().Length > 0) itemList[0].Draw(_spriteBatch, tempSpot);
            tempSpot.X = tempSpot.X+50;
            if (this.itemList.ToArray().Length > 1) itemList[1].Draw(_spriteBatch, tempSpot);
            tempSpot.X = tempSpot.X+50;
            if (this.itemList.ToArray().Length > 2) itemList[2].Draw(_spriteBatch, tempSpot);
            tempSpot.X = spot.X +5;
            tempSpot.Y = spot.Y+5;
            //draw number for how many items left
            if (this.itemList.ToArray().Length > 0) _spriteBatch.DrawString(game.font, "N:" + itemList[0].Number().ToString(), new Vector2(tempSpot.X,tempSpot.Y), Color.White);
            tempSpot.X = tempSpot.X+50;
            if (this.itemList.ToArray().Length > 1) _spriteBatch.DrawString(game.font, "N:" + itemList[1].Number().ToString(), new Vector2(tempSpot.X,tempSpot.Y), Color.White);
            tempSpot.X = tempSpot.X+50;
            if (this.itemList.ToArray().Length > 2) _spriteBatch.DrawString(game.font, "N:" + itemList[2].Number().ToString(), new Vector2(tempSpot.X,tempSpot.Y), Color.White);



            // DrawEquipmentBox(_spriteBatch, new Rectangle(410, 100, 50, 70));
            // DrawEquipmentBox(_spriteBatch, new Rectangle(460, 100, 50, 70));
            // DrawEquipmentBox(_spriteBatch, new Rectangle(510, 100, 50, 70));
            // _spriteBatch.DrawString(game.font, "1", new Vector2(415, 145), Color.White);
            // _spriteBatch.DrawString(game.font, "2", new Vector2(465, 145), Color.White);
            // _spriteBatch.DrawString(game.font, "3", new Vector2(515, 145), Color.White);


            // if (this.itemList.ToArray().Length > 0) itemList[0].Draw(_spriteBatch, new Rectangle(422, 123, 25, 35));
            // if (this.itemList.ToArray().Length > 1) itemList[1].Draw(_spriteBatch, new Rectangle(472, 123, 25, 35));
            // if (this.itemList.ToArray().Length > 2) itemList[2].Draw(_spriteBatch, new Rectangle(522, 123, 25, 35));

            // //draw number for how many items left
            // if (this.itemList.ToArray().Length > 0) _spriteBatch.DrawString(game.font, "N:" + itemList[0].Number().ToString(), new Vector2(415, 105), Color.White);
            // if (this.itemList.ToArray().Length > 1) _spriteBatch.DrawString(game.font, "N:" + itemList[1].Number().ToString(), new Vector2(465, 105), Color.White);
            // if (this.itemList.ToArray().Length > 2) _spriteBatch.DrawString(game.font, "N:" + itemList[2].Number().ToString(), new Vector2(515, 105), Color.White);



        }

        public void Update(Game1 game, Rectangle position)
        {

            for (int i = 0; i < itemList.Count; i++)
            {
                IItem item = itemList[i];
                item.Update(game, position);
                if (item.Number() <= 0)
                {
                    itemList.RemoveAt(i);
                }

            }

        }



        public void PreviousItem()
        {
            int number = itemList.Count();
            if (number > 0)
            {
                IItem finalItem = itemList[number - 1];
                itemList.RemoveAt(number - 1);
                itemList.Insert(0, finalItem);
            }


        }

        public void NextItem()
        {
            int number = itemList.Count();
            if (number > 0)
            {
                IItem startItem = itemList[0];
                itemList.RemoveAt(0);
                itemList.Add(startItem);
            }

        }



    }
}
