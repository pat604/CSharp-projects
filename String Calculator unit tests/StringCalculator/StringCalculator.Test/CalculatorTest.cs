using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StringCalculator.Test
{
    [TestFixture]
    public class CalculatorTest
    {

        // 1. Create a simple String calculator with a method int Add(string numbers). The method can take 0, 1 or 2 numbers.
        [Test]
        public void Add_Should_Return_3_When_Input_is_1_2()
        {
            // Arrange
            Calculator calculator = new Calculator();

            // Act
            var result = calculator.Add("1,2");

            // Assert
            Assert.AreEqual(3, result, "The result was not 3. The result was:" + result);
        }


        // 1. Create a simple String calculator with a method int Add(string numbers). The method can take 0, 1 or 2 numbers.
        [Test]
        public void Add_Should_Return_2_When_Input_is_2()
        {
            // Arrange
            Calculator calculator = new Calculator();

            // Act
            var result = calculator.Add("2");

            // Assert
            Assert.AreEqual(2, result, "The result was not 2. The result was:" + result);
        }


        // 1. For an empty string it will return 0.
        [Test]
        public void Add_Should_Return_0_When_Input_Is_Empty()
        {
            Calculator calculator = new Calculator();
            var result = calculator.Add("");
            Assert.AreEqual(0, result, "The result was not 0. The result was " + result);
        }


        // 2. Allow the Add method to handle an unknown amount of numbers
        [Test]
        public void Add_Should_Return_303_When_Input_is_1_2_100_200()
        {
            Calculator calculator = new Calculator();
            var result = calculator.Add("2,5,100,200");
            Assert.AreEqual(307, result, "The result was not 307. The result was:" + result);
        }



        // 3. Allow the Add method to handle new lines between numbers (instead of commas).
        [Test]
        public void Add_Should_Return_10_When_Input_is_1_n6_1_n_n_2()
        {
            Calculator calculator = new Calculator();
            var result = calculator.Add("1\n6,1\n\n2");
            Assert.AreEqual(10, result, "The result was not 10. The result was:" + result);
        }


        // 3. Allow the Add method to handle new lines between numbers (instead of commas).
        [Test]
        public void Add_Should_Return_0_When_Input_is_n_n_n()
        {
            Calculator calculator = new Calculator();
            var result = calculator.Add("\n\n\n,");
            Assert.AreEqual(0, result, "The result was not 0. The result was:" + result);
        }



        // 4. Support different delimiters    
        [Test]
        public void Add_Should_Return_3_When_Input_is_1_2_And_Separator_Is_Chosen()
        {
            Calculator calculator = new Calculator();
            var result = calculator.Add("//;\n1;2");
            Assert.AreEqual(3, result, "The result was not 3. The result was:" + result);
        }

        // 4. Support different delimiters    
        [Test]
        public void Add_Should_Return_9_When_Input_is_1_2_3_3_And_Separator_Is_Chosen()
        {
            Calculator calculator = new Calculator();
            var result = calculator.Add("//??\n1??2??3??3");
            Assert.AreEqual(9, result, "The result was not 9. The result was:" + result);
        }



        // 5. Calling Add with a negative number will throw an exception “negatives not allowed” - 
        // and the negative that was passed. If there are multiple negatives, show all of them in the exception message
        [Test]
        public void Add_Should_Throw_Exception_When_Input_Is_Negative_V1()
        {
            TestDelegate td = new TestDelegate(CalculatorWithoutReturnV1);
            Assert.Throws<NegativesNotAllowedException>(td);
        }

        private void CalculatorWithoutReturnV1()
        {
            Calculator calculator = new Calculator();
            calculator.Add("//;\n1;-2;-1");

        }


        // 5. Calling Add with a negative number will throw an exception “negatives not allowed” - 
        // and the negative that was passed. If there are multiple negatives, show all of them in the exception message
        [Test]
        public void Add_Should_Throw_Exception_When_Input_Is_Negative_V2()
        {
            TestDelegate td = new TestDelegate(CalculatorWithoutReturnV2);
            Assert.Throws<NegativesNotAllowedException>(td);
        }

        private void CalculatorWithoutReturnV2()
        {
            Calculator calculator = new Calculator();
            calculator.Add("1,1\n-10");

        }
    }
}