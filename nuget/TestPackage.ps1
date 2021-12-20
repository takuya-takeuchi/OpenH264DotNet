#***************************************
#Arguments
#%1: Test Package (OpenH264DotNet.CUDA92)
#%2: Version of Release (19.17.0.yyyyMMdd)
#***************************************
Param([Parameter(
      Mandatory=$True,
      Position = 1
      )][string]
      $Package,

      [Parameter(
      Mandatory=$True,
      Position = 2
      )][string]
      $Version,

      [Parameter(
      Mandatory=$True,
      Position = 3
      )][string]
      $PlatformTarget,

      [Parameter(
      Mandatory=$True,
      Position = 4
      )][string]
      $RuntimeIdentifier
)

Set-StrictMode -Version Latest

function Clear-PackakgeCache([string]$Package, [string]$Version)
{
   # Linux is executed on container
   if ($global:IsWindows -or $global:IsMacOS)
   {
      $path = (dotnet nuget locals global-packages --list).Replace('info : global-packages: ', '').Trim()
      if ($path)
      {
         $path = (dotnet nuget locals global-packages --list).Replace('global-packages: ', '').Trim()
      }
      $path =  Join-Path $path $Package | `
               Join-Path -ChildPath $Version
      if (Test-Path $path)
      {
         Write-Host "Remove '$path'" -Foreground Green
         Remove-Item -Path "$path" -Recurse -Force
      }
   }
}

function RunTest($BuildTargets)
{
   foreach($BuildTarget in $BuildTargets)
   {
      $package = $BuildTarget.Package

      # Test
      $WorkDir = Join-Path $OpenH264DotNet work
      $NugetDir = Join-Path $OpenH264DotNet nuget
      $TestDir = Join-Path $NugetDir artifacts | `
                  Join-Path -ChildPath test | `
                  Join-Path -ChildPath $package | `
                  Join-Path -ChildPath $Version | `
                  Join-Path -ChildPath $RuntimeIdentifier

      if (!(Test-Path "$WorkDir")) {
         New-Item "$WorkDir" -ItemType Directory > $null
      }
      if (!(Test-Path "$TestDir")) {
         New-Item "$TestDir" -ItemType Directory > $null
      }

      $env:OpenH264DotNet_VERSION = $VERSION
      $env:OpenH264DotNet_GUI_SUPPORT = 1

      $TestDir = Join-Path $OpenH264DotNet test | `
                 Join-Path -ChildPath OpenH264DotNet.Tests

      $TargetDir = Join-Path $WorkDir OpenH264DotNet.Tests
      if (Test-Path "$TargetDir") {
         Remove-Item -Path "$TargetDir" -Recurse -Force > $null
      }

      Copy-Item "$TestDir" "$WorkDir" -Recurse

      Set-Location -Path "$TargetDir"

      Clear-PackakgeCache -Package $Package -Version $Version

      # restore package from local nuget pacakge
      # And drop stdout message
      dotnet remove reference "..\..\src\OpenH264DotNet\OpenH264DotNet.csproj" > $null
      dotnet add package $package -v $VERSION --source "$NugetDir" > $null

      $ErrorActionPreference = "silentlycontinue"
      $env:PlatformTarget = $PlatformTarget
      $dotnetPath = ""
      $runsetting = ""
      if ($global:IsWindows)
      {
         switch($PlatformTarget)
         {
            "x64"
            {
               $dotnetPath = Join-Path $env:ProgramFiles "dotnet\dotnet.exe"
            }
            "x86"
            {
               $dotnetPath = Join-Path ${env:ProgramFiles(x86)} "dotnet\dotnet.exe"
            }
         }
      }
      else
      {
         $dotnetPath = "dotnet"
      }

      switch($PlatformTarget)
      {
         "x64"
         {
            $runsetting = "x64.runsettings"
         }
         "x86"
         {
            $runsetting = "x86.runsettings"
         }
      }

      Write-Host "${dotnetPath} test -c Release -r "$TestDir" -s $runsetting --logger trx" -Foreground Yellow
      & ${dotnetPath} test -c Release -r "$TestDir" -s $runsetting --logger trx
      if ($lastexitcode -eq 0) {
         Write-Host "Test Successful" -ForegroundColor Green
      } else {
         Write-Host "Test Fail for $package" -ForegroundColor Red
         Set-Location -Path $Current
         exit -1
      }

      $ErrorActionPreference = "continue"

      # move to current
      Set-Location -Path "$Current"

      # to make sure, delete
      if (Test-Path "$WorkDir") {
         Remove-Item -Path "$WorkDir" -Recurse -Force
      }
   }
}

$BuildTargets = @()
$BuildTargets += New-Object PSObject -Property @{Target = "cpu";  Architecture = 64; CUDA = 0;   Package = "OpenH264DotNet"         }

# Store current directory
$Current = Get-Location
$OpenH264DotNet = (Split-Path (Get-Location) -Parent)

$targets = $BuildTargets.Where({$PSItem.Package -eq $Package})
RunTest $targets

# Move to Root directory
Set-Location -Path $Current