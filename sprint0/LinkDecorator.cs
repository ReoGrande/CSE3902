﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sprint0
{
    public class LinkDecorator : ILinkState
    {
        private ILinkState link;

        public LinkDecorator(ILinkState link)
        {
            this.link = link;
        }

        public void ToAttacking()
        {
            link.ToAttacking();
        }

        public void ToMovingUp()
        {
            link.ToMovingUp();
        }

        public void ToMovingDown()
        {
            link.ToMovingDown();
        }

        public void ToMovingLeft()
        {
            link.ToMovingLeft();
        }

        public void ToMovingRight()
        {
            link.ToMovingRight();
        }

        public void ToStanding()
        {
            link.ToStanding();
        }

        public void ToThrowing()
        {
            link.ToThrowing();
        }

        public virtual void Update()
        {
            link.Update();
        }

        public void Draw()
        {
            link.Draw();
        }

        public Rectangle GetPosition()
        {
            return link.GetPosition();
        }
    }

    public class LinkDamagedDecorator : LinkDecorator
    {
        Color[] damagedColors;
        private ILinkState link;
        private int i;              // Loop iterator

        public LinkDamagedDecorator(ILinkState link) : base(link)
        {
            damagedColors = new Color[4];
            damagedColors[0] = Color.Red;
            damagedColors[1] = Color.Blue;
            damagedColors[2] = Color.Green;
            damagedColors[3] = Color.Yellow;

            this.link = link;
        }

        public Color GetColor()
        {
            return damagedColors[0];
        }

        public override void Update()
        {
            
        }
    }

}
