

using System;
using System.Reflection.Metadata;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
namespace sprint0
{

    public class NPCSpace
    {
        private List<INPC> NPCList;
        private int currentIndex;

        public NPCSpace()
        {
            NPCList = new List<INPC>();
            currentIndex = 0;
        }

        public void Add(INPC iNPC)
        {
            this.NPCList.Add(iNPC);
        }
        public void Remove(INPC iNPC)
        {
            this.NPCList.Remove(iNPC);
        }
        public void Clear()
        {
            this.NPCList = new List<INPC>();
        }

        public void Update(Game1 game)
        {
            foreach (INPC NPC in this.NPCList)
            { NPC.NPCUpdate(game); }


        }

        public void Draw(SpriteBatch _spriteBatch)
        {
            /*foreach (INPC NPC in this.NPCList)
            { NPC.NPCDraw(_spriteBatch); }
            */
            for (int i = 0; i < this.NPCList.Count; i++)
            {
                NPCList[i].NPCDraw(_spriteBatch);

            }


        }




        public void ReplaceList(List<INPC> NPCList)
        {
            this.NPCList = NPCList;


        }




    }
}
