using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Diagnostics;

namespace Gloorp
{
    class Sprite
    {
        public Texture2D texture;
        public Vector2 position;

        public Sprite()
        {

        }

        public void Draw(SpriteBatch batch)
        {
            batch.Draw(texture, position, Color.White);
        }
        
        public void PlayerMoved(float amount)
        {
            position.X += amount;
        }
        public Rectangle getRect()
        { return new Rectangle((int)position.X, (int)position.Y, texture.Width, texture.Height); }

        public String getName()
        { return texture.ToString(); }
        public void Reset(float offset)
        {
            position.X += offset;
        }
    }
}
