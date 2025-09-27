using UpdateDebian.ActionHandlers;
using UpdateDebian.Constants;
using UpdateDebian.Interfaces;
using UpdateDebian.Models;

namespace UpdateDebian
{
    public class Startup
    {
        private readonly IArgsParser _argsParser;
        private readonly IDebianVersionParser<DebianVersion> _debianVersionParser;

        public Startup(
            IArgsParser argsParser,
            IDebianVersionParser<DebianVersion> debianVersionParser)
        {
            _argsParser = argsParser;
            _debianVersionParser = debianVersionParser;
        }

        public async Task Run(string[] args)
        {
            try
            {
                var version = await _debianVersionParser.ParseVersionAsync(LinuxConstants.OsReleaseFilePath);
                if (!LinuxConstants.SupportedVersions.Contains(version.Version))
                {
                    throw new Exception($"Unsupported Debian version: {version.Version}. Supported versions are: {string.Join(", ", LinuxConstants.SupportedVersions)}");
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