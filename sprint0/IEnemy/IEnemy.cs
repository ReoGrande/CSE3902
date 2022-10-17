using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using static sprint0.Link;


namespace sprint0
{
    public interface IEnemy
    {
        void EnemyUpdate(Game1 game);
        void EnemyDraw(SpriteBatch _spriteBatch);


        int GetX1();
        int GetX2();
        int GetY1();
        int GetY2();

        void GetDamaged();

    }

    public abstract class Enemy : IEnemy
    {

        public IEnemyState state;
        protected Rectangle positionRectangle;
        protected Texture2D EnemyTextureSheet;
        public Direction direction;
        protected Color color;

        public abstract void EnemyUpdate(Game1 game);
        public abstract void EnemyDraw(SpriteBatch _spriteBatch);

        public int GetX1() { return positionRectangle.X; }
        public int GetX2() { return positionRectangle.X + positionRectangle.Width; }
        public int GetY1() { return positionRectangle.Y; }
        public int GetY2() { return positionRectangle.Y + positionRectangle.Height; }

        public void GetDamaged() { color = Color.Red; }
    }

    public interface IEnemyState
    {
        void ToMoving();
        void ToStatic();
        void Update(int x, int y);
        // Draw() might also be included here
    }

    public class DamagedState : IEnemyState
    {
        private Enemy enemy;

        public DamagedState(Enemy enemy)
        {
            this.enemy = enemy;
        }

        public void ToMoving()
        {
        }
        public void ToStatic() { }
        public void Update(int x, int y) { }
    }





    //non-moving,non-animated sprite
    public class StaticEnemy : Enemy
    {

        protected Rectangle rangeInSheet;


        public StaticEnemy(Texture2D textureSheet, Rectangle positionRectangle)
        {
            EnemyTextureSheet = textureSheet;
            this.positionRectangle = positionRectangle;
            this.rangeInSheet = new Rectangle(0, 0, textureSheet.Width, textureSheet.Height);
            color = Color.White;


        }


        public StaticEnemy(Texture2D textureSheet, Rectangle positionRectangle, Rectangle rangeInSheet)
        {
            EnemyTextureSheet = textureSheet;
            this.rangeInSheet = rangeInSheet;
            this.positionRectangle = positionRectangle;
            color = Color.White;

        }

        public override void EnemyUpdate(Game1 game)
        {
            //TODO: IMPLEMENT UPDATE METHODS? MAYBE
        }


        public override void EnemyDraw(SpriteBatch _spriteBatch)
        {

            _spriteBatch.Draw(
            EnemyTextureSheet,
            positionRectangle,
            rangeInSheet,
            color
            );


        }
    }

}
