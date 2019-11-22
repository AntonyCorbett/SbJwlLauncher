namespace SbJwlLauncher
{
    using System;
    using SbJwlLauncher.Exceptions;

    // ReSharper disable once ClassNeverInstantiated.Global
    internal static class Program
    {
        private static void Main(string[] args)
        {
            try
            {
                var theArguments = ParseArguments(args);
                
                var app = new MainApp();
                app.Execute(theArguments);
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine(ex.Message);
                Environment.ExitCode = 1;
            }
        }

        private static CommandLineArgs ParseArguments(string[] args)
        {
            var defaultArgs = new CommandLineArgs();

            if (args == null)
            {
                return defaultArgs;
            }

            switch (args.Length)
            {
                case 0:
                    return defaultArgs;

                case 4:
                    var result = new CommandLineArgs
                    {
                        WindowX = ParseIntegerArg(args[0], 0),
                        WindowY = ParseIntegerArg(args[1], 0),
                        WindowWidth = ParseIntegerArg(args[2], 800),
                        WindowHeight = ParseIntegerArg(args[3], 600),
                    };

                    return !result.IsValid() ? defaultArgs : result;

                default:
                    throw new CommandLineArgumentException(
                        "Specify window position and size on command-line, e.g. SbJwlLauncher 0 0 800 600");
            }
        }

        private static int ParseIntegerArg(string s, int defaultValue)
        {
            return !int.TryParse(s, out var result) ? defaultValue : result;
        }
    }
}
