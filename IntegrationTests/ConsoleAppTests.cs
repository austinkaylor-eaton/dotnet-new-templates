using System.Diagnostics;
using Microsoft.Extensions.Logging;
using Microsoft.TemplateEngine.Authoring.TemplateVerifier;

namespace IntegrationTests;

public class ConsoleAppTests
{
    private static ILogger<ConsoleAppTests> _logger;

    [Before(Class)]
    public static void SetUp()
    {
        _logger = new LoggerFactory().CreateLogger<ConsoleAppTests>();
        Console.WriteLine("Or you can define methods that do stuff before...");
    }

    [Skip("Need more research to figure this out")]
    [Test]
    public async Task Test1()
    {
        TemplateVerifierOptions options = new(templateName: "ajk-console-di")
        {
            TemplatePath = Path.GetFullPath(@"..\..\..\..\templates\templates\console-with-dependency-injection"),
            SnapshotsDirectory = Directory.CreateTempSubdirectory().ToString(),
            VerifyCommandOutput = true
        };

        VerificationEngine engine = new(_logger);
        await engine.Execute(options);
    }

    [Test]
    public async Task LongTest()
    {
        const string templateShortName = "ajk-console-di";
        const string testName = nameof(FirstTest);
        string templatePath = Path.GetFullPath(@"..\..\..\..\templates\templates\console-with-dependency-injection");
        DirectoryInfo info = new DirectoryInfo(templatePath);
        string[] templateFilesWithPath = Directory.GetFiles(templatePath, "*.*", SearchOption.AllDirectories)
            .Where(file => file.EndsWith(".cs") || file.EndsWith(".csproj"))
            .ToArray();

        List<string> templateFiles = templateFilesWithPath.Select(file => file.Replace(templatePath, "").TrimStart('\\')).ToList();

        // each template file is going to have a name and contents


        // Set the name to be used when running dotnet new as the name of the test method
        // Get the local path of the template
        // Read in the template.json file
        // Parse the template.json file
        // Get the source name from template.json
        // Get all the templates files in the template directory
        // Create copies of the template files, replacing the source name with the test name
        // Have both source template files and expected template files in the same list
        // Get the contents of each template file, putting the contents in a list

    }

