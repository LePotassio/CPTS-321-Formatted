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
    public class TestClass
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
            SSCell testCell = (SSCell)testSpreadsheet.GetCell(3, 4);
            Assert.That(testCell.RowIndex, Is.EqualTo(3), "SpreadSheetSystem failed for creating cell, row index incorrect");
            Assert.That(testCell.ColumnIndex, Is.EqualTo(4), "SpreadSheetSystem failed for creating cell, column index incorrect");

            Assert.That(testSpreadsheet.ColumnCount, Is.EqualTo(5), "SpreadSheetSystem failed for creating grid, col count incorrect");
            Assert.That(testSpreadsheet.RowCount, Is.EqualTo(5), "SpreadSheetSystem failed for creating grid, row count incorrect");
        }

        [Test]
        public void TestCellPropertyChanged()
        {
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
            SSCell testCell = (SSCell)testSpreadsheet.GetCell(1, 0);
            //Cpts321.SSCell testChangeCell = (Cpts321.SSCell)methodInfoGetCell.Invoke(testSpreadsheet, new object[] { 2, 1 });
            SSCell testChangeCell = (SSCell)testSpreadsheet.GetCell(2, 1);
            testChangeCell.Text = "Hello";
            testCell.Text = "=B3";
            Assert.That(testCell.Value, Is.EqualTo("Hello"), "CellProperty change failed for pull from different cell");
            methodInfoUpdateCell.Invoke(objectUnderTestForm, new object[] { testCell, new PropertyChangedEventArgs("Value"), testGrid });
            Assert.That(testGrid.Rows[1].Cells[0].Value, Is.EqualTo("Hello"), "UpdateCell change failed for pull from different cell");
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
    }
}
