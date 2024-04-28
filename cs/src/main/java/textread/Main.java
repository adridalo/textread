package textread;

public class Main {
    public static void main(String[] args) {
        textread();
    }

    public static void textread() {
        Utils.clear();
        final String extension = "txt";
        Utils utils = new Utils();
        utils.greetUser("Welcome to textread");

        String filePath = utils.stringValidator(
            "Enter the path to a text file (with '.txt' extension): ",
            "The file provided is invalid. Please try again..",
            "Path to provided file: "
        );

        if(utils.isValidFile(
            filePath,
            extension
        )) {
            String word = "";
            boolean wrong = false;
            do {
                word = utils.stringValidator(
                    !wrong ? "Enter a word you would like to look up within the provided file: " : "The word provided contains an invalid character. Please try again..: ",
                    "The word provided isn't valid. Please try again..",
                    "Word chosen: "
                );
                wrong = true;
            } while(Utils.regexChecker(word));
            String fileContents = utils.readFileContents(filePath, word);
            int sum = utils.calculateWord(fileContents, word);

            System.out.println("The word \"" + word + "\" appeared " + (sum == 1 ? sum + " time" : sum + " times") + "\n");
            int choice = utils.goAgain();

            if(choice == 2) {
                System.out.println("Thank you for using this service. Have a great day/night!");
            } else {
                textread();
            }
        }
    }
}