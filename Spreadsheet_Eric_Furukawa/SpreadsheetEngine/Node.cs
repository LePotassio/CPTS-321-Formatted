// <copyright file="Node.cs" company="Eric-Furukawa">
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
    /// Represents a generic node to be placed in an ExpressionTree.
    /// </summary>
    public abstract class Node
    {
        /// <summary>
        /// Returns evaluated double for tree evaluation.
        /// </summary>
        /// <returns>Evaluated double of the node.</returns>
        public abstract double Evaluate();
    }
}
