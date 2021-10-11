$path = "$PSScriptRoot"
$build = $true
$buildPropsFileName = "$path\\Directory.Build.props"
$incrementBuildVersion = $true
$tempFiles = [System.Collections.ArrayList]::new()
$packageInfoPath = "$path\\PackageInfo.xml"

. "$path/Shared.ps1"

$xmlDoc = Load-XmlFile -path $packageInfoPath

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
    
    $ct = $tempFiles.Add($newPath)
    
    if($build -eq $true)
    {
        $destinationXml.Save($newPath)
        dotnet sln "final.sln" add "$newPath"
    }
}

if($build -eq $true)
{
    dotnet pack "final.sln" --nologo -v d --output "$path\\packages\\$buildVersion"
    $patch = 0
    if([System.Int32]::TryParse($versionInfo.Patch, [ref] $patch))
    {
       $versionInfo.Patch = ($patch + 1).ToString()
    }

    $xmlDoc.Save($packageInfoPath)
    foreach($file in $tempFiles) 
    {
        $fileInfo = Get-File-Info -filePath $file 
        $fileInfo.Delete()
    }

    $solutionFile = Get-File-Info -filePath "$path//final.sln"
}
