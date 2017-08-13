using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Sage_Engine;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;

namespace EleTD
{
    class SpinButton :DrawAble
    { 
        public Vector2 FixedScreenLocation;
        public float CenterOfSpriteX;
        public float LocationOfCenterOfText;
      
        public bool isAnimation1 = true;
        public bool switchAnim = false;
        public ShadeAnimation animation1;
        public ShadeAnimation animation2;


        public SpinButton(ShadeAnimation spriteAnimation,
           ShadeAnimation spriteAnimation2,
            Vector2 location)
            : base(spriteAnimation, location, 0, 0)
        {
            this.FixedScreenLocation = location;
            CenterOfSpriteX = this.CenterofSprite.X;
            this.animation1 = spriteAnimation;
            this.animation2 = spriteAnimation2;
        }

        public ShadeAnimation GetAnimation {

            get {
                if (isAnimation1)
                {
                    return animation1;
                }
                else {
                    return animation2;
                
                }
            }


        }

        public void SwitchAnimation() {
           
            if (isAnimation1)
            {
             
                this.spriteAnimation = animation2;
                isAnimation1 = false;
                GetAnimation.doExpand = true;
            }
            else {
                
                this.spriteAnimation = animation1;
                isAnimation1 = true;
                GetAnimation.doExpand = true;
                
            }
        }

        public Boolean CheckIfClicked(Rectangle state)
        {
            if (state.Intersects(GetCollisionRect))
            {
                Game1.sound.playSound("MenuButtonClick");
                switchAnim = true;
                return true;
            }
            return false;
        }
        public float timer = 0;
        public override void Update(GameTime gameTime)
        {
           if (switchAnim==true)
           {
               timer += 0.03f;
               if (timer > 0.5f)
               {
                   timer = 0;
                   switchAnim = false;
                   SwitchAnimation();
                    
                }
            }
            
            Location = Camera.Position + FixedScreenLocation;
            base.Update(gameTime);
        }



    }
    }

