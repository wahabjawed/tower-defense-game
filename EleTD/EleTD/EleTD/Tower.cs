using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Sage_Engine;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace EleTD
{
    public enum ElementalTypes
    {
        Dark,
        Light,
        Water,
        Wind,
        Fire,
        Earth,
        Composite
    }

   public class Tower : DrawAble
    {
       public string TowerName;

       public float TurretRotation;
       public float BaseRotation;
       public Enemies currentEnemyToShoot;
       public Texture2D circleRange;
       public Vector2 circleRangeLocation=Vector2.Zero;
       public Texture2D ButtonImage;
       public bool turretShowOnAttack = false;
       bool showTurret=false;
       public List<Edge> TowersThisCanBeUpgradedInto = new List<Edge>();

       public List<Bullet> bullets = new List<Bullet>();

       public SpriteAnimation TurretAnim;

       public SpriteAnimation BaseAnim;
       public static Bullet projectile;

       public Bullet SpecificBullet;

       public int SellPrice;
       public bool AutoRotate;
       public bool BaseRotate;

       public ElementalTypes Type;  // Put the Tower Type In.

       public bool Built = false;

        public float TowerRange;
        public float DelayBetweenShots;
        public int Damage;
        public int Aoe;
        public float initialFactor;
        public float finalFactor;
        public bool TurretScale;
        bool scaleup = true;

        public void SetScale(float _intitialFactor, float _finalFactor)
        {
            TurretScale = true;
            initialFactor = _intitialFactor;
            finalFactor = _finalFactor;
            TurretAnim.scaleFactor = _intitialFactor;
        }

        public void InsertEdge(Edge e)
        {
            TowersThisCanBeUpgradedInto.Add(e);
        }

        public Tower(SpriteAnimation baseAnim,
            SpriteAnimation TurretAnim,
            Texture2D ButtonImage,
            string Name,
            ElementalTypes Type,
           Vector2 location,
           float speed,
           int collisionRadius,
            float TowerRange,
            float DelayBetweenShots,
            int Damage,
            int Aoe,
            params Edge[] edges)
            :base(baseAnim, location,speed, collisionRadius)
        {
            this.TurretAnim = TurretAnim;
            this.TowerRange = TowerRange;
            this.DelayBetweenShots = DelayBetweenShots;
            this.Damage = Damage;
            this.TowerName = Name;
            this.ButtonImage = ButtonImage;
            this.Type = Type;
            this.Aoe = Aoe;
            AutoRotate = false;
            circleRangeLocation = new Vector2((location.X - (int)TowerRange), (location.Y - (int)TowerRange));
           
            if (SpecificBullet == null)
            {
                SpecificBullet = projectile;
            }

            foreach(Edge a in edges)
            {
                TowersThisCanBeUpgradedInto.Add(a);
            }
        }


        public virtual Tower Copy(Vector2 location, int SellPrice)
        {
           Edge[] ed = new Edge[TowersThisCanBeUpgradedInto.Count];
            TowersThisCanBeUpgradedInto.CopyTo(ed);

            Tower T =  new Tower(new SpriteAnimation(spriteAnimation), new SpriteAnimation(TurretAnim), ButtonImage, TowerName, Type,location,
                speed, collisionRadius, TowerRange, DelayBetweenShots, Damage, Aoe,ed);
            T.AutoRotate = AutoRotate;
            T.BaseRotate = BaseRotate;
            T.SellPrice = SellPrice;
            T.SpecificBullet = SpecificBullet;
            T.TurretScale = TurretScale;
            T.initialFactor = initialFactor;
            T.finalFactor = finalFactor;
            T.turretShowOnAttack = turretShowOnAttack;
           // T.circleRange = CreateCircle((int)T.TowerRange);
            return T;
        }

        public void CopyElementsInOtherEdges()
        {
            if (TowersThisCanBeUpgradedInto.Count > 0)
            {
                Edge initial = TowersThisCanBeUpgradedInto[0];

                foreach (Edge e in TowersThisCanBeUpgradedInto)
                {
                    if (e != initial  && e.Destination == this)
                    {
                        foreach (ElementalTypes type in initial.ElementsRequiredToCreate)
                        {
                            e.ElementsRequiredToCreate.Add(type);
                        }
                    }
                }
            }
        }

        public bool CheckIfClicked(Rectangle mouse)
        {
            if (mouse.Intersects(GetCollisionRect))
            {
                return true;
            }
            return false;
        }

        public void Update(GameTime gameTime, List<Enemies> enemies)
        {
            currentEnemyToShoot = null;
            foreach (Enemies enemy in enemies)
            {
                float distance = Vector2.Distance(enemy.CenterofSprite, CenterofSprite);
                if (distance <= TowerRange)
                {
                    currentEnemyToShoot = enemy;
                    break;
                }
            }
            TurretRotation = RotationforTurret();
            
            
            if (TurretAnim != null)
            {
               
                TurretAnim.Rotation = TurretRotation;
            }

            BaseRotation = RotationforBase();

            rotation = BaseRotation;

            if (TurretScale)
            {

                if (scaleup && TurretAnim.scaleFactor < finalFactor)
                {
                    TurretAnim.scaleFactor += 0.05f;

                    if (TurretAnim.scaleFactor > (finalFactor - 0.1f))
                    {
                        scaleup = false;
                    }

                }
                else
                {
                    TurretAnim.scaleFactor -= 0.05f;
                    if (TurretAnim.scaleFactor < (initialFactor + 0.1f))
                    {
                        scaleup = true;
                    }

                }

            }

            
            Shoot(gameTime);
            Update(gameTime);
        }

        public override void Update(GameTime gameTime)
        {
            
            base.Update(gameTime);
     
            if (TurretAnim != null)
            {
                TurretAnim.Update(gameTime, new Vector2(CenterofSprite.X, CenterofSprite.Y));
            }
           
        }

        float PreviousAngle;
        private float RotationforTurret()
        {

            if (currentEnemyToShoot != null)
            {
                if (!AutoRotate)
                {
                    float Angle = (float)Math.Atan2((currentEnemyToShoot.CenterofSprite.Y - CenterofSprite.Y),
                         (currentEnemyToShoot.CenterofSprite.X - CenterofSprite.X));
                    PreviousAngle = Angle;
                    return Angle;
                }
                else
                {
                     PreviousAngle += (float)0.1;
                }

                  if(turretShowOnAttack){
                    showTurret = true;
                }
            }else{
            
            if (turretShowOnAttack)
                {
                    showTurret = false;
                }
            }
            
            return PreviousAngle;
        }

        float PreviousAngleForBase;
        private float RotationforBase()
        {

            if (currentEnemyToShoot != null)
            {
                if (BaseRotate)
                {
                    PreviousAngleForBase -= (float)0.1;   
                }
            }

            return PreviousAngleForBase;
        }

        float TimeElaspedSinceShooting;
        public void Shoot(GameTime gameTime)
        {
            TimeElaspedSinceShooting += gameTime.ElapsedGameTime.Milliseconds;

            if (TimeElaspedSinceShooting >= DelayBetweenShots && currentEnemyToShoot != null)
            {
                TimeElaspedSinceShooting = 0;
                Bullet bulletToAdd = SpecificBullet.Copy(CenterofSprite , currentEnemyToShoot, Damage, Aoe, this.Type);
                bulletToAdd.enemyToFollow = currentEnemyToShoot;
                bullets.Add(bulletToAdd);
            }
        }

        

        public override void Draw(SpriteBatch spriteBatch)
        {

            foreach (Bullet bullet in bullets)
            {
                bullet.Draw(spriteBatch);
            }

            base.Draw(spriteBatch);


            if (TurretAnim != null)
            {

                if (turretShowOnAttack)
                {
                    if (showTurret)
                    {
                        TurretAnim.Draw(spriteBatch);
                    }

                }
                else
                {
                    TurretAnim.Draw(spriteBatch);

                }
            }
        }
    }
}
