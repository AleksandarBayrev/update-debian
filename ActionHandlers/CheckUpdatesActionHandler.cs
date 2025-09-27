using System.Diagnostics;
using UpdateDebian.Interfaces;
using UpdateDebian.Models;

namespace UpdateDebian.ActionHandlers
{

    public class CheckUpdatesActionHandler : IActionHandler<DebianVersion>
    {
        public async Task HandleAsync(DebianVersion debianVersion)
        {
            await Console.Out.WriteLineAsync("Checking for updates for system...");
            var checkUpdatesProcess = Process.Start(new ProcessStartInfo
            {
                FileName = "apt",
                Arguments = "update",
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                UseShellExecute = false,
                CreateNoWindow = true
            });

            if (checkUpdatesProcess == null)
            {
                await Console.Out.WriteLineAsync("Failed to check for updates.");
                return;
            }

            string output = await checkUpdatesProcess.StandardOutput.ReadToEndAsync();
            string error = await checkUpdatesProcess.StandardError.ReadToEndAsync();

            await checkUpdatesProcess.WaitForExitAsync();

            if (checkUpdatesProcess.ExitCode != 0)
            {
                await Console.Out.WriteLineAsync("Failed to check for updates on the system.");
                await Console.Out.WriteLineAsync(error);
                return;
            }

            await Console.Out.WriteLineAsync(output);
        }
    }
}