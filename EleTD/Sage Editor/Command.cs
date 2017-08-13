using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Sage_Engine;
using Microsoft.Xna.Framework;

namespace Sage_Editor
{
   public abstract class Command
    {
      protected Form1 form;
    

       public Command(Form1 form)
       {
           this.form = form;
       }

       public abstract void Excute();

       public abstract void Undo();

       public abstract Command Clone();

       public abstract bool CompareTo(Command commandToCompare);
      
       
    }
}
