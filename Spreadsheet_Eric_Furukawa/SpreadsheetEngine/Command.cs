// <copyright file="Command.cs" company="Eric-Furukawa">
// Copyright (c) Eric-Furukawa. All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cpts321
{
    /// <summary>
    /// Represents basic funcitons to be implemented in every undo or redoable action.
    /// </summary>
    internal interface Command
    {
        /// <summary>
        /// Execute the command.
        /// </summary>
        void Execute();

        /// <summary>
        /// Unexecute the command.
        /// </summary>
        void Unexecute();
    }
}
