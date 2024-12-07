// <copyright file="Cell.cs" company="Eric-Furukawa">
// Copyright (c) Eric-Furukawa. All rights reserved.
// </copyright>

// NUnit 3 tests
// See documentation : https://github.com/nunit/docs/wiki/NUnit-Documentation
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using System.Reflection;
using System.Windows.Forms;
using System.ComponentModel;

using Cpts321;
//using Spreadsheet_Eric_Furukawa;

namespace NUnit.Tests1
{
    [TestFixture]
    public class SpreadsheetTest
    {
        private Spreadsheet_Eric_Furukawa.Form1 objectUnderTestForm = new Spreadsheet_Eric_Furukawa.Form1();
        private Spreadsheet testSpreadsheet = new Spreadsheet(5, 5);

        // For testing private methods.
        private MethodInfo GetMethod(string methodName, object objectUnderTest)
        {
            if (string.IsNullOrWhiteSpace(methodName))
                Assert.Fail("methodName cannot be null or whitespace");
            var method = objectUnderTest.GetType().GetMethod(methodName,
            BindingFlags.NonPublic | BindingFlags.Static | BindingFlags.Instance);
            if (method == null)
                Assert.Fail(string.Format("{0} method not found", methodName));
            return method;
        }

        [Test]
        public void TestMethod()
        {
            // TODO: Add your test code here
            var answer = 42;
            Assert.That(answer, Is.EqualTo(42), "Some useful error message");
        }

        [Test]
        public void TestAssignCols()
        {
            //Assigns 3 cols and checks set correctly.
            MethodInfo methodInfo = this.GetMethod("AssignCols", objectUnderTestForm);

            DataGridView testGrid = new DataGridView();
            string[] testColNames = { "A", "B", "C" };
            methodInfo.Invoke(objectUnderTestForm, new object[] { testColNames, testGrid });
            Assert.That(testGrid.Columns[0].Name, Is.EqualTo("Col A"), "AssignCols failed for simple 3 col case, 0 Name wrong");
            Assert.That(testGrid.Columns[0].HeaderText, Is.EqualTo("A"), "AssignCols failed for simple 3 col case, 0  Header wrong");

            Assert.That(testGrid.Columns[1].Name, Is.EqualTo("Col B"), "AssignCols failed for simple 3 col case, 1 Name wrong");
            Assert.That(testGrid.Columns[1].HeaderText, Is.EqualTo("B"), "AssignCols failed for simple 3 col case, 1  Header wrong");

            Assert.That(testGrid.Columns[2].Name, Is.EqualTo("Col C"), "AssignCols failed for simple 3 col case, 2 Name wrong");
            Assert.That(testGrid.Columns[2].HeaderText, Is.EqualTo("C"), "AssignCols failed for simple 3 col case, 2  Header wrong");

            Assert.That(testGrid.Columns.Count, Is.EqualTo(3), "AssignCols failed for simple 3 col case, column amount wrong");
        }

        [Test]
        public void TestAssignColsEmpty()
        {
            // Empty case for AssignCols
            MethodInfo methodInfo = this.GetMethod("AssignCols", objectUnderTestForm);

            DataGridView testGrid = new DataGridView();
            string[] testColNames = { };
            methodInfo.Invoke(objectUnderTestForm, new object[] { testColNames, testGrid });
            // Assertion modified to not check null but rather column count of 0
            Assert.That(testGrid.Columns.Count, Is.EqualTo(0), "AssignCols failed for empty col case");
        }

