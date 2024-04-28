import os
import re


class Utils:
    def __init__(self):
        self.file_path = ""

    @staticmethod
    def clear():
        print("\033[H\033[J", end="")

    def greet_user(self, message):
        disclaimer = [
            "DISCLAIMER: For each prompt, be sure to hit Enter/Return to complete your interaction with the CLI.",
            "\nYou are also free to use backspace/delete to edit your responses.",
            "\nEnjoy!"
        ]
        print(message)
        line = ""
        for _ in range(0, len(disclaimer[0])):
            line += "~"
        print(line)
        [print(x) for x in disclaimer]
        print(line)

    def string_validator(self, prompt_message, error, string_result_message):
        string_valid = False
        string_result = ""
        while not string_valid:
            string_result = input(prompt_message)
            if string_result is None or len(string_result) == 0:
                Utils.clear()
                print(error)
                continue
            string_valid = True
        Utils.clear()
        print(string_result_message, string_result)
        return string_result

    def is_valid_file(self, file_path, extension):
        file_valid = False
        while not file_valid:
            if os.path.exists(file_path):
                if os.path.splitext(file_path)[1] == extension:
                    file_valid = True
                    self.file_path = file_path
            if not file_valid:
                Utils.clear()
                file_path = input("Path to file provided is either in the incorrect extension or doesn't exist. Try "
                                  "again please: ")
        Utils.clear()
        return file_valid

    @staticmethod
    def regex_checker(word):
        characters = [".", "^", "$", "*", "+", "?", "|", "(", ")", "[", "]", "{", "}", "\\", "/"]
        for character in characters:
            if character in word:
                return True
        return False

    def read_file_contents(self, file_path, word):
        try:
            data = open(file_path, 'r').read()
            return data
        except Exception as e:
            Utils.clear()
            print(str(e))

    def calculate_word(self, file_contents, word):
        word = word.lower()
        file_contents = file_contents.lower()
        matches = re.findall(word, file_contents)
        return len(matches)

    def go_again(self):
        valid_choice = False
        choice = 0
        while not valid_choice:
            print("Would you like to go again?\n[1] Yes\n[2] No\n")
            try:
                choice = int(input())
            except ValueError:
                continue

            if choice == 1 or choice == 2:
                valid_choice = True
            else:
                Utils.clear()
                print("Choice entered is invalid. Please try again..")
                continue

        Utils.clear()
        return choice

