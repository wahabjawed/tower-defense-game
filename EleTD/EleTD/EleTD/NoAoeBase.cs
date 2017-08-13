using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Sage_Engine;
using Microsoft.Xna.Framework;

namespace EleTD
{
    class NoAoeBase : NoAoeEffect
    {

        

        public override void EffectOfAoe(float Aoe, Enemies EnemOfInitialHit, List<Enemies> enemy, Bullet bullet)
        {
            EffectsManager.AddBaseTowerSparksEffect(EnemOfInitialHit.CenterofSprite, EnemOfInitialHit.Direction);
            base.EffectOfAoe(Aoe, EnemOfInitialHit, enemy, bullet);
        }

    }
}
