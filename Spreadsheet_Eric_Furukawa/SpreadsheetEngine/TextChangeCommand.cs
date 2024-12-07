// <copyright file="TextChangeCommand.cs" company="Eric-Furukawa">
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
    /// Command subclass representing a cell's text being changed.
    /// </summary>
    internal class TextChangeCommand : Command
    {
        // Needs the cell changed, the text before and the text after.
        private Cell changedCell;
        private string oldText;
        private string newText;

        /// <summary>
        /// Initializes a new instance of the <see cref="TextChangeCommand"/> class.
        /// </summary>
        /// <param name="changedCell">Cell that has been changed.</param>
        /// <param name="oldText">Previous text of cell.</param>
        public TextChangeCommand(Cell changedCell, string oldText)
        {
            this.changedCell = changedCell;
            this.oldText = oldText;
            this.newText = changedCell.Text;
        }

        /// <summary>
        /// Resets cell text to its new text.
        /// </summary>
        public void Execute()
        {
            this.changedCell.Text = this.newText;
        }

        /// <summary>
        /// Resets cell text to its old text.
        /// </summary>
        public void Unexecute()
        {
            this.changedCell.Text = this.oldText;
        }
    }
}
