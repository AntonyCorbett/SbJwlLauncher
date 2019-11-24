namespace SbJwlLauncher
{
    using System;
    using SbJwlLauncher.NativeHelpers;

    internal static class WindowPositionManager
    {
        public static void SetWindowPosition(
            IntPtr windowHandle, int x, int y, int width, int height)
        {
            NativeMethods.SetWindowPos(
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
