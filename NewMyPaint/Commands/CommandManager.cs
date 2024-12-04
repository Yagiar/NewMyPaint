using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewMyPaint
{
    internal class CommandManager
    {
        private Stack<Command> commandStack = new Stack<Command>();
        public void ExecuteCommand(Command cmd)
        {
            cmd.Execute();
            commandStack.Push(cmd);
        }
        public void UndoCommand()
        {
            if (commandStack.Count > 0)
            {
                commandStack.Pop().Undo();
            }
        }
    }
}
