using CLI;
using CLI.Interfaces;
using ConsoleWyrm.Cli;

namespace ConsoleWyrm
{
    internal class Program
    {
        static void Main(string[] args)
        {
            CommandLine cli = new CommandLine([
                new ClientCommand(["client"]),
                new ServerCommand(["server"])
                ]);
            Application app = new Application(cli);
            app.Run(args);
        }
    }
}
