namespace IntegrationTests;

/// <summary>
/// Represents a template file
/// </summary>
public class TemplateFile
{
    /// <summary>
    /// The name of the template file
    /// </summary>
    public string Name { get; private set; }

    /// <summary>
    /// The contents of the template file
    /// </summary>
    public string Contents { get; private set; }
}