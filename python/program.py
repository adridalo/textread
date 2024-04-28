import asyncio

from utils import Utils


async def textread():
    EXTENSION = ".txt"
    utils = Utils()
    utils.greet_user("Welcome to textread")

    file_path = utils.string_validator(
        "Enter the path to a text file (with '.txt' extension): ",
        "The file provided is invalid. Please try again..",
        "Path to provided file:"
    )

    if utils.is_valid_file(
        file_path,
        EXTENSION
    ):
        wrong = False
        word = "."
        while Utils.regex_checker(word):
            word = utils.string_validator(
                "Enter a word you would like to look up within the provided file: " if not wrong else "The word "
                                                                                                      "provided "
                                                                                                      "contains an "
                                                                                                      "invalid "
                                                                                                      "character. "
                                                                                                      "Please try "
                                                                                                      "again..: ",
                "The word provided isn't valid. Please try again..",
                "Word chosen:"
            )
            wrong = True
        file_contents = utils.read_file_contents(file_path, word)
        word_sum = utils.calculate_word(file_contents, word)

        print("The word \"{}\" appeared {} {}".format(word, word_sum, "times" if word_sum > 1 else "time"))
        choice = utils.go_again()

        if choice == 2:
            print("Thank you for using this service. Have a great day/night!")
        else:
            await textread()

if __name__ == "__main__":
    asyncio.run(textread())