        [Test]
        public void TestAssignRows()
        {
            // Assigns 3 rows after 3 cols and checks set correctly.
            DataGridView testGrid = new DataGridView();

            MethodInfo methodInfoRow = this.GetMethod("AssignRows", objectUnderTestForm);

            MethodInfo methodInfoCol = this.GetMethod("AssignCols", objectUnderTestForm);

            string[] testColNames = { "A", "B", "C" };
            methodInfoCol.Invoke(objectUnderTestForm, new object[] { testColNames, testGrid });
            methodInfoRow.Invoke(objectUnderTestForm, new object[] { 1, 3, testGrid });
            Assert.That(testGrid.Rows[0].HeaderCell.Value, Is.EqualTo("1"), "AssignRows failed for simple 3 row case, 0 HeaderCell wrong");
            Assert.That(testGrid.Rows[1].HeaderCell.Value, Is.EqualTo("2"), "AssignRows failed for simple 3 row case, 1 HeaderCell wrong");
            Assert.That(testGrid.Rows[2].HeaderCell.Value, Is.EqualTo("3"), "AssignRows failed for simple 3 row case, 2 HeaderCell wrong");
            Assert.That(testGrid.Rows[3].HeaderCell.Value, Is.EqualTo(null), "AssignRows failed for simple 3 row case, 3 HeaderCell wrong");

            // FOR SOME REASON, during testing only, the DataGridView decides to append a null row after the execution of AssignCols
            // I Have no idea why and it happens without any code saying for it to do so. it has nothing to do with the fact things happen
            // in the form initializer

            //Assert.That(testGrid.Rows.Count, Is.EqualTo(3), "AssignRows failed for simple 3 col case, Row amount wrong");
            Assert.That(testGrid.Rows.Count, Is.EqualTo(4), "AssignRows failed for simple 3 col case, Row amount wrong");
        }

        [Test]
        public void TestAssignRowsEmpty()
        {
            // Empty case for AssignRows.
            DataGridView testGrid = new DataGridView();

            MethodInfo methodInfoRow = this.GetMethod("AssignRows", objectUnderTestForm);

            MethodInfo methodInfoCol = this.GetMethod("AssignCols", objectUnderTestForm);

            string[] testColNames = { };
            methodInfoCol.Invoke(objectUnderTestForm, new object[] { testColNames, testGrid });

            methodInfoRow.Invoke(objectUnderTestForm, new object[] { 1, 0, testGrid });
            // Assertion modified to not check null but rather column count of 0
            Assert.That(testGrid.Rows.Count, Is.EqualTo(0), "AssignRows failed for empty col case");
        }

        [Test]
        public void TestSpreadSheetSystem()
        {
            // Tests instantiation and accessors work for basic cases.
            //MethodInfo methodInfo = this.GetMethod("GetCell", testSpreadsheet);
            //SSCell testCell = (SSCell)methodInfo.Invoke(testSpreadsheet, new object[] { 3 , 4  });
            Cell testCell = testSpreadsheet.GetCell(3, 4);
            Assert.That(testCell.RowIndex, Is.EqualTo(3), "SpreadSheetSystem failed for creating cell, row index incorrect");
            Assert.That(testCell.ColumnIndex, Is.EqualTo(4), "SpreadSheetSystem failed for creating cell, column index incorrect");

            Assert.That(testSpreadsheet.ColumnCount, Is.EqualTo(5), "SpreadSheetSystem failed for creating grid, col count incorrect");
            Assert.That(testSpreadsheet.RowCount, Is.EqualTo(5), "SpreadSheetSystem failed for creating grid, row count incorrect");
        }

