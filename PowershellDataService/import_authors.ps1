#build module path (relative to script)
$pathtoModule = (Resolve-Path($PSScriptRoot + "\Modules\dataservice.psm1")).Path
#import modules
Import-Module $pathtoModule -Force
Import-Module sqlserver -Force

#use module functions
Select-Author -AuthorID 2