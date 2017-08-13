using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace Sage_Engine
{
    public class FrameAnimation
    {
       public Rectangle[] animations;
        int currentRect;
        float framesPerSecond;
        float secondsToDisplayFrame;
        float timer;

        private FrameAnimation(float framesPerSecond, float secondsToDisplayFrame, Rectangle[] animations)
        {
            this.framesPerSecond = framesPerSecond;
            this.animations = new Rectangle[animations.Length];
            this.secondsToDisplayFrame = secondsToDisplayFrame;
            animations.CopyTo(this.animations, 0);



        }

        public FrameAnimation(float framesPerSecond, int width, int height, int noOfFrames, int xOfSet, int yOfSet, Point rowCol)
        {


            this.framesPerSecond = MathHelper.Max(0.5f, framesPerSecond);
            secondsToDisplayFrame = 1 / framesPerSecond;

            animations = new Rectangle[noOfFrames];
            int x = 0;
            int y = 0;
            for (int i = 0; i < noOfFrames; i++)
            {
                animations[i] = new Rectangle(((x * width) + xOfSet), ((y * height) + yOfSet), width, height);
                x++;
                if (x >= rowCol.X)
                {
                    x = 0;
                    y++;
                }
            }

        }

        public Rectangle CurrentRect
        {
            get
            {
                return animations[currentRect];
            }

        }

        public void Update(GameTime gameTme)
        {
            timer += (float)gameTme.ElapsedGameTime.TotalSeconds;
            if (timer >= secondsToDisplayFrame)
            {
                currentRect++;
                if (currentRect > animations.Length - 1)
                {
                    currentRect = 0;
                }
                timer = 0;
            }
        }

        public FrameAnimation Clone()
        {
            return new FrameAnimation(framesPerSecond, secondsToDisplayFrame, animations);
        }



    }
}
