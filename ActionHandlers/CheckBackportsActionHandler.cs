using System.Diagnostics;
using UpdateDebian.Interfaces;
using UpdateDebian.Models;

namespace UpdateDebian.ActionHandlers
{
    public class CheckBackportsActionHandler : IActionHandler<DebianVersion>
    {
        public async Task HandleAsync(DebianVersion debianVersion)
        {
            await Console.Out.WriteLineAsync("Checking for updated versions in backports...");
            var checkUpdateProcess = Process.Start(new ProcessStartInfo
            {
                FileName = "apt",
                Arguments = $"update -t {GetBackportName(debianVersion)}",
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                UseShellExecute = false,
                CreateNoWindow = true
            });

            if (checkUpdateProcess == null)
            {
                throw new Exception("Failed to check for updates!");
            }

            await checkUpdateProcess.WaitForExitAsync();

            var hasErrors = await GetErrorOutput(checkUpdateProcess);

            if (!string.IsNullOrWhiteSpace(hasErrors))
            {
                throw new Exception($"Error while checking for updates: {hasErrors}");
            }

            await Console.Out.WriteLineAsync(await checkUpdateProcess.StandardOutput.ReadToEndAsync());

            var listUpdatesProcess = Process.Start(new ProcessStartInfo
            {
                FileName = "apt",
                Arguments = $"list --upgradable -t {GetBackportName(debianVersion)}",
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                UseShellExecute = false,
                CreateNoWindow = true
            });

            if (listUpdatesProcess == null)
            {
                throw new Exception("Failed to check for backports!");
            }

            hasErrors = await GetErrorOutput(listUpdatesProcess);

            if (!string.IsNullOrWhiteSpace(hasErrors))
            {
                throw new Exception($"Error while listing updates: {hasErrors}");
            }

            await listUpdatesProcess.WaitForExitAsync();

            var output = await listUpdatesProcess.StandardOutput.ReadToEndAsync();
            var backportsUpdatedPackagesList = output.Split(Environment.NewLine).Where(x => x.Contains("-backports"));

            if (!backportsUpdatedPackagesList.Any())
            {
                await Console.Out.WriteLineAsync("No packages have newer versions in backports.");
                return;
            }

            await Console.Out.WriteLineAsync("The following packages have newer versions in backports:");
            await Console.Out.WriteLineAsync(
                string.Join(Environment.NewLine, backportsUpdatedPackagesList)
            );

        }

        private static async Task<string> GetErrorOutput(Process process)
        {
            return (await process.StandardError.ReadToEndAsync()).Split(Environment.NewLine).FirstOrDefault(line => !string.IsNullOrWhiteSpace(line) && !line.Contains("WARNING: apt does not have a stable CLI interface. Use with caution in scripts.")) ?? "";
        }

        private static string GetBackportName(DebianVersion debianVersion)
        {
            return $"{debianVersion.Codename}-backports";
        }
    }
}