// <copyright file="BST.cs" company="Eric-Furukawa">
// Copyright (c) Eric-Furukawa. All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homework1
{
    /// <summary>
    /// Class implementation of a binary search tree data structure.
    /// </summary>
    public class BST
    {
        private BSTNode root;

        /// <summary>
        /// Gets or sets BSTNode root.
        /// </summary>
        public BSTNode Root
        {
            get { return this.root; }
            set { this.root = value; }
        }

        /// <summary>
        /// Calculates and returns the minimum level of a BST based on the number of nodes it has.
        /// </summary>
        /// <param name="nodeCount">The number of nodes in the tree the minimum levels is calculated for.</param>
        /// <returns>The minimum level of the tree.</returns>
        public static int CalculateMinLevels(int nodeCount)
        {
            if (nodeCount == 0)
            {
                return 0;
            }

            return (int)Math.Floor(Math.Log(nodeCount, 2)) + 1;
        }

        /// <summary>
        /// Adds a new BSTNode to the BST with the given int value as its data.
        /// </summary>
        /// <param name = "data">The int the new BSTNode will have.</param>
        /// <returns>Boolean indicator of function success.</returns>
        public bool AddByValue(int data)
        {
            if (this.root == null)
            {
                // Empty BST case
                this.root = new BSTNode(data);
                return true;
            }

            BSTNode comparingNode = this.root;
            BSTNode previousNode = null;

            while (comparingNode != null)
            {
                previousNode = comparingNode;

                if (data == comparingNode.Data)
                {
                    Console.WriteLine("Cannot add new BSTnode. Duplicate Value.\n");
                    return false;
                }
                else if (data < comparingNode.Data)
                {
                    comparingNode = comparingNode.Left;
                }
                else if (data > comparingNode.Data)
                {
                    comparingNode = comparingNode.Right;
                }
            }

            if (data < previousNode.Data)
            {
                previousNode.Left = new BSTNode(data);
            }
            else if (data > previousNode.Data)
            {
                previousNode.Right = new BSTNode(data);
            }

            return true;
        }

        /// <summary>
        /// Iterates through the BST and prints nodes in order.
        /// </summary>
        /// <param name="parent">Recursive parameter to keep track of parent node.</param>
        /// <param name="appendedStr">Recursive parameter to keep track of string to print.</param>
        /// <returns>Traversal ordered string separated by spaces.</returns>
        public string TraverseInOrder(BSTNode parent, string appendedStr)
        {
            if (parent != null)
            {
                appendedStr = this.TraverseInOrder(parent.Left, appendedStr);
                appendedStr = appendedStr + parent.Data + " ";
                appendedStr = this.TraverseInOrder(parent.Right, appendedStr);
            }

            return appendedStr;
        }

        /// <summary>
        /// Inital case to iterate through the BST and print nodes using root as parent instead.
        /// </summary>
        /// <returns>Traversal ordered string separated by spaces.</returns>
        public string TraverseInOrderBase()
        {
            string appendedStr = string.Empty;
            if (this.root != null)
            {
                appendedStr = this.TraverseInOrder(this.root.Left, appendedStr);
                appendedStr = appendedStr + this.root.Data + " ";
                appendedStr = this.TraverseInOrder(this.root.Right, appendedStr);
            }

            return appendedStr;
        }

        /// <summary>
        /// Iterates through the BST to count total nodes under the parent node.
        /// </summary>
        /// <param name="parent">Recursive parameter to keep track of parent node.</param>
        /// <returns>Int value for nodes counted below and including the current parent node.</returns>>
        public int CountNodes(BSTNode parent)
        {
            if (parent != null)
            {
                int leftCount = this.CountNodes(parent.Left);
                int rightCount = this.CountNodes(parent.Right);
                return leftCount + rightCount + 1;
            }

            return 0;
        }

        /// <summary>
        /// Iterates through the BST to count total nodes under the root node.
        /// </summary>
        /// <returns>Int value for nodes counted below and including the root node.</returns>>
        public int CountNodesBase()
        {
            if (this.root != null)
            {
                int leftCount = this.CountNodes(this.root.Left);
                int rightCount = this.CountNodes(this.root.Right);
                return leftCount + rightCount + 1;
            }

            return 0;
        }

        /// <summary>
        /// Iterates through the BST to determine the level of the current parent node.
        /// </summary>
        /// <param name="parent">Recursive parameter to keep track of parent node.</param>
        /// <returns>Int value for the level of the current parent node.</returns>>
        public int GetTreeLevel(BSTNode parent)
        {
            // Method: Get the longest path down from root
            if (parent != null)
            {
                int leftLevel = this.GetTreeLevel(parent.Left);
                int rightLevel = this.GetTreeLevel(parent.Right);
                if (leftLevel > rightLevel)
                {
                    return leftLevel + 1;
                }

                return rightLevel + 1;
            }

            return 0;
        }

        /// <summary>
        /// Iterates through the BST to determine the level of the tree.
        /// </summary>
        /// <returns>Int value for the level of the tree.</returns>>
        public int GetTreeLevelBase()
        {
            // Method: Get the longest path down from root
            if (this.root != null)
            {
                int leftLevel = this.GetTreeLevel(this.root.Left);
                int rightLevel = this.GetTreeLevel(this.root.Right);
                if (leftLevel > rightLevel)
                {
                    return leftLevel + 1;
                }

                return rightLevel + 1;
            }

            return 0;
        }
    }
}
