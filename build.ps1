param(
  [string]$Configuration = "Debug"
)

$ErrorActionPreference = "Stop"

dotnet build FacadeCompanyName.FacadeProjectName.sln -c $Configuration -m:1 -p:RestoreUseStaticGraphEvaluation=true
