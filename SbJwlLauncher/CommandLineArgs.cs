namespace SbJwlLauncher
{
    internal class CommandLineArgs
    {
        public int WindowX { get; set; }

        public int WindowY { get; set; }

        public int WindowWidth { get; set; }

        public int WindowHeight { get; set; }

        public bool Priority { get; set; }

        public bool IsValid()
        {
            // a bit arbitrary!
            return WindowWidth >= 200 && WindowHeight >= 200;
        }
    }
}
