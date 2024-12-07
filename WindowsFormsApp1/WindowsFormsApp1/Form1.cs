// <copyright file="Form1.cs" company="Eric-Furukawa">
// Copyright (c) Eric-Furukawa. All rights reserved.
// </copyright>
//
//--------Homework 2 Form File-----------
// Eric Furukawa, 9/23/2020, CPTS 321
// Desc: Contains the form exectuion of the program described in homework 2
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    /// <summary>
    /// Partial class which contains ecevution of homework tasks.
    /// </summary>
    public partial class Form1 : Form
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Form1"/> class.
        /// Default outer handler for form initialization.
        /// </summary>
        public Form1()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// Generates list of 10,000 random integers in range [0, 20,000].
        /// </summary>
        /// <returns>Generated list of integers.</returns>
        public static List<int> GenerateList()
        {
            List<int> generatedList = new List<int>();
            var random = new Random();
            for (int i = 0; i < 10000; i++)
            {
                generatedList.Add(random.Next(20000));
            }

            return generatedList;
        }

        /// <summary>
        /// Gets the number of distinct integers in the list by hash set method.
        /// </summary>
        /// <param name="valueList">Object sending request to load.</param>
        /// <returns>Number of distinct values.</returns>
        public static int GetDistinctNumbers1(List<int> valueList)
        {
            HashSet<int> distinctSet = new HashSet<int>();
            foreach (int val in valueList)
            {
                // The HashSet should skip over any duplicate values
                distinctSet.Add(val);
            }

            return distinctSet.Count;
        }

        /// <summary>
        /// Gets the number of distinct integers in the list with O(1) storage method.
        /// </summary>
        /// <param name="valueList">Object sending request to load.</param>
        /// <returns>Number of distinct values.</returns>
        public static int GetDistinctNumbers2(List<int> valueList)
        {
            // Method: Check if it is the first occurence of the value in the list for every value
            int distinctCount = 0;
            for (int i = 0; i < valueList.Count; i++)
            {
                for (int j = 0; j < valueList.Count; j++)
                {
                    if (valueList[j] == valueList[i])
                    {
                        if (i == j)
                        {
                            distinctCount++;
                        }

                        break;
                    }
                }
            }

            return distinctCount;
        }

        /// <summary>
        /// Gets the number of distinct integers in the list using built-in method for sort with O(n) time complexity and O(1) storage.
        /// </summary>
        /// <param name="valueList">Object sending request to load.</param>
        /// <returns>Number of distinct values.</returns>
        public static int GetDistinctNumbers3(List<int> valueList)
        {
            int distinctCount = 0;
            valueList.Sort();
            for (int i = 0; i < valueList.Count; i++)
            {
                if (i == 0 || valueList[i] != valueList[i - 1])
                {
                    distinctCount++;
                }
            }

            return distinctCount;
        }

        /// <summary>
        /// Default windows form function for actions donw at form load.
        /// </summary>
        /// <param name="sender">Object sending request to load.</param>
        /// <param name="e">Arguments to form.</param>
        public void Form1_Load(object sender, EventArgs e)
        {
            List<int> generatedList = WindowsFormsApp1.Form1.GenerateList();

            string output = string.Empty;
            output += "1. HashSet method: " + GetDistinctNumbers1(generatedList) + " unique numbers" + Environment.NewLine;
            output += "    HashSet complexity: O(n^2)" + Environment.NewLine;
            output += "    HashSet complexity explanation: According to the microsoft documentation," + Environment.NewLine;
            output += "    HashSet.Add() has a time complexity of O(1) if the count (elements in HashSet)" + Environment.NewLine;
            output += "    does not exceed the capacity of the internal array and O(n) if the HashSet" + Environment.NewLine;
            output += "    object must be resized. In the implementation of the Hashset method, the" + Environment.NewLine;
            output += "    HashSet.Add() method is executed within a loop. Since 10000 values are being" + Environment.NewLine;
            output += "    introduced, the Add method will eventually need to resize the object. This" + Environment.NewLine;
            output += "    means there is an O(n) complexity algorithm containing an O(n) algorithm," + Environment.NewLine;
            output += "    resulting in a O(n^2) complexity algorithm." + Environment.NewLine;
            output += "2. O(1) storage method: " + GetDistinctNumbers2(generatedList) + " unique numbers" + Environment.NewLine;
            output += "3. Sorted method: " + GetDistinctNumbers3(generatedList) + " unique numbers" + Environment.NewLine;

            this.textBox1.Text = output;
        }
    }
}
