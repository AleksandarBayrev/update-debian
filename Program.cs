using UpdateDebian.Services;

namespace UpdateDebian
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            await new Startup(new ArgsParser(), new DebianVersionParser()).Run(args);
        }
    }
}