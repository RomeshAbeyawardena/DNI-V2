
function Load-XmlFile(
    [string] $path
    ) {

    $xmlDoc = [System.Xml.XmlDocument]::new()
    $xmlDoc.Load("$path")
    return $xmlDoc
}

function Find-File(
        [Array] $files,
        [string]$fileName) {
   
    foreach($file in $files)
    {
        $fileInfo = Get-File-Info -filePath $file
        
        if($fileName -eq $fileInfo.Name)
        {
            return $file
        }
    }
}

function Get-File-Info (
    [string] $filePath
) {
   return [System.IO.FileInfo]::new($filePath) 
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


function Get-VersionString(
    [object] $version
    ) {

    $major = $version.Attributes.GetNamedItem("Major").Value
    $minor = $version.Attributes.GetNamedItem("Minor").Value
    $revision = $version.Attributes.GetNamedItem("Revision").Value
    $patch = $version.Attributes.GetNamedItem("Patch").Value

    return [System.String]::Format("{0}.{1}.{2}.{3}", $major, $minor, $revision, $patch)
}
