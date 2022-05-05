open System.IO
open System.Xml

let currDic = Directory.GetCurrentDirectory()

let inputPath = Path.Combine(currDic, "TestResults.xml")
let inputXml = File.ReadAllText inputPath


// XSL to HTML approach:

let xsltPath = Path.Combine(currDic, "TemplateXslt.xml")
let templateXslt = File.ReadAllText xsltPath

let transform = Xsl.XslCompiledTransform()
let xsltReader = XmlReader.Create(new StringReader(templateXslt))
transform.Load(xsltReader)

let results = new StringWriter()

let xmlReader = XmlReader.Create(new StringReader(inputXml))
transform.Transform(xmlReader, null, results)

let outputPath = Path.Combine(currDic, "TestResults.html")
File.WriteAllText(outputPath, results.ToString())


// Aspose.Tasks approach:

#r "nuget: Aspose.Tasks"

open Aspose.Tasks

let project = new Project(inputPath)

Aspose.Tasks.

project.Save(outputPath, Tasks. SaveFileFormat.)