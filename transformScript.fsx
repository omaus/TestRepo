open System.IO
open System.Xml
open System.Xml.Serialization

//let currDic = Directory.GetCurrentDirectory()
let currDic = __SOURCE_DIRECTORY__

let inputPath = Path.Combine(currDic, "TestResults.xml")
let inputXml = File.ReadAllText inputPath

let outputPath = Path.Combine(currDic, "TestResults.html")


// XSL to HTML approach:

let xsltPath = Path.Combine(currDic, "TemplateXslt.xml")
let templateXslt = File.ReadAllText xsltPath

let transform = Xsl.XslCompiledTransform()
let xsltReader = XmlReader.Create(new StringReader(templateXslt))
transform.Load(xsltReader)

let results = new StringWriter()

let xmlReader = XmlReader.Create(new StringReader(inputXml))
transform.Transform(xmlReader, null, results)

File.WriteAllText(outputPath, results.ToString())


// Aspose.Tasks approach:

//#r "nuget: Aspose.Tasks"

//open Aspose.Tasks

//let project = new Project(inputPath)

//project.Save(outputPath, Saving.SaveFileFormat.Html)


// Individual parsing approach:

type Employee = {
    Name    : string
    Age     : string // int
}


//type Company() = 
//    //[<XmlElement(ElementName = "Employee")>]
//    member this.Employees   : Employee list
//    end

[<CLIMutable>]
[<XmlTypeAttribute("item")>]
type item = {
    
}

let path = Path.Combine(__SOURCE_DIRECTORY__, "textXml.xml")

let ser = new XmlSerializer(typeof<Company>)