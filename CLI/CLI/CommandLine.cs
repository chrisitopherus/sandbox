using CLI.Interfaces;

namespace CLI;

public class CommandLine
{
    private readonly CommandLineParser cliParser;
    public CommandLine(ICommand[] commands)
    {
        this.Commands = [..commands, new HelpCommand(["-h", "--help"], commands)];
        this.cliParser = new CommandLineParser(this.Commands);
    }

    public ICommand[] Commands
    {
        get;
        private set;
    }

    public void Execute(string[] args)
    {
        CLIParserResult parserResult = this.cliParser.Parse(args);
        parserResult.Command.Execute(new CommandContext(parserResult.ModifierValues, parserResult.CommandValue));
    }
}
