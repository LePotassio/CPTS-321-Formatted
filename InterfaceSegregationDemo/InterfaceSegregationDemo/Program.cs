using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterfaceSegregationDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            Dynamite dynamite = new Dynamite();
            dynamite.Use();

            Oven oven = new Oven();
            oven.Use();
        }
    }
}
