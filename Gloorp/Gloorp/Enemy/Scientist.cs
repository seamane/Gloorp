using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using System;

namespace Gloorp
{

    public enum BossState
    {
        Static,
        Start,
        Scan,
        End
    }
    class Scientist
    {

        public Sprite sprite;
        public Texture2D vision;
        private Vector2 visionOffset = new Vector2(70, 139);
        private Vector2 eye;
        private Vector2 lookAt = new Vector2(0, 1);
        private float viewRadius = (float)Math.PI / 20.0f;
        public bool playerAtCheckpoint = false;
        public bool playerWins = false;
        public int playerSequenceCount = 0;
        private float speed = 1;
        public bool appeared=true;

        public BossState currState = BossState.Static;
        
        public Scientist(Vector2 position)
        {
            sprite = new Gloorp.Sprite();
            sprite.position = position;
        }

        public void UpdateMovement()
        {
            //if(playerAtCheckpoint)
            {
                if(currState == BossState.Start)
                {
                    
                    if(sprite.position.Y > 0)
                    {
                        if (sprite.position.Y < 120)
                            appeared = true;
                        else
                            appeared = false;
                        sprite.position.Y -= speed;
                    }
                    else
                    {
                        currState = BossState.Scan;
                    }
                }
                else if(currState == BossState.Scan)
                {
                    if(playerSequenceCount >= 10)
                    {
                        currState = BossState.End;
                    }
                }
                else if(currState == BossState.End)
                {
                    if(sprite.position.Y < 550)
                    {
                       
                        sprite.position.Y += speed;
                    }
                }
            }
        }

        public void PlayerMoved(float amount)
        {
            sprite.position.X += amount;
        }

        public virtual bool CanSeePlayer(Player player)
        {
            // player - enemy position
            Vector2 towardPlayer = player.sprite.position - eye;
            towardPlayer.Normalize();

            float angleBetweenEnemyAndPlayer = (float)Math.Acos(Vector2.Dot(towardPlayer, lookAt));

            return Math.Abs(angleBetweenEnemyAndPlayer) <= viewRadius;
        }

        public void Reset(float offset)
        {
            sprite.position.X += offset;
            currState = BossState.Static;
            playerAtCheckpoint = false;
            playerSequenceCount = 0;
        }

        public virtual void Draw(SpriteBatch batch)
        {
            batch.Draw(sprite.texture, sprite.position, null, Color.White, 0.0f, new Vector2(0, 0), 0.38f, SpriteEffects.None, 0.0f);
            if(currState == BossState.Scan)
            {
                batch.Draw(vision, sprite.position + visionOffset, null, Color.White, 0.0f, new Vector2(0, 0), 0.38f, SpriteEffects.None, 0.0f);
            }
        }
    }
}
