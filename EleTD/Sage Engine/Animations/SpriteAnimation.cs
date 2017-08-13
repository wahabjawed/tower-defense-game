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

    public class SpriteAnimation
    {
        public Texture2D texture;
        protected string currentAnimation;
        public Vector2 location;
        protected bool animating = true;
        public Dictionary<string, FrameAnimation> animations = new Dictionary<string, FrameAnimation>();
        protected float rotation = 0;
        protected Effect shader;
        public Color TintColor = Color.White;
        public float scaleFactor = 1.0f;

        public Effect Shader
        {
            get
            {
                return shader;
            }

            set
            {
                shader = value;
            }
        }



        public float Rotation
        {
            get
            {
                return rotation;
            }
            set
            {
                rotation = value % MathHelper.TwoPi;
            }
        }

        public string CurrentAnimationName
        {
            set
            {
                if (animations.ContainsKey(value))
                {
                    currentAnimation = value;
                }
            }

            get
            {
                return currentAnimation;
            }
        }

        public bool isAnimating
        {
            get
            {
                return this.animating;
            }

            set
            {
                this.animating = value;
            }
        }

        public FrameAnimation CurrentAnimation
        {
            get
            {
                if (animations.Count == 0)
                {
                    this.AddAnimations("Default1", new FrameAnimation(1, texture.Width, texture.Height, 1, 0, 0, new Point(1, 1)));
                    currentAnimation = "Default1";
                    return CurrentAnimation;
                }
                else
                {
                    if (CurrentAnimationName == null)
                    {
                        string[] keys = new string[animations.Count];
                        animations.Keys.CopyTo(keys, 0);
                        CurrentAnimationName = keys[0];
                    } 
                }
                return animations[currentAnimation];
            } 

        }




   public FrameAnimation GetDefaultAnimation() {

            string[] keys = new string[animations.Count];
            animations.Keys.CopyTo(keys, 0);
            return animations[currentAnimation];     
   }

        public SpriteAnimation(Texture2D texture)
        {
            this.texture = texture;
            location = Vector2.Zero;

        }

        public virtual void Update(GameTime gameTime, Vector2 location)
        {
            if (!isAnimating)
                return;

            this.location = location;
            FrameAnimation anim = CurrentAnimation;
            if (anim == null)
            {
                return;
            }
            anim.Update(gameTime);

        }

        public void Draw(SpriteBatch spriteBatch)
        {
            FrameAnimation anim = CurrentAnimation;
            if (anim == null)
            {

                return;
            }





            //Might not be wise to put Begin here for everysprite but for now lets leave it here and test.
            spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, null, null, null, null, Camera.TransFormMatrix);

            if (Shader != null)
            {
                shade();
            }
            //spriteBatch.Draw(texture, location, anim.CurrentRect, Color.White);
            spriteBatch.Draw(
               texture,
               location,
               anim.CurrentRect,
               TintColor,
               Rotation,
               new Vector2(anim.CurrentRect.Width / 2, anim.CurrentRect.Height / 2),
               scaleFactor,
               SpriteEffects.None,
               0.0f);

            //spriteBatch.Draw(texture, 
            //    new Rectangle(
            //       (int)location.X, (int)location.Y, CurrentAnimation.CurrentRect.Width, CurrentAnimation.CurrentRect.Height),
            //        anim.CurrentRect, Color.White, Rotation,
            //    new Vector2(CurrentAnimation.CurrentRect.Width / 2, CurrentAnimation.CurrentRect.Height / 2), SpriteEffects.None, 0);

            spriteBatch.End();
            ///Some Test this Code and tell me.
        }

        public virtual void shade()
        {
            Shader.CurrentTechnique.Passes[0].Apply();
        }

        /// <summary>
        /// Copy Constructer
        /// </summary>
        /// <param name="anim"></param>
        public SpriteAnimation(SpriteAnimation anim)
        {
            texture = anim.texture;
            currentAnimation = anim.currentAnimation;
            location = anim.location;
            animating = true;

            foreach (KeyValuePair<string, FrameAnimation> s in anim.animations)
                animations.Add(s.Key, s.Value.Clone());

        }


        /// <summary>
        /// Use this to Add more frame Animation's for this sprite.
        /// </summary>
        /// <param name="Key"></param>
        /// <param name="Animation"></param>
        public void AddAnimations(string Key, FrameAnimation Animation)
        {
            if (Animation != null)
                animations.Add(Key, Animation);
        }



    }
}
