$targets = @(
   "CPU"
)

$ScriptPath = $PSScriptRoot
$OpenH264DotNet = Split-Path $ScriptPath -Parent

$source = Join-Path $OpenH264DotNet src | `
          Join-Path -ChildPath OpenH264DotNet
dotnet restore ${source}
dotnet build -c Release ${source}

foreach ($target in $targets)
{
   pwsh CreatePackage.ps1 $target
}