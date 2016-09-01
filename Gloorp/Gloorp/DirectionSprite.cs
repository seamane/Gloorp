using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gloorp
{
    class DirectionSprite
    {
        public Sprite sprite;
        private Vector2 anchor;
        float speed;
        public int directionNumber;
        public bool appearanceStatus=false;
        const float initialSpeed = 3.5f;


        public DirectionSprite(Vector2 anchor, int direction)
        {
            directionNumber = direction;
            this.anchor = anchor;
            sprite = new Sprite();
            sprite.position = anchor;
            speed = initialSpeed;
        }

        public Sprite getDirectionSprite()
        {
            return sprite;
        }

        public void UpdateMovement()
        {
            if (Math.Abs(anchor.Y - sprite.position.Y) >= 250)
            {
                sprite.position = anchor;
                appearanceStatus = false;
            }
            sprite.position.Y += 1 * speed;
        }
        public void Draw(SpriteBatch batch, float nearObjectX)
        {
            batch.Draw(sprite.texture, new Vector2(nearObjectX,sprite.position.Y), Color.White);
        }
        public void Reset(bool resetSpeed)
        {
            sprite.position = anchor;
            appearanceStatus = false;
            if(resetSpeed)
            {
                speed = initialSpeed;
            }
        }

        public void IncrementSpeed()
        {
            speed *= 1.05f;
        }
    }
}
