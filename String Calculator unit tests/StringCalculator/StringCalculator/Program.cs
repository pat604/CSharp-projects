using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StringCalculator
{
    class Program
    {
        static void Main(string[] args)
        {
            Calculator calculator = new Calculator();
            try
            {
                calculator.Add("//;\n1;-2;-1");
            }
            catch (NegativesNotAllowedException e)
            {
                Console.WriteLine(e.Message);
                Console.ReadLine();
            }

        }
    }
}
