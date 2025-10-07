using UpdateDebian.Interfaces;
using UpdateDebian.Models;

namespace UpdateDebian.ActionHandlers
{
    public class HistoryActionHandler : IActionHandler<DebianVersion>
    {
        public async Task HandleAsync(DebianVersion debianVersion)
        {
            await Console.Out.WriteLineAsync("Getting apt history...");
            var file = await File.ReadAllLinesAsync(Path.GetFullPath(Constants.LinuxConstants.AptHistoryFile));
            await Console.Out.WriteLineAsync(string.Join(Environment.NewLine, file));
        }
    }
}