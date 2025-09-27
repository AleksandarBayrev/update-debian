using System.Diagnostics;

namespace UpdateDebian
{
    public static class Helpers
    {
        public static async Task<string> GetErrorOutput(Process process)
        {
            if (process == null)
            {
                throw new Exception($"Process is null.");
            }

            return (await process.StandardError.ReadToEndAsync())
                .Split(Environment.NewLine)
                .FirstOrDefault(line =>
                    !string.IsNullOrWhiteSpace(line)
                    && !line.Contains("WARNING: apt does not have a stable CLI interface. Use with caution in scripts.")) ?? "";
        }
    }
}