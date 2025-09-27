using System.Text.Json;
using UpdateDebian.ActionHandlers;
using UpdateDebian.Constants;
using UpdateDebian.Interfaces;
using UpdateDebian.Models;
using UpdateDebian.Services;

namespace UpdateDebian
{
    public class Program
    {
        private static readonly IArgsParser _argsParser = new ArgsParser();
        private static readonly IDebianVersionParser<DebianVersion> _debianVersionParser = new DebianVersionParser();
        private static readonly string[] SupportedVersions = new[] { "12", "13"};

        public static async Task Main(string[] args)
        {
            try
            {
                var version = await _debianVersionParser.ParseVersionAsync(LinuxConstants.OsReleaseFilePath);
                if (!SupportedVersions.Contains(version.Version))
                {
                    throw new Exception($"Unsupported Debian version: {version.Version}. Supported versions are: {string.Join(", ", SupportedVersions)}");
                }

                await Console.Out.WriteLineAsync($"Detected {version}");

                if (Environment.UserName != LinuxConstants.RootUsername)
                {
                    throw new Exception("This application must be run as root.");
                }

                var action = _argsParser.GetAction(args);

                var handler = GetActionHandler(action);
                await handler.HandleAsync(version);
            }
            catch (Exception ex)
            {
                await Console.Out.WriteLineAsync($"Error: {ex.Message}");
            }
        }

        private static IActionHandler<DebianVersion> GetActionHandler(string action)
        {
            return action switch
            {
                Actions.CheckBackports => new CheckBackportsActionHandler(),
                Actions.Upgrade => new UpgradeActionHandler(),
                Actions.CheckUpdates => new CheckUpdatesActionHandler(),
                _ => throw new Exception($"Invalid action specified: {action}")
            };
        }
    }
}