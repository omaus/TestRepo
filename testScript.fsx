#r "nuget: FSharpAux"

open System
open System.IO
open FSharpAux

//let path = Path.Combine(Directory.GetCurrentDirectory(), "README.md")
let path = Path.Combine(__SOURCE_DIRECTORY__, "README.md")

let currentText = File.ReadAllLines path

let currentAttempt = 
    Array.last currentText 
    |> String.toCharArray
    |> Array.skipWhile (Char.IsDigit >> not)
    |> String
    |> int

let newText = [|
    yield! currentText
    $"\nHello, World (from GitHub actions & FSI script)!\n&nbsp;&nbsp;&nbsp;&nbsp;Attempt #{currentAttempt + 1}"
|]

File.WriteAllLines(path, newText)