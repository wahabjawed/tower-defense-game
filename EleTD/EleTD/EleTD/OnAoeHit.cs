using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EleTD
{
   public interface OnAoeHit
    {
        void EffectOfAoe(float Aoe, Enemies EnemOfInitialHit, List<Enemies> enemy, Bullet bullet);
    }
}
