namespace SbJwlLauncher
{
    using System;

    internal class MainApp
    {
        public void Execute(CommandLineArgs args)
        {
            JwlManager.JwLauncherEvent += HandleJwlManagerEvent;

            JwlManager.Launch();

            if (args == null)
            {
                Console.WriteLine("No window position specified");
                return;
            }

            JwlManager.SetWindowPosition(
                args.WindowX, 
                args.WindowY, 
                args.WindowWidth, 
                args.WindowHeight);
        }

        private void HandleJwlManagerEvent(object sender, EventArgs.JwLauncherEventArgs e)
        {
            Console.WriteLine(e.Message);
        }
    }
}
