// <copyright file="ValueNode.cs" company="Eric-Furukawa">
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
    /// Represents a node with a constant numerical value.
    /// </summary>
    public class ValueNode : Node
    {
        // Deprocated -> This should be done in tree evaluate to follow suit with variable node.
        /*
        /// <summary>
        /// Returns the double converteed value of the value node.
        /// </summary>
        /// <returns>Double value of the node.</returns>
        public double GetFloatValue()
        {
            return double.Parse(this.value);
        }
        */

        private readonly double value;

        /// <summary>
        /// Initializes a new instance of the <see cref="ValueNode"/> class.
        /// </summary>
        /// <param name="value">Constant value of the node to be set.</param>
        public ValueNode(double value)
        {
            this.value = value;
        }

        /// <summary>
        /// Returns value of value node.
        /// </summary>
        /// <returns>The value of the node.</returns>
        public override double Evaluate()
        {
            return this.value;
        }
    }
}