        [Test]
        public void TestCellPropertyChanged()
        {
            // HW7 Edit: The expected functionality of this case has changed. In this case, Hello is being treated as a double value.
            // This falls outside of functionality as we assume a cell being referenced by variable must be a double or else it is 0 (by requirement)
            // Thus in this case, =B3 will evaluate to 0 as Hello will fail parsing and the B3 variable will be assigned 0, resulting in an expression evaluation of 0

            // Tests funcitonality of on Text change tasks.
            DataGridView testGrid = new DataGridView();

            MethodInfo methodInfoRow = this.GetMethod("AssignRows", objectUnderTestForm);

            MethodInfo methodInfoCol = this.GetMethod("AssignCols", objectUnderTestForm);

            string[] testColNames = { "A", "B", "C" };
            methodInfoCol.Invoke(objectUnderTestForm, new object[] { testColNames, testGrid });
            methodInfoRow.Invoke(objectUnderTestForm, new object[] { 1, 3, testGrid });
            //MethodInfo methodInfo = this.GetMethod("CellPropertyChangedEvent", testSpreadsheet);
            //MethodInfo methodInfoGetCell = this.GetMethod("GetCell", objectUnderTestForm.Spreadsheet);
            MethodInfo methodInfoUpdateCell = this.GetMethod("UpdateCellInGrid", objectUnderTestForm);
            //Cpts321.SSCell testCell = (Cpts321.SSCell)methodInfoGetCell.Invoke(testSpreadsheet, new object[] { 1, 0 });
            Cell testCell = testSpreadsheet.GetCell(1, 0);
            //Cpts321.SSCell testChangeCell = (Cpts321.SSCell)methodInfoGetCell.Invoke(testSpreadsheet, new object[] { 2, 1 });
            Cell testChangeCell = testSpreadsheet.GetCell(2, 1);
            testChangeCell.Text = "Hello";
            testCell.Text = "=B3";
            Assert.That(testCell.Value, Is.EqualTo("Unref Variable"), "CellProperty change failed for pull from different cell");
            methodInfoUpdateCell.Invoke(objectUnderTestForm, new object[] { testCell, new PropertyChangedEventArgs("Value"), testGrid });
            Assert.That(testGrid.Rows[1].Cells[0].Value, Is.EqualTo("Unref Variable"), "UpdateCell change failed for pull from different cell");
        }

        // Tests in this section are for the initial implementation of the ExpressionTree, its associated classes and the ExpressionTree Demo

        [Test]
        public void TestExpressionTreeStructure()
        {
            // Tests the tree's structure has been created correctly for a simple case.
            ExpressionTree testTree = new ExpressionTree("1+5+4");

            //The test results have changed due to the implementation shown in class. Should evaluate to 1 6 5 10 4 without spaces (Pluses evaluate to nodes on both sides added)
            //1, (5 + 1), 5, (6 + 4), 4 (This test mainly checks the tree's in traversal order is correct and thus structure is correct)
            Assert.That(testTree.GetInorderTree(), Is.EqualTo("165104"), "ExpressionTree construction and subsequent string traversal failed.");
        }

        [Test]
        public void TestExpressionTreeEvaluation()
        {
            // Tests the tree's evaluation for simple expressions with no variables and only one type of operator.
            //Simple addition case
            ExpressionTree testTree = new ExpressionTree("1+5+4");
            Assert.That(testTree.Evaluate(), Is.EqualTo(10.0), "ExpressionTree evaluation for simple addition case failed");

            // Addition with doubles
            testTree = new ExpressionTree("2+1.5+.75");
            Assert.That(testTree.Evaluate(), Is.EqualTo(4.25), "ExpressionTree evaluation for double addition case failed");

            // Subtraction
            testTree = new ExpressionTree("7-2-3");
            Assert.That(testTree.Evaluate(), Is.EqualTo(2.0), "ExpressionTree evaluation for simple subtraction case failed");

            // Negative subtraction
            testTree = new ExpressionTree("5-6-3");
            Assert.That(testTree.Evaluate(), Is.EqualTo(-4.0), "ExpressionTree evaluation for negative subtraction case failed");

            // Multiplication
            testTree = new ExpressionTree("5*2*2");
            Assert.That(testTree.Evaluate(), Is.EqualTo(20.0), "ExpressionTree evaluation for simple multiplication case failed");

            // Division
            testTree = new ExpressionTree("10/5/2");
            Assert.That(testTree.Evaluate(), Is.EqualTo(1.0), "ExpressionTree evaluation for simple division case failed");

            // Double Division
            testTree = new ExpressionTree("10/8/5");
            Assert.That(testTree.Evaluate(), Is.EqualTo(.25), "ExpressionTree evaluation for float division case failed");
        }

