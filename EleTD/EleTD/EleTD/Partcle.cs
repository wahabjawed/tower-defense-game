using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Sage_Engine;

namespace EleTD
{
   public class Particle
    {

        private Texture2D tex;
        private int initialDuration;
        private int remainingDuration;
        private Color finalColor;
        private Color initialColor;
        private Color CurrentColor;
        public float ColorMultiplier = 1f;
        float ColorMultiplierVariation = 0f;
        private float speed;
        private Vector2 Direction;
        private Vector2 Location;
        Vector2 acceleration=Vector2.Zero;
        public bool Active;
      
       //rotation variable
       bool isRotating = false;
        float rotationFactor = 0f;
        float rotatingSpeed = 0f;

       //scale variables; 
        bool isScale = false;
        float initialScaleFactor = 1f;
        float scaleVaraition = 0f;
        float finalSacleFactor = 0f;
        
       //Impulse variable
        public bool isImpulse = false;
        Boolean canImpulse = true;
        
        public Vector2 Triggeroffset {

            set {

                Location += value; 
            }
           
        }


        public void SetOpacity(float initialOpacity, float variation) {
            ColorMultiplier = initialOpacity;
            ColorMultiplierVariation = variation;
        }


       public void SetRotation(float _rotationFactor, float _rotationSpeed) {
            isRotating = true;
            rotationFactor = _rotationFactor;
            rotatingSpeed = _rotationSpeed;

        }

        public void SetScale(float _initialScaleFactor, float _scaleVaraition,float _finalSacleFactor) {
            isScale = true;
            initialScaleFactor = _initialScaleFactor;
            scaleVaraition = _scaleVaraition;
            finalSacleFactor = _finalSacleFactor;
        } 

        public int ElaspedRegion
        {
            get
            {
                return initialDuration - remainingDuration;
            }
        }

        public float DurationProgress
        {
            get
            {
                return (float)ElaspedRegion / (float)initialDuration;
            }
        }

        public bool isActive
        {
            get
            {
                return (remainingDuration > 0);
            }
        }

        public Particle(Texture2D Texture,
            Vector2 location,
            Vector2 Direction,
            float speed,
            int duration,
            Color initialColor,
            Color finalColor
            )
            
        {
            this.initialDuration = duration;
            this.remainingDuration = duration;
            this.initialColor = initialColor;
            this.finalColor = finalColor;
            this.Direction = Direction;
            this.Location = location;
            Direction.Normalize();
            tex = Texture;
            this.speed = speed;
            Active = true;
        }

        public void Update(GameTime gameTime)
        {
            if (remainingDuration <= 0)
                Active = false;

            if (Active)
            {
                
                Location +=(Direction * (speed * (float)gameTime.ElapsedGameTime.TotalSeconds));
                
                CurrentColor = Color.Lerp(initialColor, finalColor, DurationProgress);
                remainingDuration--;
                if (isRotating) {
                    rotationFactor += rotatingSpeed;
                }
                if (isScale) {
                    if (initialScaleFactor<finalSacleFactor)
                    {
                        initialScaleFactor += scaleVaraition;
                    }
                    else
                    {
                       // initialScaleFactor += scaleVaraition;
                    }
                        
                }

                if (ColorMultiplierVariation != 0f && ColorMultiplier>0.0f) {
                    ColorMultiplier += ColorMultiplierVariation;
                }
                if (isImpulse) {
                    canImpulse = !canImpulse;
                }
            }

        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (canImpulse)
            {
                spriteBatch.Draw(tex, Location, null, CurrentColor * ColorMultiplier, rotationFactor, Vector2.Zero, initialScaleFactor, SpriteEffects.None, 0f);
            }
        }
       
    }
}
