$path = "$PSScriptRoot"

$xmlDoc = Load-XmlFile -path "$path\\PackageInfo.xml"

cd $path

dotnet new sln --name "final" --force

$files = ls "*.csproj" -Recurse
$xmlNodes = $xmlDoc.SelectNodes("//Packages/Package")


foreach($node in $xmlNodes)
{
    $nodeNameAttribute = $node.Attributes.GetNamedItem("name")
    $nodeName = $nodeNameAttribute.Value
    $fileToFind = "$nodeName.csproj"
    $projectFile = Find-File -files $files -fileName $fileToFind
    $childNodes = $node.InnerXml;
    $destinationXml = Load-XmlFile -path $projectFile.FullName
    $propertyGroupNode = $destinationXml.SelectSingleNode("//Project/PropertyGroup")
    
    $propertyGroupNode.InnerXml = $propertyGroupNode.InnerXml + $childNodes + 
    "<PackageId>$nodeName</PackageId>" +
    "<AssemblyName>$nodeName</AssemblyName>" +
    "<RootNamespace>$nodeName</RootNamespace>"
    $directory = $projectFile.Directory.FullName
    $newPath = "$directory\\$nodeName.final.csproj"
    
    $destinationXml.Save($newPath)

    dotnet sln "final.sln" add "$newPath"
}

dotnet pack "final.sln" --output "$path\\packages"

function Load-XmlFile(
    [string] $path
    ) {
    $xmlDoc = [System.Xml.XmlDocument]::new()
    $xmlDoc.Load("$path")
    $xmlDoc
}

function Find-File(
        [Array] $files,
        [string]$fileName) {
   
    foreach($file in $files)
    {
        $fileInfo = [System.IO.FileInfo]::new($file)
        
        if($fileName -eq $fileInfo.Name)
        {
            return $file
        }
    }
}