// <copyright file="CellSelfReferenceException.cs" company="Eric-Furukawa">
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
    /// An exception describing attempting to access a variable corresponding to the cell requestin the access.
    /// </summary>
    public class CellSelfReferenceException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CellSelfReferenceException"/> class.
        /// </summary>
        public CellSelfReferenceException()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CellSelfReferenceException"/> class.
        /// </summary>
        /// <param name="message">Exception message to display.</param>
        public CellSelfReferenceException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CellSelfReferenceException"/> class.
        /// </summary>
        /// <param name="message">Exception message to display.</param>
        /// <param name="inner">Inner exception of the exception.</param>
        public CellSelfReferenceException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
