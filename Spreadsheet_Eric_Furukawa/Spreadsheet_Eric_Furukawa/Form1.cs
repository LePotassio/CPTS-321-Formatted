// <copyright file="Form1.cs" company="Eric-Furukawa">
// Copyright (c) Eric-Furukawa. All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Spreadsheet_Eric_Furukawa
{
    /// <summary>
    /// Class containing handlers and implementation of form program.
    /// </summary>
    public partial class Form1 : Form
    {
        private Cpts321.Spreadsheet spreadsheet;

        /// <summary>
        /// Initializes a new instance of the <see cref="Form1"/> class.
        /// </summary>
        public Form1()
        {
            this.InitializeComponent();

            // Create Columns A to Z.
            // This could have also been done using ascii...
            string[] colNames = { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z" };

            this.AssignCols(colNames, this.dataGridView1);
            this.AssignRows(1, 50, this.dataGridView1);
            this.spreadsheet = new Cpts321.Spreadsheet(50, 26);
            this.spreadsheet.CellPropertyChanged += this.UpdateCell;
        }

        /// <summary>
        /// Gets spreadsheet object.
        /// </summary>
        public Cpts321.Spreadsheet Spreadsheet
        {
            get
            {
                return this.spreadsheet;
            }
        }

        private void DataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // Implementation Here
        }

        private void AssignCols(string[] colItems, DataGridView grid)
        {
            /*
            DataGridViewColumn alphaCol = new DataGridViewColumn();
            alphaCol.ValueType = typeof(string);
            this.dataGridView1.Columns.Add(alphaCol);
            */

            foreach (string item in colItems)
            {
                grid.Columns.Add("Col " + item, item);
            }
        }

        private void AssignRows(int minVal, int maxVal, DataGridView grid)
        {
            for (int c = minVal; c <= maxVal; c++)
            {
                DataGridViewRow newRow = new DataGridViewRow();
                newRow.HeaderCell.Value = c.ToString();
                grid.Rows.Add(newRow);
            }
        }

        private void UpdateCell(object sender, PropertyChangedEventArgs e)
        {
            this.UpdateCellInGrid(sender, e, this.dataGridView1);
        }

        private void UpdateCellInGrid(object sender, PropertyChangedEventArgs e, DataGridView grid)
        {
            if (e.PropertyName == "Value")
            {
                int row = ((Cpts321.Cell)sender).RowIndex;
                int col = ((Cpts321.Cell)sender).ColumnIndex;
                grid.Rows[row].Cells[col].Value = ((Cpts321.Cell)sender).Value;
            }
        }

        private void Demo_Click(object sender, EventArgs e)
        {
            // Set 50 random cells to "Hello World"
            var rand = new Random();
            for (int i = 0; i < 50; i++)
            {
                this.spreadsheet.GetCell(rand.Next(0, 50), rand.Next(0, 26)).Text = "Hello World!";
            }

            // Set all cells in col B to "This cell is B#"
            for (int i = 0; i < 50; i++)
            {
                this.spreadsheet.GetCell(i, 1).Text = "This cell is B" + (i + 1);
            }

            // Set all cells in col A to "=B#"
            for (int i = 0; i < 50; i++)
            {
                this.spreadsheet.GetCell(i, 0).Text = "=B" + (i + 1);
            }
        }

        /// <summary>
        /// Handler when cell begins to be edited.
        /// </summary>
        /// <param name="sender">Object sending handler.</param>
        /// <param name="e">Event argument representing sending cell info.</param>
        private void DataGridView1_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            this.dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = this.spreadsheet.GetCell(e.RowIndex, e.ColumnIndex).Text;
        }

        /// <summary>
        /// Handler when cell ends editing.
        /// </summary>
        /// <param name="sender">Object sending handler.</param>
        /// <param name="e">Event argument representing sending cell info.</param>
        private void DataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            this.spreadsheet.GetCell(e.RowIndex, e.ColumnIndex).Text = (string)this.dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value;

            // this.dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = this.spreadsheet.GetCell(e.RowIndex, e.ColumnIndex).Value;
        }
    }
}
