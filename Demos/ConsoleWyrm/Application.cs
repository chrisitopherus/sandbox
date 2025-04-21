using CLI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleWyrm;

public class Application
{
    private readonly CommandLine cli;
    public Application(CommandLine cli)
    {
        this.cli = cli;
        this.Setup();
    }

    public void Run(string[] args)
    {
        this.cli.Execute(args);
    }

    private void Setup()
    {

    }
}
