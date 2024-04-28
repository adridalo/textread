const { Utils } = require('./utils')

const textread = async () => {
    const extension = ".txt"
    const utils = new Utils()
    utils.greetUser("Welcome to textread")

    const filePath = utils.stringValidator(
        "Enter the path to a text file (with '.txt' extension): ",
        "The file provided is invalid. Please try again..",
        "Path to provided file:"
    )

    if(utils.isValidFile(
        filePath,
        extension
    )) {
        let word = ""
        let wrong = false
        do {
            word = utils.stringValidator(
                !wrong ? "Enter a word you would like to look up within the provided file: " : "The word provided contains an invalid character. Please try again..: ",
                "The word provided isn't valid. Please try again..",
                "Word chosen:"
            )   
            wrong = true
        } while (Utils.regexChecker(word))
        const fileContents = await utils.readFileContents(filePath, word)
        const sum = await utils.calculateWord(fileContents, word)

        console.log(`The word \"${word}\" appeared ${sum == 1 ? sum + " time" : sum + " times"}\n`)
        const choice = utils.goAgain()

        if(choice === 2) {
            console.log("Thank you for using this service. Have a great day/night!")
            process.exit(0)
        } else {
            textread()
        }
    }
}

textread();