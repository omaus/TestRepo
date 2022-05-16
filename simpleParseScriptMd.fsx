open System.IO

let inputPath = Path.Combine(__SOURCE_DIRECTORY__, "TestResults.xml")
let outputPath = Path.Combine(__SOURCE_DIRECTORY__, "README.md")

let input = File.ReadAllText inputPath

let parsedInput = 
    [|
        for c in input do
            match c with 
            //| '<' -> yield! "&lt"
            //| '>' -> yield! "&gt"
            | '\n' -> yield! "</p><p>"
            | _ -> yield c
    |]
    |> System.String

let res =
    """# Unit Test Result:  
    <p>
    """
    + parsedInput +
    """</p>"""

File.WriteAllText(outputPath, res)