namespace UpdateDebian.Constants
{
    public static class LinuxConstants
    {
        public const string RootUsername = "root";
        public const string OsReleaseFilePath = "/etc/os-release";
        public static readonly string[] SupportedVersions = new[] { "12", "13" };
        public const string AptHistoryFile = "/var/log/apt/history.log";
    }
}