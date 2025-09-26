using UpdateDebian.Interfaces;
using UpdateDebian.Models;

namespace UpdateDebian.Services
{
    public class DebianVersionParser : IDebianVersionParser<DebianVersion>
    {
        public async Task<DebianVersion> ParseVersionAsync(string fileName)
        {
            var fileContents = (await File.ReadAllTextAsync(fileName))?.Split(Environment.NewLine) ?? [];

            if (fileContents.Length == 0)
            {
                throw new Exception($"Cannot parse Debian version from file {fileName}");
            }

            var props = ParseFileContents(fileContents);

            if (!props.ContainsKey("ID") || props["ID"] != "debian")
            {
                throw new Exception($"File {fileName} does not contain Debian version information");
            }

            return new DebianVersion
            {
                Version = props["VERSION_ID"],
                Codename = props["VERSION_CODENAME"]
            };
        }

        private static Dictionary<string, string> ParseFileContents(IEnumerable<string> fileContents)
        {
            var props = new Dictionary<string, string>();

            foreach (var line in fileContents)
            {
                var parts = line.Split('=');

                if (parts.Length == 2)
                {
                    props[parts[0].Trim()] = parts[1].Trim().Trim('"');
                }
            }

            return props;
        }
    }
}