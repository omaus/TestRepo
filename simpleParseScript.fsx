open System.IO

let inputPath = Path.Combine(__SOURCE_DIRECTORY__, "TestResults.xml")
let outputPath = Path.Combine(__SOURCE_DIRECTORY__, "TestResults.html")

let input = File.ReadAllText inputPath

let parsedInput = 
    [|
        for c in input do
            match c with 
            | '<' -> yield! "&lt"
            | '>' -> yield! "&gt"
            | '\n' -> yield! "</p><p>"
            | _ -> yield c
    |]
    |> System.String

let res = 
    """<!DOCTYPE html>
    <html>
    <head>
    <title>TestResults.xsml</title>
    </head>
    <body>
    <p>
    """
    + parsedInput +
    """</p>
    </body>
    </html>
    """

File.WriteAllText(outputPath, res)