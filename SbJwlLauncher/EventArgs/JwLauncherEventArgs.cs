namespace SbJwlLauncher.EventArgs
{
    internal class JwLauncherEventArgs
    {
        public JwLauncherEventArgs(string message, bool error = false)
        {
            Message = message;
            Error = error;
        }

        public string Message { get; }

        public bool Error { get; }
    }
}