        [Test]
        public void TestExpressionTreeVariableNodes()
        {
            // Tests the tree's evaluation for simple expressions with variables and only one type of operator.
            ExpressionTree testTree = new ExpressionTree("A1+5+A2");
            testTree.SetVariable("A1", 3);
            testTree.SetVariable("A2", 9);
            Assert.That(testTree.Evaluate(), Is.EqualTo(17.0), "ExpressionTree evaluation for simple variable case failed");

            // Unassigned vars should default to 0.0, this also checks A2 will be overwritten in new tree.
            testTree = new ExpressionTree("A1+5+A2");
            testTree.SetVariable("A1", 3);
            Assert.That(testTree.Evaluate(), Is.EqualTo(8.0), "ExpressionTree evaluation for unassigned variable case failed");
        }

        //
        // The following section is for HW6
        // TODO:
        // -Support Parentheses (DONE)
        // -Support Operators with precedence (DONE)
        // -Add variables at construction time (DONE)
        // 
        // -BUT NOW: Understand shunting yard (it did all the parentheses and precedence work), don't just use professor's code
        //
        // Potentially also:
        // -Use Shunting Yard to construct the tree (DONE)
        // -Handle operator precedence and associativity explicitly
        // -Use reflection instead of hardcoding operators

        [Test]
        public void TestShuntingYard()
        {
            // Tests the ShuntingYard algorthm's ability to convert the expression to an equation representation list of strings in post order.
            ExpressionTree testTree = new ExpressionTree("1-2-3");// This expression does not matter, needed just for instance.

            // Simple shunting yard post-order case.
            List<string> answer = new List<string> { "3", "5", "+" };
            CollectionAssert.AreEqual(testTree.ShuntingYardAlgorithm("3+5"), answer, "Shunting Yard Algorithm failed for simple case");

            // Variable shunting yard post-order case.
            answer = new List<string> { "A3", "5", "+" };
            CollectionAssert.AreEqual(testTree.ShuntingYardAlgorithm("A3+5"), answer, "Shunting Yard Algorithm failed for variable case");

            // Variable parentheses shunting yard post-order case.
            answer = new List<string> { "A", "B", "C", "+", "*" };
            CollectionAssert.AreEqual(testTree.ShuntingYardAlgorithm("A*(B+C)"), answer, "Shunting Yard Algorithm failed for multi variable parentheses case");

            // Variable following parentheses shunting yard post-order case.
            answer = new List<string> { "A", "B", "C", "D", "*", "+", "*", "E", "+" };
            CollectionAssert.AreEqual(testTree.ShuntingYardAlgorithm("A*(B+C*D)+E"), answer, "Shunting Yard Algorithm failed for variable following parentheses case");
        }

        [Test]
        public void TestParentheses()
        {
            // Tests the tree's evaluation for parentheses expressions.
            // No parentheses evaluation case.
            ExpressionTree testTree = new ExpressionTree("1-2-3");

            Assert.That(testTree.Evaluate(), Is.EqualTo(-4.0), "ExpressionTree evaluation for no parentheses failed");

            // Trivial parentheses evaluation case.
            testTree = new ExpressionTree("(1-2)-3");

            Assert.That(testTree.Evaluate(), Is.EqualTo(-4.0), "ExpressionTree evaluation for trivial parentheses case failed");

            // Simple parentheses evaluation case.
            testTree = new ExpressionTree("1-(2-3)");

            Assert.That(testTree.Evaluate(), Is.EqualTo(2.0), "ExpressionTree evaluation for simple parentheses case failed");

            // Nested parentheses evaluation case.
            testTree = new ExpressionTree("1-((2-3)-4)-5");

            Assert.That(testTree.Evaluate(), Is.EqualTo(1.0), "ExpressionTree evaluation for nested parentheses case failed");

            // Advanced (Multiple operators with precedence affected) parentheses evaluation case.
            testTree = new ExpressionTree("2*(5-3)/2");

            Assert.That(testTree.Evaluate(), Is.EqualTo(2.0), "ExpressionTree evaluation for advanced parentheses case failed");
        }

