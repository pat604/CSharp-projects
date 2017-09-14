using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StringCalculator
{
    public class Calculator
    {
        public Calculator() { }

        private int result;
        private string[] separator;


        public int Add(string input)
        {

            if (input == "")            // üres-e az input
            {
                result = 0;
                Display(result);
                return result;
            }
            else
            {
                if (!FindSeparator(ref input))        // ha nem volt megadva separator, akkor "," és "\n" szerint van Split
                {
                    separator = new string[2] { ",", "\n" };
                }

                string[] splittednumbers = (input.Split(separator, System.StringSplitOptions.RemoveEmptyEntries));

                int[] numbers = new int[splittednumbers.Count()];

                int i = 0;
                string negatives = "";
                foreach (string number in splittednumbers)
                {
                    numbers[i] = Int32.Parse(number);
                    if (numbers[i] < 0)
                        negatives += numbers[i] + "  ";

                    i++;
                }

                if (negatives != "")
                {
                    throw new NegativesNotAllowedException("negative numbers are not allowed: " + negatives);
                }

                result = numbers.Sum();
                Display(result);
                return result;
            }
        }
        

        private bool FindSeparator(ref string input)
        {
            // //[delimiter]\n[numbers…]

            input.ToArray<char>();
            int i = 1;
            string separator = "";
            if (input[0] == '/' && input[1] == '/')
            {
                do
                {
                    i++;
                    separator += input[i];      // ha ez már a \n lenne, akkor a felhasználó nem adott meg separatort, hiba
                }
                while (input[i + 1] == '\\' && input[i + 2] == 'n');

                string modifiedInput = "";
                for (int j = i + 2; j < input.Length; j++)      // ezzel kihagyom az input string első sorát
                {
                    modifiedInput += input[j];
                }

                this.separator = new string[1] { separator };      // a separatort tömbbé alakítom, mert így kéri be a Split()

                input = modifiedInput;

                return true;
            }
            else return false;


        }

        private void Display(int result)
        {
            Console.WriteLine(result);
            Console.ReadLine();
        }

    }
}
