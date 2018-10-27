using McMaster.Extensions.CommandLineUtils;
using NLog;

#pragma warning disable CS3021

namespace Nexus
{
    public static class Program
    {
        public static int Main(string[] args)
        {
            var config = new NLog.Config.LoggingConfiguration();
            var logConsole = new NLog.Targets.ConsoleTarget("logconsole");

            #if DEBUG
            config.AddRule(LogLevel.Debug, LogLevel.Fatal, logConsole);
            #else
            config.AddRule(LogLevel.Info, LogLevel.Fatal, logConsole);
            #endif

            LogManager.Configuration = config;
            var logger = LogManager.GetCurrentClassLogger();

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
                        logger.Error("Missing input path.");
                    }

                    if (!optionOutput.HasValue())
                    {
                        logger.Error("Missing output path.");
                    }

                    app.ShowHint();
                    return 1;
                }

                var configuration = new Configuration(optionInput.Value(), optionOutput.Value());
                configuration.Read();

                var files = FileParser.ParseProject(configuration);
                var units = Compiler.Compile(files);
                FileParser.WriteFiles(configuration, units);
                return 0;
            });
            app.OnValidationError(result => { app.ShowHelp(); });

            return app.Execute(args);
        }
    }
}
