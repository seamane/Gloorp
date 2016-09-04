using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;

namespace Gloorp
{
    abstract class Enemy
    {
        public Sprite sprite;
        protected Vector2 anchor;
        private float visOffSet = 50;
        protected int direction;//either -1 or 1; for now at least...
        protected int patrollingRadius = 100;
        protected float speed = 1;

      

        public Enemy(Vector2 anchor)
        {
            this.anchor = anchor;
            sprite = new Sprite();
            sprite.position = anchor;
            
            direction = 1;
        }

        public void UpdateMovement()
        {
            if (Math.Abs(anchor.X - sprite.position.X) >= patrollingRadius)
            {
                direction *= -1;
            }
            sprite.position.X += direction * speed;
           
        }

        public void PlayerMoved(float amount)
        {
            anchor.X += amount;
            sprite.position.X += amount;
        }

        public virtual bool CanSeePlayer(Player player)
        {
            return false;
        }

        public void Reset(float offset)
        {
            anchor.X += offset;
            sprite.position = anchor;
            direction = 1;
        }

        public virtual void Draw(SpriteBatch batch)
        {
            

            batch.Draw(sprite.texture, sprite.position, Color.White);
            
        }
    }
}
