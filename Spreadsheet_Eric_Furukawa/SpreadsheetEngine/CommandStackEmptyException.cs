// <copyright file="CommandStackEmptyException.cs" company="Eric-Furukawa">
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
    /// An exception describing a variable evaluated not assigned in the current expression tree.
    /// </summary>
    public class CommandStackEmptyException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CommandStackEmptyException"/> class.
        /// </summary>
        public CommandStackEmptyException()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CommandStackEmptyException"/> class.
        /// </summary>
        /// <param name="message">Exception message to display.</param>
        public CommandStackEmptyException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CommandStackEmptyException"/> class.
        /// </summary>
        /// <param name="message">Exception message to display.</param>
        /// <param name="inner">Inner exception of the exception.</param>
        public CommandStackEmptyException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
