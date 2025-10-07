using UpdateDebian.Interfaces;
using UpdateDebian.Models;

namespace UpdateDebian.ActionHandlers
{
    public class HistoryActionHandler : IActionHandler<DebianVersion>
    {
        public async Task HandleAsync(DebianVersion debianVersion)
        {
            await Console.Out.WriteLineAsync("Getting apt history...");
            await Console.Out.WriteLineAsync(
                string.Join(
                    Environment.NewLine,
                    await File.ReadAllLinesAsync(Path.GetFullPath(Constants.LinuxConstants.AptHistoryFile))
                )
            );

            //TODO: Add grouping for each type - install, update, remove and present them
        }
    }
}