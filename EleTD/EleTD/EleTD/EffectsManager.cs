using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Sage_Engine;

namespace EleTD
{
    public static class EffectsManager
    {
        //private static List<Particle> Effects = new List<Particle>();
        public static LinkedList<Particle> EffectAdditive = new LinkedList<Particle>();
        public static LinkedList<Particle> EffectAlpha = new LinkedList<Particle>();
        public static List<SpriteAnimation> AnimatingEffects = new List<SpriteAnimation>();

        public static List<TextEffect> TextEffects = new List<TextEffect>();

        public static SpriteFont font;
        static Random rand = new Random();
        static Texture2D[] textureList = new Texture2D[26];


        static GraphicsDevice graphics;

        public static void Initialisze(ContentManager content, GraphicsDevice device)
        {
            textureList[0] = content.Load<Texture2D>(@"Particles/bigDot");
            textureList[1] = content.Load<Texture2D>(@"Particles/fire");
            textureList[2] = content.Load<Texture2D>(@"Particles/glowParticle");
            textureList[3] = content.Load<Texture2D>(@"Particles/particle");
            textureList[4] = content.Load<Texture2D>(@"Particles/smoke");
            textureList[5] = content.Load<Texture2D>(@"Particles/sprite");
            textureList[6] = content.Load<Texture2D>(@"Particles/SpriteSheet1");
            textureList[7] = content.Load<Texture2D>(@"Particles/Beam");
            textureList[8] = content.Load<Texture2D>(@"Particles/BeamBlurred");
            textureList[9] = content.Load<Texture2D>(@"Particles/Cloud001");
            textureList[10] = content.Load<Texture2D>(@"Particles/Cloud002");
            textureList[11] = content.Load<Texture2D>(@"Particles/Cloud003");
            textureList[12] = content.Load<Texture2D>(@"Particles/Cloud004");
            textureList[13] = content.Load<Texture2D>(@"Particles/Flame");
            textureList[14] = content.Load<Texture2D>(@"Particles/Laser");
            textureList[15] = content.Load<Texture2D>(@"Particles/Splash");
            textureList[16] = content.Load<Texture2D>(@"Particles/LensFlare");
            textureList[17] = content.Load<Texture2D>(@"Particles/MultiDot");
            textureList[18] = content.Load<Texture2D>(@"Particles/Laser");
            textureList[19] = content.Load<Texture2D>(@"Particles/Particle001");
            textureList[20] = content.Load<Texture2D>(@"Particles/Particle002");
            textureList[21] = content.Load<Texture2D>(@"Particles/Particle003");
            textureList[22] = content.Load<Texture2D>(@"Particles/Particle004");
            textureList[23] = content.Load<Texture2D>(@"Particles/Particle005");
            textureList[24] = content.Load<Texture2D>(@"Particles/Particle006");
            textureList[25] = content.Load<Texture2D>(@"Particles/Particle007");
            graphics = device;
        }

        public static void AddFrostSparksEffects(Vector2 location, Vector2 enemDirection)
        {
            int particleCount = rand.Next(25, 30);
            for (int i = 0; i < particleCount; i++)
            {
                Particle p = new Particle(textureList[0],
                    location,
                    RandomDirection(),
                    (float)rand.Next(20, 30) * 7,
                    40,
                    Color.LightBlue,
                    Color.Aqua);
                p.ColorMultiplier = 1;
                EffectAlpha.AddLast(p);
            }
        }



        public static void AddBaseTowerSparksEffect(Vector2 location, Vector2 enemDirection)
        {
            int particleCount = rand.Next(25, 30);
            for (int i = 0; i < particleCount; i++)
            {
                Particle p = new Particle(textureList[0],
                    location,
                    RandomDirection(),
                    (float)rand.Next(20, 30) * 7,
                    40,
                    new Color(50, 0, 0),
                    new Color(200, 0, 0));
                p.ColorMultiplier = 1;
                EffectAdditive.AddLast(p);
            }
        }

