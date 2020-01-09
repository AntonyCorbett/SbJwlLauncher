namespace SbJwlLauncher
{
    using System;
    using SbJwlLauncher.NativeHelpers;

    internal static class WindowPositionManager
    {
        public static void SetWindowPosition(
            IntPtr windowHandle, int x, int y, int width, int height)
        {
            const int SW_NORMAL = 1;
            NativeMethods.ShowWindow(windowHandle, SW_NORMAL);

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
