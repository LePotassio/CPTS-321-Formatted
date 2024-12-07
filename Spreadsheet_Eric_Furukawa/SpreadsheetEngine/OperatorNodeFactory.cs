// <copyright file="OperatorNodeFactory.cs" company="Eric-Furukawa">
// Copyright (c) Eric-Furukawa. All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Cpts321
{
    /// <summary>
    /// Factory class for the creation of Operator nodes with different operators.
    /// </summary>
    public class OperatorNodeFactory
    {
        private Dictionary<char, Type> operators = new Dictionary<char, Type>();

        /// <summary>
        /// Initializes a new instance of the <see cref="OperatorNodeFactory"/> class.
        /// </summary>
        public OperatorNodeFactory()
        {
            this.TraverseAvaliableOperators((op, type) => this.operators.Add(op, type));
        }

        private delegate void OnOperator(char op, Type type);

        // Deprocated for reflection based operators.
        /*
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
        */

        // Deprocated for reflection based operators.
        /*
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
        */

        // Deprocated for reflection based operators.
        /*
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
        */

        // Deprocated for reflection based operators.
        /*
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
        */

        /// <summary>
        /// Creates operator node of specified type.
        /// </summary>
        /// <param name="op">Type of operator to be created.</param>
        /// <returns>New operator instance of specified type.</returns>
        public OperatorNode CreateOperatorNode(char op)
        {
            if (this.operators.ContainsKey(op))
            {
                object operatorNodeObject = System.Activator.CreateInstance(this.operators[op]);
                if (operatorNodeObject is OperatorNode)
                {
                    return (OperatorNode)operatorNodeObject;
                }
            }

            throw new Exception("Unhandled Operator");
        }

        /// <summary>
        /// Returns if char is recognized operator.
        /// </summary>
        /// <param name="op">Char to be determined if operator or not.</param>
        /// <returns>Represents if char is an operator or not.</returns>
        public bool IsOperator(char op)
        {
            if (this.operators.ContainsKey(op))
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// Get the precedence property of the operator.
        /// </summary>
        /// <param name="op">Operator to get precedence of.</param>
        /// <returns>The precedence of the given operator.</returns>
        public ushort GetPrecedence(char op)
        {
            /*
            ushort precedenceValue = 0;
            if (this.operators.ContainsKey(op))
            {
                Type type = this.operators[op];
                PropertyInfo propertyInfo = type.GetProperty("Precedence");
                if (propertyInfo != null)
                {
                    object propertyValue = propertyInfo.GetValue(type);
                    if (propertyValue is ushort)
                    {
                        precedenceValue = (ushort)propertyValue;
                    }
                }
            }

            return precedenceValue;
            */

            return (ushort)this.GetOperatorProperty(op, "Precedence");
        }

        /// <summary>
        /// Get the associativity property of the operator.
        /// </summary>
        /// <param name="op">Operator to get associativity of.</param>
        /// <returns>The associativity of the given operator.</returns>
        public OperatorNode.Associative GetAssociativity(char op)
        {
            /*
            OperatorNode.Associative associativityValue = 0;
            if (this.operators.ContainsKey(op))
            {
                Type type = this.operators[op];
                PropertyInfo propertyInfo = type.GetProperty("Associativity");
                if (propertyInfo != null)
                {
                    object propertyValue = propertyInfo.GetValue(type);
                    if (propertyValue is OperatorNode.Associative)
                    {
                        associativityValue = (OperatorNode.Associative)propertyValue;
                    }
                }
            }

            return associativityValue;
            */

            return (OperatorNode.Associative)this.GetOperatorProperty(op, "Associativity");
        }

        /// <summary>
        /// Get the property of the operator.
        /// </summary>
        /// <param name="op">Operator to get property of.</param>
        /// <param name="property">Property to get.</param>
        /// <returns>The property's value of the given operator.</returns>
        public object GetOperatorProperty(char op, string property)
        {
            object propertyValue = 0;
            if (this.operators.ContainsKey(op))
            {
                Type type = this.operators[op];
                PropertyInfo propertyInfo = type.GetProperty(property);
                if (propertyInfo != null)
                {
                    propertyValue = propertyInfo.GetValue(type);
                    if (propertyValue is object)
                    {
                        return Convert.ChangeType(propertyValue, propertyValue.GetType());
                    }
                }
            }

            return Convert.ChangeType(propertyValue, propertyValue.GetType());
        }

        /// <summary>
        /// Traverse all operators, whom are the child classes of OperatorNode, using reflection.
        /// </summary>
        /// <param name="onOperator">Delegate for operator types.</param>
        private void TraverseAvaliableOperators(OnOperator onOperator)
        {
            Type operatorNodeType = typeof(OperatorNode);

            foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies())
            {
                IEnumerable<Type> operatorTypes = assembly.GetTypes().Where(type => type.IsSubclassOf(operatorNodeType));
                foreach (var type in operatorTypes)
                {
                    PropertyInfo operatorField = type.GetProperty("Operator");
                    if (operatorField != null)
                    {
                        object value = operatorField.GetValue(type);
                        if (value is char)
                        {
                            char operatorSymbol = (char)value;
                            onOperator(operatorSymbol, type);
                        }
                    }
                }
            }
        }
    }
}
