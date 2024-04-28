package textread;

import java.io.File;
import java.io.FileNotFoundException;
import java.util.ArrayList;
import java.util.Scanner;
import java.util.regex.Matcher;
import java.util.regex.Pattern;

import org.apache.commons.io.FilenameUtils;

public class Utils {
    private String filePath;
    private Scanner scanner;
    private File file;

    public Utils() {
        this.filePath = "";
        this.scanner = new Scanner(System.in);
    }

    public static void clear() {
        System.out.print("\033[H\033[2J");  
        System.out.flush(); 
    }

    public void greetUser(String message) {
        ArrayList<String> disclaimer = new ArrayList<>();
        disclaimer.add("DISCLAIMER: For each prompt, be sure to hit Enter/Return to complete your interaction with the CLI.");
        disclaimer.add("\nYou are also free to use backspace/delete to edit your responses.");
        disclaimer.add("\nEnjoy!");

        System.out.println(message);
        String line = "";
        for(int i = 0; i < disclaimer.get(0).length(); i++) {
            line += "~";
        }
        System.out.println(line);
        System.out.println(String.join("\n", disclaimer));
        System.out.println(line);
    }

    public String stringValidator(String promptMessage, String error, String stringResultMessage) {
        boolean stringValid = false;
        String stringResult = "";
        while(!stringValid) {
            System.out.print(promptMessage);
            stringResult = scanner.nextLine();
            if(stringResult == null || stringResult.length() == 0) {
                clear();
                System.out.println(error);
                continue;
            }
            stringValid = true;
        }
        clear();
        System.out.print(stringResultMessage);
        System.out.println(stringResult);
        return stringResult;
    }

    public boolean isValidFile(String filePath, String extension) {
        boolean fileValid = false;
        while(!fileValid) {
            this.file = new File(filePath);
            if(file.exists()) {
                if(FilenameUtils.getExtension(filePath).equals(extension)) {
                    fileValid = true;
                    this.filePath = filePath;
                }
            }

            if(!fileValid) {
                clear();
                System.out.print("Path to file provided is either in the incorrect extension or doesn't exist. Try again please: ");
                filePath = scanner.nextLine();
            }
        }
        clear();
        return fileValid;
    }

    public static boolean regexChecker(String word) {
        String[] characters = new String[] {".", "^", "$", "*", "+", "?", "|", "(", ")", "[", "]", "{", "}", "\\", "/"};
        for (String character : characters) {
            if(word.contains(character)) {
                return true;
            }
        }
        return false;
    }

    public String readFileContents(String filePath, String word) {
        try {
            String data = "";
            Scanner fileScanner = new Scanner(this.file);
            while (fileScanner.hasNextLine()) {
                data += fileScanner.nextLine();
            }
            fileScanner.close();
            return data;
        } catch (FileNotFoundException e) {
            e.printStackTrace();
            return "";
        }
    }

    public int calculateWord(String fileContents, String word) {
        word = word.toLowerCase();
        Pattern pattern = Pattern.compile(Pattern.quote(word), Pattern.CASE_INSENSITIVE);
        Matcher matcher = pattern.matcher(fileContents);

        int wordCount = 0;
        while(matcher.find()) {
            wordCount++;
        }
        return wordCount;
    }

    public int goAgain() {
        boolean validChoice = false;
        int choice = 0;
        while(!validChoice) {
            System.out.println("Would you like to go again?\n[1] Yes\n[2] No\n");
            try {
                choice = scanner.nextInt();
            } catch(Exception e) {
                continue;
            }

            if(choice == 1 || choice == 2) {
                validChoice = true;
            } else {
                clear();
                System.out.println("Choice entered is invalid. Please try again..");
                continue;
            }
        }
        clear();
        return choice;
    }
}
