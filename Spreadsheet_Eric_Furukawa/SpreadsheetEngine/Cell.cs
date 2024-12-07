// <copyright file="Cell.cs" company="Eric-Furukawa">
// Copyright (c) Eric-Furukawa. All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cpts321
{
    /// <summary>
    /// Represents a cell to be used in a WinForms DataGridView.
    /// </summary>
    public abstract class Cell : INotifyPropertyChanged
    {
        // Elements have been kept protected as per instructions, however I think they should be private.
        protected int rowIndex;
        protected int columnIndex;
        protected string text;
        protected string value;

        /*
        /// <summary>
        /// Represents functions to call after property changed.
        /// </summary>
        /// <param name="sender">Object sending request for function calls.</param>
        /// <param name="e">Arguments sent by sender.</param>
        public delegate void PropertyChangedEventHandler(object sender, PropertyChangedEventArgs e);
        */

        /// <summary>
        /// Initializes a new instance of the <see cref="Cell"/> class.
        /// </summary>
        /// <param name="row">Row index for Cell to initialize with.</param>
        /// /// <param name="col">Column index for Cell to initialize with.</param>
        public Cell(int row, int col)
        {
            this.rowIndex = row;
            this.columnIndex = col;
        }

        /// <summary>
        /// Event handler for property change notification.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged = (sender, e) => { };

        /// <summary>
        /// Gets readonly rowIndex field.
        /// </summary>
        public int RowIndex
        {
            get
            {
                return this.rowIndex;
            }
        }

        /// <summary>
        /// Gets readonly rowIndex field.
        /// </summary>
        public int ColumnIndex
        {
            get
            {
                return this.columnIndex;
            }
        }

        /// <summary>
        /// Gets or sets text field.
        /// </summary>
        public string Text
        {
            get
            {
                return this.text;
            }

            set
            {
                if (this.text != value)
                {
                    this.text = value;
                    this.value = this.text;
                    this.PropertyChanged(this, new PropertyChangedEventArgs("Text"));
                }
            }
        }

        /// <summary>
        /// Gets evaluated value field.
        /// </summary>
        public string Value
        {
            get
            {
                return this.value;
            }
        }

        /// <summary>
        /// Sets the value field of the cell. There is no way to limit class access to a specific class.
        /// </summary>
        /// <param name="newValue">The new value steing the value should be set to.</param>
        internal void SetValue(string newValue)
        {
            this.value = newValue;
        }
    }
}
