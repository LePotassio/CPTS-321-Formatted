// <copyright file="ExpressionTree.cs" company="Eric-Furukawa">
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
    /// Class representing a tree of constant, variable and binary operator nodes which evalueate to a double.
    /// </summary>
    public class ExpressionTree
    {
        private Dictionary<string, double> variables;
        private Node root;

        /// <summary>
        /// Initializes a new instance of the <see cref="ExpressionTree"/> class.
        /// </summary>
        /// <param name="expression">Expression the Expression Tree is to be constructed by.</param>
        public ExpressionTree(string expression)
        {
            this.variables = new Dictionary<string, double>();

            this.root = this.PopulateTree(expression);
        }

        /// <summary>
        /// Gets string, float dictionary member: variables.
        /// </summary>
        public Dictionary<string, double> Variables
        {
            get
            {
                return this.variables;
            }
        }

        /// <summary>
        /// Sets the specified variable within the ExpressionTree variables dictionary.
        /// </summary>
        /// <param name="variableName">Name of the variable to be set.</param>
        /// <param name="variableValue">Value of the variable to be set.</param>
        public void SetVariable(string variableName, double variableValue)
        {
            // Implementation Here
            this.variables.Add(variableName, variableValue);
        }

        /// <summary>
        /// Evaluates the expression tree to a double value.
        /// </summary>
        /// <returns>The double value in which the tree evaluates to.</returns>
        public double Evaluate()
        {
            return this.root.Evaluate();
        }

        /// <summary>
        /// Evaluates the expression to a double value.
        /// </summary>
        /// <param name="node">Node to be evaluated.</param>
        /// <returns>The double value in which the tree evaluates to.</returns>
        public double Evaluate(Node node)
        {
            // Implementation Here
            ValueNode valNode = node as ValueNode;
            if (valNode != null)
            {
                return valNode.Evaluate();
            }

            VariableNode varNode = node as VariableNode;
            if (varNode != null)
            {
                return this.variables[varNode.Name];
            }

            OperatorNode opNode = node as OperatorNode;
            if (opNode != null)
            {
                return opNode.Evaluate();
            }

            return 0.0;
        }

        /// <summary>
        /// Returns the string for the in order traversal of the tree starting from the root, no parentheses re-added.
        /// </summary>
        /// <returns>In order traversal string.</returns>
        public string GetInorderTree()
        {
            string result = string.Empty;
            if (this.root is OperatorNode)
            {
                result += this.GetInorderTreeHelper(((OperatorNode)this.root).LeftChild);
                result += this.root.Evaluate();
                result += this.GetInorderTreeHelper(((OperatorNode)this.root).RightChild);
            }

            return result;
        }

        /// <summary>
        /// Returns the string for the in order traversal of the tree, no parentheses re-added.
        /// </summary>
        /// <param name="parent">Parent node of current traversal iteration.</param>
        /// <returns>In order traversal string.</returns>
        public string GetInorderTreeHelper(Node parent)
        {
            string result = string.Empty;
            if (parent is OperatorNode)
            {
                result += this.GetInorderTreeHelper(((OperatorNode)parent).LeftChild);
                result += parent.Evaluate();
                result += this.GetInorderTreeHelper(((OperatorNode)parent).RightChild);
            }
            else
            {
                result += parent.Evaluate();
            }

            return result;
        }

        /// <summary>
        /// Popluates tree with nodes correctly corresponding to expression.
        /// </summary>
        /// <param name="expression">Expression the Expression Tree is to be constructed by.</param>
        private Node PopulateTree(string expression)
        {
            if (string.IsNullOrEmpty(expression))
            {
                return null;
            }

            char[] operators = { '+', '-', '*', '/' };
            foreach (char op in operators)
            {
                Node n = this.PopulateTree(expression, op);
                if (n != null)
                {
                    return n;
                }
            }

            double number;
            if (double.TryParse(expression, out number))
            {
                return new ValueNode(number);
            }
            else
            {
                return new VariableNode(expression, ref this.variables);
            }
        }

        private Node PopulateTree(string expression, char op)
        {
            for (int expressionIndex = expression.Length - 1; expressionIndex >= 0; expressionIndex--)
            {
                // if the counter is at 0 and we have the operator that we are looking for
                if (op == expression[expressionIndex])
                {
                    OperatorNode operatorNode = OperatorNodeFactory.CreateOperatorNode(expression[expressionIndex]);
                    operatorNode.LeftChild = this.PopulateTree(expression.Substring(0, expressionIndex));
                    operatorNode.RightChild = this.PopulateTree(expression.Substring(expressionIndex + 1));
                    return operatorNode;
                }
            }

            return null;
        }
    }
}
