using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EleTD
{
   public class NoAoeEffect : OnAoeHit
    {
        public virtual void EffectOfAoe(float Aoe, Enemies EnemOfInitialHit, List<Enemies> enemy, Bullet bullet)
        {
            return;
        }
    }
}
