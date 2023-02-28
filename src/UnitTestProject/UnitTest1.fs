module UnitTestProject

open Expecto

[<Tests>]
let test1 =
    testList "Node1" [
        testList "Node2" [
            testList "Node3" [
                testCase "Tip2" <| fun () -> Expect.isTrue true "is not true"
            ]
        ]
        testCase "Tip1" <| fun () -> Expect.isTrue true "is not true"
    ]

