using System;

namespace textread
{
    class Program
    {
        public static void Main(string[] args)
        {
            textread();
        }

        private static void textread()
        {
            const string EXTENSION = ".txt";
            var utils = new Utils();
            utils.GreetUser("Welcome to textread");

            var filePath = utils.StringValidator(
                "Enter the path to a text file (with '.txt' extension): ",
                "The file provided is invalid. Please try again..",
                "Path to provided file: "
            );

            if (utils.IsValidFile(
                filePath,
                EXTENSION
            ))
            {
                var word = "";
                var wrong = false;
                do
                {
                    word = utils.StringValidator(
                        !wrong
                            ? "Enter a word you would like to look up within the provided file: "
                            : "The word provided contains an invalid character. Please try again..: ",
                        "The word provided isn't valid. Please try again..",
                        "Word chosen: "
                    );
                    wrong = true;
                } while (Utils.RegexChecker(word));
                
                var fileContents = utils.ReadFileContents(filePath, word);
                var sum = utils.CalculateWord(fileContents, word);

                Console.WriteLine("The word \"{0}\" appeared {1} {2}", word, sum, sum == 1 ? " time" : " times");
                var choice = utils.GoAgain();

                if (choice == 2) 
                {
                    Console.WriteLine("Thank you for using this service. Have a great day/night!");
                }
                else
                {
                    textread();
                }
            }
        }
    }
}