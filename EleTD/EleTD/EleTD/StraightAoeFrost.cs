using System;
using System.Collections.Generic;
using System.Linq;
using Sage_Engine;
using Microsoft.Xna.Framework;

namespace EleTD
{
    class StraightAoeFrost : StraightAoeDamage
    {
        public StraightAoeFrost(int Damage)
            :base(Damage){}

        public override void EffectOfAoe(float Aoe, Enemies EnemOfInitialHit, List<Enemies> enemy, Bullet bullet)
        {
            EffectsManager.AddFrostSparksEffects(EnemOfInitialHit.CenterofSprite, EnemOfInitialHit.Direction);
            base.EffectOfAoe(Aoe, EnemOfInitialHit, enemy, bullet);
        }
    
    }
}
