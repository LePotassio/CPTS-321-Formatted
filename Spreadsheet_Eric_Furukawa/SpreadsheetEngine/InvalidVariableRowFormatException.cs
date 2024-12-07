// <copyright file="InvalidVariableRowFormatException.cs" company="Eric-Furukawa">
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
    /// An exception describing a variable with an a valid row indicator (one that is not parsable into an int).
    /// </summary>
    public class InvalidVariableRowFormatException : FormatException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="InvalidVariableRowFormatException"/> class.
        /// </summary>
        public InvalidVariableRowFormatException()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="InvalidVariableRowFormatException"/> class.
        /// </summary>
        /// <param name="message">Exception message to display.</param>
        public InvalidVariableRowFormatException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="InvalidVariableRowFormatException"/> class.
        /// </summary>
        /// <param name="message">Exception message to display.</param>
        /// <param name="inner">Inner exception of the exception.</param>
        public InvalidVariableRowFormatException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
