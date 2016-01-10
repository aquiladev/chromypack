param($installPath, $toolsPath, $package, $project)

function CopyFile
{
	param([string]$filePath)
	$file = Join-Path $toolsPath $filePath | Get-ChildItem

	$project.ProjectItems.Item($file.Name).Delete()

	$project.ProjectItems.AddFromFile($file.FullName);
	$pi = $project.ProjectItems.Item($file.Name);
	$pi.Properties.Item("BuildAction").Value = [int]2;
	$pi.Properties.Item("CopyToOutputDirectory").Value = [int]2;
}

CopyFile '..\content\nuspec.pkg'
CopyFile '..\content\tools\install.pkg'
CopyFile '..\content\tools\uninstall.pkg'