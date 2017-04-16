#load nuget:https://www.myget.org/F/cake-contrib/api/v2?package=Cake.Recipe&prerelease

Environment.SetVariableNames();

BuildParameters.SetParameters(context: Context, 
                            buildSystem: BuildSystem,
                            sourceDirectoryPath: "./src",
                            title: "Cake.EntityFramework",
                            repositoryOwner: "louisfischer",
                            repositoryName: "Cake.EntityFramework",
                            appVeyorAccountName: "louisfischer");

BuildParameters.PrintParameters(Context);

ToolSettings.SetToolSettings(context: Context,
                            dupFinderExcludePattern: new string[] 
                            { 
                                BuildParameters.RootDirectoryPath + "/src/Cake.EntityFramework.TestProject.Postgres/**/*.cs",
                                BuildParameters.RootDirectoryPath + "/src/Cake.EntityFramework6.Tests/**/*.cs"
                            },
                            testCoverageFilter: "+[*]* -[xunit.*]* -[Cake.Core]* -[Cake.Testing]* -[*.Tests]* ",
                            testCoverageExcludeByAttribute: "*.ExcludeFromCodeCoverage*",
                            testCoverageExcludeByFile: "*/*Designer.cs;*/*.g.cs;*/*.g.i.cs");

Build.Run();
