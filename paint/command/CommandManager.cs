using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace paint
{
    internal class CommandManager
    {
        Stack<Command> commands = new Stack<Command>();
        Stack<Command> undocommands = new Stack<Command>();
        public void ExecuteCommand(Command cmd)
        {
            cmd.Execute();
            commands.Push(cmd);
            return;
        }
        public void undoCommand()
        {
            if (commands.Count > 0)
            {
                Command cmd = commands.Pop();
                cmd.undo();
                undocommands.Push(cmd);
            }
            return;
        }
        public void UndoUndoCommand()
        {
            if (undocommands.Count > 0)
            {
                Command cmd = undocommands.Pop();
                cmd.Execute();
                commands.Push(cmd);
            }
            return;
        }
    }
}
