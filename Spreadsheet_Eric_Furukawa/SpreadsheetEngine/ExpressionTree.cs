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

            // this.root = this.PopulateTree(expression);
            this.root = this.Build(expression);
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
            if (!this.variables.ContainsKey(variableName))
            {
                this.variables.Add(variableName, variableValue);
            }
            else
            {
                this.variables[variableName] = variableValue;
            }
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
        /// Converts expression into a post order traversal list of strings. Given by professor in class.
        /// </summary>
        /// <param name="expression">Expression to be converted into post order.</param>
        /// <returns>Post order converted list of strings representing expression.</returns>
        public List<string> ShuntingYardAlgorithm(string expression)
        {
            List<string> postfixExpression = new List<string>();
            Stack<char> operators = new Stack<char>();
            int operandStart = -1; // Represents starting of an operand when detected.

            // For every char c in expression.
            for (int i = 0; i < expression.Length; i++)
            {
                char c = expression[i];
                if (IsOperatorOrParenthesis(c))
                {
                    // Flagged case for adding operand.
                    if (operandStart != -1)
                    {
                        string operand = expression.Substring(operandStart, i - operandStart);
                        postfixExpression.Add(operand);
                        operandStart = -1;
                    }

                    // If it is a left parenthesis put it on the stack.
                    if (IsLeftParenthesis(c))
                    {
                        operators.Push(c);
                    }

                    // If it is a right parenthesis, cycle through operators and add until the left parenthesis is re-encountered.
                    else if (IsRightParenthesis(c))
                    {
                        char op = operators.Pop();
                        while (!IsLeftParenthesis(op))
                        {
                            postfixExpression.Add(op.ToString());
                            op = operators.Pop();
                        }
                    }

                    // If it is a Operator...
                    else if (OperatorNodeFactory.IsOperator(c))
                    {
                        // Empty operatorstack or left parenthesis on top of the operator stack.
                        if (operators.Count == 0 || IsLeftParenthesis(operators.Peek()))
                        {
                            operators.Push(c);
                        }

                        // Check if operator is either higher precedence or "tie-breaker" right associative. Push op onto stack if so.
                        else if (IsHigherPrecedence(c, operators.Peek()) || (IsSamePrecedence(c, operators.Peek()) && IsRightAssociative(c)))
                        {
                            operators.Push(c);
                        }

                        // Check if lower or same and left associative. Pop and add operators until empty or same/higher precedce OR not same precedence or not left associative. Then push operator.
                        else if (IsLowerPrecedence(c, operators.Peek()) || (IsSamePrecedence(c, operators.Peek()) && IsLeftAssociative(c)))
                        {
                            do
                            {
                                char op = operators.Pop();
                                postfixExpression.Add(op.ToString());
                            }
                            while (operators.Count > 0 && (IsLowerPrecedence(c, operators.Peek()) || (IsSamePrecedence(c, operators.Peek()) && IsLeftAssociative(c))));

                            operators.Push(c);
                        }
                    }
                }

                // Flagged case non-parenthesis or operator. Set operandStart to index of expression iteration. Marking an operand detected.
                else if (operandStart == -1)
                {
                    operandStart = i;
                }
            }

            // Last possible operand.
            if (operandStart != -1)
            {
                postfixExpression.Add(expression.Substring(operandStart, expression.Length - operandStart));
                operandStart = -1;
            }

            // Clean up remaining operators on stack.
            while (operators.Count > 0)
            {
                postfixExpression.Add(operators.Pop().ToString());
            }

            return postfixExpression;
        }

        /// <summary>
        /// Returns a list of the strings representing variables in the expression.
        /// </summary>
        /// <returns>List of strings for every variable in the expression.</returns>
        public List<string> GetVariableNames()
        {
            return new List<string>(this.variables.Keys);
        }

        // Helper functions for boolean checks (Comments for private methods not enforced for stylecop by default).

        /// <summary>
        /// Returns if char is left parenthesis.
        /// </summary>
        /// <param name="c">Char to be checked.</param>
        /// <returns>If char is left parenthesis or not.</returns>
        private static bool IsLeftParenthesis(char c)
        {
            if (c == '(')
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// Returns if char is right parenthesis.
        /// </summary>
        /// <param name="c">Char to be checked.</param>
        /// <returns>If char is right parenthesis or not.</returns>
        private static bool IsRightParenthesis(char c)
        {
            if (c == ')')
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// Returns if char is an operator or parenthesis.
        /// </summary>
        /// <param name="c">Char to be checked.</param>
        /// <returns>If char is an operator or parenthesis.</returns>
        private static bool IsOperatorOrParenthesis(char c)
        {
            if (IsLeftParenthesis(c) || IsRightParenthesis(c) || OperatorNodeFactory.IsOperator(c))
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// Compares the precedence of two char operators.
        /// </summary>
        /// <param name="left">First char operator to compare.</param>
        /// <param name="right">Second char operator to compare.</param>
        /// <returns>Comparison valueof the precedence, -1 if left is higher, 1 if righht is higher, 0 otherwise.</returns>
        private static int ComparePrecedence(char left, char right)
        {
            uint leftPrecedence = OperatorNodeFactory.GetPrecedence(left);
            uint rightPrecedence = OperatorNodeFactory.GetPrecedence(right);
            return (leftPrecedence > rightPrecedence) ? -1 : (leftPrecedence < rightPrecedence) ? 1 : 0;
        }

        /// <summary>
        /// Checks if an operator has higher precedence than other or not.
        /// </summary>
        /// <param name="left">Operator to compare if higher.</param>
        /// <param name="right">Operator to compare if equal or lower.</param>
        /// <returns>If the first operator has higher precedence.</returns>
        private static bool IsHigherPrecedence(char left, char right)
        {
            return ComparePrecedence(left, right) > 0;
        }

        /// <summary>
        /// Checks if two operators has same precedence or not.
        /// </summary>
        /// <param name="left">First operator to compare.</param>
        /// <param name="right">Second operator to compare.</param>
        /// <returns>If the operators have equal precedence or not.</returns>
        private static bool IsSamePrecedence(char left, char right)
        {
            return ComparePrecedence(left, right) == 0;
        }

        /// <summary>
        /// Checks if an operator has lower precedence than other or not.
        /// </summary>
        /// <param name="left">Operator to compare if lower.</param>
        /// <param name="right">Operator to compare if equal or higher.</param>
        /// <returns>If the first operator has lower precedence.</returns>
        private static bool IsLowerPrecedence(char left, char right)
        {
            return ComparePrecedence(left, right) < 0;
        }

        /// <summary>
        /// Checks if an operator has lower or same precedence than other or not.
        /// </summary>
        /// <param name="left">Operator to compare if lower or equal.</param>
        /// <param name="right">Operator to compare if higher.</param>
        /// <returns>If the first operator has lower or equal precedence.</returns>
        private static bool IsLowerOrSamePrecedence(char left, char right)
        {
            return ComparePrecedence(left, right) <= 0;
        }

        /// <summary>
        /// Checks if an operator is right associative.
        /// </summary>
        /// <param name="c">Operator to check associativity.</param>
        /// <returns>If the operator has right associativity.</returns>
        private static bool IsRightAssociative(char c)
        {
            return OperatorNodeFactory.GetAssociativity(c) == OperatorNode.Associative.Right;
        }

        /// <summary>
        /// Checks if an operator is left associative.
        /// </summary>
        /// <param name="c">Operator to check associativity.</param>
        /// <returns>If the operator has left associativity.</returns>
        private static bool IsLeftAssociative(char c)
        {
            return OperatorNodeFactory.GetAssociativity(c) == OperatorNode.Associative.Left;
        }

        // Helper functions for boolean checks END.

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

        /// <summary>
        /// Operator helper for populating the tree withrout the shunting yard.
        /// </summary>
        /// <param name="expression">Expression to be added.</param>
        /// <param name="op">Operator type to be tried.</param>
        /// <returns>OperatorNode created.</returns>
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

        /// <summary>
        /// Builds the tree using the ShuntingYardAlgorithm.
        /// </summary>
        /// <param name="expression">Expression the tree is to be built on.</param>
        /// <returns>Root node of the built tree.</returns>
        private Node Build(string expression)
        {
            Stack<Node> nodes = new Stack<Node>();
            var postfixExpression = this.ShuntingYardAlgorithm(expression);
            foreach (var item in postfixExpression)
            {
                if (item.Length == 1 && IsOperatorOrParenthesis(item[0]))
                {
                    OperatorNode node = OperatorNodeFactory.CreateOperatorNode(item[0]);
                    node.RightChild = nodes.Pop();
                    node.LeftChild = nodes.Pop();
                    nodes.Push(node);
                }
                else
                {
                    double num = 0.0;
                    if (double.TryParse(item, out num))
                    {
                        nodes.Push(new ValueNode(num));
                    }
                    else
                    {
                        nodes.Push(new VariableNode(item, ref this.variables));
                    }
                }
            }

            return nodes.Pop();
        }
    }
}
