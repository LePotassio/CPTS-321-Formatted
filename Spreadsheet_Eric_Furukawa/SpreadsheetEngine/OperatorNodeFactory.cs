// <copyright file="OperatorNodeFactory.cs" company="Eric-Furukawa">
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
    /// Factory class for the creation of Operator nodes with different operators.
    /// </summary>
    public class OperatorNodeFactory
    {
        /// <summary>
        /// Creates and returns a new operator node with given operator type.
        /// </summary>
        /// <param name="op">Operator type node is to be created with.</param>
        /// <returns>New OperatorNode of the specified type.</returns>
        public static OperatorNode CreateOperatorNode(char op)
        {
            switch (op)
            {
                case '+':
                    return new PlusOperatorNode();
                case '-':
                    return new MinusOperatorNode();
                case '*':
                    return new StarOperatorNode();
                case '/':
                    return new SlashOperatorNode();
                default:
                    return null;
            }
        }
    }
}
