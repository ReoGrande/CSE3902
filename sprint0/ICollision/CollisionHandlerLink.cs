using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sprint0
{
    internal class CollisionHandlerLink
    {
        Game1 game;
        Link link;
        LinkDamagedDecorator dLink;
        int dmgTimer;

        public CollisionHandlerLink(Game1 game)
        {
            this.game = game;
            this.link = (Link)game.character;
            this.dLink = new LinkDamagedDecorator(link);
            this.dmgTimer = 0;
        }

        public bool TakeDamage(bool damaged)
        {
            if (damaged)
            {
                if (dmgTimer == 0)
                {
                    game.character = dLink;
                    dmgTimer++;
                }
                else if (dmgTimer < 135)
                {
                    dmgTimer++;
                }
                else if (dmgTimer < 170)
                {
                    link.color = Color.White;
                    dmgTimer++;
                }
                else
                {
                    damaged = false;
                    link.color = Color.White;
                    game.character = (ILinkState)link;
                    dmgTimer = 0;
                }
            }

            return damaged;
        }
    }
}
