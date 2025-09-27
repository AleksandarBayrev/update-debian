namespace UpdateDebian
{
    public static class Actions
    {
        public const string CheckUpdates = "check-updates";
        public const string CheckBackports = "check-backports";
        public const string Upgrade = "upgrade";
        public static readonly IDictionary<string, string> ValidActions = new Dictionary<string, string>
        {
            { CheckUpdates, "checks for available updates." },
            { Upgrade, "upgrades the current version system packages." },
            { CheckBackports, "checks for updates in the backports repository." },
        };
    }
}