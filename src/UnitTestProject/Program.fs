module Program =

    [<EntryPoint>]
    let main _ = 
    
        Expecto.Tests.runTestsInAssemblyWithCLIArgs [||] [|"--junit-summary"; "./TestResultsJUnitExpecto.xml"|]
    
        //0
