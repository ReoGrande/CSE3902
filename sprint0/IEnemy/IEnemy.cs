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
        int MaxHP();
        void ChangeHP(int value);
        int Power();
        void ToDeath();
        bool NeedToBeRemoved();
        bool Attackable();
        bool Touchable();
        bool IsDeathCloud();
        bool CanBeAttactedByShieldBall();
        void SetCannotBeAttactedByShieldBall();
        void GetDamaged();
        void SetSpeed(int v);
        void SetMovePattern(int i);
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
        public int maxHp;
        public int power;
        public int damageTimer;
        protected bool needTObeRemoved;
        protected bool touchable;
        protected bool attackable;
        protected bool isDeathCloud;
        protected bool canBeAttactedByShieldBall;

        public abstract void SetSpeed(int v);
        public abstract void SetMovePattern(int i);

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
            hp += value;

        }

        public int HP()
        {
            return this.hp;

        }
        public int MaxHP()
        {
            return this.maxHp;

        }

        public int Power()
        {
            return this.power;
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

        public bool CanBeAttactedByShieldBall()
        {
            return canBeAttactedByShieldBall;
        }

        public void SetCannotBeAttactedByShieldBall()
        {

            canBeAttactedByShieldBall = false;
        }


        public bool IsDeathCloud()
        {
            return isDeathCloud;
        }

        public bool Touchable()
        {
            return touchable;
        }

        public bool Attackable()
        {
            return attackable;
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


