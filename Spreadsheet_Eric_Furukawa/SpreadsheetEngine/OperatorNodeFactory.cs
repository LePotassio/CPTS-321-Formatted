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

        /// <summary>
        /// Gets the precedent of a specified operator. It is hardcoded for now as the factory implementation will change to user-defined operators using reflections.
        /// </summary>
        /// <param name="op">Operator type to get precedence of.</param>
        /// <returns>Precedent value of the operator.</returns>
        public static ushort GetPrecedence(char op)
        {
            switch (op)
            {
                case '+':
                    return 7;
                case '-':
                    return 7;
                case '*':
                    return 6;
                case '/':
                    return 6;
                default:
                    return 0;
            }
        }

        /// <summary>
        /// Gets the precedent of a specified operator. It is hardcoded for now as the factory implementation will change to user-defined operators using reflections.
        /// </summary>
        /// <param name="op">Operator type to get precedence of.</param>
        /// <returns>Precedent value of the operator.</returns>
        public static OperatorNode.Associative GetAssociativity(char op)
        {
            switch (op)
            {
                case '+':
                    return OperatorNode.Associative.Left;
                case '-':
                    return OperatorNode.Associative.Left;
                case '*':
                    return OperatorNode.Associative.Left;
                case '/':
                    return OperatorNode.Associative.Left;
                default:
                    return OperatorNode.Associative.Left;
            }
        }

        /// <summary>
        /// Returns if char is recognized operator. It is hardcoded for now as the factory implementation will change to user-defined operators using reflections.
        /// </summary>
        /// <param name="op">Char to be determined if operator or not.</param>
        /// <returns>Represents if char is an operator or not.</returns>
        public static bool IsOperator(char op)
        {
            switch (op)
            {
                case '+':
                    return true;
                case '-':
                    return true;
                case '*':
                    return true;
                case '/':
                    return true;
                default:
                    return false;
            }
        }
    }
}
