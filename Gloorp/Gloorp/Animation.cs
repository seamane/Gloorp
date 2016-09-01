using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gloorp
{
    class Animation
    {
        Texture2D animation;
        Rectangle sourceRect;//this decides what is shown
        Vector2 position;//where the sprites are located

        float elapsed;//used to check the time between frames. if elapsed is longer then frame time the frame will change.
        float frameTime;//time between each frame
        int numOfFrames;//how many frames in the sheet
        int currentFrame;
        int width;//width of the frame
        int height;//
        int frameWidth;//used in 
        int frameHeight;
        bool looping;

        public Animation(ContentManager content, string asset, float frameSpeed, int numOfFrames, bool looping)
        {
            this.frameTime = frameSpeed;
            this.numOfFrames = numOfFrames;
            this.looping = looping;
            this.animation = content.Load<Texture2D>(asset);//asset is the name of the asset
            frameWidth = (animation.Width / numOfFrames);
            frameHeight = (animation.Height);
            //position = new Vector2(100, 100);//this is where the animation will play
        }

        public Vector2 Position
        {
            get { return this.position; }
            set { this.position = value; }
        }
        public int FrameHeight
        {
            get { return this.frameHeight; }
        }
        public int FrameWidth
        {
            get { return this.frameWidth; }
        }
        public int CurrentFrame
        {
            set { currentFrame = value; }
        }

        public void PlayAnim(GameTime gameTime)
        {
            elapsed += (float)gameTime.ElapsedGameTime.TotalMilliseconds;
            sourceRect = new Rectangle(currentFrame * frameWidth, 0, frameWidth, frameHeight);
            

            if (elapsed >= frameTime)
            {
                if (currentFrame >= numOfFrames - 1)
                {
                    if (looping)
                    {
                        currentFrame = 0;
                    }
                }
                else
                {
                    currentFrame++;
                }

                elapsed = 0;

            }
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(animation, position, sourceRect, Color.White, 0f, new Vector2(0, 0), 1f, SpriteEffects.None, 1f);
        }
    }
}
