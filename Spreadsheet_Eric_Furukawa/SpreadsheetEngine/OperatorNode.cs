// <copyright file="OperatorNode.cs" company="Eric-Furukawa">
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
    /// Represents a node with an operator associated which modifies the evaluation of neighboring nodes.
    /// </summary>
    public abstract class OperatorNode : Node
    {
        private Node leftChild;
        private Node rightChild;

        /// <summary>
        /// Enums for the associativity of the operator.
        /// </summary>
        public enum Associative
        {
            /// <summary>
            /// Right associative.
            /// </summary>
            Right,

            /// <summary>
            /// Left associative.
            /// </summary>
            Left,
        }

        /// <summary>
        /// Gets or sets leftChild node member of the operator node.
        /// </summary>
        public Node LeftChild
        {
            get
            {
                return this.leftChild;
            }

            set
            {
                this.leftChild = value;
            }
        }

        /// <summary>
        /// Gets or sets rightChild node member of the operator node.
        /// </summary>
        public Node RightChild
        {
            get
            {
                return this.rightChild;
            }

            set
            {
                this.rightChild = value;
            }
        }
    }
}
