<# Build the plugin and install it into the game.

    .\build.ps1              # build + deploy to the game next to this folder
    .\build.ps1 -NoDeploy    # build only
    .\build.ps1 -Reset       # also clear the first-run flag so the picker shows again
#>
param(
  [string]$GameDir = "$PSScriptRoot\..\SarahsHouse_v0.11.6",
  [switch]$NoDeploy,
  [switch]$Reset
)
$ErrorActionPreference = "Stop"
$plugin = "$GameDir\BepInEx\plugins\SarahsHouseI18n"

dotnet build "$PSScriptRoot\SarahsHouseI18n.csproj" -c Release --nologo -v minimal -p:GameDir="$GameDir"
if ($LASTEXITCODE -ne 0) { throw "build failed" }
if ($NoDeploy) { return }

if (Get-Process -Name "SarahsHouse" -ErrorAction SilentlyContinue) {
  throw "the game is running — close it first, Windows locks the loaded dll"
}

New-Item -ItemType Directory -Force -Path "$plugin\i18n","$plugin\ui","$plugin\fonts" | Out-Null
Copy-Item "$PSScriptRoot\bin\Release\SarahsHouseI18n.dll" "$plugin\" -Force
Copy-Item "$PSScriptRoot\..\packs\*.json" "$plugin\i18n\" -Force
if (Test-Path "$PSScriptRoot\..\packs\human.txt") { Copy-Item "$PSScriptRoot\..\packs\human.txt" "$plugin\i18n\" -Force }
Copy-Item "$PSScriptRoot\assets\ui\*" "$plugin\ui\" -Recurse -Force
# fonts/: only the prebuilt TMP bundles ship; the TTFs in assets/fonts are build-time
# build sources for the SDF font bundles; never read at runtime
Copy-Item "$PSScriptRoot\assets\fonts\*_u2021" "$plugin\fonts\" -Force
if ($Reset) { Remove-Item "$plugin\i18n\.lang_selected" -Force -ErrorAction SilentlyContinue }

"deployed -> $plugin"
