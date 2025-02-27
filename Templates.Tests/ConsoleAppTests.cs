using System.Diagnostics;
using Microsoft.Extensions.Logging;
using Microsoft.TemplateEngine.Authoring.TemplateVerifier;

namespace Templates.Tests;

/// <summary>
/// Tests for Console App Templates
/// </summary>
/// <remarks>
/// Currently tests the following templates:
/// <para>
/// 1. ajk-console-di - A console app with dependency injection
/// </para>
/// </remarks>
public class ConsoleAppTests
{
    private readonly ILogger _logger;

    public ConsoleAppTests()
    {
        _logger = LoggerFactory.Create(builder =>
        {
            builder.AddConsole();
        }).CreateLogger<ConsoleAppTests>();
    }

    [Fact]
    public Task VerifyConsoleAppDependencyInjectionTemplate()
    {
        const string templateShortName = "ajk-console-di";
        string templatePath = Path.GetFullPath(@"..\..\..\..\templates\console-with-dependency-injection\.");
        string outputPath = Path.Combine(Path.GetTempPath(), "ajk-console-di");
        //string outputPath = Path.GetTempFileName();

        // Create a new folder in the output path
        DirectoryInfo testDirectory = Directory.CreateTempSubdirectory("Templates.Tests");
        // Make sure dotnet sdk is installed
        if (File.Exists(@"C:\Program Files\dotnet\dotnet.exe") == false)
        {
            _logger.LogError("Dotnet SDK is not installed. Please install it from https://dotnet.microsoft.com/download");
            return Task.CompletedTask;
        }
        // Create a new solution in the output path
        using (Process createNewSolution = new())
        {
            createNewSolution.StartInfo.UseShellExecute = false;
            createNewSolution.StartInfo.FileName = @"C:\Program Files\dotnet\dotnet.exe";
            createNewSolution.StartInfo.CreateNoWindow = true;
            createNewSolution.StartInfo.Arguments = $"new sln -o {testDirectory.FullName}/{nameof(VerifyConsoleAppDependencyInjectionTemplate)} --name {nameof(VerifyConsoleAppDependencyInjectionTemplate)}";
            createNewSolution.Start();
            // This code assumes the process you are starting will terminate itself.
            // Given that it is started without a window so you cannot terminate it
            // on the desktop, it must terminate itself or you can do it programmatically
            // from this application using the Kill method.
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
            // on the desktop, it must terminate itself or you can do it programmatically
            // from this application using the Kill method.
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
                _logger.LogError("Template is not installed. Please install it from {templatePath}", templatePath);
                return Task.CompletedTask;
            }

            if(string.IsNullOrEmpty(output))
            {
                _logger.LogError("Template is not installed. Please install it from {templatePath}", templatePath);
                return Task.CompletedTask;
            }
            verifyTemplate.WaitForExit();
        }
        // Clean up the new folder in the output path
        Directory.Delete(testDirectory.FullName, true);

        return Task.CompletedTask;
    }
}