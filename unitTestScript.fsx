#r "nuget: Expecto"
#r "nuget: FsSpreadsheet.ExcelIO"

open System.IO
open Expecto
open Expecto.Logging
open FsSpreadsheet.ExcelIO

let log = Expecto.Logging.Log.create "ExpectoLog"

// IO conditions
let shouldExistFolder = true
let shouldExistFile = true
let shouldExistText = true
let currDic = Directory.GetCurrentDirectory()
let testfolder = Path.Combine(currDic, "testfolder")
let testfileTxt = Path.Combine(testfolder, "testfile.txt")
let testfileXlsx = Path.Combine(testfolder, "testfile.xlsx")
let doesExistFolder = Directory.Exists testfolder
let doesExistFile = File.Exists testfileTxt
let doesExistText = 
    let doc = Spreadsheet.fromFile testfileXlsx false
    let sst = Spreadsheet.tryGetSharedStringTable doc
    let sd = Spreadsheet.tryGetSheetBySheetName "Tabelle1" doc
    if sd.IsSome then
        let c = SheetData.getCellAt 1u 1u sd.Value
        let v = Cell.tryGetValue sst c
        if v.IsNone then
            log.error (fun ll -> Expecto.Logging.Message.event ll "no cell value")
        v.IsSome
    else 
        log.error (fun ll -> Expecto.Logging.Message.event ll "no sd")
        false

// IO tests
let ioTests = Expecto.Tests.testList "IO tests" [
    Tests.testCase "folder exists" <| fun () ->
        Expect.isTrue doesExistFolder "folder does exist"
    Tests.testCase "file exists" <| fun () ->
        Expect.isTrue doesExistFile "file does exist"
    Tests.testCase "text exists" <| fun () ->
        Expect.isTrue doesExistText "text does exist"
]

let allTheTests = Expecto.Tests.testList "allTheTests" [ioTests]

Expecto.Tests.runTestsWithCLIArgs [] [|"--nunit-summary"; "TestResults.xml"|] allTheTests 
|> exit