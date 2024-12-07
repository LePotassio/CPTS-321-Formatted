// <copyright file="Form1.cs" company="Eric-Furukawa">
// Copyright (c) Eric-Furukawa. All rights reserved.
// </copyright>

// NUnit 3 tests
// See documentation : https://github.com/nunit/docs/wiki/NUnit-Documentation
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using NUnit.Framework;

using HW11_ThreadingApp;

namespace NUnit.Tests1
{
    [TestFixture]
    public class TestClass
    {
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


        // Testing related to forms ommited due to not being able to test forms.

        [Test]
        public void TestURLDataExtraction()
        {
            // Tests if data is successfully extracted from URL using GetUrlData().

            Form1 testForm = new Form1();

            MethodInfo methodInfo = this.GetMethod("GetUrlData", testForm);

            // Will be completed once known how URL extraction works (format)
            string url = "http://www.stealthboats.com/";
            string expextedWebpageData = "\r\n\r\n<HTML>\r\n<HEAD>\r\n<TITLE>Stealth boats - Stealthboats.com";
            Assert.That(((string)methodInfo.Invoke(testForm, new object[] { url })).Substring(0, expextedWebpageData.Length), Is.EqualTo(expextedWebpageData), "Url data extraction failed, data doesn't match expected.");
        }

        // Testing for random numbers difficult...
        // That and Sort being used is built in so doesn't make sense to test... 

        [Test]
        public void TestRNG()
        {
            // Tests if lists are filled with random numbers using GenerateLists(). WIll fail under the extreme case random numbers generated are all 0.

            Form1 testForm = new Form1();

            MethodInfo methodInfo = this.GetMethod("GenerateLists", testForm);
            List<int[]> integerArrays = (List<int[]>)methodInfo.Invoke(testForm, new object[] { });
            bool answer = CheckForNonZero(integerArrays);

            Assert.That(answer , "List not randomized");
        }

        public bool CheckForNonZero(List<int[]> integerArrays)
        {
            foreach (int[] arr in integerArrays)
            {
                foreach (int num in arr)
                {
                    if (num != 0)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        [Test]
        public void TestSorting()
        {
            // Omitted as testing built in sort not needed.
        }

        // NOTE: Design choice found. If we generate the list in the UI thread, it doesn't for the user to use the UI while generating numbers.
        // If we move it into the threads, it resolves this but makes it so the single and multi thread functions are sorting different numbers.

        // This could be resolved by putting another thread in which generates the numbers and then starts the separate threads...
        // Due to the large numbers in array size, variation in time cases should be almost undetectable.


        // UPDATE: Changed to starting a new thread for number generation, reverted separate number generation. Allows for user to interact with UI during num gen.
        // while allowing the single and multi thread cases to work with exact same numbers.
    }
}
