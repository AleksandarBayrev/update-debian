using UpdateDebian.Interfaces;

namespace UpdateDebian.Services
{
    public class ArgsParser : IArgsParser
    {
        private static Exception GetErrorMessage()
        {
            return new Exception($"No valid action specified, valid actions are:{Environment.NewLine}{string.Join(Environment.NewLine, Actions.ValidActions.Select(kv => $" - {kv.Key}: {kv.Value}"))}");
        }

        public string GetAction(string[] args)
        {
            if (args.Length != 1)
            {
                throw GetErrorMessage();
            }

            if (args[0] is null || !Actions.ValidActions.Keys.Contains(args[0]))
            {
                throw GetErrorMessage();
            }

            return args[0];
        }
    }
}