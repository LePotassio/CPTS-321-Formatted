// <copyright file="ColorChangeCommand.cs" company="Eric-Furukawa">
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
    /// Command subclass representing a cell's color being changed.
    /// </summary>
    internal class ColorChangeCommand : Command
    {
        private Cell changedCell;
        private uint oldColor;
        private uint newColor;

        /// <summary>
        /// Initializes a new instance of the <see cref="ColorChangeCommand"/> class.
        /// </summary>
        /// <param name="changedCell">Cell that has been changed.</param>
        /// <param name="oldColor">Previous color of cell.</param>
        public ColorChangeCommand(Cell changedCell, uint oldColor)
        {
            this.changedCell = changedCell;
            this.oldColor = oldColor;
            this.newColor = changedCell.BGColor;
        }

        /// <summary>
        /// Resets cell color to its new color.
        /// </summary>
        public void Execute()
        {
            this.changedCell.BGColor = this.newColor;
        }

        /// <summary>
        /// Resets cell color to its new color.
        /// </summary>
        public void Unexecute()
        {
            this.changedCell.BGColor = this.oldColor;
        }
    }
}
