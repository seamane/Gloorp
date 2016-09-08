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
        public Sprite nearObject;
        private Vector2 anchor;
        float speed;
        public int directionNumber;
        public bool appearanceStatus=false;
        const float initialSpeed = 2.5f;
        float scale = 0f;


        public DirectionSprite(Vector2 anchor, int direction)
        {
            directionNumber = direction;
            this.anchor = anchor;
            sprite = new Sprite();
            sprite.position = anchor;
            speed = initialSpeed;
        }
        public float Scale
        {
            get{ return scale; }
            set{ scale = value; }
        }
        public Sprite getDirectionSprite()
        {
            return sprite;
        }

        public void UpdateMovement()
        {
            if (Math.Abs(anchor.Y - sprite.position.Y) >= 300)
            {
                sprite.position = anchor;
                appearanceStatus = false;
            }
            sprite.position.Y += 1 * speed;
        }
        public void Draw(SpriteBatch batch, Sprite nearObject)
        {
            batch.Draw(sprite.texture, new Vector2(nearObject.position.X + ((nearObject.texture.Width / 2) - 32), sprite.position.Y), Color.White);
        }
        public void Reset(bool resetSpeed)
        {
            sprite.position = anchor;
            appearanceStatus = false;
            if(resetSpeed||speed==0)
            {
                speed = initialSpeed;
            }
        }

        public void IncrementSpeed()
        {
            speed *= 1.05f;
        }

        internal void ResetToOriginalPosition(int v,DirectionTarget directionTarget,Sprite nearObject)
        {
            Vector2 pos = new Vector2(nearObject.position.X + ((nearObject.texture.Width / 2) - 32), nearObject.position.Y - 65);
            sprite.position = pos;
            appearanceStatus = true;          
            speed = v;
        }
    }
}
