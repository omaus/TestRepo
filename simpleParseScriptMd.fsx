#r "nuget: FSharpAux"

open System.IO
open FSharpAux

let inputPath = Path.Combine(__SOURCE_DIRECTORY__, "TestResults.xml")
let outputPath = Path.Combine(__SOURCE_DIRECTORY__, "README.md")

let input = File.ReadAllText inputPath

let splittedInput = String.split '>' input

let recoveredSplittedInput = splittedInput |> Array.map (fun str -> $"{str}>")

let rec indent level i (oldArr : string []) newArr =
    let line = oldArr.[i]
    let currLevel = 
        if String.contains "</" line then level - 1
        else level
    let spaces = String.init currLevel (fun _ -> "  ")
    if i < oldArr.Length - 1 then
        let newLevel =
            if String.contains "?>" line then level
            elif 
                String.contains ">" line && 
                not <| String.contains "/>" line &&
                not <| String.contains "</" line
                then currLevel + 1
            else currLevel
        let newNewArr = [|yield! newArr; $"{spaces}{line}"|]
        indent newLevel (i + 1) oldArr newNewArr
    else 
        [|yield! newArr; $"{spaces}{line}"|]

let parsedInput = indent 0 0 recoveredSplittedInput [||]

let adjustedParsedInput = parsedInput |> Array.rev |> Array.skip 1 |> Array.rev

let concatinatedParsedInput = adjustedParsedInput |> String.concat "\n"

let res =
    """# Unit Test Result:  
```xml
"""
    + concatinatedParsedInput + 
    """
```"""

File.WriteAllText(outputPath, res)