using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gloorp
{
    class Platform
    {
        public Sprite sprite;

        public Platform(Vector2 position, Texture2D texture)
        {
            sprite = new Sprite();
            sprite.position = position;
            sprite.texture = texture;
        }

        public void PlayerMoved(float amount)
        {
            sprite.position.X += amount;
        }
        public void Draw(SpriteBatch batch)
        {
            batch.Draw(sprite.texture, sprite.position, Color.White);
        }
        public Rectangle getBoundingBox()
        {
            { return new Rectangle((int)sprite.position.X, (int)sprite.position.Y, sprite.texture.Width, sprite.texture.Height); }
        }

        public void Reset(float offset)
        {
            sprite.position.X += offset;
        }
    }
}
