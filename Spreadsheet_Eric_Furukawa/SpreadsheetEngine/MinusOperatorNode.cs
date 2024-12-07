// <copyright file="MinusOperatorNode.cs" company="Eric-Furukawa">
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
    /// Represents an Operator node with subtraction evaluation.
    /// </summary>
    public class MinusOperatorNode : OperatorNode
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MinusOperatorNode"/> class.
        /// </summary>
        public MinusOperatorNode()
        {
        }

        /// <summary>
        /// Gets char operator representation of the minus operator.
        /// </summary>
        public static char Operator => '-';

        // public static ushort Precedence => 7;

        // public static Associative Associativity => Associative.Left;

        /// <summary>
        /// Returns evaluated double of subtracting right child from left.
        /// </summary>
        /// <returns>Difference of left and right children's evaluated value.</returns>
        public override double Evaluate()
        {
            return this.LeftChild.Evaluate() - this.RightChild.Evaluate();
        }
    }
}
