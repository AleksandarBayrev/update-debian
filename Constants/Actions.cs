namespace UpdateDebian
{
    public static class Actions
    {
        public const string CheckUpdates = "check-updates";
        public const string CheckBackports = "check-backports";
        public const string Upgrade = "upgrade";
        public static readonly string[] ValidActions = new[] { CheckBackports, Upgrade, CheckUpdates };
    }
}