        public static void AddEarthSparksEffects(Vector2 location, Vector2 EnemDirection)
        {
            int particleCount = rand.Next(100, 120);
            for (int i = 0; i < particleCount; i++)
            {
                Particle p = new Particle(textureList[4],
                    location,
                    RandomDirection(),
                    (float)rand.Next(8, 12),
                   rand.Next(150, 160),
                    Color.SaddleBrown,
                    Color.SandyBrown);
                p.ColorMultiplier = 0.3f;
                EffectAlpha.AddLast(p);
            }
            particleCount = rand.Next(10, 15);
            for (int i = 0; i < particleCount; i++)
            {
                Particle p = new Particle(textureList[5],
                    location,
                    RandomDirection(),
                    (float)rand.Next(15, 20),
                    80,
                    Color.SaddleBrown,
                    Color.SandyBrown);
                p.ColorMultiplier = 0.5f;
                EffectAlpha.AddLast(p);
            }
        }

        public static void AddSparksEffect(Texture2D texture, Vector2 Location, Vector2 impactVelocity)
        {
            int particleCount = rand.Next(20, 30);
            for (int i = 0; i < particleCount; i++)
            {
                Particle p = new Particle(texture,
                    Location - (impactVelocity / 50),
                    RandomDirection(),
                    (float)rand.Next(20, 30),
                    300,
                    Color.Yellow,
                    Color.Orange);
                EffectAlpha.AddLast(p);
            }
        }
        public static void AddSparksEffectColor(Vector2 Location, Vector2 impactVelocity, Color StartColor, Color EndColor)
        {
            int particleCount = rand.Next(20, 30);


            for (int i = 0; i < particleCount; i++)
            {
                Particle p = new Particle(textureList[0],
                    Location - (impactVelocity / 50),
                    RandomDirection(),
                    (float)rand.Next(20, 30),
                    30,
                   StartColor,
                    EndColor);
                EffectAlpha.AddLast(p);
            }
        }


        public static void MenuSparkEffect(Vector2 Location, Color StartColor, Color EndColor)
        {
            int particleCount = rand.Next(15, 20);


            for (int i = 0; i < particleCount; i++)
            {
                Particle p = new Particle(textureList[5],
                    Location,
                    NormalizeIt(new Vector2(
                    rand.Next(0, 10),
                    -(rand.Next(20, 50)))),
                    (float)rand.Next(45, 50),
                   3000,
                   StartColor,
                    EndColor);
                Particle q = new Particle(textureList[5],
                    Location,
                    NormalizeIt(new Vector2(
                    -rand.Next(0, 10),
                    -(rand.Next(20, 50)))),
                    (float)rand.Next(45, 50),
                   3000,
                   StartColor,
                    EndColor);
                EffectAdditive.AddLast(p);
                EffectAdditive.AddLast(q);

            }
        }

        private static Vector2 NormalizeIt(Vector2 toBeNormalize)
        {

            toBeNormalize.Normalize();
            return toBeNormalize;
        }

        private static Vector2 RandomDirection()
        {
            Vector2 Direction;
            do
            {
                Direction = new Vector2(
                    rand.Next(0, 100) - 50,
                    rand.Next(0, 100) - 50);
            }
            while (Direction.Length() == 0);

            Direction.Normalize();
            return Direction;
        }
        public static void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin(SpriteSortMode.Texture, BlendState.AlphaBlend, null, null, null, null, Camera.TransFormMatrix);
            foreach (Particle p in EffectAlpha)
            {
                p.Draw(spriteBatch);
            }

            spriteBatch.End();

            spriteBatch.Begin(SpriteSortMode.Texture, BlendState.Additive, null, null, null, null, Camera.TransFormMatrix);
            foreach (Particle p in EffectAdditive)
            {
                p.Draw(spriteBatch);
            }

            spriteBatch.End();

            spriteBatch.Begin();
            foreach (TextEffect e in TextEffects)
            {
                e.Draw(spriteBatch);
            }
            spriteBatch.End();

            foreach (SpriteAnimation anim in AnimatingEffects)
            {
                anim.Draw(spriteBatch);
            }
        }

