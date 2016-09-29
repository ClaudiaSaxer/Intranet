// Read start arguments
var target = Argument("target", "Default");
var configuration = Argument("configuration", "Release");
var verbosity = Argument("verbosity", "Diagnostic");

// Paths to the root directories
var sourceDirectory = Directory("../.Src");
var testDirectory = Directory("../.Src/Intranet.Testing");
var toolDirectory = Directory("../.Tools");
var buildDirectory = Directory("../.Build");
var outputDirectory = Directory("../.Output");
var outputNuGetDirectory = Directory("../.Output/NuGet");

// The path to the solution file
var solution = sourceDirectory + File("Intranet.sln");

// Executables
var nuGet = toolDirectory + File("NuGet/nuget.exe");
var xUnit = toolDirectory + File("XUnit/xunit.console.exe");

// Clean all build output
Task("Clean")
    .Does(() =>
{	
    CleanDirectory( sourceDirectory + Directory("Labor/Bll/bin") );
    CleanDirectory( sourceDirectory + Directory("Bll/bin") );
    CleanDirectory( sourceDirectory + Directory("Common/bin") );
    CleanDirectory( sourceDirectory + Directory("Dal/bin") );
    CleanDirectory( sourceDirectory + Directory("Labor/Dal/bin") );
    CleanDirectory( sourceDirectory + Directory("Definitions/bin") );
    CleanDirectory( sourceDirectory + Directory("Labor/Definitions/bin") );
    CleanDirectory( sourceDirectory + Directory("Model/bin") );
    CleanDirectory( sourceDirectory + Directory("ViewModel/bin") );
    CleanDirectory( sourceDirectory + Directory("Web/bin") );
    CleanDirectory( testDirectory + Directory("Bll/bin") );    
    CleanDirectory( testDirectory + Directory("Labor/Bll/bin") );
    CleanDirectory( testDirectory + Directory("Common/bin") );
    CleanDirectory( testDirectory + Directory("Dal/bin") );
    CleanDirectory( testDirectory + Directory("Labor/Dal/bin") );
    CleanDirectory( testDirectory + Directory("Model/bin") );
    CleanDirectory( testDirectory + Directory("ViewModel/bin") );
    CleanDirectory( testDirectory + Directory("Web/bin") );
    CleanDirectory( outputDirectory );
});

// Restore all NuGet packages
Task("RestorePackages")
    .IsDependentOn("Clean")
    .Does(() =>
{
    NuGetRestore(solution, new NuGetRestoreSettings
        { 
            ToolPath = nuGet
        } );
});

// Build the solution
Task("Build")
    .IsDependentOn("RestorePackages")
    .Does(() =>
{	
    MSBuild(solution, settings =>  
        settings.SetConfiguration(configuration)
            .SetVerbosity( Verbosity.Minimal ) );
});

// Run the unit tests
Task("RunTests")
    .IsDependentOn("Build")
    .Does(() =>
{    
    XUnit2( testDirectory.ToString() + "/**/bin/" + configuration + "/*.Test.dll", new XUnit2Settings
		{ 
			ToolPath = xUnit,
			Parallelism = ParallelismOption.All,
			HtmlReport = true,
			NoAppDomain = true,
			OutputDirectory = outputDirectory
		});    
});


// Default task
Task("Default")
  .IsDependentOn("RunTests")
  .Does(() =>
{
    Information("Default task started");
});

RunTarget(target);

/// <summary>
/// Gets the version of the current build.
/// </summary>
/// <returns>Returns the version of the current build.</returns>
private String GetBuildVersion()
{
    // Try to get the version from AppVeyor
    var appVeyorProvider = BuildSystem.AppVeyor;
    if(appVeyorProvider.IsRunningOnAppVeyor)
        return appVeyorProvider.Environment.Build.Version;

    // Get the version from the built DLL
    var outputDll = System.IO.Directory.EnumerateFiles(outputNuGetDirectory, "*", System.IO.SearchOption.AllDirectories).First(x => x.Contains(".dll"));
    var assembly = System.Reflection.Assembly.LoadFile(  MakeAbsolute(File(outputDll)).ToString() );
    var version = assembly.GetName().Version;
    return version.ToString();
}