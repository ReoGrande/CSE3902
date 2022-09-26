﻿


namespace sprint0
{

    public class ItemSpace
    {
        private List<IItem> itemList;
        private int currentIndex;

        public ItemSpace()
        {
            itemList = new List<IItem>();
            currentIndex = 0;
        }

        public void Add(IItem iItem)
        {
            this.itemList.Add(iItem);
        }



        public void Draw(SpriteBatch _spriteBatch)
        {
            itemList[currentIndex].ItemDraw(_spriteBatch);

        }

        public void PreviousItem()
        {
            if (currentIndex > 0)
            {
                currentIndex--;
            }
            else
            {
                currentIndex = itemList.Count - 1;
            }
        }

        public void NextItem()
        {
            if (currentIndex < itemList.Count - 1)
            {
                currentIndex++;
            }
            else
            {
                currentIndex = 0;
            }
        }



    }
}
