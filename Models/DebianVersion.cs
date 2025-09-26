namespace UpdateDebian.Models
{
    public class DebianVersion
    {
        public string Version { get; init; } = "";
        public string Codename { get; init; } = "";

        public override string ToString()
        {
            return $"Debian {Version} ({Codename})";
        }
    }
}