    [Test]
    [Skip("This test is not ready yet")]
    public Task FirstTest()
    {
        /*********************** SET UP - START ***********************/
        Console.Title = "ConsoleAppTests.FirstTest";
        const string templateShortName = "ajk-console-di";
        const string testName = nameof(FirstTest);
        string templatePath = Path.GetFullPath(@"..\..\..\..\templates\templates\console-with-dependency-injection");
        string[] templateFilesWithPath = Directory.GetFiles(templatePath, "*.*", SearchOption.AllDirectories)
            .Where(file => file.EndsWith(".cs") || file.EndsWith(".csproj"))
            .ToArray();

        List<string> templateFiles = templateFilesWithPath.Select(file => file.Replace(templatePath, "").TrimStart('\\')).ToList();


        // Create a new folder in the output path
        DirectoryInfo testDirectory = Directory.CreateTempSubdirectory("Templates.Tests");
        // Make sure dotnet sdk is installed
        if (File.Exists(@"C:\Program Files\dotnet\dotnet.exe") == false)
        {
            return Task.FromException(
                new ApplicationException("Dotnet SDK is not installed. Please install it from https://dotnet.microsoft.com/download"));
        }
        // Create a new solution in the output path
        using (Process createNewSolution = new())
        {
            createNewSolution.StartInfo.UseShellExecute = false;
            createNewSolution.StartInfo.FileName = @"C:\Program Files\dotnet\dotnet.exe";
            createNewSolution.StartInfo.CreateNoWindow = true;
            createNewSolution.StartInfo.Arguments = $"new sln -o {testDirectory.FullName}/{testName} --name {testName}";
            createNewSolution.Start();
            // This code assumes the process you are starting will terminate itself.
            // Given that it is started without a window so you cannot terminate it
            // on the desktop, it must terminate itself, or you can do it programmatically
            // from this application using the Kill method.
            createNewSolution.WaitForExit();
        }
        // Install the template to dotnet new
        using (Process installTemplate = new())
        {
            installTemplate.StartInfo.UseShellExecute = false;
            installTemplate.StartInfo.FileName = @"C:\Program Files\dotnet\dotnet.exe";
            installTemplate.StartInfo.CreateNoWindow = true;
            installTemplate.StartInfo.Arguments = $"new install {templatePath}";
            installTemplate.Start();
            // This code assumes the process you are starting will terminate itself.
            // Given that it is started without a window so you cannot terminate it
            // on the desktop, it must terminate itself, or you can do it programmatically
            // from this application using the Kill method.
            installTemplate.WaitForExit();
        }
        // Make sure the template is installed
        using (Process verifyTemplate = new())
        {
            verifyTemplate.StartInfo.UseShellExecute = false;
            verifyTemplate.StartInfo.FileName = @"C:\Program Files\dotnet\dotnet.exe";
            verifyTemplate.StartInfo.CreateNoWindow = true;
            verifyTemplate.StartInfo.Arguments = $"new list {templateShortName}";
            verifyTemplate.StartInfo.RedirectStandardOutput = true;
            verifyTemplate.StartInfo.RedirectStandardError = true;
            verifyTemplate.Start();
            // This code assumes the process you are starting will terminate itself.
            // Given that it is started without a window so you cannot terminate it
            // on the desktop, it must terminate itself, or you can do it programmatically
            // from this application using the Kill method.
            string output = verifyTemplate.StandardOutput.ReadToEnd();

            if (verifyTemplate.ExitCode == 103)
            {
                //https://github.com/dotnet/templating/blob/main/docs/Exit-Codes.md#103
                return Task.FromException(
                    new ApplicationException($"Template is not installed. Please install it from {templatePath}"));
            }

            if(string.IsNullOrEmpty(output))
            {
                return Task.FromException(
                    new ApplicationException($"Template is not installed. Please install it from {templatePath}"));
            }
            verifyTemplate.WaitForExit();
        }

        // Use the template to create a new project
        using (Process createNewProject = new())
        {
            createNewProject.StartInfo.UseShellExecute = false;
            createNewProject.StartInfo.FileName = @"C:\Program Files\dotnet\dotnet.exe";
            createNewProject.StartInfo.CreateNoWindow = true;
            createNewProject.StartInfo.Arguments = $"new {templateShortName} -o {testDirectory.FullName}\\{testName} --name {testName}";
            createNewProject.Start();
            // This code assumes the process you are starting will terminate itself.
            // Given that it is started without a window so you cannot terminate it
            // on the desktop, it must terminate itself, or you can do it programmatically
            // from this application using the Kill method.
            createNewProject.WaitForExit();
        }

        // Add the project to the solution
        using (Process addProjectToSolution = new())
        {
            addProjectToSolution.StartInfo.UseShellExecute = false;
            addProjectToSolution.StartInfo.FileName = @"C:\Program Files\dotnet\dotnet.exe";
            addProjectToSolution.StartInfo.CreateNoWindow = true;
            addProjectToSolution.StartInfo.Arguments = $"sln {testDirectory.FullName}\\{testName}\\{testName}.sln add {testDirectory.FullName}\\{testName}\\{testName}.csproj";
            addProjectToSolution.Start();
            // This code assumes the process you are starting will terminate itself.
            // Given that it is started without a window so you cannot terminate it
            // on the desktop, it must terminate itself, or you can do it programmatically
            // from this application using the Kill method.
            addProjectToSolution.WaitForExit();
        }

        // Build the project
        using (Process buildProject = new())
        {
            buildProject.StartInfo.UseShellExecute = false;
            buildProject.StartInfo.FileName = @"C:\Program Files\dotnet\dotnet.exe";
            buildProject.StartInfo.CreateNoWindow = true;
            buildProject.StartInfo.Arguments = $"build {testDirectory.FullName}\\{testName}\\{testName}.sln";
            buildProject.Start();
            // This code assumes the process you are starting will terminate itself.
            // Given that it is started without a window so you cannot terminate it
            // on the desktop, it must terminate itself, or you can do it programmatically
            // from this application using the Kill method.
            buildProject.WaitForExit();
        }

        // Check to make sure the template files are in the project

        /*********************** SET UP - END ***********************/

        /*********************** CLEAN UP - START ***********************/
        // Uninstall the template
        using (Process uninstallTemplate = new())
        {
            uninstallTemplate.StartInfo.UseShellExecute = false;
            uninstallTemplate.StartInfo.FileName = @"C:\Program Files\dotnet\dotnet.exe";
            uninstallTemplate.StartInfo.CreateNoWindow = true;
            uninstallTemplate.StartInfo.Arguments = $"new uninstall {templatePath}";
            uninstallTemplate.Start();
            // This code assumes the process you are starting will terminate itself.
            // Given that it is started without a window so you cannot terminate it
            // on the desktop, it must terminate itself, or you can do it programmatically
            // from this application using the Kill method.
            uninstallTemplate.WaitForExit();
        }
        // Clean up the new folder in the output path
        Directory.Delete(testDirectory.FullName, true);
        /*********************** CLEAN UP - END ***********************/

        return Task.CompletedTask;
    }
}