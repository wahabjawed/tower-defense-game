using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EleTD
{
    class NoAoeBeelze: NoAoeEffect
    {

        public override void EffectOfAoe(float Aoe, Enemies EnemOfInitialHit, List<Enemies> enemy, Bullet bullet)
        {
            EffectsManager.AddBeelzeEffects(EnemOfInitialHit.CenterofSprite, EnemOfInitialHit.Direction);
            base.EffectOfAoe(Aoe, EnemOfInitialHit, enemy, bullet);
        }

    }
}
