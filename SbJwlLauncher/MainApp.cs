namespace SbJwlLauncher
{
    internal class MainApp
    {
        public void Execute(CommandLineArgs args)
        {
            JwlManager.Launch();

            JwlManager.SetWindowPosition(
                args.WindowX, 
                args.WindowY, 
                args.WindowWidth, 
                args.WindowHeight);
        }
    }
}