        public static void Update(GameTime gameTime)
        {

            LinkedListNode<Particle> TempParticle = EffectAlpha.First;
            bool Active;

            while (TempParticle != null)
            {

                TempParticle.Value.Update(gameTime);
                Active = TempParticle.Value.Active;
                TempParticle = TempParticle.Next;
                if (!Active)
                {
                    if (TempParticle == null)
                    {
                        EffectAlpha.RemoveLast();
                    }
                    else
                    {
                        EffectAlpha.Remove(TempParticle.Previous);
                    }
                }

            }

            TempParticle = EffectAdditive.First;


            while (TempParticle != null)
            {

                TempParticle.Value.Update(gameTime);
                Active = TempParticle.Value.Active;
                TempParticle = TempParticle.Next;
                if (!Active)
                {
                    if (TempParticle == null)
                    {
                        EffectAdditive.RemoveLast();
                    }
                    else
                    {
                        EffectAdditive.Remove(TempParticle.Previous);
                    }
                }

            }


            for (int i = TextEffects.Count - 1; i >= 0; i--)
            {
                TextEffects[i].Update();

                if (TextEffects[i].IsComplete)
                {
                    TextEffects.RemoveAt(i);
                }
            }

            foreach (SpriteAnimation anim in AnimatingEffects)
            {
                anim.Update(gameTime, anim.location);
            }

            for (int i = AnimatingEffects.Count - 1; i >= 0; i--)
            {
                if (AnimatingEffects[i].isAnimating == false)
                {
                    AnimatingEffects.RemoveAt(i);
                }
            }
        }


        public static void AddSpriteAnimationEffect(SpriteAnimation animation)
        {
            AnimatingEffects.Add(animation);
        }


        public static void ClearEffects()
        {
            EffectAlpha.Clear();
            EffectAdditive.Clear();
        }


        public static void WritingEffect(Vector2 Location, int gold) // +2 effect
        {
            TextEffect T = new TextEffect(Location, "+" + gold, Color.Gold,
                font, 100, 2.5f, 0.05f);
            T.FixedWorldPostion = true;
            TextEffects.Add(T);

        }

        public static void NotEnoughMateria()
        {
            TextEffects.Add(new TextEffect(new Vector2(graphics.Viewport.Width / 2, graphics.Viewport.Height / 2),
                "You Dont Have The Required Elements To Build ",
                Color.Violet, font, 100, 4.0f));
        }

        public static void NotEnoughElements()
        {
            TextEffects.Add(new TextEffect(new Vector2(graphics.Viewport.Width / 2, graphics.Viewport.Height / 2),
                "You Dont Have The Proper Element ",
                Color.Violet, font, 100, 4.0f));
        }

        public static void NoGoldEffect()
        {
            TextEffects.Add(new TextEffect(new Vector2(graphics.Viewport.Width / 2, graphics.Viewport.Height / 2),
                "Sorry You aint got no Gold !",
                Color.Yellow, font, 100, 4.0f));

        }

        public static void lifeLostEffect()
        {
            TextEffects.Add(new TextEffect(new Vector2(graphics.Viewport.Width / 2, graphics.Viewport.Height / 2),
                "You lost a life ",
                Color.Red, font, 100, 4.0f));

        }
        public static void gameOverEffect()
        {
            TextEffects.Add(new TextEffect(new Vector2(graphics.Viewport.Width / 2, graphics.Viewport.Height / 2),
                "GAME OVER ! ",
                Color.Snow, font, 5000, 6.0f));

        }

        public static void CantBuildEffect()
        {
            TextEffects.Add(new TextEffect(new Vector2(graphics.Viewport.Width / 2, graphics.Viewport.Height / 2),
                "You Cant Build A Tower There",
                Color.Red, font, 100, 4.0f));
        }


        public static void AddLightSparksEffects(Vector2 location, Vector2 EnemDirection)
        {
            int particleCount = rand.Next(45, 50);
            for (int i = 0; i < particleCount; i++)
            {
                Particle p = new Particle(textureList[0],
                    location,
                    RandomDirection() + EnemDirection,
                    (float)rand.Next(20, 30),
                    60,
                    Color.Yellow,
                    Color.Purple);
                p.ColorMultiplier = 1;
                EffectAlpha.AddLast(p);
            }
        }

