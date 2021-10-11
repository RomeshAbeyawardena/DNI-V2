$path = "$PSScriptRoot"
$build = $false
$buildPropsFileName = "$path\\Directory.Build.props"

$xmlDoc = Load-XmlFile -path "$path\\PackageInfo.xml"

cd $path

if($build -eq $true)
{
    dotnet new sln --name "final" --force
}

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
    $childNodes = $node.InnerXml;
    $destinationXml = Load-XmlFile -path $projectFile.FullName
    $propertyGroupNode = $destinationXml.SelectSingleNode("//Project/PropertyGroup")
    
    $propertyGroupNode.InnerXml = $propertyGroupNode.InnerXml + $childNodes + 
    "<PackageId>$nodeName</PackageId>" +
    "<AssemblyName>$nodeName</AssemblyName>" +
    "<RootNamespace>$nodeName</RootNamespace>"
    $directory = $projectFile.Directory.FullName
    $newPath = "$directory\\$nodeName.final.csproj"
    
    if($build -eq $true)
    {
        $destinationXml.Save($newPath)
        dotnet sln "final.sln" add "$newPath"
    }
}

if($build -eq $true)
{
    dotnet pack "final.sln" --output "$path\\packages"
}

function Get-VersionString(
    [object] $version
    ) {

    $major = $version.Attributes.GetNamedItem("Major").Value
    $minor = $version.Attributes.GetNamedItem("Minor").Value
    $revision = $version.Attributes.GetNamedItem("Revision").Value
    $patch = $version.Attributes.GetNamedItem("Patch").Value

    return [System.String]::Format("{0}.{1}.{2}.{3}", $major, $minor, $revision, $patch)
}

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

function Get-Node-By-Name (
        [Array] $nodes,
        [string] $nodeName
    ) {
    
    foreach($node in $nodes)
    {
        if($node.name -eq $nodeName)
        {
            return $node
        }
    }
    
}