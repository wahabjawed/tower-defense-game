using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Sage_Engine;

namespace EleTD
{
   public class Bullet : DrawAble
    {
        public int Damage;
        public int Aoe;
        public OnAoeHit AoeEffect;
        public Enemies enemyToFollow;
        public bool AutoRotate = false;
        public float acceleration = 15;
        public bool isGrow = false;
        private bool growIt = true;
        public ElementalTypes Type;
       

        public Bullet(SpriteAnimation spriteAnimation,
           Vector2 location,
           float speed,
           int Damage,
           int Aoe)
            :base(spriteAnimation, location, speed, 0)
        {
            this.Damage = Damage;
            this.Aoe = Aoe;
            this.AoeEffect = new NoAoeEffect();
        }

        public Bullet(SpriteAnimation spriteAnimation, int speed = 120)
            : base(spriteAnimation, Vector2.Zero, speed, 0)
        {
            this.AoeEffect = new NoAoeEffect();
        }


        public override void Update(GameTime gameTime)
        {
            if (isGrow) {
                if (growIt && this.ScaleFactor < 2.0f) {
                    this.ScaleFactor += 0.05f;
                    if(this.ScaleFactor>1.9f){
                        growIt = false;

                    }


                }
                else if (this.ScaleFactor > 0.0f)
                {
                    this.ScaleFactor -= 0.05f;
                    if (this.ScaleFactor < 0.1f)
                    {
                        growIt = true;

                    }
                
                }
            
            
            
            }


            speed += acceleration * (float)gameTime.ElapsedGameTime.TotalSeconds;
            base.Update(gameTime);
        }

        public void OnHit(List<Enemies> listOfEnemies, Enemies enemCollidedWith)
        {
            AoeEffect.EffectOfAoe(Aoe, enemCollidedWith, listOfEnemies, this);    
            
        }

        float RotaionIncrement;
        public override void Movement(GameTime gameTime)
        {
            if (enemyToFollow.Active == true)
            {
                Direction = enemyToFollow.CenterofSprite - CenterofSprite;
                if (!AutoRotate)
                {
                    rotation = (float)Math.Atan2((enemyToFollow.CenterofSprite.Y - CenterofSprite.Y),
                            (enemyToFollow.CenterofSprite.X - CenterofSprite.X));
                }
                else
                {
                    RotaionIncrement += 0.2f;
                    rotation = (float)RotaionIncrement;
                }
            }
            else
            {
                this.Active = false;
            }
            base.Movement(gameTime);
            
        }

        public Bullet Copy(Vector2 location, Enemies enemToFollow, int Damage, int Aoe, ElementalTypes Type)
        {
            Bullet b = new Bullet(new SpriteAnimation(spriteAnimation), location, speed, Damage, Aoe);
            b.AoeEffect = AoeEffect;
            b.Type = Type;
            b.enemyToFollow = enemyToFollow;
            b.AutoRotate = AutoRotate;
            b.isGrow = isGrow;
            return b;

        }
    }
}