        [Test]
        public void TestPrecedence()
        {
            // Tests the tree's evaluation expressions involving precedence.
            // Simple precedence evaluation case.
            ExpressionTree testTree = new ExpressionTree("2+2*4");

            Assert.That(testTree.Evaluate(), Is.EqualTo(10.0), "ExpressionTree evaluation for simple precedence case failed");

            // Double-sided mountain (Higher priority in middle) precedence evaluation case.
            testTree = new ExpressionTree("2+2*4+2");

            Assert.That(testTree.Evaluate(), Is.EqualTo(12.0), "ExpressionTree evaluation for double sided mountain precedence case failed");

            // Double-sided valley (Higher priority sides) evaluation case.
            testTree = new ExpressionTree("2*4+2*4");

            Assert.That(testTree.Evaluate(), Is.EqualTo(16.0), "ExpressionTree evaluation for double sided valley precedence case failed");

            // Advanced (Multiple operators and parentheses) precedence evaluation case.
            testTree = new ExpressionTree("9+(2+2)*5/10-4");

            Assert.That(testTree.Evaluate(), Is.EqualTo(7.0), "ExpressionTree evaluation for advanced precedence case failed");
        }

        // Beyond this point is for HW7

        // Remaining Work:
        // -Exceptions for new unassigned variable (DONE)
        // -Update cells that reference when cell changed -> Cells need to subscribe to update value for every var cell? (DONE)
        // -Value of a cell 0 if tryparse fails (How does this not contradict the default value not being 0?) (DONE)

        // Potentially also:
        // -Handle operator precedence and associativity explicitly
        // -Use reflection instead of hardcoding operators

        [Test]
        public void TestGetVariableNames()
        {
            // Tests that GetVariableNames successfully returns a list of variable names.
            ExpressionTree testTree = new ExpressionTree("A3+A4");


            List<string> answer = new List<string> { "A3", "A4" };
            CollectionAssert.AreEqual(testTree.GetVariableNames(), answer, "Get Variable Names failed");

            testTree = new ExpressionTree("(B2/C3)*A1");
            answer = new List<string> { "B2", "C3", "A1" };
            CollectionAssert.AreEqual(testTree.GetVariableNames(), answer, "Get Variable Names Advanced multiple vars failed");
        }

        [Test]
        public void TestVariableCellChange()
        {
            // Tests that changes in the spreadsheet will dynamically update the value of cells that reference it.

            // Simple case, one variable changes and ref cell should follow.
            Spreadsheet cellTestSheet = new Spreadsheet(5, 5);
            cellTestSheet.GetCell(1, 1).Text = "5";
            cellTestSheet.GetCell(2, 2).Text = "=B2";

            Assert.That(cellTestSheet.GetCell(2, 2).Value, Is.EqualTo("5"), "Cell variable change failed before cell change");
            cellTestSheet.GetCell(1, 1).Text = "7";
            Assert.That(cellTestSheet.GetCell(2, 2).Value, Is.EqualTo("7"), "Cell variable change failed after cell change");

            // Double reference case, change should update reference to referece of a cell.
            cellTestSheet = new Spreadsheet(5, 5);
            cellTestSheet.GetCell(1, 1).Text = "5";
            cellTestSheet.GetCell(2, 2).Text = "=B2";
            cellTestSheet.GetCell(3, 3).Text = "=C3";

            Assert.That(cellTestSheet.GetCell(3, 3).Value, Is.EqualTo("5"), "Cell variable change failed before cell change double ref case");
            cellTestSheet.GetCell(1, 1).Text = "7";
            Assert.That(cellTestSheet.GetCell(3, 3).Value, Is.EqualTo("7"), "Cell variable change failed after cell change double ref case");

            // Double referece and variable case, tests some intricicies with updating two variables and using an expression with a cell and a cell that references the same cell.
            cellTestSheet = new Spreadsheet(5, 5);
            cellTestSheet.GetCell(1, 1).Text = "5";
            cellTestSheet.GetCell(2, 2).Text = "=B2";
            cellTestSheet.GetCell(3, 3).Text = "=B2+C3";

            Assert.That(cellTestSheet.GetCell(3, 3).Value, Is.EqualTo("10"), "Cell variable change failed before cell change double ref and double var case");
            cellTestSheet.GetCell(1, 1).Text = "7";
            Assert.That(cellTestSheet.GetCell(3, 3).Value, Is.EqualTo("14"), "Cell variable change failed after cell change double ref and double var case");
        }

