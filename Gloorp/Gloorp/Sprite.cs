﻿using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Diagnostics;

namespace MyFirstMonoGame
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
        
        public void PlayerMoved(int amount)
        {
            position.X += amount;
        }
        public Rectangle getRect()
        { return new Rectangle((int)position.X, (int)position.X, texture.Width, texture.Height); }

        public void Reset(float offset)
        {
            position.X += offset;
        }
    }
}
