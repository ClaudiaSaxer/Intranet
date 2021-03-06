// Read start arguments
var target = Argument("target", "Default");
var configuration = Argument("configuration", "Release");
var verbosity = Argument("verbosity", "Diagnostic");

// Paths to the root directories
var sourceDirectory = Directory("../.Src");
var sourcecodeDirectory = Directory("../.Src/Intranet");
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
    CleanDirectory( sourcecodeDirectory + Directory("Bll/bin") );
    CleanDirectory( sourcecodeDirectory + Directory("Common/bin") );
    CleanDirectory( sourcecodeDirectory + Directory("Dal/bin") );
    CleanDirectory( sourcecodeDirectory + Directory("Definitions/bin") );
    CleanDirectory( sourcecodeDirectory + Directory("Model/bin") );
    CleanDirectory( sourcecodeDirectory + Directory("ViewModel/bin") );
    CleanDirectory( sourcecodeDirectory + Directory("Web/bin") );
    CleanDirectory( sourcecodeDirectory + Directory("Labor/Definitions/bin") );
	CleanDirectory( sourcecodeDirectory + Directory("Labor/Bll/bin") );
    CleanDirectory( sourcecodeDirectory + Directory("Labor/Dal/bin") );
	CleanDirectory( sourcecodeDirectory + Directory("Labor/Definitions/bin") );
    CleanDirectory( sourcecodeDirectory + Directory("Labor/Model/bin") );
	CleanDirectory( sourcecodeDirectory + Directory("Labor/ViewModel/bin") );
    CleanDirectory( testDirectory + Directory("Bll/bin") );    
    CleanDirectory( testDirectory + Directory("Common/bin") );
    CleanDirectory( testDirectory + Directory("Dal/bin") );
	CleanDirectory( testDirectory + Directory("Integrationtest/bin") );
    CleanDirectory( testDirectory + Directory("Model/bin") );
    CleanDirectory( testDirectory + Directory("ViewModel/bin") );
    CleanDirectory( testDirectory + Directory("TestEnvironment/bin") );
    CleanDirectory( testDirectory + Directory("Web/bin") );
	CleanDirectory( testDirectory + Directory("Labor/Bll/bin") );
	CleanDirectory( testDirectory + Directory("Labor/Dal/bin") );
	CleanDirectory( testDirectory + Directory("Labor/Model/bin") );
	CleanDirectory( testDirectory + Directory("Labor/ViewModel/bin") );
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
	//  XUnit2( testDirectory.ToString()+ "/**/bin/" + configuration + "/*.Test.dll", new XUnit2Settings
	XUnit2(new []{
		testDirectory.ToString()+"/Web/bin/Release/Intranet.Web.Test.dll",
		testDirectory.ToString()+"/Bll/bin/Release/Intranet.Bll.Test.dll",
		testDirectory.ToString()+"/Common/bin/Release/Intranet.Common.Test.dll",
		testDirectory.ToString()+"/Dal/bin/Release/Intranet.Dal.Test.dll",
		testDirectory.ToString()+"/Labor/Bll/bin/Release/Intranet.Labor.Bll.Test.dll",
		testDirectory.ToString()+"/Labor/Dal/bin/Release/Intranet.Labor.Dal.Test.dll",
		testDirectory.ToString()+"/Labor/Model/bin/Release/Intranet.Labor.Model.Test.dll",
		testDirectory.ToString()+"/Labor/ViewModel/bin/Release/Intranet.Labor.ViewModel.Test.dll",
		testDirectory.ToString()+"/ViewModel/bin/Release/Intranet.ViewModel.Test.dll"
	}, new XUnit2Settings
		{ 
			ToolPath = xUnit,
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