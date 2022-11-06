


using Microsoft.Xna.Framework.Graphics;

namespace sprint0
{

    public class OutItemSpace
    {
        private List<IItem> outItemList;

        public OutItemSpace()
        {
            outItemList = new List<IItem>();
        }

        public List<IItem> OutItemList()
        { return outItemList; }




        public void Add(IItem iItem)
        {
            this.outItemList.Add(iItem);
        }




        public void Clear()
        {
            this.outItemList = new List<IItem>();
        }

        public void Draw(SpriteBatch _spriteBatch)
        {


            foreach (IItem item in outItemList)
            {
                item.ItemDraw(_spriteBatch);
            }

        }

        public void Update(Game1 game)
        {
            for (int i = 0; i < outItemList.Count; i++)
            {
                IItem item = outItemList[i];
                
                item.Update(game, item.GetX1(), item.GetY1());
                if (item.IsDamaged())
                {
                    outItemList.RemoveAt(i);
                }

            }


        }






    }
}
