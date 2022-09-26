
using System;

namespace sprint0
{

    public class BlockSpace
    {
        private List<IBlock> blockList;

        private BlockSpace()
        {
            blockList = new List<IBlock>;
        }

        public Add(IBlock iblock)
        {
            this.blockList.Add(iblock);
        }

        public Draw(SpriteBatch _spriteBatch)
        {
            foreach (IBlock block in blockList)
            {
                block.BlockDraw(_spriteBatch);
            }


        }



    }
}

