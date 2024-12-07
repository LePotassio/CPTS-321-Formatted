// <copyright file="ProjectMain.cs" company="Eric-Furukawa">
// Copyright (c) Eric-Furukawa. All rights reserved.
// </copyright>
//
//--------Homework 1 Main File-----------
// Eric Furukawa, 9/9/2020, CPTS 321
// Desc: Contains the main exectuion of the program described in homework 1

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homework1
{
    /// <summary>
    /// Class ProjectMain contains the main execution of the program.
    /// </summary>
    public class ProjectMain
    {
        /// <summary>
        /// Main execution of the program.
        /// </summary>
        /// <param name="args">Application passed arguments.</param>
        public static void Main(string[] args)
        {
            Console.WriteLine("PROGRAM START\n");

            // Get a list of integer numbers from the user on a single line
            // Example input: 5 4 20 5 99 2 ...
            Console.WriteLine("Please enter a list of numbers with spaces between:\n");
            string input = Console.ReadLine();
            Console.Clear();

            // Console.WriteLine(string.Join("\n", splitList));

            // Add all the numbers to a binary search tree in the order they were entered
            // How: loop through string and split into add data to BST
            BST myBST = new BST();
            PopulateTree(myBST, input);

            // Display the number in sorted order (smallest first, largest last)
            Console.WriteLine("BST Printed in Order: ");
            Console.WriteLine(myBST.TraverseInOrderBase());
            Console.WriteLine("\n");

            // Display the following statistics about the tree

            // Number of items
            int nodeCount = myBST.CountNodesBase();
            Console.WriteLine("BST Node Count: " + nodeCount + "\n");

            // Number of levels in the tree
            Console.WriteLine("BST Level: " + myBST.GetTreeLevelBase() + "\n");

            // Theoretical minimum number of levels with given number of nodes
            Console.WriteLine("Minimum Level with [" + nodeCount + "] Nodes: " + BST.CalculateMinLevels(nodeCount) + "\n");
            Console.WriteLine("PROGRAM END");
        }

        /// <summary>
        /// Adds nodes to the BST with data parsed from the input string.
        /// </summary>
        /// <param name="newBST">New BST object to be populated with nodes from list.</param>
        /// <param name="inputList">Unsplit string list of data to be added.</param>
        public static void PopulateTree(BST newBST, string inputList)
        {
            string[] splitList = inputList.Split(' ');

            int c = 0;
            if (splitList[0] != string.Empty)
            {
                while (c < splitList.Length)
                {
                    newBST.AddByValue(int.Parse(splitList[c]));
                    c++;
                }
            }
        }
    }
}
