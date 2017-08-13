using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Sage_Engine;

namespace EleTD
{
    public class TextEffect
    {
        public Vector2 Location;
        private SpriteFont font;
        public string Text;
        public Color DrawColor;
        public int DisplayCounter;
        private int maxDisplayCounter = 50;
        private float scale = 0.2f;
        private float lastScaleAmount;
        private float scaleAmount = 0.1f;
        private float MaxScaleAmount;
        public bool FixedWorldPostion = false;
        public Vector2 WorldLocation;
        
        public bool ExponentialIncrease;

        public float Scale
        {
            get
            {
                float amount =  scaleAmount * DisplayCounter;
                if (amount <= MaxScaleAmount)
                {
                    return amount;
                }
                return MaxScaleAmount;
            }
        }

        public bool IsComplete
        {
            get
            {
                return DisplayCounter > maxDisplayCounter;
            }
        }

        public TextEffect(Vector2 Location, string DisplayText, Color DrawColor, SpriteFont font, int MaxDisplayCounter,
            float MaxScaleAmount, float scaleAmount = 0.2f, bool ExponentialIncrease = true)
        {
            Text = DisplayText;
            this.DrawColor = DrawColor;
            this.Location = Location;
            this.font = font;
            this.maxDisplayCounter = MaxDisplayCounter;
            this.MaxScaleAmount = MaxScaleAmount;
            this.scaleAmount = scaleAmount;
            this.ExponentialIncrease = ExponentialIncrease;
            this.WorldLocation = Location;
        }

        public void Update()
        {
            if (!IsComplete)
            {
                if (FixedWorldPostion)
                {
                    Location = WorldLocation - Camera.Position;
                }

                if (scale < MaxScaleAmount)
                {
                    scale += lastScaleAmount + scaleAmount;
                    if (ExponentialIncrease)
                    {
                        lastScaleAmount += scaleAmount;
                    }
                }
                DisplayCounter++;
            }
        }

        public void Draw(SpriteBatch spritrBatch)
        {
            spritrBatch.DrawString(font, Text, Location, DrawColor, 0.0f, new Vector2(font.MeasureString(Text).X / 2, font.MeasureString(Text).Y / 2),
                Scale, SpriteEffects.None, 0.0f);
        }

    }
}
