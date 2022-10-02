﻿


using Microsoft.Xna.Framework.Graphics;

namespace sprint0
{

    public class OutItemSpace
    {
        private List<IItem> outItemList;
        private int currentIndex;

        public OutItemSpace()
        {
            outItemList = new List<IItem>();
            currentIndex = 0;
        }

        public List<IItem> OutItemList()
        { return outItemList; }




        public void Add(IItem iItem)
        {
            this.outItemList.Add(iItem);
        }

        public void Draw(SpriteBatch _spriteBatch)
        {


            foreach (IItem item in outItemList)
            {
                item.ItemDraw(_spriteBatch);
            }

        }

        public void Update(int x, int y)
        {
            foreach (IItem item in outItemList)
            {
                item.Update(x, y);
            }

        }






    }
}
