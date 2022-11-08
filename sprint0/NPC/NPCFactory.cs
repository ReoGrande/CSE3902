using System.Diagnostics;
using System.Runtime.CompilerServices;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;


namespace sprint0
{

    public class NPCFactory
    {
        private Texture2D OldManSheet;


        private static NPCFactory instance = new NPCFactory();

        public static NPCFactory Instance
        {
            get
            {
                return instance;
            }
        }

        private NPCFactory()
        {
        }

        public void LoadAllTextures(Game1 game)
        {
            OldManSheet = game.Content.Load<Texture2D>("NPC/ZeldaSpriteOldMan");

        }


        public INPC CreateOldMan(Rectangle positionRectangle)
        {
            return new NPC(OldManSheet, positionRectangle);
        }





    }


    // More public ISprite returning methods follow
    // ...
}
