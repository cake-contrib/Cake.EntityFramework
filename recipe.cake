/*
* https://cake-contrib.github.io/Cake.Recipe/docs/usage/creating-release
* https://cake-contrib.github.io/Cake.Recipe/docs/fundamentals/environment-variables

*[Environment]::SetEnvironmentVariable("GITHUB_USERNAME", "")
*[Environment]::SetEnvironmentVariable("GITHUB_PASSWORD", "")
*/

#load nuget:?package=Cake.Recipe&version=1.0.0

Environment.SetVariableNames();

BuildParameters.SetParameters(context: Context,
                            buildSystem: BuildSystem,
                            sourceDirectoryPath: "./src",
                            title: "Cake.EntityFramework",
                            repositoryOwner: "cake-contrib",
                            repositoryName: "Cake.EntityFramework",
                            appVeyorAccountName: "cakecontrib",
                            shouldRunGitVersion: true);

BuildParameters.PrintParameters(Context);

ToolSettings.SetToolSettings(context: Context,
                            dupFinderExcludePattern: new string[]
                            {
                                BuildParameters.RootDirectoryPath + "/src/Cake.EntityFramework.TestProject.Postgres/**/*.cs",
                                BuildParameters.RootDirectoryPath + "/src/Cake.EntityFramework.TestProject.SqlServer/**/*.cs",
                                BuildParameters.RootDirectoryPath + "/src/Cake.EntityFramework.Tests/**/*.cs"
                            },
                            testCoverageFilter: "+[*]* -[xunit.*]* -[Cake.Core]* -[Cake.Testing]* -[*.Tests]* -[Cake.EntityFramework.TestProject.Postgres]* -[Cake.EntityFramework.TestProject.SqlServer]* -[FluentAssertions]* -[FluentAssertions.Core]* -[*]Costura.AssemblyLoader",
                            testCoverageExcludeByAttribute: "*.ExcludeFromCodeCoverage*",
                            testCoverageExcludeByFile: "*/*Designer.cs;*/*.g.cs;*/*.g.i.cs");

Build.Run();
