using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace NHS_Number_Generator_And_Validator
{
    class ValidateGenerateNhsNumber
    {
        [STAThread]
        static void Main()
        {
            while (true)
            {
                int selection = 0;

                Console.WriteLine("1. Generate NHS Number");
                Console.WriteLine("2. Validate NHS Number");
                Console.WriteLine("3. Quit");
                Console.WriteLine();
                Console.Write("Your Selection: ");

                try { selection = Convert.ToInt32(Console.ReadLine()); }
                catch { }

                switch (selection)
                {
                    case 1:
                        GenerateNhsNumber();
                        break;
                    case 2:
                        ValidateNhsNumber();
                        break;
                    case 3:
                        Quit();
                        break;
                    default:
                        Console.WriteLine("Invalid Selection, Try Again");
                        Console.WriteLine();
                        break;
                }
            }
        }

        private static void GenerateNhsNumber()
        {
            IReadOnlyList<int> digits = new List<int>() { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };

            void OutputNhsNumber()
            {
                string nhsNumber = "";
                int checksum = 0;

                var random = new Random();

                for (int i = 1; i < 10; i++)
                {
                    int digit = digits[random.Next(digits.Count)];

                    nhsNumber =
                        (i == 3 || i == 6) ? nhsNumber + digit.ToString() + " "
                                           : nhsNumber + digit.ToString();

                    checksum = checksum + (digit * (11 - i));
                }

                checksum = 11 - (checksum % 11);
                if (checksum == 11) { checksum = 0; }
                nhsNumber = nhsNumber + checksum.ToString();

                if (checksum == 10) { OutputNhsNumber(); }
                else { Console.WriteLine(nhsNumber); Console.WriteLine("The NHS Number Has Been Copied To Your Clipboard"); }

                System.Windows.Forms.Clipboard.SetText(nhsNumber);
            }

            OutputNhsNumber();

            Console.WriteLine();
        }

        private static void ValidateNhsNumber()
        {
            Console.Write("Enter NHS Number: ");
            string enteredValue = Console.ReadLine();
            string formattedValue = Regex.Replace(enteredValue, @"\s+", "");

            if (formattedValue.Length != 10)
            {
                Console.WriteLine("\"{0}\" Is Not Ten Characters Long", enteredValue);
                Console.WriteLine();
                return;
            }

            else
            {
                try { Convert.ToInt64(formattedValue); }
                catch
                {
                    Console.WriteLine("\"{0}\" Is Not Numeric-Only", enteredValue);
                    Console.WriteLine();
                    return;
                }
            }

            void ValidateInput()
            {
                int[] formattedValueArray = formattedValue.Select(value => Convert.ToInt32(value) - 48).ToArray();

                int checksum = 0;

                for (int i = 1; i < 10; i++)
                {
                    checksum = checksum + (formattedValueArray[i - 1] * (11 - i));
                }

                checksum = 11 - (checksum % 11);

                if (checksum == 11) { checksum = 0; }

                if (checksum == 10)
                {
                    Console.WriteLine("Computed Check Digit \"{0}\" Is Invalid", checksum);
                    if (checksum != formattedValueArray[9]) { Console.WriteLine("Computed Check Digit \"{0}\" Does Not Match Entered Check Digit \"{1}\"", checksum, formattedValueArray[9]); }
                    Console.WriteLine("\"{0}\" Is Not A Valid NHS Number", enteredValue);
                    Console.WriteLine();
                    return;
                }

                if (checksum == formattedValueArray[9])
                {
                    Console.WriteLine("\"{0}\" Is A Valid NHS Number", enteredValue);
                    Console.WriteLine();
                    return;
                }

                else if (checksum != formattedValueArray[9])
                {
                    Console.WriteLine("Computed Check Digit \"{0}\" Does Not Match Entered Check Digit \"{1}\"", checksum, formattedValueArray[9]);
                    Console.WriteLine("\"{0}\" Is Not A Valid NHS Number", enteredValue);
                    Console.WriteLine();
                    return;
                }
            }

            ValidateInput();
        }

        private static void Quit()
        {
            Environment.Exit(0);
        }
    }
}
