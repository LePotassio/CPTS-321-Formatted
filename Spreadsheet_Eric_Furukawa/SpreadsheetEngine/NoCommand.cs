// <copyright file="NoCommand.cs" company="Eric-Furukawa">
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
    /// Represents an empty command.
    /// </summary>
    internal class NoCommand : Command
    {
        /// <summary>
        /// Executes nothing.
        /// </summary>
        public void Execute()
        {
        }

        /// <summary>
        /// Unexecutes nothing.
        /// </summary>
        public void Unexecute()
        {
        }
    }
}
