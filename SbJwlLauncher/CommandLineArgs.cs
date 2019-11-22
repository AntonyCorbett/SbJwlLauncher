namespace SbJwlLauncher
{
    internal class CommandLineArgs
    {
        public int WindowX { get; set; }

        public int WindowY { get; set; }

        public int WindowWidth { get; set; } = 800;

        public int WindowHeight { get; set; } = 600;

        public bool IsValid()
        {
            // a bit arbitrary!
            return WindowWidth >= 200 && WindowHeight >= 200;
        }
    }
}
