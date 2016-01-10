param($installPath, $toolsPath, $package, $project)

function RemoveFile
{
	param([string]$filePath)
	$file = Join-Path $toolsPath $filePath | Get-ChildItem

	$project.ProjectItems.Item($file.Name).Delete()
}

RemoveFile '..\content\nuspec.pkg'
RemoveFile '..\content\tools\install.pkg'
RemoveFile '..\content\tools\uninstall.pkg'