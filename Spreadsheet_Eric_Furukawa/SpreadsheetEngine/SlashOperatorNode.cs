// <copyright file="SlashOperatorNode.cs" company="Eric-Furukawa">
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
    /// Represents an Operator node with division evaluation.
    /// </summary>
    public class SlashOperatorNode : OperatorNode
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SlashOperatorNode"/> class.
        /// </summary>
        public SlashOperatorNode()
        {
        }

        /// <summary>
        /// Gets char operator representation of the slash operator.
        /// </summary>
        public static char Operator => '/';

        // public static ushort Precedence => ;

        // public static Associative Associativity => Associative.;

        /// <summary>
        /// Returns evaluated double of dividing right child with left.
        /// </summary>
        /// <returns>Quotient of left and right children's evaluated value.</returns>
        public override double Evaluate()
        {
            return this.LeftChild.Evaluate() / this.RightChild.Evaluate();
        }
    }
}
