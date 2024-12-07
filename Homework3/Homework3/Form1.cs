// <copyright file="Form1.cs" company="Eric-Furukawa">
// Copyright (c) Eric-Furukawa. All rights reserved.
// </copyright>
//
//--------Homework 3 Form1 File-----------
// Eric Furukawa, ID: 011580506, 9/28/2020, CPTS 321
// Desc: Contains the form handlers and form info of the program described in homework 3

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Homework3
{
    /// <summary>
    /// Partial class for the form of program application.
    /// </summary>
    public partial class Form1 : Form
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Form1"/> class.
        /// </summary>
        public Form1()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// Gets or sets textBox1's text field.
        /// </summary>
        public string TextBoxText
        {
            get
            {
                return this.textBox1.Text;
            }

            set
            {
                this.textBox1.Text = value;
            }
        }

        /// <summary>
        /// Functionalized assignment of Dialgue fields for saving and loading.
        /// </summary>
        /// <param name="fd">File dialogue whose properties are to be changed.</param>
        public static void ConfigureDialogue(FileDialog fd)
        {
            fd.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
            fd.FilterIndex = 2;
            fd.RestoreDirectory = true;
        }

        /// <summary>
        /// Creates filestream with given file name and loads text from it. DEPROCATED: This function ended up being not needed as handler and LoadText cover functionality.
        /// </summary>
        /// <param name="fileName">Name of file to be loaded from.</param>
        /// <returns>String loaded from stream.</returns>
        public static string LoadFromStream(string fileName)
        {
            FileStream fs = new FileStream(fileName, FileMode.Create, FileAccess.Read);
            string loadedText = LoadFromStream(fs);
            fs.Dispose();
            return loadedText;
        }

        /// <summary>
        /// loads text from given filestream. DEPROCATED: This function ended up being not needed as handler and LoadText cover functionality.
        /// </summary>
        /// <param name="inStream">Stream to be loaded from.</param>
        /// <returns>String loaded from stream.</returns>
        public static string LoadFromStream(Stream inStream)
        {
            string loadedText = string.Empty;
            using (StreamReader sr = new StreamReader(inStream))
            {
                loadedText = sr.ReadToEnd();
            }

            return loadedText;
        }

        /// <summary>
        /// Creates filestream with given file name and writes given text.
        /// </summary>
        /// <param name="fileName">Name of file to be written to.</param>
        /// <param name="text">Text to be written to created stream.</param>
        public static void WriteToStream(string fileName, string text)
        {
            FileStream fs = new FileStream(fileName, FileMode.Create, FileAccess.Write);
            WriteToStream(fs, text);
            fs.Dispose();
        }

        /// <summary>
        /// Writes given string to given stream.
        /// </summary>
        /// <param name="outStream">Stream to be written to.</param>
        /// <param name="text">Text to be written to stream.</param>
        public static void WriteToStream(Stream outStream, string text)
        {
            using (StreamWriter sw = new StreamWriter(outStream))
            {
                sw.WriteLine(text);
            }
        }

        /// <summary>
        /// Saves the text of textBox1 to the stream. Based on https://docs.microsoft.com/en-us/dotnet/api/system.windows.forms.savefiledialog?redirectedfrom=MSDN&view=netcore-3.1.
        /// </summary>
        public void SaveToFile()
        {
            // Extra function step added in case later testing requires the call of SaveToFile (Where handler arguments will not be avaliable)
            Stream myStream;
            SaveFileDialog saveFileDialog = new SaveFileDialog();

            ConfigureDialogue(saveFileDialog);

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                if ((myStream = saveFileDialog.OpenFile()) != null)
                {
                    WriteToStream(myStream, this.TextBoxText);
                    myStream.Close();
                }
            }
        }

        private void FileOptions_Click(object sender, EventArgs e)
        {
            // No new code needed
        }

        /// <summary>
        /// Execution for loadFromFile button. Loads text from opendialogue file and sets textbox1 text to it. Based on https://docs.microsoft.com/en-us/dotnet/api/system.windows.forms.openfiledialog?view=netcore-3.1.
        /// </summary>
        /// <param name="sender">The object sending the execution request.</param>
        /// <param name="e">Arguments from executer.</param>
        private void LoadFromFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Implementation Here
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                ConfigureDialogue(openFileDialog);

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    var fileStream = openFileDialog.OpenFile();

                    using (StreamReader reader = new StreamReader(fileStream))
                    {
                        this.LoadText(reader);
                    }
                }
            }
        }

        /// <summary>
        /// Gets text from text reader and sets textbox1's text to it.
        /// </summary>
        /// <param name="sr">TextReader read data is to be gathered from.</param>
        private void LoadText(TextReader sr)
        {
            string fileContent = sr.ReadToEnd();
            this.TextBoxText = fileContent;
        }

        /// <summary>
        /// Execution for loadFibonacciNumbersfirst50 button.
        /// </summary>
        /// <param name="sender">The object sending the execution request.</param>
        /// <param name="e">Arguments from executer.</param>
        private void LoadFibonacciNumbersfirst50ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.LoadFibonacciNumbers(50);
        }

        /// <summary>
        /// Execution for loadFibonacciNumbersfirst100 button.
        /// </summary>
        /// <param name="sender">The object sending the execution request.</param>
        /// <param name="e">Arguments from executer.</param>
        private void LoadFibonacciNumbersfirst100ToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            this.LoadFibonacciNumbers(100);
        }

        /// <summary>
        /// Execution for saveToFile button.
        /// </summary>
        /// <param name="sender">The object sending the execution request.</param>
        /// <param name="e">Arguments from executer.</param>
        private void SaveToFileToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            this.SaveToFile();
        }

        /// <summary>
        /// Adds specified amount of lines of the Fibonaci series to the textbox (by replacement).
        /// </summary>
        /// <param name="maxLines">Maximum lines of the series to add to textbox.</param>
        private void LoadFibonacciNumbers(int maxLines)
        {
            using (FibonacciTextReader reader = new FibonacciTextReader(maxLines))
            {
                this.LoadText(reader);
            }
        }
    }
}
