using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EleTD
{
    public class NoAoeDark : NoAoeEffect
    {
        public override void EffectOfAoe(float Aoe, Enemies EnemOfInitialHit, List<Enemies> enemy, Bullet bullet)
        {
            EffectsManager.AddDarkSparksEffects(EnemOfInitialHit.CenterofSprite, EnemOfInitialHit.Direction);
            base.EffectOfAoe(Aoe, EnemOfInitialHit, enemy, bullet);
        }
    }
}
