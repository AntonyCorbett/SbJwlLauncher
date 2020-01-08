namespace SbJwlLauncher
{
    using System;
    using System.Diagnostics;

    internal class MainApp
    {
        public void Execute(CommandLineArgs args)
        {
            if (JwlManager.IsRunning())
            {
                return;
            }

            JwlManager.JwLauncherEvent += HandleJwlManagerEvent;

            var processId = JwlManager.Launch();

            if (processId > 0)
            {
                Process.GetProcessById((int)processId).PriorityClass = ProcessPriorityClass.AboveNormal;
            }

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

        private static void HandleJwlManagerEvent(object sender, EventArgs.JwLauncherEventArgs e)
        {
            Console.WriteLine(e.Message);
        }
    }
}
