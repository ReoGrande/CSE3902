



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
        
        equipmentBoxSheet= game.Content.Load<Texture2D>("Ornament/EquipmentBox");
        }

        private void DrawEquipmentBox(SpriteBatch _spriteBatch,Rectangle position)
        {
               _spriteBatch.Draw(
            equipmentBoxSheet,
            position,
            new Rectangle(3,8,210,210),
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


        public void Draw(Game1 game,SpriteBatch _spriteBatch)
        {
            
              DrawEquipmentBox(_spriteBatch,new Rectangle(10,10,50,50));
              DrawEquipmentBox(_spriteBatch,new Rectangle(60,10,50,50));
              DrawEquipmentBox(_spriteBatch,new Rectangle(110,10,50,50));
            _spriteBatch.DrawString(game.font, "1", new Vector2(30,60), Color.Black);
            _spriteBatch.DrawString(game.font, "2", new Vector2(80,60), Color.Black);
            _spriteBatch.DrawString(game.font, "3", new Vector2(130,60), Color.Black);
            if(this.itemList.ToArray().Length > 0)itemList[0].Draw(_spriteBatch,new Rectangle(22,23,25,25));
            if(this.itemList.ToArray().Length > 1)itemList[1].Draw(_spriteBatch,new Rectangle(72,23,25,25));
            if(this.itemList.ToArray().Length > 2)itemList[2].Draw(_spriteBatch,new Rectangle(122,23,25,25));



        }

        public void Update(Game1 game, int x, int y)
        {
            foreach (IItem item in itemList)
            {
                item.Update(game, x, y);
            }

        }



        public void PreviousItem()
        {
            int number=itemList.Count();
            if(number >0)
            {IItem finalItem=itemList[number-1];
                itemList.RemoveAt(number-1);
            itemList.Insert(0, finalItem);
            }
            
            
        }

        public void NextItem()
        {  
            int number=itemList.Count();
            if(number >0)
            {IItem startItem=itemList[0];
                itemList.RemoveAt(0);
            itemList.Add(startItem);
            }
            
        }



    }
}
