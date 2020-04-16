

#region Create
function New-Author { #assuming AuthorID is autogenerated by database
    Param (
        [Parameter(Mandatory)]
        [string] $FirstName,
        [Parameter(Mandatory)]
        [string] $LastName
    )
    # using direct sql statement
    $query = "insert into Authors (AuthorID, FirstName, LastName) values "
    $query += "('$AuthorID', '$FirstName', '$LastName')"

    # # using stored procedure
    # $query = "exec CreateAuthor @AuthorID = '$AuthorID',@FirstName = '$FirstName',@LastName = '$LastName'"
    
    #execute statement
    return Invoke-Sqlcmd -ConnectionString $Global:ConnectionString -Query $query
}

function New-Book { #assuming AuthorID is autogenerated by database
    Param (
        [Parameter(Mandatory)]
        [string] $FirstName,
        [Parameter(Mandatory)]
        [string] $LastName
    )
    # using direct sql statement
    $query = "insert into Authors (AuthorID, FirstName, LastName) values "
    $query += "('$AuthorID', '$FirstName', '$LastName')"

    # # using stored procedure
    # $query = "exec CreateAuthor @AuthorID = '$AuthorID',@FirstName = '$FirstName',@LastName = '$LastName'"
    
    #execute statement
    return Invoke-Sqlcmd -ConnectionString $Global:ConnectionString -Query $query
}
#endregion Create

#region Read
function Select-Author {
    param (
        [int]$AuthorID = $null
    )
    #direct sql statement
    $query = "select AuthorID, FirstName, LastName from Authors"

    # #stored procedure
    # $query = "exec GetAuthors"

    if ($AuthorID -ne $null) {
        #for direct sql
        $query += " where AuthorID = $AuthorID"

        # #for stored procedure
        # $query += " @AuthorID = '$AuthorID'"
    }
    #execute statement
    return Invoke-Sqlcmd -ConnectionString $Global:ConnectionString -Query $query
}
#endregion Read

#region Update
function Update-Author {
    param (
        [Parameter(Mandatory)]
        [int] $AuthorID,
        [string] $FirstName = $null,
        [string] $LastName = $null
    )
     # using direct sql statement
     $query = "update a "
     $query += "set a.FirstName = iif(a.FirstName <> '$FirstName', '$FirstName', a.FirstName), "
     $query += "set a.LastName = iif(a.LastName <> '$LastName', '$LastName', a.LastName) "
     $query += "from Authors a "
     $query += "where a.AuthorID = $AuthorID"
 
    #  # using stored procedure
    #  $query = "exec UpdateAuthor @AuthorID = $AuthorID,@FirstName = '$FirstName',@LastName = '$LastName'"
     
     #execute statement
     return Invoke-Sqlcmd -ConnectionString $Global:ConnectionString -Query $query
}
#endregion Read

#region Delete
function Remove-Author {
    param (
        [Parameter(Mandatory)]
        [int] $AuthorID
    )
     # using direct sql statement
     $query = "delete from Authors "
     $query += "where AuthorID = $AuthorID"
 
    #  # using stored procedure
    #  $query = "exec DeleteAuthor @AuthorID = $AuthorID"
     
     #execute statement
     return Invoke-Sqlcmd -ConnectionString $Global:ConnectionString -Query $query
}
#endregion Delete

#region Utils
function Get-DatabaseConnection {
    $pathtoModuleConfig = (Resolve-Path($PSScriptRoot + "\dataservice.config")).Path
    [xml] $doc = Get-Content -Path $pathtoModuleConfig
    return $doc.Settings.DatabaseConnectionString
}
#endregion Utils

#region Exports

$Global:ConnectionString = Get-DatabaseConnection

Export-ModuleMember New-*, Select-*,Update-*,Remove-*, Get-DatabaseConnection
#endregion Exports