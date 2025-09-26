using System.Diagnostics;
using UpdateDebian.Interfaces;
using UpdateDebian.Models;

namespace UpdateDebian.ActionHandlers
{
    public class UpdateActionHandler : IActionHandler<DebianVersion>
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

            var updateSystemProcess = Process.Start(new ProcessStartInfo
            {
                FileName = "apt",
                Arguments = "dist-upgrade -y",
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                UseShellExecute = false,
                CreateNoWindow = true
            });

            if (updateSystemProcess == null)
            {
                await Console.Out.WriteLineAsync("Failed to update the system.");
                return;
            }

            string output = await updateSystemProcess.StandardOutput.ReadToEndAsync();
            string error = await updateSystemProcess.StandardError.ReadToEndAsync();

            await updateSystemProcess.WaitForExitAsync();

            if (updateSystemProcess.ExitCode == 0)
            {
                await Console.Out.WriteLineAsync("System updated successfully.");
                await Console.Out.WriteLineAsync(output);
            }
            else
            {
                await Console.Out.WriteLineAsync("Failed to update the system.");
                await Console.Out.WriteLineAsync(error);
            }
        }
    }
}