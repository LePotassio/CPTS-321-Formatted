// <copyright file="ExpressionTreeDemo.cs" company="Eric-Furukawa">
// Copyright (c) Eric-Furukawa. All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Cpts321;

namespace ExpressionTreeStandaloneDemo
{
    /// <summary>
    /// Contains execution of the stand alone Expression Tree demo.
    /// </summary>
    public class ExpressionTreeDemo
    {
        private static ExpressionTree demoTree = new ExpressionTree("A1+B1+C1");
        private static string expression = "A1+B1+C1";

        /// <summary>
        /// Main execution of stand alone ExpressionTree demo.
        /// </summary>
        /// <param name="args">Arguments from runtime.</param>
        public static void Main(string[] args)
        {
            // ExpressionTree demoTree = new ExpressionTree("A1+B1+C1");
            do
            {
                DisplayMenu();
            }
            while (DoMenuOption(int.Parse(Console.ReadLine())) != 0);
        }

        /// <summary>
        /// Prints out the menu string to console.
        /// </summary>
        public static void DisplayMenu()
        {
            Console.WriteLine("Menu");
            Console.WriteLine("Current Expression: " + expression);

            // Implement display current expression here.
            Console.Write("1 = Enter a new expression\n2 = Set a variable value\n3 = Evaluate tree\n4 = Quit\n");
        }

        /// <summary>
        /// Calls function depending on user menu selection.
        /// </summary>
        /// <param name="input">Integer parsed user menu selection.</param>
        /// <returns>Integer success or failure code.</returns>
        public static int DoMenuOption(int input)
        {
            Console.Clear();
            switch (input)
            {
                case 1:
                    EnterExpression();
                    break;
                case 2:
                    SetVariable();
                    break;
                case 3:
                    EvaluateTree();
                    break;
                case 4:
                    Quit();
                    return 0;
                default:
                    Console.WriteLine("Command not recognized");
                    break;
            }

            Console.WriteLine();
            return 1;
        }

        private static void EnterExpression()
        {
            Console.WriteLine("Entering Expression");

            // Implementation Here
            // Will probably construct and return a new tree? Variables ARE cleared out with new expression
            Console.WriteLine("Enter a new expression:");
            string input = Console.ReadLine();
            demoTree = new ExpressionTree(input);
            expression = input;
        }

        private static void SetVariable()
        {
            Console.WriteLine("Setting Variable");

            // Implementation Here
            Console.WriteLine("Enter a variable name:");
            string name = Console.ReadLine();
            Console.WriteLine("Enter a variable value:");
            double val = double.Parse(Console.ReadLine());
            demoTree.SetVariable(name, val);
        }

        private static void EvaluateTree()
        {
            Console.WriteLine("Evaluating Tree");

            // Implementation Here
            Console.WriteLine("Tree evaluates to:" + demoTree.Evaluate());
        }

        private static void Quit()
        {
            Console.WriteLine("Exiting Demo");

            // Implementation Here
        }
    }
}
