// <copyright file="VariableNode.cs" company="Eric-Furukawa">
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
    /// Represents a node with a variable associated with a numerical value.
    /// </summary>
    public class VariableNode : Node
    {
        // Deprocated -> This should be done in tree evaluate as no access to the variable dictionary here.
        /*
        /// <summary>
        /// Returns the double converteed variable value of the variable node by lookup in Tree dictionary.
        /// </summary>
        /// <returns>Double variable value of the node.</returns>
        public double GetFloatValue()
        {
            // Lookup variable from tree here
            return 0.0;
        }
        */

        private readonly string name;

        private Dictionary<string, double> variables;

        /// <summary>
        /// Initializes a new instance of the <see cref="VariableNode"/> class.
        /// </summary>
        /// <param name="name">Name of variable in the variable node.</param>
        /// <param name="variables">Reference to dictionary of variables.</param>
        public VariableNode(string name, ref Dictionary<string, double> variables)
        {
            this.name = name;
            this.variables = variables;

            // Variables now added at tree construction with default 0, demo add variables implmented to allow seting vars not in tree and accounts existing variables alread in dictionary.
            if (!this.variables.ContainsKey(name))
            {
                this.variables.Add(name, 0);
            }
        }

        /// <summary>
        /// Gets name string member of the variable node.
        /// </summary>
        public string Name
        {
            get
            {
                return this.name;
            }
        }

        /// <summary>
        /// Returns associated value of the variable node from the dictionary, returning 0.0 if not found.
        /// </summary>
        /// <returns>0.0or the value assigned to the variable in the variables dictionary.</returns>
        public override double Evaluate()
        {
            double value = 0.0;
            if (this.variables.ContainsKey(this.name))
            {
                value = this.variables[this.name];
            }

            return value;
        }
    }
}
