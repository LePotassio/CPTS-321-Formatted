// NUnit 3 tests
// See documentation : https://github.com/nunit/docs/wiki/NUnit-Documentation
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;

namespace NUnit.Tests1
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
        public void TestGenerateList()
        {
            List<int> generatedList= WindowsFormsApp1.Form1.GenerateList();
            Assert.That(generatedList, Is.Not.EqualTo(null), "GenerateList case failed, returned null");
            Assert.That(generatedList.Count, Is.EqualTo(10000), "GenerateList case failed, returned list length is not 10000");
            //How will we test that the numbers are being generated randomly?
            //Test for extremly unlikely case all three first values are 0
            Assert.That(generatedList[0] == 0
                     && generatedList[1] == 0
                     && generatedList[2] == 0, Is.EqualTo(false), "GenerateList case failed, list values are likely all 0");
        }

        [Test]
        public void TestGetDistinctNumbers1()
        {
            //Simple case of no repeats in the list
            List<int> generatedList = new List<int> { 5, 2, 4444, 4, 555, 3, 0 };
            Assert.That(WindowsFormsApp1.Form1.GetDistinctNumbers1(generatedList), Is.EqualTo(7), "GetDistinctNumbers1 failed for simple no-repeat case");
            //Simple case of repeats in the list
            generatedList = new List<int>{ 5, 2, 4444, 5 , 4, 555, 3, 555, 0, 5 };
            Assert.That(WindowsFormsApp1.Form1.GetDistinctNumbers1(generatedList), Is.EqualTo(7), "GetDistinctNumbers1 failed for simple repeat case");
            //Edge case list is empty
            generatedList = new List<int> { };
            Assert.That(WindowsFormsApp1.Form1.GetDistinctNumbers1(generatedList), Is.EqualTo(0), "GetDistinctNumbers1 failed for empty list case");
        }

        [Test]
        public void TestGetDistinctNumbers2()
        {
            //Simple case of no repeats in the list
            List<int> generatedList = new List<int> { 5, 2, 4444, 4, 555, 3, 0 };
            Assert.That(WindowsFormsApp1.Form1.GetDistinctNumbers2(generatedList), Is.EqualTo(7), "GetDistinctNumbers1 failed for simple no-repeat case");
            //Simple case of repeats in the list
            generatedList = new List<int> { 5, 2, 4444, 5, 4, 555, 3, 555, 0, 5 };
            Assert.That(WindowsFormsApp1.Form1.GetDistinctNumbers2(generatedList), Is.EqualTo(7), "GetDistinctNumbers1 failed for simple repeat case");
            //Edge case list is empty
            generatedList = new List<int> { };
            Assert.That(WindowsFormsApp1.Form1.GetDistinctNumbers2(generatedList), Is.EqualTo(0), "GetDistinctNumbers1 failed for empty list case");
        }

        [Test]
        public void TestGetDistinctNumbers3()
        {
            //Simple case of no repeats in the list
            List<int> generatedList = new List<int> { 5, 2, 4444, 4, 555, 3, 0 };
            Assert.That(WindowsFormsApp1.Form1.GetDistinctNumbers3(generatedList), Is.EqualTo(7), "GetDistinctNumbers1 failed for simple no-repeat case");
            //Simple case of repeats in the list
            generatedList = new List<int> { 5, 2, 4444, 5, 4, 555, 3, 555, 0, 5 };
            //In this case, the list will be sorted into -> { 0, 2, 3, 4, 5, 5, 5, 555, 555, 4444 }
            Assert.That(WindowsFormsApp1.Form1.GetDistinctNumbers3(generatedList), Is.EqualTo(7), "GetDistinctNumbers1 failed for simple repeat case");
            //Edge case list is empty
            generatedList = new List<int> { };
            Assert.That(WindowsFormsApp1.Form1.GetDistinctNumbers3(generatedList), Is.EqualTo(0), "GetDistinctNumbers1 failed for empty list case");
        }
    }
}
