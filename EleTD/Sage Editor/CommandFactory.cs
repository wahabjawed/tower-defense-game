using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sage_Editor
{
    public enum Commands
    {
        SetTileCommand,
        FillCellIndex,
        EraseCellCommand,
        FillCellErase,
    }

   public static class CommandFactory
    {
     
     static List<Command> AvailableCommands = new List<Command>();

       public static void Initilise(Form1 form)
       {
           AvailableCommands.Add(new SetTileCommand(form));
           AvailableCommands.Add(new FillCellUndo(form));
           AvailableCommands.Add(new EraseCellCommand(form));
           AvailableCommands.Add(new FillCellErase(form));
       }

       public static Command Execute(Commands commandType)
       {
           return AvailableCommands[(int)commandType].Clone();
       }

    }
}
