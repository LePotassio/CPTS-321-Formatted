// <copyright file="PlusOperatorNode.cs" company="Eric-Furukawa">
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
    /// Represents an Operator node with addition evaluation.
    /// </summary>
    public class PlusOperatorNode : OperatorNode
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PlusOperatorNode"/> class.
        /// </summary>
        public PlusOperatorNode()
        {
        }

        /// <summary>
        /// Gets char operator representation of the plus operator.
        /// </summary>
        public static char Operator => '+';

        /// <summary>
        /// Gets order of priority in equation evaluation.
        /// </summary>
        public static ushort Precedence => 7;

        /// <summary>
        /// Gets grouping method of operator with same precedence.
        /// </summary>
        public static Associative Associativity => Associative.Left;

        /// <summary>
        /// Returns evaluated double of adding left and right children.
        /// </summary>
        /// <returns>Sum of left and right children's evaluated value.</returns>
        public override double Evaluate()
        {
            return this.LeftChild.Evaluate() + this.RightChild.Evaluate();
        }
    }
}
