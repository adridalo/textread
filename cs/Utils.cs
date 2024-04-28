using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace textread
{
    public class Utils
    {
        private string _filePath;

        public Utils()
        {
            this._filePath = "";
        }

        public void GreetUser(string message)
        {
            List<String> disclaimer = new List<string>()
            {
                "DISCLAIMER: For each prompt, be sure to hit Enter/Return to complete your interaction with the CLI.",
                "\nYou are also free to use backspace/delete to edit your responses.",
                "\nWhen asked for a path to a file, the absolute path may be required if we can't find it relatively.",
                "\nEnjoy!"
            };
            Console.WriteLine(message);
            string line = "";
            for (int i = 0; i < disclaimer[0].Length; i++)
            {
                line += "~";
            }
            Console.WriteLine(line);
            Console.WriteLine(string.Join("\n", disclaimer));
            Console.WriteLine(line);
        }

        public string StringValidator(string promptMessage, string error, string stringResultMessage)
        {
            var stringValid = false;
            var stringResult = "";
            while (!stringValid)
            {
                Console.Write(promptMessage);
                stringResult = Console.ReadLine();
                if (string.IsNullOrEmpty(stringResult))
                {
                    Console.Clear();
                    Console.WriteLine(error);
                    continue;
                }
                stringValid = true;
            }
            Console.Clear();
            Console.Write(stringResultMessage);
            Console.WriteLine(stringResult);
            return stringResult;
        }

        public bool IsValidFile(string filePath, string extension)
        {
            var fileValid = false;
            while (!fileValid)
            {
                if (File.Exists(filePath))
                {
                    if (new FileInfo(filePath).Extension.Equals(extension))
                    {
                        fileValid = true;
                        this._filePath = filePath;
                    }
                }

                if (!fileValid)
                {
                    Console.Clear();
                    Console.Write("Path to file provided is either in the incorrect extension or doesn't exist. Try again please: ");
                    filePath = Console.ReadLine();
                }
            }
            return fileValid;
        }

        public static bool RegexChecker(string word)
        {
            var characters = new List<String>()
            {
                ".", "^", "$", "*", "+", "?", "|", "(", ")", "[", "]", "{", "}", "\\", "/"
            };
            foreach (var character in characters)
            {
                if (word.Contains(character))
                {
                    return true;
                }
            }
            return false;
        }

        public string ReadFileContents(string filePath, string word)
        {
            var data = "";
            try
            {
                var streamReader = new StreamReader(filePath);
                var line = streamReader.ReadLine();
                while (line != null)
                {
                    data += line;
                    line = streamReader.ReadLine();
                }
                streamReader.Close();
                return data;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            return "";
        }

        public int CalculateWord(string fileContents, string word)
        {
            var matches = Regex.Matches(fileContents.ToLower(), word.ToLower());
            return matches.Count;
        }

        public int GoAgain()
        {
            var validChoice = false;
            var choice = 0;
            while (!validChoice)
            {
                Console.WriteLine("Would you like to go again?\n[1] Yes\n[2] No\n");
                try
                {
                    choice = Convert.ToInt32(Console.ReadLine());
                }
                catch (Exception e)
                {
                    continue;
                }

                if (choice == 1 || choice == 2)
                {
                    validChoice = true;
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine("Choice entered is invalid. Please try again..");
                    continue;
                }
            }
            Console.Clear();
            return choice;
        }
    }
}