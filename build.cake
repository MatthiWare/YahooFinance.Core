#tool "nuget:?package=xunit.runner.console&version=2.4.1"

///////////////////////////////////////////////////////////////////////////////
// ARGUMENTS
///////////////////////////////////////////////////////////////////////////////

var target = Argument("target", "Default");
var configuration = Argument("configuration", "Release");

//////////////////////////////////////////////////////////////////////
// PARAMETERS
//////////////////////////////////////////////////////////////////////

var project = "YahooFinance.Core";
var solution = $"{project}/{project}.sln";
var cmdParserProject = $"{project}/{project}/{project}.csproj";
var tests = $"{project}/{project}.Tests/{project}.Tests.csproj";
var publishPath = MakeAbsolute(Directory("./output"));
var nugetPackageDir = MakeAbsolute(Directory("./nuget"));

///////////////////////////////////////////////////////////////////////////////
// TASKS
///////////////////////////////////////////////////////////////////////////////

Task("Build")
    .Does(() => 
	{
		DotNetCoreBuild(solution,
			new DotNetCoreBuildSettings 
			{
				NoRestore = false,
				Configuration = configuration
			});
	});

Task("Test")
    .IsDependentOn("Build")
    .Does( () => {
		
        var testSettings = new DotNetCoreTestSettings {
                NoBuild = true,
                NoRestore = true,
                Configuration = configuration
            };

		DotNetCoreTest(tests, testSettings);
});

Task("AppVeyor")
    .IsDependentOn("Test");

RunTarget(target);