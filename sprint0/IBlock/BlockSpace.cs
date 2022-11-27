
using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
namespace sprint0
{

    public class BlockSpace
    {
        private List<IBlock> blockList;
        private int currentIndex;

        public BlockSpace()
        {
            blockList = new List<IBlock>();
            currentIndex = 0;
        }


        public List<IBlock> BlockList()
        {

            return this.blockList;

        }


        public void Add(IBlock iblock)
        {
            this.blockList.Add(iblock);
        }

        public void Clear()
        {
            this.blockList = new List<IBlock>();
        }

        public void Update()
        {

            for (int i = 0; i < blockList.Count; i++)
            {
                IBlock block = blockList[i];

                if (block.NeedRemove())
                {
                    blockList.RemoveAt(i);
                }

            }
        }



        public void Draw(SpriteBatch _spriteBatch)
        {
            foreach (IBlock block in this.blockList)
            {
                block.BlockDraw(_spriteBatch);
            }

        }

        public void PreviousBlock()
        {
            if (currentIndex > 0)
            {
                currentIndex--;
            }
            else
            {
                currentIndex = blockList.Count - 1;
            }
        }

        public void NextBlock()
        {
            if (currentIndex < blockList.Count - 1)
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
