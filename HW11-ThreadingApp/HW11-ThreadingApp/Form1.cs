// <copyright file="Form1.cs" company="Eric-Furukawa">
// Copyright (c) Eric-Furukawa. All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HW11_ThreadingApp
{
    /// <summary>
    /// Winform object for application to run.
    /// </summary>
    public partial class Form1 : Form
    {
        private Stopwatch multiThreadTimer;
        private bool[] threadsComplete;

        /// <summary>
        /// Initializes a new instance of the <see cref="Form1"/> class.
        /// </summary>
        public Form1()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// Handler for Download Button being pressed.
        /// </summary>
        /// <param name="sender">Object sending request of handler.</param>
        /// <param name="e">Arguments for sender.</param>
        private void DownloadButton_Click(object sender, EventArgs e)
        {
            this.URLTextBox.Enabled = false;
            this.DownloadButton.Enabled = false;

            Thread t = new Thread(() => this.DoDownload());
            t.Start();
        }

        /// <summary>
        /// Downloads URL data and sets download textbox text to data.
        /// </summary>
        private void DoDownload()
        {
            string url = this.URLTextBox.Text;
            string downloadText = string.Empty;

            downloadText = this.GetUrlData(url);

            // Tell UI to update
            this.BeginInvoke(new Action<string>(this.DownloadComplete), new object[] { downloadText });
        }

        /// <summary>
        /// Gets the url data of the requested URL.
        /// </summary>
        /// <param name="url">URL of data to extract.</param>
        /// <returns>Data at specified URL.</returns>
        private string GetUrlData(string url)
        {
            WebClient client = new WebClient();

            client.Headers.Add("user-agent", "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.2; .NET CLR 1.0.3705;)");

            Stream data = client.OpenRead(url);
            StreamReader reader = new StreamReader(data);
            string s = reader.ReadToEnd();
            data.Close();
            reader.Close();

            return s;
        }

        /// <summary>
        /// Updates UI elements of form after download finishes.
        /// </summary>
        /// <param name="downloadText">Text of the newly downloaded url data.</param>
        private void DownloadComplete(string downloadText)
        {
            this.URLTextBox.Enabled = true;
            this.DownloadButton.Enabled = true;
            this.DownloadTextBox.Text = downloadText;
        }

        /// <summary>
        /// Handler for Sort Button being pressed.
        /// </summary>
        /// <param name="sender">Object sending request of handler.</param>
        /// <param name="e">Arguments for sender.</param>
        private void SortButton_Click(object sender, EventArgs e)
        {
            this.SortButton.Enabled = false;
            this.ThreadTimeTextBox.Text = string.Empty;
            Thread t = new Thread(() => this.DoSortThreading());
            t.Start();
        }

        private void DoSortThreading()
        {

            List<int[]> integerArrays = this.GenerateLists();

            // Should prevent need of unsorting arrays if sort is not returning sorted arrays and is sorting the array itself
            List<int[]> singleThreadList = new List<int[]>(integerArrays);
            List<int[]> multiThreadList = new List<int[]>(integerArrays);

            // int[][] list = this.GenerateLists(); // 8 Lists of 200 elements
            // this.SingleThreadSort(singleThreadList);
            this.SingleThreadSortInThread(singleThreadList);

            // Might Need this to "unsort" the list but would nullify efforts to makes sure sorting same lists
            // integerArrays = this.GenerateLists();
            this.MultiThreadSort(multiThreadList);
        }

        /// <summary>
        /// Generates random numbers 0-100 for arrays of given list.
        /// </summary>
        /// <returns>List of int arrays to be filled with random numbers.</returns>
        private List<int[]> GenerateLists()
        {
            var rand = new Random();
            List<int[]> lists = new List<int[]>();
            int[] newList = null;

            for (int r = 0; r < 8; r++)
            {
                newList = new int[10000000];
                for (int c = 0; c < newList.Length; c++)
                {
                    newList[c] = rand.Next(101);
                }

                lists.Add(newList);
            }

            return lists;
        }

        /// <summary>
        /// Sorts each int array of the given list in a single thread while timing and outputting to UI. Not used as was causing both cases to perform at same time.
        /// </summary>
        /// <param name="integerArrays">Integer array to sort.</param>
        private void SingleThreadSort(List<int[]> integerArrays)
        {
            Thread t = new Thread(() => this.SingleThreadSortInThread(integerArrays));
            t.Start();
        }

        /// <summary>
        /// Sorts each int array of the given list in a single thread while timing and outputting to UI.
        /// </summary>
        /// <param name="integerArrays">Integer array to sort.</param>
        private void SingleThreadSortInThread(List<int[]> integerArrays)
        {
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();
            for (int r = 0; r < integerArrays.Count; r++)
            {
                // integerArrays[r].ToList().ForEach(i => Console.WriteLine(i.ToString()));
                Array.Sort(integerArrays[r]);

                // integerArrays[r].ToList().ForEach(i => Console.WriteLine(i.ToString()));
            }

            stopWatch.Stop();
            this.BeginInvoke(new Action<string>(this.SingleThreadComplete), new object[] { "Single thread sort time: " + stopWatch.ElapsedMilliseconds + " ms" });
        }

        /// <summary>
        /// Updates UI elements of form after sorting finishes.
        /// </summary>
        /// <param name="sortTime">Text of the newly downloaded url data.</param>
        private void SingleThreadComplete(string sortTime)
        {
            this.ThreadTimeTextBox.Text += sortTime;
        }

        /// <summary>
        /// Sorts each int array of the given list in multiple threads while timing and outputting to UI.
        /// </summary>
        /// <param name="integerArrays">Integer array to sort.</param>
        private void MultiThreadSort(List<int[]> integerArrays)
        {
            // How will we update the total time at the end? Last thread must know to stop the timer and update text.
            this.threadsComplete = new bool[8];
            this.multiThreadTimer = new Stopwatch();
            this.multiThreadTimer.Start();

            // Console.WriteLine("Timer started at " + this.multiThreadTimer.ElapsedMilliseconds);
            for (int index = 0; index < integerArrays.Count; index++)
            {
                int temp = index;
                Thread t = new Thread(() => this.SortArray(integerArrays[temp], temp));
                t.Start();
            }

            /*
            int arrIndex = 0;
            foreach (int[] arr in integerArrays)
            {
                Console.WriteLine("Timer check at " + this.multiThreadTimer.ElapsedMilliseconds);
                // Idk why I need a temp, we aren't passing by reference...
                int temp = arrIndex;
                Thread t = new Thread(() => this.SortArray(arr, temp));
                t.Start();
                arrIndex++;
            }
            */
        }

        /// <summary>
        /// Sorts given array and update UI if all other threads indicated to be done.
        /// </summary>
        /// <param name="arr">Array to be sorted.</param>
        /// <param name="arrIndex">Index of the array in the greater list.</param>
        private void SortArray(int[] arr, int arrIndex)
        {
            // Console.WriteLine("Thread " + arrIndex + " started at " + this.multiThreadTimer.ElapsedMilliseconds);

            // Something about concurrent sorting here causes console app to become unresponsive during the duration
            Array.Sort(arr);

            // Console.WriteLine("Arr Index inthread" + arrIndex);
            this.threadsComplete[arrIndex] = true;

            // Console.WriteLine("Thread " + arrIndex + " complete at " + this.multiThreadTimer.ElapsedMilliseconds);
            if (this.CheckArrayTrue())
            {
                this.multiThreadTimer.Stop();
                this.BeginInvoke(new Action<string>(this.ThreadSortingComplete), new object[] { Environment.NewLine + "Multi thread sort time: " + this.multiThreadTimer.ElapsedMilliseconds + " ms" });
            }
        }

        /// <summary>
        /// Checks if all the multithreads are done sorting.
        /// </summary>
        /// <returns>Boolean for if all threads are done.</returns>
        private bool CheckArrayTrue()
        {
            foreach (bool threadStatus in this.threadsComplete)
            {
                if (!threadStatus)
                {
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// Updates UI elements of form after sorting finishes.
        /// </summary>
        /// <param name="sortTime">Text of the newly downloaded url data.</param>
        private void ThreadSortingComplete(string sortTime)
        {
            this.ThreadTimeTextBox.Text += sortTime;
            this.SortButton.Enabled = true;
        }
    }
}
