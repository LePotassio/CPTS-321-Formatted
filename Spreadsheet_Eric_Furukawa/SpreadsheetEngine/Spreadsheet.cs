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
                    SSCell newCell = new SSCell(r, c);
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
            if (e.PropertyName == "Text" && ((Cell)sender).Text[0] == '=')
            {
                string rowC = ((Cell)sender).Text.Substring(2);
                char colC = ((Cell)sender).Text[1];
                int row = int.Parse(rowC) - 1;
                int col = colC - 65;
                ((Cell)sender).SetValue(this.GetCell(row, col).Text);
            }

            this.CellPropertyChanged(sender, new PropertyChangedEventArgs("Value"));
        }
    }
}
