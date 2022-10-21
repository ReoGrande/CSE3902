


using Microsoft.Xna.Framework.Graphics;

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


        public void Draw(SpriteBatch _spriteBatch)
        {
            if(this.itemList.ToArray().Length > 0)itemList[currentIndex].ItemDraw(_spriteBatch);

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
