$path = "$PSScriptRoot"

. "$path/Shared.ps1"

$build = $true
$newId = [System.Guid]::NewGuid()
$tempPath = "$env:APPDATA\\Automated-Builds\\$newId"
$solutionDirectory = "$tempPath//v2.1"

$buildPropsFileName = "$solutionDirectory\\Directory.Build.props"
$incrementBuildVersion = $true
$packageInfoPath = "$solutionDirectory\\PackageInfo.xml"

cd $path

dotnet clean

mkdir $tempPath

Copy-Item $path -Destination $tempPath -Recurse

cd "$solutionDirectory"

$xmlDoc = Load-XmlFile -path $packageInfoPath

$files = ls "*.csproj" -Recurse

$xmlNodes = $xmlDoc.SelectNodes("//Packages/Package")
$versionInfo = $xmlDoc.SelectSingleNode("//Packages/Info/Version")

$buildVersion = Get-VersionString -version $versionInfo

$buildProperties = Load-XmlFile -path $buildPropsFileName

$propertyGroupnode = $buildProperties.SelectSingleNode("//Project/PropertyGroup")

$versionNodeNames = "AssemblyVersion","FileVersion","Version"

foreach($nodeName in $versionNodeNames)
{
   $node = Get-Node-By-Name -nodes $propertyGroupnode.ChildNodes -nodeName $nodeName
   $node.InnerText = $buildVersion
}

$buildProperties.Save($buildPropsFileName)

foreach($node in $xmlNodes)
{
    $nodeNameAttribute = $node.Attributes.GetNamedItem("name")
    $nodeName = $nodeNameAttribute.Value
    $fileToFind = "$nodeName.csproj"
    $projectFile = Find-File -files $files -fileName $fileToFind
    $childNodes = $node.InnerXml

    $destinationXml = Load-XmlFile -path $projectFile.FullName
    $propertyGroupNode = $destinationXml.SelectSingleNode("//Project/PropertyGroup")


    $propertyGroupNode.InnerXml = $propertyGroupNode.InnerXml + $childNodes + 
    "<PackageId>$nodeName</PackageId>" +
    "<AssemblyName>$nodeName</AssemblyName>" +
    "<RootNamespace>$nodeName</RootNamespace>"
    $directory = $projectFile.Directory.FullName
    
    if($build -eq $true)
    {
        $destinationXml.Save($projectFile)
    }
}

dotnet pack --output "$path\\packages\\$buildVersion" --include-symbols
cd $path
Remove-Item $tempPath -Recurse -Force