// NUnit 3 tests
// See documentation : https://github.com/nunit/docs/wiki/NUnit-Documentation
//
//--------Homework 3 Test File-----------
// Eric Furukawa, ID: 011580506, 9/28/2020, CPTS 321
// Desc: Contains tests for the program described in homework 3

using System.Collections;
using System.Collections.Generic;
using System.Windows.Forms;
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
        public void TestWriteToLoadFromStream()
        {
            // It seems extremly difficult to test directly the handler or a function which uses the Save or Open dialogue, therefore inner
            // helper funciton is tested here in its place.
            // Additionally, it seems the write and load funcitons are co-dependent in order to test. As such, they are both tested here.

            // ISSUE:

            // General opinion is unit-tets should be run in separation from file system.

            // Mock file systems are used to get around this issue... However...

            // A mock file system would defeat the purpose of this test as the code is different for reading and writing from a mock fs.

            // Thus no test will be provided here


            // The following comment reflects code which would work if unit tests could interact with the file system.

            /*
            Homework3.Form1 TestForm = new Homework3.Form1();
            TestForm.TextBoxText = "This is example text\n It is very short";

            Homework3.Form1.WriteToStream("testFile.txt", TestForm.TextBoxText);

            string loadedText = Homework3.Form1.LoadFromStream("testFile.txt");

            Assert.That(loadedText, Is.EqualTo("This is example text\n It is very short"), "WriteToLoadFromStream failed for generic case.");
            */

            // It also follows that there will be no proper way to unit test the handlers for the fibonacci buttons as well.
        }

        [Test]
        public void TestConfigureDialogue()
        {
            // This Test asserts that COnfigureDialogue can successfully set the fields of a FileDialogue object correctly for form use.
            SaveFileDialog saveFileDialog = new SaveFileDialog();

            //This tests for the save file type object.
            Homework3.Form1.ConfigureDialogue(saveFileDialog);
            Assert.That(saveFileDialog.Filter, Is.EqualTo("txt files (*.txt)|*.txt|All files (*.*)|*.*"), "ConfigureDialogue SaveFileDialogue filter incorrect");
            Assert.That(saveFileDialog.FilterIndex, Is.EqualTo(2), "ConfigureDialogue SaveFileDialogue filterindex incorrect");
            Assert.That(saveFileDialog.RestoreDirectory, Is.EqualTo(true), "ConfigureDialogue SaveFileDialogue restore directory incorrect");

            OpenFileDialog openFileDialog = new OpenFileDialog();

            //This tests for the open file type object.
            Homework3.Form1.ConfigureDialogue(openFileDialog);
            Assert.That(openFileDialog.Filter, Is.EqualTo("txt files (*.txt)|*.txt|All files (*.*)|*.*"), "ConfigureDialogue OpenFileDialogue filter incorrect");
            Assert.That(openFileDialog.FilterIndex, Is.EqualTo(2), "ConfigureDialogue OpenFileDialogue filterindex incorrect");
            Assert.That(openFileDialog.RestoreDirectory, Is.EqualTo(true), "ConfigureDialogue OpenFileDialogue restore directory incorrect");
        }

        [Test]
        public void TestFibonacciReadLine()
        {
            //This Test creates a FibonacciTextReader for 10 elements and checks they match the actual fib series.
            Homework3.FibonacciTextReader fibTextReader = new Homework3.FibonacciTextReader(10);

            Assert.That(fibTextReader.ReadLine(), Is.EqualTo("0"), "Fib ReadLine failed on 1st element");
            Assert.That(fibTextReader.ReadLine(), Is.EqualTo("1"), "Fib ReadLine failed on 2nd element");
            Assert.That(fibTextReader.ReadLine(), Is.EqualTo("1"), "Fib ReadLine failed on 3rd element");
            Assert.That(fibTextReader.ReadLine(), Is.EqualTo("2"), "Fib ReadLine failed on 4th element");
            Assert.That(fibTextReader.ReadLine(), Is.EqualTo("3"), "Fib ReadLine failed on 5th element");
            Assert.That(fibTextReader.ReadLine(), Is.EqualTo("5"), "Fib ReadLine failed on 6th element");
            Assert.That(fibTextReader.ReadLine(), Is.EqualTo("8"), "Fib ReadLine failed on 7th element");
            Assert.That(fibTextReader.ReadLine(), Is.EqualTo("13"), "Fib ReadLine failed on 8th element");
            Assert.That(fibTextReader.ReadLine(), Is.EqualTo("21"), "Fib ReadLine failed on 9th element");
            Assert.That(fibTextReader.ReadLine(), Is.EqualTo("34"), "Fib ReadLine failed on 10th element");
            Assert.That(fibTextReader.ReadLine(), Is.EqualTo(null), "Fib ReadLine failed on end");
        }

        [Test]
        public void TestFibonacciReadBig()
        {
            // This Test was added after functionality was implemented. However, it arose that such a test may be useful to test BigInteger is successfully
            // removing the exceeding max int size problem. Thus I found it justified to break TDD here. I think it is ok as the solution to such a case was
            // provided beforehand
            Homework3.FibonacciTextReader fibTextReader = new Homework3.FibonacciTextReader(100);
            
            for (int i = 0; i < 99; i++)
            {
                fibTextReader.ReadLine();
            }

            Assert.That(fibTextReader.ReadLine(), Is.EqualTo("218922995834555169026"), "Fib ReadLine failed on 100th element");
        }

        [Test]
        public void TestFibonacciReadToEnd()
        {
            //This Test checks the functionality of FibonacciReader's ReadToEnd member function. It Checks the first 10 values of the series match in string format.
            Homework3.FibonacciTextReader fibTextReader = new Homework3.FibonacciTextReader(10);
            fibTextReader.LinesOn = false;
            Assert.That(fibTextReader.ReadToEnd(), Is.EqualTo("0 1 1 2 3 5 8 13 21 34 "), "Fib ReadToEnd failed for 10 element case");
        }

        [Test]
        public void TestFibonacciReadToEndFormatted()
        {
            //This Test checks the functionality of FibonacciReader's ReadToEnd member function with line numbers and tabs. It Checks the first 10 values of the series match in string format.
            Homework3.FibonacciTextReader fibTextReader = new Homework3.FibonacciTextReader(10);
            fibTextReader.LinesOn = true;
            Assert.That(fibTextReader.ReadToEnd(), Is.EqualTo("1. 0\r\n2. 1\r\n3. 1\r\n4. 2\r\n5. 3\r\n6. 5\r\n7. 8\r\n8. 13\r\n9. 21\r\n10. 34\r\n"), "Fib ReadToEnd failed for 10 element formatted case");
        }
    }
}