        public static void AddDarkSparksEffects(Vector2 location, Vector2 EnemDirection)
        {
            int particleCount = rand.Next(45, 50);
            for (int i = 0; i < particleCount; i++)
            {
                Particle p = new Particle(textureList[1],
                     new Vector2(location.X - textureList[1].Width / 2, location.Y - textureList[1].Height / 2),
                    RandomDirection() + EnemDirection,
                    (float)rand.Next(20, 30),
                    150,
                    Color.Purple,
                    Color.Magenta);
                p.ColorMultiplier = 0.4f;
                EffectAlpha.AddLast(p);
                 p = new Particle(textureList[2],
                    new Vector2(location.X - textureList[2].Width / 2, location.Y - textureList[2].Height / 2),
                   RandomDirection() + EnemDirection,
                   (float)rand.Next(20, 30),
                   80,
                   Color.Purple,
                   Color.Magenta);
                p.ColorMultiplier = 0.6f;
                EffectAlpha.AddLast(p);

            }
        }

        public static void AddVortexSparksEffects(Vector2 location, Vector2 EnemDirection)
        {
            int particleCount = rand.Next(45, 50);
            for (int i = 0; i < particleCount; i++)
            {
                Particle p = new Particle(textureList[0],
                    location,
                    RandomDirection() + EnemDirection,
                    (float)rand.Next(20, 30),
                    60,
                    Color.Yellow,
                    Color.LightGoldenrodYellow);
                p.ColorMultiplier = 1;
                EffectAlpha.AddLast(p);
            }
        }

        public static void AddFireSparksEffects(Vector2 location, Vector2 EnemDirection)
        {
            int particleCount = rand.Next(60, 80);
            for (int i = 0; i < particleCount; i++)
            {
                Particle p = new Particle(textureList[5],
                   new Vector2(location.X - textureList[5].Width/2, location.Y - textureList[5].Height/2),
                   RandomDirection() + EnemDirection,
                   (float)rand.Next(10, 12),
                   300,
                   Color.SandyBrown,
                   Color.Red);
                EffectAdditive.AddLast(p);

                p = new Particle(textureList[1],
                   location,
                   RandomDirection() + EnemDirection,
                   (float)rand.Next(8, 12),
                   450,
                   Color.Orange,
                   Color.Red);
                p.ColorMultiplier = 1;
                EffectAdditive.AddLast(p);
            }

        }

        public static void AddGunSparksEffects(Vector2 location, Vector2 EnemDirection)
        {
             int particleCount = rand.Next(60, 80);
             for (int i = 0; i < particleCount; i++)
             {
                 Particle p = new Particle(textureList[5],
                    new Vector2(location.X - textureList[5].Width / 2, location.Y - textureList[5].Height / 2),
                    RandomDirection() + EnemDirection,
                    (float)rand.Next(10, 12),
                    300,
                    Color.SandyBrown,
                    Color.Red);
                 EffectAdditive.AddLast(p);

                 p = new Particle(textureList[1],
                    location,
                    RandomDirection() + EnemDirection,
                    (float)rand.Next(8, 12),
                    450,
                    Color.Orange,
                    Color.Red);
                 p.ColorMultiplier = 1;
                 EffectAdditive.AddLast(p);


             }
        }

        public static void AddBeelzeEffects(Vector2 location, Vector2 EnemDirection)
        {

            int particleCount = rand.Next(60, 80);
            for (int i = 0; i < particleCount; i++)
            {
                Particle p = new Particle(textureList[25],
                   new Vector2(location.X - textureList[25].Width / 2, location.Y - textureList[25].Height / 2),
                   RandomDirection() + EnemDirection,
                   (float)rand.Next(10, 12),
                   300,
                   Color.DarkRed,
                   Color.Red);
                p.SetScale(0.1f, 0.01f, 0.2f);
                //p.SetScale();
                EffectAlpha.AddLast(p);

            }
        }
    }
}