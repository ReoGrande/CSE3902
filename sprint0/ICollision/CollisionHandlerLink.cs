﻿using Microsoft.Xna.Framework;
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
        Link lLink;
        LinkDamagedDecorator dLink;
        int dmgTimer;

        public CollisionHandlerLink(Game1 game)
        {
            this.game = game;
            this.lLink = (Link)game.character;
            this.dLink = new LinkDamagedDecorator(lLink);
            this.dmgTimer = 0;
        }

        public bool TakeDamage()
        {
            bool damaged = true;
            if (dmgTimer == 0)
            {
                game.character = dLink;
                dmgTimer++;
            }
            else if (dmgTimer < 50)
            {
                dmgTimer++;
            } else if (dmgTimer < 65) {
                lLink.color = Color.White;
                dmgTimer++;
            }
            else if (dmgTimer >= 65)
            {
                damaged = false;
                lLink.color = Color.White;
                game.character = (ILinkState)lLink;
                dmgTimer = 0;

            }

            return damaged;
        }
    }
}
