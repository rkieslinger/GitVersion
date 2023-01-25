namespace GitVersion.Commands;

public record OutputProjectSettings : OutputSettings
{
    [Option("--project-file", "The project file")]
    public required string ProjectFile { get; init; }
}
