using System;
using McMaster.Extensions.CommandLineUtils;

#pragma warning disable CS3021

namespace Nexus
{
    internal static class Program
    {
        private static int Main(string[] args)
        {
            var app = new CommandLineApplication
            {
                Name = "NexusParser"
            };

            app.HelpOption();

            var optionInput = app.Option("-i|--input <path>", "The source file root directory", CommandOptionType.SingleValue);
            var optionOutput = app.Option("-o|--output <path>", "The compiled source file root directory",
                CommandOptionType.SingleValue);

            app.OnExecute(() =>
            {
                if (!optionInput.HasValue() || !optionOutput.HasValue())
                {
                    if (!optionInput.HasValue())
                    {
                        Console.WriteLine("Missing input path.");
                    }

                    if (!optionOutput.HasValue())
                    {
                        Console.WriteLine("Missing output path.");
                    }

                    app.ShowHint();
                    return 1;
                }

                var configuration = new Configuration(optionInput.Value(), optionOutput.Value());
                configuration.Read();

                var files = FileParser.ParseProject(configuration);
                var units = Compiler.Compile(files);
                FileParser.WriteFiles(configuration.OutputPath, units);
                return 0;
            });
            app.OnValidationError(result => { app.ShowHelp(); });

            return app.Execute(args);
        }
    }
}
