// <copyright file="CellOutOfRangeException.cs" company="Eric-Furukawa">
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
    /// An exception describing attempting to access a cell for a variable outside of the grid's range.
    /// </summary>
    public class CellOutOfRangeException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CellOutOfRangeException"/> class.
        /// </summary>
        public CellOutOfRangeException()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CellOutOfRangeException"/> class.
        /// </summary>
        /// <param name="message">Exception message to display.</param>
        public CellOutOfRangeException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CellOutOfRangeException"/> class.
        /// </summary>
        /// <param name="message">Exception message to display.</param>
        /// <param name="inner">Inner exception of the exception.</param>
        public CellOutOfRangeException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
