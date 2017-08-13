using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Sage_Engine;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Sage_Engine
{
    public enum DirectionFacing
    {
        Up,
        Right,
        Down,
        Left,
    }

    public abstract class Humonoid : DrawAble
    {
        public int health;
        protected float mana;
        protected float manaRegen;
        protected Vector2 OldLocation;
        protected DirectionFacing directionFacing;
        protected SpellManager spellManager;
        
      
        public Humonoid(SpriteAnimation spriteAnimation,
           Vector2 location,
           float speed,
           int collisionRadius)
            : base(spriteAnimation, location, speed, collisionRadius)
        {
            
        }

        #region Properties

        public override void Update(GameTime gameTime)
        {
            if (health <= 0)
            {
                Active = false;
            }
            base.Update(gameTime);
        }

        public float Mana
        {
            get { return this.mana; }
            set
            {
                if (mana + value <= 100)
                {
                    mana += value;
                }
                else
                {
                    health = 100;
                }
            }
        }
        #endregion

        #region HelperCode
        
        protected int manaSeconds;
        protected void manaTicks(GameTime gameTime)
        {
            manaSeconds += gameTime.ElapsedGameTime.Milliseconds;
            if (manaSeconds >= 1000)
            {
                manaSeconds = 0;
                if (manaRegen + mana <= 100)
                {
                    mana += manaRegen;
                }
                else
                {
                    mana = 100;
                }
            }
        }

        protected void CalculateDirectionFacing(Vector2 DirectionToFace)
        {

            Vector2 location = CenterofSprite;

            if ((location != null) && (DirectionToFace != null))
            {
                float Angle = (float)Math.Atan2(( DirectionToFace.Y), (DirectionToFace.X));

                Angle = MathHelper.ToDegrees(Angle);
               
                if (Angle > -45 && Angle < 45)
                {
                    directionFacing = DirectionFacing.Left;
                    
                }
                else if (Angle > -135 && Angle < -45)
                {
                   directionFacing = DirectionFacing.Up;
                  
                }
                else if (Angle < 135 && Angle > 45)
                {
                    directionFacing = DirectionFacing.Down;

                }
                else
                {
                    directionFacing = DirectionFacing.Right;
                }
            }
        }


        public void SwitchAnimationToFaceDirection()
        {
            switch(directionFacing)
            {
                case DirectionFacing.Up:
                    SwitchAnimations("Up");
                    break;
                case DirectionFacing.Down:
                    SwitchAnimations("Down");
                    break;
                case DirectionFacing.Left:
                    SwitchAnimations("Left");
                    break;
                case DirectionFacing.Right:
                    SwitchAnimations("Right");
                    break;
            }
        }
        #endregion
    }
}
