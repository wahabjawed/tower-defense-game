using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EleTD
{
    class StraightAoeFire:StraightAoeDamage
    {

          public StraightAoeFire(int Damage)
            :base(Damage){}

        public override void EffectOfAoe(float Aoe, Enemies EnemOfInitialHit, List<Enemies> enemy, Bullet bullet)
        {
            EffectsManager.AddFireSparksEffects(EnemOfInitialHit.CenterofSprite, EnemOfInitialHit.Direction);
            base.EffectOfAoe(Aoe, EnemOfInitialHit, enemy, bullet);
        }
    }
}
