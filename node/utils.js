const fs = require("fs")
const fsA = require("fs/promises")
const path = require("node:path")
const prompt = require("prompt-sync")({sigint:true})

class Utils {
    filePath

    constructor() {
        this.filePath = ""
    }

    greetUser = message => {
        const disclaimer = [
            "DISCLAIMER: For each prompt, be sure to hit Enter/Return to complete your interaction with the CLI.",
            "\nYou are also free to use backspace/delete to edit your responses.",
            "\nEnjoy!"
        ]
        console.log(message);
        let line = ""
        for(let i = 0; i < disclaimer[0].length; i++) {
            line += "~"
        }
        console.log(line)
        console.log(disclaimer.join('\n'))
        console.log(line)
    }
    
    isValidFile = (filePath, extension) => {
        let fileValid = false
        while(!fileValid) {
            if(fs.existsSync(filePath)) {
                if(path.extname(filePath) === extension) {
                    fileValid = true
                    this.filePath = filePath
                }
            }
    
            if(!fileValid) {
                console.clear()
                filePath = prompt("Path to file provided is either in the incorrect extension or doesn't exist. Try again please: ")
            }
        }
        console.clear()
        return fileValid
    }
    
    stringValidator = (promptMessage, error, stringResultMessage) => {
        let stringValid = false
        let stringResult = ""
        while(!stringValid) {
            stringResult = prompt(promptMessage)
            if(stringResult === null || stringResult.length === 0) {
                console.clear()
                console.log(error)
                continue
            }
            stringValid = true
        }
        console.clear()
        console.log(stringResultMessage, stringResult)
        return stringResult
    }
    
    readFileContents = async (word) => {
        try {
            const data = await fsA.readFile(this.filePath, {
                encoding: 'utf-8'
            })
            return data
        } catch (e) {
            console.error(e)
            process.exit(1)
        }
    }
    
    calculateWord = async (content, word) => {
        const splitContent = content.split('\r\n')
        let wordCount = 0
        const lowerWord = word.toLowerCase()
        splitContent.forEach(line => {
            const lowerLine = line.toLowerCase()
            const regex = new RegExp(lowerWord, 'g')
            const match = lowerLine.match(regex)
            if(match !== null) {
                wordCount += match.length
            }
        })
        return wordCount
    }

    static regexChecker = (word) => {
        const characters = [".", "^", "$", "*", "+", "?", "|", "(", ")", "[", "]", "{", "}", "\\", "/"]
        return characters.some(char => word.includes(char))
    }

    goAgain = () => {
        let validChoice = false
        let choice = 0
        while(!validChoice) {
            console.log("Would you like to go again?\n[1] Yes\n[2] No\n")
            try {
                choice = parseInt(prompt())
            } catch(e) {
                continue
            }

            if(choice === 1 || choice === 2) {
                validChoice = true
            } else {
                console.clear()
                console.log("Choice entered is invalid. Please try again..")
                continue
            }
        }
        console.clear()
        return choice
    }
}

module.exports = {
    Utils
}