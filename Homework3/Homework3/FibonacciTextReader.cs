// <copyright file="FibonacciTextReader.cs" company="Eric-Furukawa">
// Copyright (c) Eric-Furukawa. All rights reserved.
// </copyright>
//
//--------Homework 3 FibonacciTextReader File-----------
// Eric Furukawa, ID: 011580506, 9/28/2020, CPTS 321
// Desc: Contains textreader inheriting class for reading fibonacci sequence number strings

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Homework3
{
    /// <summary>
    /// Fibonacci TextReader object providing overrides to fit fibonacci functionality.
    /// </summary>
    public class FibonacciTextReader : TextReader
    {
        // Toggle this for formatted lines//
        private bool linesOn = true;
        ////////////////////////////////////

        private int maxLines;
        private int readCount;
        private BigInteger element1;
        private BigInteger element2;

        /// <summary>
        /// Initializes a new instance of the <see cref="FibonacciTextReader"/> class.
        /// </summary>
        /// <param name = "maxNum">Maximum number of lines avaliable.</param>
        public FibonacciTextReader(int maxNum)
        {
            this.maxLines = maxNum;
            this.element1 = 0;
            this.element2 = 0;
            this.readCount = 0;
        }

        /// <summary>
        /// Gets or sets maxLines field.
        /// </summary>
        public int MaxLines
        {
            get
            {
                return this.maxLines;
            }

            set
            {
                this.maxLines = value;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether to format fib series or not.
        /// </summary>
        public bool LinesOn
        {
            get
            {
                return this.linesOn;
            }

            set
            {
                this.linesOn = value;
            }
        }

        /// <summary>
        /// Override which delivers next number in Fibonacci sequence.
        /// </summary>
        /// <returns>Next number in fib sequence as string type.</returns>
        public override string ReadLine()
        {
            // Be careful, readCoutn starts at zero but size starts at 1, off by one error caution
            if (this.readCount == this.maxLines)
            {
                return null;
            }

            if (this.element2 == 0)
            {
                this.readCount++;
                this.element2 = -1;
                return "0";
            }

            if (this.element2 == -1)
            {
                this.readCount++;
                this.element2 = 1;
                return "1";
            }
            else
            {
                // This implementation requires the above special case but avoids pre calculating the next element or needing to check if it is the last element.
                // Overall less intensive(?) so went with this one.
                this.readCount++;
                BigInteger sum = this.element1 + this.element2;
                this.element1 = this.element2;
                this.element2 = sum;
                return sum.ToString();
            }

            /*
            // This implementation eliminates the need for a special 2nd case but adds one extra number calculation (could be eliminated with last element check).
            else
            {
                this.readCount++;
                BigInteger sum = this.element1 + this.element2;
                this.element1 = this.element2;
                this.element2 = sum;
                return this.element1.ToString();
            }
            */
        }

        /// <summary>
        /// Override which delivers concatenated Fibonacci sequence (First 100 numbers).
        /// </summary>
        /// <returns>Fib sequence as string type.</returns>
        public override string ReadToEnd()
        {
            string result = string.Empty;

            // This functionality must be kept separate from readLine due to readline keeping track of the location via member fields.
            BigInteger tempElement1 = 0;
            BigInteger tempElement2 = 0;
            int localReadCount = 0;
            for (int i = 0; i < this.maxLines - 1; i++)
            {
                if (tempElement2 == 0)
                {
                    localReadCount++;
                    tempElement2 = -1;
                    if (this.linesOn)
                    {
                        result += "1. 0" + Environment.NewLine;
                    }
                    else
                    {
                        result += "0 ";
                    }
                }

                if (tempElement2 == -1)
                {
                    localReadCount++;
                    tempElement2 = 1;
                    if (this.linesOn)
                    {
                        result += "2. 1" + Environment.NewLine;
                    }
                    else
                    {
                        result += "1 ";
                    }
                }
                else
                {
                    localReadCount++;
                    BigInteger sum = tempElement1 + tempElement2;
                    tempElement1 = tempElement2;
                    tempElement2 = sum;
                    if (this.linesOn)
                    {
                        result += localReadCount + ". " + sum.ToString() + Environment.NewLine;
                    }
                    else
                    {
                        result += sum.ToString() + " ";
                    }
                }
            }

            return result;
        }
    }
}
