// <copyright file="BSTNode.cs" company="Eric-Furukawa">
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
    /// Implementation of node object for the binary search tree data structure.
    /// </summary>
    public class BSTNode
    {
        private BSTNode left;

        private BSTNode right;

        private int data;

        /// <summary>
        /// Initializes a new instance of the <see cref="BSTNode"/> class.
        /// </summary>
        /// <param name="data">The int data the created BSTNode's data field will be set to.</param>
        public BSTNode(int data)
        {
            this.data = data;
        }

        /// <summary>
        /// Gets or sets left BSTNode field.
        /// </summary>
        public BSTNode Left
        {
            get { return this.left; }
            set { this.left = value; }
        }

        /// <summary>
        /// Gets or sets right BSTNode field.
        /// </summary>
        public BSTNode Right
        {
            get { return this.right; }
            set { this.right = value; }
        }

        /// <summary>
        /// Gets or sets int data field.
        /// </summary>
        public int Data
        {
            get { return this.data; }
            set { this.data = value; }
        }
    }
}
