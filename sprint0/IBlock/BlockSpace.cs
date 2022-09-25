
using System;

namespace sprint0
{

    public class BlockSpace
    {
        private List<IBlock> blockList;

        public BlockSpace()
        {
            blockList = new List<IBlock>();
        }

        public void Add(IBlock iblock)
        {
            this.blockList.Add(iblock);
        }

        public void Draw(SpriteBatch _spriteBatch)
        {
            foreach (IBlock block in blockList)
            {
                block.BlockDraw(_spriteBatch);
            }


        }



    }
}

