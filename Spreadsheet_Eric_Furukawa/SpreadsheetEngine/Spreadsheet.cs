// <copyright file="Spreadsheet.cs" company="Eric-Furukawa">
// Copyright (c) Eric-Furukawa. All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

[assembly: InternalsVisibleTo("NUnit.Tests1")]

namespace Cpts321
{
    /// <summary>
    /// Represents the grid of cells to be used by a DataGridView.
    /// </summary>
    public class Spreadsheet
    {
        private Cell[,] grid;

        /// <summary>
        /// Initializes a new instance of the <see cref="Spreadsheet"/> class.
        /// </summary>
        /// <param name="rows">Number of rows of cells the cell grid is to be initialized with.</param>
        /// <param name="cols">Number of cols of cells the cell grid is to be initialized with.</param>
        public Spreadsheet(int rows, int cols)
        {
            this.grid = new Cell[rows, cols];

            for (int r = 0; r < rows; r++)
            {
                for (int c = 0; c < cols; c++)
                {
                    SpreadsheetCell newCell = new SpreadsheetCell(r, c);
                    this.grid[r, c] = newCell;
                    newCell.PropertyChanged += this.CellPropertyChangedEvent;
                }
            }
        }

        /// <summary>
        /// Event handler for property change notification.
        /// </summary>
        public event PropertyChangedEventHandler CellPropertyChanged = (sender, e) => { };

        /// <summary>
        /// Gets number of columns in cell grid.
        /// </summary>
        public int ColumnCount
        {
            get
            {
                return this.grid.GetLength(0);
            }
        }

        /// <summary>
        /// Gets number of rows in cell grid.
        /// </summary>
        public int RowCount
        {
            get
            {
                return this.grid.GetLength(1);
            }
        }

        /// <summary>
        /// Returns cell from grid at specified coords.
        /// </summary>
        /// <param name="row">Row of cell to retrieve.</param>
        /// <param name="col">COlumn of cell to retrieve.</param>
        /// <returns>Retrieved cell from grid.</returns>
        public Cell GetCell(int row, int col)
        {
            return this.grid[row, col];
        }

        /// <summary>
        /// Event called after cell text changed.
        /// </summary>
        /// <param name="sender">Sending cell object.</param>
        /// <param name="e">Sending arguments.</param>
        private void CellPropertyChangedEvent(object sender, PropertyChangedEventArgs e)
        {
            // Unsubscribe from previous variables
            ((Cell)sender).ResetPropertyChangedEvent();
            ((Cell)sender).PropertyChanged += this.CellPropertyChangedEvent;

            if (e.PropertyName == "Text" && ((Cell)sender).Text[0] == '=')
            {
                // What was the point of all this??

                // string rowC = ((Cell)sender).Text.Substring(2);
                // char colC = ((Cell)sender).Text[1];
                // int row = int.Parse(rowC) - 1;
                // int col = colC - 65;

                // ((Cell)sender).SetValue(this.GetCell(row, col).Text);
                ExpressionTree expressionTree = new ExpressionTree(((Cell)sender).Text.Substring(1));

                // Also need to add the corresponding values to every variable...
                try
                {
                    this.AssignVariables(expressionTree);
                }
                catch (UnassignedVariableException)
                {
                    Console.WriteLine("Unassigned Cell Referenced");
                    ((Cell)sender).SetValue("Unref Variable");
                    this.CellPropertyChanged(sender, new PropertyChangedEventArgs("Value"));
                    return;
                }

                ((Cell)sender).SetValue(expressionTree.Evaluate().ToString());

                // Try evaluating and catch unassigned variable
                /*try
                {
                    ((Cell)sender).SetValue(expressionTree.Evaluate().ToString());
                }
                catch (UnassignedVariableException)
                {
                    Console.WriteLine("Unassigned Cell Referenced");
                    ((Cell)sender).SetValue("Unref Variable");
                    return;
                }*/

                // double evaluatedCell;

                // Subscribe to variables...
                List<string> variableNames = expressionTree.GetVariableNames();
                foreach (string variable in variableNames)
                {
                    string rowC = variable.Substring(1);
                    char colC = variable[0];
                    int row = int.Parse(rowC) - 1;
                    int col = colC - 65;

                    this.grid[row, col].PropertyChanged += ((Cell)sender).ReferencedCellChangedEvent;
                }

                // Also update all cells effected...
            }

            this.CellPropertyChanged(sender, new PropertyChangedEventArgs("Value"));
        }

        /// <summary>
        /// Adds variables to the expression tree dictionary based on cells in the datagridview.
        /// </summary>
        /// <param name="expressionTree">Expresion tree to add variables to.</param>
        private void AssignVariables(ExpressionTree expressionTree)
        {
            List<string> variableNames = expressionTree.GetVariableNames();
            foreach (string variable in variableNames)
            {
                string rowC = variable.Substring(1);
                char colC = variable[0];
                int row = int.Parse(rowC) - 1;
                int col = colC - 65;

                double variableValue;

                // Consider the double value of a cell to be:
                // Numerical value parsed if double.TryParse on the value string succeeds
                if (!double.TryParse(this.grid[row, col].Value, out variableValue))
                {
                    // 0 otherwise
                    variableValue = 0;
                    throw new UnassignedVariableException();
                }

                expressionTree.SetVariable(variable, variableValue);
            }
        }

        /// <summary>
        /// Instantiable cell which is limited to instantiation in only the Spreadsheet class.
        /// </summary>
        private class SpreadsheetCell : Cell
        {
            /// <summary>
            /// Initializes a new instance of the <see cref="SpreadsheetCell"/> class.
            /// </summary>
            /// <param name="row">Row for rowindex to be set to.</param>
            /// <param name="col">Column for columnindex to be set to.</param>
            public SpreadsheetCell(int row, int col)
                : base(row, col)
            {
            }
        }
    }
}
