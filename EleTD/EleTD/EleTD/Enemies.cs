using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Sage_Engine;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace EleTD
{
   public class Enemies : Humonoid
    {
        private static List<Vector2> pathForEveryone;
        public List<Vector2> pathToFollow;
        public int gold = 2;
        public int totalHealth;
        public ElementalTypes Type;
        public static Texture2D HealthBar;

        static public float[,] damageTable = {{1,0.5f,1,1,1.5f,0.5f,1f},
                                           {1.5f,1,1.5f,0.5f,1,0.5f,1},
                                           {1,1.5f,1,0.5f,1.5f,0.5f ,1},
                                           {1,1.5f,1.5f,1,0.5f,0.5f,1},
                                           {1,1.5f,0.5f,1.5f,1f,0.5f,1},
                                           {1,0.5f,1.5f,0.5f,1.5f,1 ,1},
                                           {1,1f,1f,1f,1f,1 ,1}
                                           };
       

        public static void Initialise(List<Vector2> pathForeveryone, Texture2D Healthbar)
        {
            pathForEveryone = pathForeveryone;
            HealthBar = Healthbar;
        }

        public void GetHit(int Damage, ElementalTypes TypeAttacked)
        {
           
            health -= (int)(Damage * damageTable[(int)TypeAttacked, (int)Type]);

        }

        public Enemies(SpriteAnimation spriteAnimation,
           Vector2 location,
           float speed,
           int collisionRadius, int health)
            : base(spriteAnimation, location, speed, collisionRadius)
        {
            if(pathForEveryone != null)
            pathToFollow = new List<Vector2>(pathForEveryone);
            this.health = health;
            totalHealth = health;
        }

        private void CalculateDirection()
        {
            if (pathToFollow != null)
            {
                if (pathToFollow.Count > 1)
                {
                    Vector2 PathToFollow = pathToFollow[0];
                    PathToFollow *= TileLayer.GetTileWidth;
                    PathToFollow.X += TileLayer.GetTileWidth / 2;
                    PathToFollow.Y += TileLayer.GetTileHeight / 2;


                    float Distance = Vector2.Distance(CenterofSprite, PathToFollow);


                    Direction = PathToFollow - CenterofSprite;

                    if (Distance < 3)
                    {
                        pathToFollow.RemoveAt(0);
                    }
                }
                else
                {
                    Direction = Vector2.Zero;
                }
            }
        }

       public int HealthBarSubtraction = 0;
        public override void Update(GameTime gameTime)
        {

            HealthBarSubtraction = (health * 60) / totalHealth; 

            if (hit)
            {
                timeElaspedSinceHit += gameTime.ElapsedGameTime.Milliseconds;

                if (timeElaspedSinceHit >= TimeToDisplayHit)
                {
                    hit = false;
                    timeElaspedSinceHit = 0;
                    spriteAnimation.TintColor = Color.White;
                }
            }

            CalculateDirection();
           // CalculateDirectionFacing(Direction);
            //SwitchAnimationToFaceDirection();
            this.rotation = RotationforSprite();
            base.Update(gameTime);
        }


        public bool CheckIfClicked(Rectangle MouseRec)
        {
            if (GetCollisionRect.Intersects(MouseRec))
            {
                return true;
            }
            return false;
        }

        float previousAngle;
        private float RotationforSprite()
        {
            Vector2 Path = pathToFollow[0];
            Path *=  TileLayer.GetTileWidth;
            if (pathToFollow.Count  > 1)
            {
                float Angle = (float)Math.Atan2((Direction.Y),
                   (Direction.X));
                previousAngle = Angle;
                return Angle;
            }
            return previousAngle;
        }

        const float TimeToDisplayHit = 100;
        float timeElaspedSinceHit;
        public bool hit = false;
        public void GetHit()
        {
            hit = true;
            spriteAnimation.TintColor = Color.Red;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
            DrawHealthBar(spriteBatch);
        }

        private void DrawHealthBar(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, null , null, null, null, Camera.TransFormMatrix);

            spriteBatch.Draw(HealthBar, new Vector2(location.X, location.Y - 15), new Rectangle(0, -5, 60, 27), Color.Black);
            spriteBatch.Draw(HealthBar, new Vector2(location.X, location.Y - 15), new Rectangle(0, -5, HealthBarSubtraction, 15), Color.GreenYellow);
            
            spriteBatch.End();
        }


        public Enemies Copy(float Speed, int health, int Gold, ElementalTypes type)
        {
            Enemies enem =  new Enemies(new SpriteAnimation(spriteAnimation), location, Speed, 0, health);
            enem.Type = Type;
            enem.Type = type;
            enem.gold = Gold;
            return enem;
        }
    }
}
