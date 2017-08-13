using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Sage_Engine;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace EleTD
{
   public class TowerDataHolder
    {
       public SpriteAnimation spriteAnimation;
       public SpriteAnimation TurretAnim;
       public float TowerRange;
       public float DelayBetweenShots;
       public int Damage;
       public int Aoe;
       public ElementalTypes Type;

       public Texture2D ButtonImage;

        public TowerDataHolder(SpriteAnimation spriteAnimation,
            SpriteAnimation TurretAnim,
            ElementalTypes Type,
            Texture2D ButtonImage,
            float TowerRange,
            float DelayBetweenShots,
            int Damage,
            int Aoe)
        {
            this.spriteAnimation = spriteAnimation;
            this.TurretAnim = TurretAnim;
            this.TowerRange = TowerRange;
            this.DelayBetweenShots = DelayBetweenShots;
            this.Damage = Damage;
            this.Type = Type;
            this.ButtonImage = ButtonImage;
            this.Aoe = Aoe;
        }
    }
}
