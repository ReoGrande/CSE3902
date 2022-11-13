using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using static sprint0.Link;


namespace sprint0
{
    public interface IEnemy
    {
        void EnemyUpdate(Game1 game);
        void EnemyDraw(SpriteBatch _spriteBatch);

        Direction GetDirection();
        int GetX1();
        int GetX2();
        int GetY1();
        int GetY2();
        Rectangle GetPosition();

        int HP();
        void ChangeHP(int value);
        void ToDeath();
        bool NeedToBeRemoved();
        bool Touchable();
        bool IsDeathCloud();
        void GetDamaged();
        Rectangle ChangePosition(Rectangle newPosition);




    }

    public abstract class Enemy : IEnemy
    {

        public IEnemyState state;
        protected Rectangle positionRectangle;
        protected Texture2D EnemyTextureSheet;
        public Direction direction;
        protected Color color;
        public int timer;
        public int hp;
        public int damageTimer;
        protected bool needTObeRemoved;
        protected bool touchable;
        protected bool isDeathCloud;

        public abstract void EnemyUpdate(Game1 game);
        public Direction GetDirection() { return this.direction; }
        public abstract void EnemyDraw(SpriteBatch _spriteBatch);

        public int GetX1() { return positionRectangle.X; }
        public int GetX2() { return positionRectangle.X + positionRectangle.Width; }
        public int GetY1() { return positionRectangle.Y; }
        public int GetY2() { return positionRectangle.Y + positionRectangle.Height; }
        public Rectangle GetPosition() { return positionRectangle; }

        public Rectangle ChangePosition(Rectangle newPosition)
        {
            this.positionRectangle = newPosition;
            return this.positionRectangle;
        }


        public void ChangeHP(int value)
        {
            hp+=value;

        }

        public int HP()
        {
            return this.hp;

        }


        public void GetDamaged()
        {
            state.ToDamaged();
        }

        public void ToDeath()
        {

        
        }

        public bool NeedToBeRemoved()
        {
            return needTObeRemoved;
        }

        public bool IsDeathCloud()
        {
            return isDeathCloud;
        }

        public bool Touchable()
        {
            return touchable;
        } 


        
        public void ToNormal()
        {
            state.ToNormal();
        }

        public void TurnRed() { color = Color.Red; }
        public void TurnWhite() { color = Color.White; }
    }

    public interface IEnemyState
    {
        void ToDamaged();
        void ToNormal();


        void Update();
        // Draw() might also be included here
    }

    public class DamagedState : IEnemyState
    {
        private Enemy enemy;

        public DamagedState(Enemy enemy)
        {
            this.enemy = enemy;
        }

        public void ToDamaged()
        {
        }




        public void ToNormal()
        {
            enemy.state = new NomalState(enemy);
        }
        public void Update()
        {
            enemy.TurnRed();
            if (enemy.damageTimer > 8)
            {
                ToNormal();
                enemy.damageTimer = 0;
            }
            else
            {
                enemy.damageTimer++;
            }
        }
    }

    public class NomalState : IEnemyState
    {
        private Enemy enemy;

        public NomalState(Enemy enemy)
        {
            this.enemy = enemy;
        }

        public void ToDamaged()
        {
            enemy.state = new DamagedState(enemy);
        }
        public void ToNormal() { }



        public void Update()
        {
            enemy.TurnWhite();
        }
    }




}


