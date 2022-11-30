using Microsoft.Xna.Framework;
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
        private Link link;
        public enum Direction { Up, Down, Left, Right };

        public LinkDecorator(Link link)
        {
            this.link = link;
        }

        public void ToAttacking()
        {
            link.ToAttacking();
        }

        public void ToMoving()
        {
            link.ToMoving();
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

        public void Taunt()
        {
            link.Taunt();
        }

        public virtual void TakeDamage(int val)
        {
            link.TakeDamage(val);
        }
        public void ChangeHP(int value)
        {
            link.ChangeHP(value);

        }
        public int HP()
        {
            return link.HP();

        }
        public int MaxHP()
        {
            return link.maxHp;

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

        public bool IsAttacking()
        {
            return link.IsAttacking();
        }
    }

    public class LinkDamagedDecorator : LinkDecorator
    {
        Game1 game;
        Color[] damagedColors;
        private Link link;
        private int dmgTimer;
        private int i;              // Loop iterator

        public LinkDamagedDecorator(Link link) : base(link)
        {
            this.game = link.game;

            damagedColors = new Color[4];
            damagedColors[0] = Color.Red;
            damagedColors[1] = Color.Blue;
            damagedColors[2] = Color.Green;
            damagedColors[3] = Color.Yellow;

            i = 0;
            dmgTimer = 0;

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

        public override void TakeDamage(int val) { }

        public override void Update()
        {
            ChangeFrames();

            if (dmgTimer < 70)
            {
                dmgTimer++;
            }
            else if (dmgTimer < 100)
            {
                link.color = Color.White;
                dmgTimer++;
            }
            else
            {   
                link.color = Color.White;
                game.character = link;
            }

            link.Update();
        }
        
    }
}
