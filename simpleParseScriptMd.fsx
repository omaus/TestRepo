#r "nuget: FSharpAux"

open System.IO
open FSharpAux

let inputPath = Path.Combine(__SOURCE_DIRECTORY__, "TestResults.xml")
let outputPath = Path.Combine(__SOURCE_DIRECTORY__, "README.md")

let input = File.ReadAllText inputPath

let splittedInput = String.split '>' input

let rec indent level i (oldArr : string []) newArr =
    if i < oldArr.Length - 1 then
        let line = oldArr.[i]
        let spaces = String.init level (fun _ -> " ")
        let newLevel = 
            if String.contains "/>" line then level - 1 
            elif String.contains ">" line && String.contains "/>" line |> not then level + 1
            else level
        let newNewArr = [|yield! newArr; $"{spaces}{line}"|]
        indent newLevel (i + 1) oldArr newNewArr
    else 
        let line = oldArr.[i]
        let spaces = String.init level (fun _ -> " ")
        let newLevel = 
            if String.contains "/>" line then level - 1 
            elif String.contains ">" line && String.contains "/>" line |> not then level + 1
            else level
        [|yield! newArr; $"{spaces}{line}"|]

let parsedInput = indent 0 0 splittedInput [||]

let concatinatedParsedInput = parsedInput |> String.concat "\n"

let res =
    """# Unit Test Result:  
    """
    + concatinatedParsedInput

File.WriteAllText(outputPath, res)