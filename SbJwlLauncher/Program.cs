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
            if (args == null)
            {
                return null;
            }

            switch (args.Length)
            {
                case 0:
                    return null;

                case 4:
                    var x = ParseIntegerArg(args[0]);
                    var y = ParseIntegerArg(args[1]);
                    var w = ParseIntegerArg(args[2]);
                    var h = ParseIntegerArg(args[3]);

                    if (x == null || y == null || w == null || h == null)
                    {
                        return null;
                    }

                    var result = new CommandLineArgs
                    {
                        WindowX = x.Value,
                        WindowY = y.Value,
                        WindowWidth = w.Value,
                        WindowHeight = h.Value,
                    };

                    return !result.IsValid() ? null : result;

                default:
                    throw new CommandLineArgumentException(
                        "Specify window position and size on command-line, e.g. SbJwlLauncher 0 0 800 600");
            }
        }

        private static int? ParseIntegerArg(string s)
        {
            return !int.TryParse(s, out var result) ? (int?)null : result;
        }
    }
}
