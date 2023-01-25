using GitVersion.Infrastructure;

namespace GitVersion.Commands;

[Command<OutputCommand>("wix", "Outputs version to wix file")]
public class OutputWixCommand : ICommand<OutputWixSettings>
{
    private readonly ILogger logger;
    private readonly IService service;

    public OutputWixCommand(ILogger logger, IService service)
    {
        this.logger = logger;
        this.service = service;
    }

    public Task<int> InvokeAsync(OutputWixSettings settings)
    {
        var value = service.Call();
        logger.LogInformation($"Command : 'output wix', LogFile : '{settings.LogFile}', WorkDir : '{settings.OutputDir}', InputFile: '{settings.InputFile}', WixFile: '{settings.WixFile}' ");
        return Task.FromResult(value);
    }
}