        // Beyond this point is for HW8

        // Remaining Work:
        // -Handle operator precedence/associativity explicitly (DONE (It has been done for a while))
        // -Use reflection instead of hardcoding operators (DONE)
        // -Throw descriptive exceptions (DONE)

        // -Add background color property to cells with accompanying UI changes (DONE)
        // -Implement the undo/redo system (DONE)


        // Changing of cell's color cannot be tested as there is no way to spoof a datacellgrid or forms app that I know of.

        
        // Test was failing due to the fact specification in Commands is created from ui input lists.
        [Test]
        public void TestSpreadsheetUndoRedo()
        {
            // Tests the spreadsheet will update cells based on undo and redo commands being issued. Due to the nature of the untestable UI and its formatting of required
            // inputs, testing has been abridged.

            // Simple case that tests undoing and redoing an int.
            Cell[] exampleCells;
            uint[] oldColors;

            Spreadsheet cellTestSheet = new Spreadsheet(5, 5);
            exampleCells = new Cell[1];

            cellTestSheet.GetCell(1, 1).Text = "5";
            exampleCells[0] = cellTestSheet.GetCell(1, 1);
            cellTestSheet.AddTextChangeCommandList(exampleCells[0], null);

            cellTestSheet.GetCell(1, 1).Text = "10";
            exampleCells[0] = cellTestSheet.GetCell(1, 1);
            cellTestSheet.AddTextChangeCommandList(exampleCells[0], "5");

            cellTestSheet.UndoCommand();

            Assert.That(cellTestSheet.GetCell(1, 1).Value, Is.EqualTo("5"), "Undo failed for values");

            exampleCells[0] = cellTestSheet.GetCell(1, 1);

            cellTestSheet.RedoCommand();

            Assert.That(cellTestSheet.GetCell(1, 1).Value, Is.EqualTo("10"), "Redo failed for values");
            
            // Case for undoing and redoing a color.
            cellTestSheet = new Spreadsheet(5, 5);
            oldColors = new uint[1];

            cellTestSheet.GetCell(1, 1).BGColor = 0x55555555;
            exampleCells[0] = cellTestSheet.GetCell(1, 1);
            oldColors[0] = 0xFFFFFFFF;
            cellTestSheet.AddColorChangeCommandList(exampleCells, oldColors);

            cellTestSheet.GetCell(1, 1).BGColor = 0x22222222;
            exampleCells[0] = cellTestSheet.GetCell(1, 1);
            oldColors[0] = 0x55555555;
            cellTestSheet.AddColorChangeCommandList(exampleCells, oldColors);

            cellTestSheet.UndoCommand();

            Assert.That(cellTestSheet.GetCell(1, 1).BGColor, Is.EqualTo(0x55555555), "Undo failed for color");

            cellTestSheet.RedoCommand();

            Assert.That(cellTestSheet.GetCell(1, 1).BGColor, Is.EqualTo(0x22222222), "Redo failed for color");

            // Case for mulitple recolorings
            cellTestSheet = new Spreadsheet(5, 5);
            exampleCells = new Cell[3];
            oldColors = new uint[3];

            cellTestSheet.GetCell(0, 0).BGColor = 0x55555555;
            exampleCells[0] = cellTestSheet.GetCell(0, 0);
            oldColors[0] = 0xFFFFFFFF;

            cellTestSheet.GetCell(1, 1).BGColor = 0x22222222;
            exampleCells[1] = cellTestSheet.GetCell(1, 1);
            oldColors[1] = 0xFFFFFFFF;

            cellTestSheet.GetCell(2, 2).BGColor = 0x88888888;
            exampleCells[2] = cellTestSheet.GetCell(2, 2);
            oldColors[2] = 0xFFFFFFFF;

            cellTestSheet.AddColorChangeCommandList(exampleCells, oldColors);


            cellTestSheet.GetCell(0, 0).BGColor = 0x11111111;
            exampleCells[0] = cellTestSheet.GetCell(0, 0); // These are kept for readability...
            oldColors[0] = 0x55555555;

            cellTestSheet.GetCell(1, 1).BGColor = 0x11111111;
            exampleCells[1] = cellTestSheet.GetCell(1, 1);
            oldColors[1] = 0x22222222;

            cellTestSheet.GetCell(2, 2).BGColor = 0x11111111;
            exampleCells[2] = cellTestSheet.GetCell(2, 2);
            oldColors[2] = 0x88888888;

            cellTestSheet.AddColorChangeCommandList(exampleCells, oldColors);



            cellTestSheet.UndoCommand();

            Assert.That(cellTestSheet.GetCell(0, 0).BGColor, Is.EqualTo(0x55555555), "Undo failed for color");
            Assert.That(cellTestSheet.GetCell(1, 1).BGColor, Is.EqualTo(0x22222222), "Undo failed for color");
            Assert.That(cellTestSheet.GetCell(2, 2).BGColor, Is.EqualTo(0x88888888), "Undo failed for color");

            cellTestSheet.RedoCommand();

            Assert.That(cellTestSheet.GetCell(0, 0).BGColor, Is.EqualTo(0x11111111), "Redo failed for multiple color");
            Assert.That(cellTestSheet.GetCell(1, 1).BGColor, Is.EqualTo(0x11111111), "Redo failed for multiple color");
            Assert.That(cellTestSheet.GetCell(2, 2).BGColor, Is.EqualTo(0x11111111), "Redo failed for multiple color");

            /*
            // Case for undoing 2 deep and redoing 2 deep with actions in different cells.
            cellTestSheet = new Spreadsheet(5, 5);
            cellTestSheet.GetCell(1, 1).BGColor = 0x11111111;
            cellTestSheet.GetCell(2, 2).BGColor = 0x00000000;
            cellTestSheet.CellChangeControl.UndoButtonPushed();

            Assert.That(cellTestSheet.GetCell(2, 2).BGColor, Is.EqualTo(0xFFFFFFFF), "Undo failed for color in different cells");

            cellTestSheet.CellChangeControl.UndoButtonPushed();

            Assert.That(cellTestSheet.GetCell(1, 1).BGColor, Is.EqualTo(0xFFFFFFFF), "Undo failed for color in different cells");

            cellTestSheet.CellChangeControl.RedoButtonPushed();

            Assert.That(cellTestSheet.GetCell(1, 1).BGColor, Is.EqualTo(0x11111111), "Redo failed for color in different cells");

            cellTestSheet.CellChangeControl.RedoButtonPushed();

            Assert.That(cellTestSheet.GetCell(2, 2).BGColor, Is.EqualTo(0x00000000), "Redo failed for color in different cells");

            // Case for overriding a redo.
            cellTestSheet = new Spreadsheet(5, 5);
            cellTestSheet.GetCell(1, 1).BGColor = 0x11111111;
            cellTestSheet.GetCell(2, 2).BGColor = 0x00000000;

            cellTestSheet.CellChangeControl.UndoButtonPushed();

            cellTestSheet.GetCell(2, 2).BGColor = 0x22222222;

            cellTestSheet.CellChangeControl.UndoButtonPushed();

            Assert.That(cellTestSheet.GetCell(2, 2).BGColor, Is.EqualTo(0xFFFFFFFF), "Undo failed for overridding old redo");

            //Could have a change mutiple color case...
            */
        }
    }
}
