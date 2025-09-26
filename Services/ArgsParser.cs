using UpdateDebian.Interfaces;

namespace UpdateDebian.Services
{
    public class ArgsParser : IArgsParser
    {
        public string GetAction(string[] args)
        {
            if (args.Length != 1)
            {
                throw new Exception($"No action specified, valid actions are: {Environment.NewLine}check-backports{Environment.NewLine}update");
            }

            return args[0];
        }
    }
}