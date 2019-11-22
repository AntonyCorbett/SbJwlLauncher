namespace SbJwlLauncher
{
    using System;
    using SbJwlLauncher.NativeMethods;

    internal static class WindowPositionManager
    {
        public static void SetWindowPosition(
            IntPtr windowHandle, int x, int y, int width, int height)
        {
            NativeHelpers.SetWindowPos(
                windowHandle, 
                IntPtr.Zero,
                x, 
                y, 
                width, 
                height, 
                0);
        }
    }
}
