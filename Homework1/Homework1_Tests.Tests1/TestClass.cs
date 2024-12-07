// NUnit 3 tests
// See documentation : https://github.com/nunit/docs/wiki/NUnit-Documentation
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;

namespace Homework1_Tests.Tests1
{
    [TestFixture]
    public class TestClass
    {
        [Test]
        public void TestMethod()
        {
            // TODO: Add your test code here
            var answer = 42;
            Assert.That(answer, Is.EqualTo(42), "Some useful error message");
        }

        [Test]
        public void TestAddByValue1()
        {
            Homework1.BST tree = new Homework1.BST();
            tree.AddByValue(5);
            Assert.That(tree.Root.Data, Is.EqualTo(5), "Single node BST data incorrect");
        }

        [Test]
        public void TestAddByValue2()
        {
            Homework1.BST tree = new Homework1.BST();
            tree.AddByValue(5);
            tree.AddByValue(2);
            Assert.That(tree.Root.Left.Data, Is.EqualTo(2), "Left node BST data incorrect");
        }

        [Test]
        public void TestAddByValue3()
        {
            Homework1.BST tree = new Homework1.BST();
            tree.AddByValue(5);
            tree.AddByValue(8);
            Assert.That(tree.Root.Right.Data, Is.EqualTo(8), "Right node BST data incorrect");
        }

        [Test]
        public void TestAddByValue4()
        {
            Homework1.BST tree = new Homework1.BST();
            tree.AddByValue(5);
            tree.AddByValue(2);
            tree.AddByValue(8);
            tree.AddByValue(7);
            Assert.That(tree.Root.Right.Left.Data, Is.EqualTo(7), "Level > 2 BST data incorrect");
        }

        [Test]
        public void TestPopulateTree()
        {
            Homework1.BST tree = new Homework1.BST();
            string testString = "5 2 8 7";
            Homework1.ProjectMain.PopulateTree(tree, testString);
            Assert.That(tree.Root.Right.Left.Data, Is.EqualTo(7), "Populate tree BST data incorrect");
        }

        [Test]
        public void TestTraverseInOrderBase1()
        {
            Homework1.BST tree = new Homework1.BST();
            string testString = "5 2 8 7";
            Homework1.ProjectMain.PopulateTree(tree, testString);
            string output = tree.TraverseInOrderBase();
            Assert.That(output, Is.EqualTo("2 5 7 8 "), "In order traversal balanced case incorrect");
        }

        [Test]
        public void TestTraverseInOrderBase2()
        {
            Homework1.BST tree = new Homework1.BST();
            string testString = "1 2 3 4";
            Homework1.ProjectMain.PopulateTree(tree, testString);
            string output = tree.TraverseInOrderBase();
            Assert.That(output, Is.EqualTo("1 2 3 4 "), "In order traversal unbalanced case incorrect");
        }

        [Test]
        public void TestTraverseInOrderBase3()
        {
            Homework1.BST tree = new Homework1.BST();
            string testString = string.Empty;
            Homework1.ProjectMain.PopulateTree(tree, testString);
            string output = tree.TraverseInOrderBase();
            Assert.That(output, Is.EqualTo(""), "In order traversal empty case incorrect");
        }

        [Test]
        public void TestCountNodes1()
        {
            Homework1.BST tree = new Homework1.BST();
            string testString = "5 2 8 7";
            Homework1.ProjectMain.PopulateTree(tree, testString);
            int nodeCount = tree.CountNodesBase();
            Assert.That(nodeCount, Is.EqualTo(4), "Node count of 4 node tree incorrect");
        }

        [Test]
        public void TestCountNodes2()
        {
            Homework1.BST tree = new Homework1.BST();
            string testString = "";
            Homework1.ProjectMain.PopulateTree(tree, testString);
            int nodeCount = tree.CountNodesBase();
            Assert.That(nodeCount, Is.EqualTo(0), "Node count of 0 node tree incorrect");
        }

        [Test]
        public void TestGetTreeLevel1()
        {
            Homework1.BST tree = new Homework1.BST();
            string testString = "5 2 8 7";
            Homework1.ProjectMain.PopulateTree(tree, testString);
            int level = tree.GetTreeLevelBase();
            Assert.That(level, Is.EqualTo(3), "Balanced tree level incorrect");
        }

        [Test]
        public void TestGetTreeLevel2()
        {
            Homework1.BST tree = new Homework1.BST();
            string testString = "2 5 7 8";
            Homework1.ProjectMain.PopulateTree(tree, testString);
            int level = tree.GetTreeLevelBase();
            Assert.That(level, Is.EqualTo(4), "\"Skewed\" unbalanced tree level incorrect");
        }

        [Test]
        public void TestGetTreeLevel3()
        {
            Homework1.BST tree = new Homework1.BST();
            string testString = "";
            Homework1.ProjectMain.PopulateTree(tree, testString);
            int level = tree.GetTreeLevelBase();
            Assert.That(level, Is.EqualTo(0), "Empty tree level incorrect");
        }

        [Test]
        public void TestCalculateMinLevels1()
        {
            Assert.That(Homework1.BST.CalculateMinLevels(5), Is.EqualTo(3), "Min level basic case incorrect");
        }

        [Test]
        public void TestCalculateMinLevels2()
        {
            Assert.That(Homework1.BST.CalculateMinLevels(7), Is.EqualTo(3), "Min level complete tree case incorrect");
        }

        [Test]
        public void TestCalculateMinLevels3()
        {
            Assert.That(Homework1.BST.CalculateMinLevels(0), Is.EqualTo(0), "Min level empty case incorrect");
        }
    }
}
