// <copyright file="Form1.cs" company="Eric-Furukawa">
// Copyright (c) Eric-Furukawa. All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

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

            this.spreadsheet.ControlStackChanged += this.UpdateUndoRedo;
            this.undoToolStripMenuItem.Enabled = false;
            this.redoToolStripMenuItem.Enabled = false;
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

        /// <summary>
        /// Populate columns of datagridview.
        /// </summary>
        /// <param name="colItems">Column names to popluate with.</param>
        /// <param name="grid">Grid to poulate.</param>
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

        /// <summary>
        /// Populate rows of datagridview.
        /// </summary>
        /// <param name="minVal">Minimum row label value.</param>
        /// <param name="maxVal">Maximum row label value.</param>
        /// <param name="grid">Grid to populate.</param>
        private void AssignRows(int minVal, int maxVal, DataGridView grid)
        {
            for (int c = minVal; c <= maxVal; c++)
            {
                DataGridViewRow newRow = new DataGridViewRow();
                newRow.HeaderCell.Value = c.ToString();
                grid.Rows.Add(newRow);
            }
        }

        /// <summary>
        /// Handler for updating a cell.
        /// </summary>
        /// <param name="sender">Sending object of request to update cell.</param>
        /// <param name="e">Arguments of update cell request.</param>
        private void UpdateCell(object sender, PropertyChangedEventArgs e)
        {
            this.UpdateCellInGrid(sender, e, this.dataGridView1);
        }

        /// <summary>
        /// Updates a cell in the grid depending on a property change in the cell.
        /// </summary>
        /// <param name="sender">Sending cell of change.</param>
        /// <param name="e">Arguments of event handler call.</param>
        /// <param name="grid">Grid in quesiton to be updated.</param>
        private void UpdateCellInGrid(object sender, PropertyChangedEventArgs e, DataGridView grid)
        {
            if (e.PropertyName == "Value")
            {
                int row = ((Cpts321.Cell)sender).RowIndex;
                int col = ((Cpts321.Cell)sender).ColumnIndex;
                grid.Rows[row].Cells[col].Value = ((Cpts321.Cell)sender).Value;
            }
            else if (e.PropertyName == "BGColor")
            {
                int row = ((Cpts321.Cell)sender).RowIndex;
                int col = ((Cpts321.Cell)sender).ColumnIndex;
                grid.Rows[row].Cells[col].Style.BackColor = Color.FromArgb((int)((Cpts321.Cell)sender).BGColor);
            }
        }

        /// <summary>
        /// Handler for demo button clicked.
        /// </summary>
        /// <param name="sender">Object sending request for handler.</param>
        /// <param name="e">Arguments of handler request.</param>
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
            string oldText = this.spreadsheet.GetCell(e.RowIndex, e.ColumnIndex).Text;
            this.spreadsheet.GetCell(e.RowIndex, e.ColumnIndex).Text = (string)this.dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value;
            this.spreadsheet.AddTextChangeCommandList(this.spreadsheet.GetCell(e.RowIndex, e.ColumnIndex), oldText);
            /*
            // Could also potentially call a funciton in spreadsheet with old and new text to hide Commands from UI.
            Cpts321.Command[] newUndo = new Cpts321.Command[1];
            newUndo[0] = new Cpts321.TextChangeCommand(this.spreadsheet.GetCell(e.RowIndex, e.ColumnIndex), oldText);
            this.spreadsheet.cellChangeControl.AddUndo(newUndo);
            */

            // this.dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = this.spreadsheet.GetCell(e.RowIndex, e.ColumnIndex).Value;
        }

        /// <summary>
        /// Handler for change cells color button.
        /// </summary>
        /// <param name="sender">Sending object of handler.</param>
        /// <param name="e">Arguments of the button pressed event.</param>
        private void ChangeSelectedCellColorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // UI Should not be making commands!
            ColorDialog colorDlg = new ColorDialog();
            uint[] oldColors = new uint[this.dataGridView1.SelectedCells.Count];
            Cpts321.Cell[] changedCells = new Cpts321.Cell[this.dataGridView1.SelectedCells.Count];
            int colorIndex = 0;

            if (colorDlg.ShowDialog() == DialogResult.OK)
            {
                // Update all selected cell's color with dialogue's result (colorDlg.Color)
                foreach (DataGridViewCell cell in this.dataGridView1.SelectedCells)
                {
                    uint oldColor = this.spreadsheet.GetCell(cell.RowIndex, cell.ColumnIndex).BGColor;
                    oldColors[colorIndex] = this.spreadsheet.GetCell(cell.RowIndex, cell.ColumnIndex).BGColor;
                    this.spreadsheet.GetCell(cell.RowIndex, cell.ColumnIndex).BGColor = (uint)colorDlg.Color.ToArgb();
                    changedCells[colorIndex] = this.spreadsheet.GetCell(cell.RowIndex, cell.ColumnIndex);
                    colorIndex++;
                }

                this.spreadsheet.AddColorChangeCommandList(changedCells, oldColors);
            }
        }

        /// <summary>
        /// Handler for undo button pressed event.
        /// </summary>
        /// <param name="sender">Requesting object of handler.</param>
        /// <param name="e">Arguments of request.</param>
        private void UndoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.spreadsheet.UndoCommand();
        }

        /// <summary>
        /// handler for redo button pressed event.
        /// </summary>
        /// <param name="sender">Requesting object of handler.</param>
        /// <param name="e">Arguments of request.</param>
        private void RedoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.spreadsheet.RedoCommand();
        }

        /// <summary>
        /// Event handler for when undo or redo options have been updated.
        /// </summary>
        /// <param name="sender">Sending object of handler request.</param>
        /// <param name="e">Arguments sent by sending object.</param>
        private void UpdateUndoRedo(object sender, PropertyChangedEventArgs e)
        {
            // We want this to be called whenever the undo or redo stack changes.
            // Console.WriteLine("Undo and Redo Button Updated!!");
            if (this.spreadsheet.UndoStackEmpty())
            {
                this.undoToolStripMenuItem.Enabled = false;
            }
            else
            {
                this.undoToolStripMenuItem.Enabled = true;
            }

            if (this.spreadsheet.RedoStackEmpty())
            {
                this.redoToolStripMenuItem.Enabled = false;
            }
            else
            {
                this.redoToolStripMenuItem.Enabled = true;
            }
        }

        private void SaveButtonToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FileStream saveStream = new FileStream("SpreadsheetData.xml", FileMode.OpenOrCreate, FileAccess.Write);
            saveStream.SetLength(0);
            this.spreadsheet.SaveSheet(saveStream);
            saveStream.Close();
        }

        private void LoadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FileStream loadStream = new FileStream("SpreadsheetData.xml", FileMode.OpenOrCreate, FileAccess.Read);
            this.spreadsheet.LoadSheet(loadStream);
            loadStream.Close();
        }
    }
}
