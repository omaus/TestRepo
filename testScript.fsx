open System.IO

File.WriteAllLines(
    Path.Combine(__SOURCE_DIRECTORY__, "README.md"), 
    Array.singleton "Hello, World (from GitHub actions & FSI script)!"
)

//File.WriteAllLines(
//    Path.Combine(Directory.GetCurrentDirectory(), "README.md"), 
//    Array.singleton "Hello, World (from GitHub actions & FSI script)!"
//)