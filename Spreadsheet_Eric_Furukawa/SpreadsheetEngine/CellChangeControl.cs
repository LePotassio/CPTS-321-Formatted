// <copyright file="CellChangeControl.cs" company="Eric-Furukawa">
// Copyright (c) Eric-Furukawa. All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cpts321
{
    /// <summary>
    /// The INVOKER class.
    /// </summary>
    internal class CellChangeControl
    {
        // private Command[] onCommands;
        // private Command[] offCommands;
        private Stack<Command[]> undoCommands;
        private Stack<Command[]> redoCommands;

        // public event PropertyChangedEventHandler PropertyChanged = (sender, e) => { };

        // private Command undoCommand;

        /// <summary>
        /// Initializes a new instance of the <see cref="CellChangeControl"/> class.
        /// </summary>
        public CellChangeControl()
        {
            this.undoCommands = new Stack<Command[]>();
            this.redoCommands = new Stack<Command[]>();
        }

        /// <summary>
        /// Adds a list of commands to the top of the undo stack.
        /// </summary>
        /// <param name="newUndo">Command list to add to stack.</param>
        public void AddUndo(Command[] newUndo)
        {
            this.undoCommands.Push(newUndo);

            // this.PropertyChanged(this, new PropertyChangedEventArgs("Undo"));
        }

        /// <summary>
        /// Internal handling of when undo button is pushed.
        /// </summary>
        internal void UndoButtonPushed()
        {
            // In theory this exception should never occur due to UI button disabling, could also use a try on pop and catch popping empty stack possibly.
            if (this.UndoStackEmpty())
            {
                throw new CommandStackEmptyException("Undo Stack was empty");
            }

            UnexecuteCommandList(this.undoCommands.Peek());
            this.redoCommands.Push(this.undoCommands.Peek());
            this.undoCommands.Pop();

            // this.PropertyChanged(this, new PropertyChangedEventArgs("UndoRedo"));
        }

        /// <summary>
        /// Internal handling of when redo button is pushed.
        /// </summary>
        internal void RedoButtonPushed()
        {
            // In theory this exception should never occur due to UI button disabling, could also use a try on pop and catch popping empty stack possibly.
            if (this.RedoStackEmpty())
            {
                throw new CommandStackEmptyException("Redo Stack was empty");
            }

            ExecuteCommandList(this.redoCommands.Peek());
            this.undoCommands.Push(this.redoCommands.Peek());
            this.redoCommands.Pop();

            // this.PropertyChanged(this, new PropertyChangedEventArgs("UndoRedo"));
        }

        /// <summary>
        /// Returns if Undo stack is empty.
        /// </summary>
        /// <returns>Booolean value of if empty or not.</returns>
        internal bool UndoStackEmpty()
        {
            if (this.undoCommands.Count != 0)
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// Returns if Redo stack is empty.
        /// </summary>
        /// <returns>Booolean value of if empty or not.</returns>
        internal bool RedoStackEmpty()
        {
            if (this.redoCommands.Count != 0)
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// Clears redo stack of all command lists.
        /// </summary>
        internal void ClearRedoStack()
        {
            this.redoCommands.Clear();
        }

        /// <summary>
        /// Calls unexecute for each command in a command list.
        /// </summary>
        /// <param name="commandList">Command list to unexecute.</param>
        private static void UnexecuteCommandList(Command[] commandList)
        {
            foreach (Command command in commandList)
            {
                command.Unexecute();
            }
        }

        /// <summary>
        /// Calls execute for each command in a command list.
        /// </summary>
        /// <param name="commandList">Command list to execute.</param>
        private static void ExecuteCommandList(Command[] commandList)
        {
            foreach (Command command in commandList)
            {
                command.Execute();
            }
        }
    }
}
