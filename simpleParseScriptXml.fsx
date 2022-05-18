#r "nuget: FSharpAux"

open System.IO
open FSharpAux

let xmlPath = Path.Combine(__SOURCE_DIRECTORY__, "TestResults.xml")

let jsonPath = Path.Combine(__SOURCE_DIRECTORY__, "ShieldsIO.json")

let input = File.ReadAllText xmlPath

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

let unitTestRes = 
    let noOfTests =
        input
        |> String.splitS "total=\""
        |> Array.item 1
        |> String.takeWhile System.Char.IsDigit
        |> int
    let noOfErrors =
        input
        |> String.splitS "errors=\""
        |> Array.item 1
        |> String.takeWhile System.Char.IsDigit
        |> int
    1. - float noOfErrors / float noOfTests
    |> (*) 100.
    |> round 0
    |> int

let color = 
    match unitTestRes with
    | x when x < 100 && x > 49  -> "yellow"
    | x when x < 50 && x > 0    -> "orange"
    | 0                         -> "red"
    | 100                       -> "green"
    | _                         -> failwith "UnitTest result under 0 or above 100 \%. Should never happen."

let jsonString = 
    sprintf 
        "{\n  \"schemaVersion\": 1,\n  \"label\": \"UnitTests\",\n  \"message\": \"%i\",\n  \"color\": \"%s\"\n}" 
        unitTestRes 
        color

File.WriteAllText(xmlPath, concatinatedParsedInput)
File.WriteAllText(jsonPath, jsonString)