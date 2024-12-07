// <copyright file="Spreadsheet.cs" company="Eric-Furukawa">
// Copyright (c) Eric-Furukawa. All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

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
        private CellChangeControl cellChangeControl;

        private Cell[,] grid;

        /// <summary>
        /// Initializes a new instance of the <see cref="Spreadsheet"/> class.
        /// </summary>
        /// <param name="rows">Number of rows of cells the cell grid is to be initialized with.</param>
        /// <param name="cols">Number of cols of cells the cell grid is to be initialized with.</param>
        public Spreadsheet(int rows, int cols)
        {
            this.cellChangeControl = new CellChangeControl();
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

            this.cellChangeControl.AddUndo(newUndo);
            this.cellChangeControl.ClearRedoStack();
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
            this.cellChangeControl.AddUndo(newUndo);
            this.cellChangeControl.ClearRedoStack();
            this.ControlStackChanged(this, new PropertyChangedEventArgs("Undo"));
        }

        /// <summary>
        /// Informs controller to undo and notifies UI of stack changes.
        /// </summary>
        public void UndoCommand()
        {
            this.cellChangeControl.UndoButtonPushed();
            this.ControlStackChanged(this, new PropertyChangedEventArgs("UndoRedo"));
        }

        /// <summary>
        /// Informs controller to redo and notifies UI of stack changes.
        /// </summary>
        public void RedoCommand()
        {
            this.cellChangeControl.RedoButtonPushed();
            this.ControlStackChanged(this, new PropertyChangedEventArgs("UndoRedo"));
        }

        /// <summary>
        /// Returns if undo stack of controller is empty.
        /// </summary>
        /// <returns>Boolean value of if stack is empty.</returns>
        public bool UndoStackEmpty()
        {
            return this.cellChangeControl.UndoStackEmpty();
        }

        /// <summary>
        /// Returns if redo stack of controller is empty.
        /// </summary>
        /// <returns>Boolean value of if stack is empty.</returns>
        public bool RedoStackEmpty()
        {
            return this.cellChangeControl.RedoStackEmpty();
        }

        /// <summary>
        /// Clears the command controller undo stack will also informing UI of change.
        /// </summary>
        public void ClearUndoStack()
        {
            this.cellChangeControl.ClearUndoStack();
            this.ControlStackChanged(this, new PropertyChangedEventArgs("UndoRedo"));
        }

        /// <summary>
        /// Clears the command controller redo stack will also informing UI of change.
        /// </summary>
        public void ClearRedoStack()
        {
            this.cellChangeControl.ClearRedoStack();
            this.ControlStackChanged(this, new PropertyChangedEventArgs("Redo"));
        }

        /// <summary>
        /// Saves a sheet to a given XML file stream.
        /// </summary>
        /// <param name="saveStream">Stream of XML file to save to.</param>
        public void SaveSheet(Stream saveStream)
        {
            this.FormatXmlSave().Save(saveStream);
        }

        /// <summary>
        /// Loads a sheet from a given XML file stream. Resilient to other tags and different orders (assuming format is correct and no duplicate tages of text, color, ect...).
        /// </summary>
        /// <param name="loadStream">Stream of XML file to load.</param>
        public void LoadSheet(Stream loadStream)
        {
            this.ClearSpreadsheet();

            // Clear the undo and redo stacks
            this.ClearUndoStack();
            this.ClearRedoStack();

            // Implementation here
            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.Load(loadStream);

            // Now iterate through the children of spreadsheet element and LoadCell each based on the children of it
            XmlNode root = xmlDocument.GetElementsByTagName("spreadsheet")[0];
            foreach (XmlNode xmlCell in root.SelectNodes("cell"))
            {
                string cellName = xmlCell.Attributes["name"].Value;
                string text = xmlCell.SelectNodes("text")[0].InnerText;
                string colorString = xmlCell.SelectNodes("bgcolor")[0].InnerText;
                this.LoadCell(cellName, text, colorString);
            }

            // Do once more to update for any later variables
            foreach (XmlNode xmlCell in root.SelectNodes("cell"))
            {
                string cellName = xmlCell.Attributes["name"].Value;
                string text = xmlCell.SelectNodes("text")[0].InnerText;
                string colorString = xmlCell.SelectNodes("bgcolor")[0].InnerText;
                this.LoadCell(cellName, text, colorString);
            }
        }

        /// <summary>
        /// Removes the promise to update for other cells using those cells as a variable.
        /// </summary>
        /// <param name="cell">Cell of variable reference.</param>
        /// <param name="oldText">Previous text of the cell for variable extraction.</param>
        public void ClearUpdateDelegates(Cell cell, string oldText)
        {
            if (oldText == null || oldText[0] != '=')
            {
                return;
            }

            ExpressionTree expressionTree = new ExpressionTree(oldText.Substring(1));
            List<string> variableNames = expressionTree.GetVariableNames();

            foreach (string variable in variableNames)
            {
                int row = 0, col = 0;
                AssignRowColumns(ref row, ref col, variable);
                this.grid[row, col].PropertyChanged -= cell.ReferencedCellChangedEvent;
            }
        }

        /// <summary>
        /// Add the promise to the cells of each variable to update the given cell on text change.
        /// </summary>
        /// <param name="cell">Cell to assign variable updates to.</param>
        /// <param name="text">Text of the cell for variable extraction.</param>
        public void AddUpdateDelegates(Cell cell, string text)
        {
            if (text == null || text[0] != '=')
            {
                return;
            }

            ExpressionTree expressionTree = new ExpressionTree(text.Substring(1));
            List<string> variableNames = expressionTree.GetVariableNames();

            foreach (string variable in variableNames)
            {
                int row = 0, col = 0;
                try
                {
                    AssignRowColumns(ref row, ref col, variable);
                }
                catch (InvalidVariableRowFormatException)
                {
                    // Here in case there is a self reference cell and a bad name + reference to self reference cell
                }

                this.grid[row, col].PropertyChanged += cell.ReferencedCellChangedEvent;
            }
        }

        /// <summary>
        /// Adds the update promises for a cell containing circular references.
        /// </summary>
        /// <param name="cell">Cell to assign variable updates to.</param>
        /// <param name="text">Text of the cell for variable extraction.</param>
        public void AddCircularUpdateDelegates(Cell cell, string text)
        {
            if (text == null || text[0] != '=' || text == "=")
            {
                return;
            }

            ExpressionTree expressionTree = new ExpressionTree(text.Substring(1));
            List<string> variableNames = expressionTree.GetVariableNames();

            if (this.CheckForCircularReference(variableNames, cell))
            {
                this.AddUpdateDelegates(cell, text);
            }
        }

        /// <summary>
        /// Traverses each variable of a cell and sets the value to Circular Reference.
        /// </summary>
        /// <param name="sender">CEll to change value of.</param>
        public void AssignCircularReferences(Cell sender)
        {
            if (sender.Text == null || sender.Text[0] != '=' || sender.Text == "=")
            {
                return;
            }

            ExpressionTree expressionTree = new ExpressionTree(sender.Text.Substring(1));
            List<string> variableNames = expressionTree.GetVariableNames();

            string cellName = ConvertToCellName(sender.RowIndex, sender.ColumnIndex);

            // Need to NOT check for just a self reference
            foreach (string variable in variableNames)
            {
                if (cellName == variable)
                {
                    return;
                }
            }

            foreach (string variable in variableNames)
            {
                if (this.ExploreForVariable(variable, cellName))
                {
                    // Set to circular... Will this work? Need to have those that use this cell to be set as well... YES because circle
                    int row = 0;
                    int col = 0;

                    // Should never encounter Invalid row
                    AssignRowColumns(ref row, ref col, variable);
                    this.grid[row, col].SetValue("Circular Reference");
                    this.CellPropertyChanged(this.grid[row, col], new PropertyChangedEventArgs("Value"));
                }
            }
        }

        /// <summary>
        /// Returns the row indicating portion of the variable string in int format.
        /// </summary>
        /// <param name="variableReference">Variable string the row number is to be extracted from.</param>
        /// <returns>Int representation of the row refereced.</returns>
        private static int FormatRowReference(string variableReference)
        {
            string rowC = variableReference.Substring(1);
            int row = 0;

            // Could have also done try around AssignRowColumns in Assign Variables instead.
            if (!int.TryParse(rowC, out row))
            {
                throw new InvalidVariableRowFormatException();
            }

            return row - 1;

            // return int.Parse(rowC) - 1;
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
        /// Converts coordinates to the format of cell names in the sheet.
        /// </summary>
        /// <param name="row">Row of cell name to create.</param>
        /// <param name="col">Column of cell name to create.</param>
        /// <returns>String of the cell name specified for.</returns>
        private static string ConvertToCellName(int row, int col)
        {
            string rowString = (row + 1).ToString();
            int colAscii = col + 65;

            return (char)colAscii + rowString;
        }

        /// <summary>
        /// Formats an XML save of the changed cells in an XmlDocument object. Split up for potential testing to be possible.
        /// </summary>
        /// <returns>The save xml for the current spreadsheet state.</returns>
        private XmlDocument FormatXmlSave()
        {
            XmlDocument xmlDocument = new XmlDocument();
            XmlElement root = xmlDocument.CreateElement("spreadsheet");
            xmlDocument.AppendChild(root);

            // Loop through the grid
            for (int row = 0; row < this.grid.GetLength(0); row++)
            {
                for (int col = 0; col < this.grid.GetLength(1); col++)
                {
                    // Check if cell was modified or not
                    if ((this.grid[row, col].Text != null && this.grid[row, col].Text != string.Empty) || this.grid[row, col].BGColor != 4294967295)
                    {
                        XmlElement cellElement = xmlDocument.CreateElement("cell");
                        XmlAttribute nameAttribute = xmlDocument.CreateAttribute("name");
                        nameAttribute.Value = ConvertToCellName(row, col);
                        cellElement.Attributes.Append(nameAttribute);

                        XmlElement colorElement = xmlDocument.CreateElement("bgcolor");
                        colorElement.InnerText = this.grid[row, col].BGColor.ToString();
                        cellElement.AppendChild(colorElement);

                        XmlElement textElement = xmlDocument.CreateElement("text");
                        textElement.InnerText = this.grid[row, col].Text;
                        cellElement.AppendChild(textElement);

                        root.AppendChild(cellElement);
                    }
                }
            }

            return xmlDocument;
        }

        /// <summary>
        /// Sets a cell based on string name text and color.
        /// </summary>
        /// <param name="cellName">String name of the cell to set.</param>
        /// <param name="text">String text of the cell to set.</param>
        /// <param name="colorString">String color of the cell to set.</param>
        private void LoadCell(string cellName, string text, string colorString)
        {
            int row = FormatRowReference(cellName);
            int col = FormatColReference(cellName);

            this.grid[row, col].Text = text;
            this.grid[row, col].BGColor = uint.Parse(colorString);
        }

        /// <summary>
        /// Sets all cells in grid bad to default color and text. (Could have altrenatively creaded new spreadsheet in UI layer and set all datagridview values to 0).
        /// </summary>
        private void ClearSpreadsheet()
        {
            for (int row = 0; row < this.grid.GetLength(0); row++)
            {
                for (int col = 0; col < this.grid.GetLength(1); col++)
                {
                    this.grid[row, col].Text = null;
                    this.grid[row, col].BGColor = 4294967295;
                }
            }
        }

        /// <summary>
        /// Event called after cell text changed.
        /// </summary>
        /// <param name="sender">Sending cell object.</param>
        /// <param name="e">Sending arguments.</param>
        private void CellPropertyChangedEvent(object sender, PropertyChangedEventArgs e)
        {
            Console.WriteLine(((Cell)sender).Text);

            // Unsubscribe from previous variables
            /*if (e.PropertyName == "Text")
            {
                ((Cell)sender).ResetPropertyChangedEvent();
                ((Cell)sender).PropertyChanged += this.CellPropertyChangedEvent;
            }*/

            if ((e.PropertyName == "Text" || e.PropertyName == "TextVar") && ((Cell)sender).Text != null && ((Cell)sender).Text != "=" && ((Cell)sender).Text != string.Empty && ((Cell)sender).Text[0] == '=')
            {
                // What was the point of all this??

                // string rowC = ((Cell)sender).Text.Substring(2);
                // char colC = ((Cell)sender).Text[1];
                // int row = int.Parse(rowC) - 1;
                // int col = colC - 65;

                // ((Cell)sender).SetValue(this.GetCell(row, col).Text);
                ExpressionTree expressionTree = new ExpressionTree(((Cell)sender).Text.Substring(1));

                // Also need to add the corresponding values to every variable...
                List<string> variableNames = expressionTree.GetVariableNames();

                if (e.PropertyName == "Text" && this.CheckForCircularReference(variableNames, (Cell)sender))
                {
                    // Sequentially set all involved to "Circular Reference"
                    ((Cell)sender).SetValue("Circular Reference");
                    this.CellPropertyChanged(sender, new PropertyChangedEventArgs("Value"));

                    // this.AssignCircularReferences(variableNames, (Cell)sender);
                    return;
                }

                /*
                if (!this.visited[((Cell)sender).RowIndex, ((Cell)sender).ColumnIndex])
                {
                    ((Cell)sender).ResetPropertyChangedEvent();
                    ((Cell)sender).PropertyChanged += this.CellPropertyChangedEvent;
                    Console.WriteLine("Flushing all of (" + ((Cell)sender).RowIndex + ", " + ((Cell)sender).ColumnIndex + ")");
                }
                */

                // We can't just clear list now as we lose the other direction
                /*
                if (!this.visited[((Cell)sender).RowIndex, ((Cell)sender).ColumnIndex] && e.PropertyName == "Text")
                {
                    // ((Cell)sender).ResetPropertyChangedEvent();
                    // ((Cell)sender).PropertyChanged += this.CellPropertyChangedEvent;

                    // We need to somehow get rid of the OLD delegates from each old variable
                }
                */

                // Infinite delegate problem... need to do "If it were to be circular" at delegate add, THis is unnecissary as the delegate loop cannot be stopped here
                /*if (this.CheckForCircularReference(variableNames, (Cell)sender))
                {
                    Console.WriteLine("Circular!");
                    if (this.visited[((Cell)sender).RowIndex, ((Cell)sender).ColumnIndex])
                    {
                        ((Cell)sender).SetValue("Circular Reference");
                        this.CellPropertyChanged(sender, new PropertyChangedEventArgs("Value"));
                        return;
                    }
                    else
                    {
                        this.visited[((Cell)sender).RowIndex, ((Cell)sender).ColumnIndex] = true;
                    }
                }
                */

                /*
                 Console.WriteLine("Circular Ref detected");
                    if (this.visited[((Cell)sender).RowIndex, ((Cell)sender).ColumnIndex])
                    {
                        return;
                    }
                    else
                    {
                        ((Cell)sender).SetValue("Circular Reference");
                        Console.WriteLine("Setting to circ");
                        this.CellPropertyChanged(sender, new PropertyChangedEventArgs("Value"));
                        this.visited[((Cell)sender).RowIndex, ((Cell)sender).ColumnIndex] = true;
                    }
                 */

                /*
                if (this.CheckForCircularReference(variableNames, (Cell)sender))
                {
                    // Need to stop infinite updating
                    Console.WriteLine("Circular Ref detected");
                    ((Cell)sender).SetValue("Circular Reference");
                    this.CellPropertyChanged(sender, new PropertyChangedEventArgs("Value"));

                    foreach (string variable in variableNames)
                    {
                        int row = 0, col = 0;
                        AssignRowColumns(ref row, ref col, variable);

                        // Somehow prevent Property changed
                        // "If that variable changes, this cell must too"
                        // When circular "that var changes, then this var changes, then that and so on..."
                        this.grid[row, col].PropertyChanged += ((Cell)sender).ReferencedCellChangedEvent;
                    }

                    Console.WriteLine("Before Return");
                    return;
                }
                */

                try
                {
                    this.AssignVariables(expressionTree, (Cell)sender);
                }
                catch (Exception exception)
                {
                    if (exception is UnassignedVariableException)
                    {
                        // Console.WriteLine("Unassigned Cell Referenced");
                        ((Cell)sender).SetValue("Unref Variable");
                        this.CellPropertyChanged(sender, new PropertyChangedEventArgs("Value"));

                        // variableNames = expressionTree.GetVariableNames();
                        if (e.PropertyName != "TextVar")
                        {
                            this.AddUpdateDelegates((Cell)sender, ((Cell)sender).Text);
                            /*
                            foreach (string variable in variableNames)
                            {
                                int row = 0, col = 0;
                                AssignRowColumns(ref row, ref col, variable);

                                Console.WriteLine("Var at (" + row + ", " + col + ") subscribing");
                                this.grid[row, col].PropertyChanged += ((Cell)sender).ReferencedCellChangedEvent;
                            }
                            */
                        }

                        return;
                    }
                    else if (exception is InvalidVariableRowFormatException || exception is CellOutOfRangeException)
                    {
                        ((Cell)sender).SetValue("Bad Reference");
                        this.CellPropertyChanged(sender, new PropertyChangedEventArgs("Value"));

                        // No need to subscribe to variables as formula MUST be changed before valid (which will subscribe then)
                        return;
                    }
                    else if (exception is CellSelfReferenceException)
                    {
                        ((Cell)sender).SetValue("Self Reference");
                        this.CellPropertyChanged(sender, new PropertyChangedEventArgs("Value"));
                        return;
                    }

                    throw;
                }

                // Console.WriteLine("Val set");
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
                // List<string> variableNames = expressionTree.GetVariableNames();
                if (e.PropertyName != "TextVar")
                {
                    this.AddUpdateDelegates((Cell)sender, ((Cell)sender).Text);
                    /*
                    foreach (string variable in variableNames)
                    {
                        int row = 0, col = 0;
                        AssignRowColumns(ref row, ref col, variable);

                        Console.WriteLine("Var at (" + row + ", " + col + ") subscribing");
                        this.grid[row, col].PropertyChanged += ((Cell)sender).ReferencedCellChangedEvent;
                    }
                    */
                }

                // Also update all cells effected...
            }

            if (e.PropertyName == "BGColor")
            {
                this.CellPropertyChanged(sender, new PropertyChangedEventArgs("BGColor"));
            }
            else
            {
                this.CellPropertyChanged(sender, new PropertyChangedEventArgs("Value"));
            }
        }

        /// <summary>
        /// Adds variables to the expression tree dictionary based on cells in the datagridview.
        /// </summary>
        /// <param name="expressionTree">Expresion tree to add variables to.</param>
        private void AssignVariables(ExpressionTree expressionTree, Cell sender)
        {
            List<string> variableNames = expressionTree.GetVariableNames();
            foreach (string variable in variableNames)
            {
                int row = 0, col = 0;
                AssignRowColumns(ref row, ref col, variable);

                double variableValue;

                // Consider the double value of a cell to be:
                // Numerical value parsed if double.TryParse on the value string succeeds

                // Currently is catching all cases in which cell at row, col's value cannot be converted to a double (should only be when value = null)
                // Need to check in range here as well
                // Hypothetically, if someone copied in a hyphen instead of a dash for a negative, would need to cover that as well...
                // Alternatively could have done a try on this.gird[row, col] and caught the indexoutofbounds or just use that exception to catch in PropertyChange

                // For now, will do error case for first encountered in order of variables

                // Takes in sender now to avoid need for separate HasVariable search loop function for identifying self reference
                if (sender.RowIndex == row && sender.ColumnIndex == col)
                {
                    throw new CellSelfReferenceException();
                }

                if (row > this.grid.GetLength(0) - 1 || row < 0 || col > this.grid.GetLength(1) - 1 || col < 0)
                {
                    throw new CellOutOfRangeException();
                }

                if (!double.TryParse(this.grid[row, col].Value, out variableValue))
                {
                    // 0 otherwise
                    variableValue = 0;

                    // COMMENT OUT THIS LINE IF YOU WANT DEFAULT VARIABLE VALUE OF 0 INSTEAD OF "Unref Var"
                    throw new UnassignedVariableException();
                }

                expressionTree.SetVariable(variable, variableValue);
            }
        }

        /// <summary>
        /// Checks if the sending cell is referencing a variable that references itself. Excludes simple self references (returns false when encountered).
        /// </summary>
        /// <param name="variableNames">List of variable names to search through.</param>
        /// <param name="sender">Cell to search for within variables.</param>
        /// <returns>If the cell has a circular reference or not.</returns>
        private bool CheckForCircularReference(List<string> variableNames, Cell sender)
        {
            bool result = false;
            string cellName = ConvertToCellName(sender.RowIndex, sender.ColumnIndex);

            // Need to NOT check for just a self reference
            foreach (string variable in variableNames)
            {
                if (cellName == variable)
                {
                    return false;
                }
            }

            foreach (string variable in variableNames)
            {
                if (this.ExploreForVariable(variable, cellName))
                {
                    result = true;
                    break;
                }
            }

            return result;
        }

        /// <summary>
        /// Recursive function to find if a variable can be found within another.
        /// </summary>
        /// <param name="variableName">Variable to search through.</param>
        /// <param name="searchName">Variable to search for.</param>
        /// <returns>If the variable was found or not.</returns>
        private bool ExploreForVariable(string variableName, string searchName)
        {
            int row = 0;
            int col = 0;
            try
            {
                AssignRowColumns(ref row, ref col, variableName);
            }
            catch (InvalidVariableRowFormatException)
            {
                return false;
            }

            if (row > this.grid.GetLength(0) - 1 || row < 0 || col > this.grid.GetLength(1) - 1 || col < 0 || this.grid[row, col].Text == null || this.grid[row, col].Text[0] != '=')
            {
                return false;
            }
            else
            {
                ExpressionTree variableTree = new ExpressionTree(this.grid[row, col].Text.Substring(1));

                foreach (string variable in variableTree.GetVariableNames())
                {
                    if (variable == searchName)
                    {
                        return true;
                    }

                    // Case of another cell referencing a self referencial cell (Changes all to a circular case)
                    if (variable == variableName)
                    {
                        return true;
                    }

                    // Missing case: Another cell references a circular loop, it never reaches itself again or have a self reference but does get stuck
                    if (this.CheckForCircularReference(variableTree.GetVariableNames(), this.grid[row, col]))
                    {
                        return true;
                    }

                    if (this.ExploreForVariable(variable, searchName))
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        /// <summary>
        /// Converts coordinates to the format of cell names in the sheet. NOTE: Non-Static version for testing (reason in test file).
        /// </summary>
        /// <param name="row">Row of cell name to create.</param>
        /// <param name="col">Column of cell name to create.</param>
        /// <returns>String of the cell name specified for.</returns>
        private string InstanceConvertToCellName(int row, int col)
        {
            // Could also just
            // return ConvertToCellName(row, col);
            string rowString = (row + 1).ToString();
            int colAscii = col + 65;

            return (char)colAscii + rowString;
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
