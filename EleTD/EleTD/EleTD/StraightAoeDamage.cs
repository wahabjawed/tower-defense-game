using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Sage_Engine;
using Microsoft.Xna.Framework;

namespace EleTD
{
   public class StraightAoeDamage : OnAoeHit 
    {
       public int Damage;
       public StraightAoeDamage(int Damage)
       {
           this.Damage = Damage;
       }

       public virtual void EffectOfAoe(float Aoe, Enemies EnemOfInitialHit, List<Enemies> enemy, Bullet bullet)
       {

           foreach (Enemies enem in enemy)
           {
               if (!(enem == EnemOfInitialHit))
               {

                   float Distance = Vector2.Distance(EnemOfInitialHit.CenterofSprite, enem.CenterofSprite);
                   if (Distance <= Aoe)
                   {
                       enem.GetHit(Damage, bullet.Type);
                   }
               }
           }
       }
    }
}
