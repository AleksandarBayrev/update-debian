using System.Text;
using UpdateDebian.Interfaces;

namespace UpdateDebian.Services
{
    public class ArgsParser : IArgsParser
    {
        private static Exception GetErrorMessage()
        {
            var sb = new StringBuilder();

            sb.AppendLine("usage: `update-debian [action]`");
            sb.AppendLine("Valid actions are:");

            foreach (var action in Actions.ValidActions)
            {
                sb.AppendLine($" - {action.Key}: {action.Value}");
            }

            return new Exception(sb.ToString());
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