using System;

namespace NHS_Number_Generator_And_Validator
{
    class Program
    {
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
            throw new NotImplementedException();
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
