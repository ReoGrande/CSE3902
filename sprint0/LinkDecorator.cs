using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sprint0
{
    // WILL IMPLEMENT INTO THE REST OF THE GAME AFTER SPRINT2
    public class LinkDecorator : ILinkState
    {
        private Link link;
        private Game1 game;
        public enum Direction { Up, Down, Left, Right };

        public LinkDecorator(Link link)
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

        public Rectangle ChangePosition(Rectangle newPosition){
            return link.ChangePosition(newPosition);
        }

        public Link.Direction GetDirection()
        {
            return link.GetDirection();
        }
    }

    public class LinkDamagedDecorator : LinkDecorator
    {
        Color[] damagedColors;
        private Link link;
        private int i;              // Loop iterator

        public LinkDamagedDecorator(Link link) : base(link)
        {
            damagedColors = new Color[4];
            damagedColors[0] = Color.Red;
            damagedColors[1] = Color.Blue;
            damagedColors[2] = Color.Green;
            damagedColors[3] = Color.Yellow;

            i = 0;

            this.link = link;
        }

        public void ChangeFrames()
        {
            if (i > damagedColors.Length - 1)
            {
                i = 0;
            }
            link.color = damagedColors[i];
            i++;
        }

        public override void Update()
        {
            ChangeFrames();
            link.Update();
        }
        
    }

}
