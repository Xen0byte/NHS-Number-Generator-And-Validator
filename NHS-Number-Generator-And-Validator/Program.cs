using System;
using System.Collections.Generic;

namespace NHS_Number_Generator_And_Validator
{
    class Program
    {
        [STAThread]
        static void Main()
        {
            while (true)
            {
                int selection = 0;

                Console.WriteLine("1. Generate NHS Number & Copy To Clipboard");
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
                else { Console.WriteLine(nhsNumber); }

                System.Windows.Forms.Clipboard.SetText(nhsNumber);
            }

            OutputNhsNumber();

            Console.WriteLine();
        }

        private static void ValidateNhsNumber()
        {
            throw new NotImplementedException();
        }

        private static void Quit()
        {
            Environment.Exit(0);
        }
    }
}
