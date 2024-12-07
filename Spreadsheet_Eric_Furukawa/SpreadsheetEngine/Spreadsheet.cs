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
        /// <summary>
        /// Controller of command tracking for undo and redo functionality.
        /// </summary>
        private CellChangeControl CellChangeControl;

        private Cell[,] grid;

        /// <summary>
        /// Initializes a new instance of the <see cref="Spreadsheet"/> class.
        /// </summary>
        /// <param name="rows">Number of rows of cells the cell grid is to be initialized with.</param>
        /// <param name="cols">Number of cols of cells the cell grid is to be initialized with.</param>
        public Spreadsheet(int rows, int cols)
        {
            this.CellChangeControl = new CellChangeControl();
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
        /// Event delegate for property change notification.
        /// </summary>
        public event PropertyChangedEventHandler CellPropertyChanged = (sender, e) => { };

        /// <summary>
        /// Event delegate for command stack change notification.
        /// </summary>
        public event PropertyChangedEventHandler ControlStackChanged = (sender, e) => { };

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
        /// Formats and passes cell color changes to controller.
        /// </summary>
        /// <param name="changedCells">Array of changed cells.</param>
        /// <param name="oldColors">Array of original colors of cells.</param>
        public void AddColorChangeCommandList(Cell[] changedCells, uint[] oldColors)
        {
            Command[] newUndo = new Command[changedCells.Length];
            for (int c = 0; c < changedCells.Length; c++)
            {
                newUndo[c] = new ColorChangeCommand(changedCells[c], oldColors[c]);
            }

            this.CellChangeControl.AddUndo(newUndo);
            this.CellChangeControl.ClearRedoStack();
            this.ControlStackChanged(this, new PropertyChangedEventArgs("Undo"));
        }

        /// <summary>
        /// Formats and passes cell text changes to controller.
        /// </summary>
        /// <param name="changedCell">Cell that has been modified.</param>
        /// <param name="oldText">Test of cell before change.</param>
        public void AddTextChangeCommandList(Cell changedCell, string oldText)
        {
            Command[] newUndo = new Command[1];
            newUndo[0] = new TextChangeCommand(changedCell, oldText);
            this.CellChangeControl.AddUndo(newUndo);
            this.CellChangeControl.ClearRedoStack();
            this.ControlStackChanged(this, new PropertyChangedEventArgs("Undo"));
        }

        /// <summary>
        /// Informs controller to undo and notifies UI of stack changes.
        /// </summary>
        public void UndoCommand()
        {
            this.CellChangeControl.UndoButtonPushed();
            this.ControlStackChanged(this, new PropertyChangedEventArgs("UndoRedo"));
        }

        /// <summary>
        /// Informs controller to redo and notifies UI of stack changes.
        /// </summary>
        public void RedoCommand()
        {
            this.CellChangeControl.RedoButtonPushed();
            this.ControlStackChanged(this, new PropertyChangedEventArgs("UndoRedo"));
        }

        /// <summary>
        /// Returns if undo stack of controller is empty.
        /// </summary>
        /// <returns>Boolean value of if stack is empty.</returns>
        public bool UndoStackEmpty()
        {
            return this.CellChangeControl.UndoStackEmpty();
        }

        /// <summary>
        /// Returns if redo stack of controller is empty.
        /// </summary>
        /// <returns>Boolean value of if stack is empty.</returns>
        public bool RedoStackEmpty()
        {
            return this.CellChangeControl.RedoStackEmpty();
        }

        /// <summary>
        /// Returns the row indicating portion of the variable string in int format.
        /// </summary>
        /// <param name="variableReference">Variable string the row number is to be extracted from.</param>
        /// <returns>Int representation of the row refereced.</returns>
        private static int FormatRowReference(string variableReference)
        {
            string rowC = variableReference.Substring(1);
            return int.Parse(rowC) - 1;
        }

        /// <summary>
        /// Returns the column indicating portion of the variable string in int format.
        /// </summary>
        /// <param name="variableReference">Variable string the column number is to be extracted from.</param>
        /// <returns>Int representation of the column refereced.</returns>
        private static int FormatColReference(string variableReference)
        {
            char colcC = variableReference[0];
            return colcC - 65;
        }

        /// <summary>
        /// Assigns the integer values of the row and col based on a variable reference string.
        /// </summary>
        /// <param name="row">Integer row value of the reference.</param>
        /// <param name="col">Integer col value of the reference.</param>
        /// <param name="variableReference">String for which the row and col are to be extracted from.</param>
        private static void AssignRowColumns(ref int row, ref int col, string variableReference)
        {
            row = FormatRowReference(variableReference);
            col = FormatColReference(variableReference);
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
            if (e.PropertyName == "Text" && (((Cell)sender).Text != null && ((Cell)sender).Text[0] == '='))
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
                    int row = 0, col = 0;
                    AssignRowColumns(ref row, ref col, variable);

                    this.grid[row, col].PropertyChanged += ((Cell)sender).ReferencedCellChangedEvent;
                }

                // Also update all cells effected...
            }

            if (e.PropertyName != "BGColor")
            {
                this.CellPropertyChanged(sender, new PropertyChangedEventArgs("Value"));
            }
            else
            {
                this.CellPropertyChanged(sender, new PropertyChangedEventArgs("BGColor"));
            }
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
                int row = 0, col = 0;
                AssignRowColumns(ref row, ref col, variable);

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
