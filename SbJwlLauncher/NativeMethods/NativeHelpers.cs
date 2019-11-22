namespace SbJwlLauncher.NativeMethods
{
    using System;
    using System.Runtime.InteropServices;

    internal static class NativeHelpers
    {
        [DllImport("kernel32")]
        public static extern int OpenPackageInfoByFullName(
            [MarshalAs(UnmanagedType.LPWStr)] string fullName, 
            uint reserved, 
            out IntPtr packageInfo);

        [DllImport("kernel32")]
        public static extern int GetPackageApplicationIds(
            IntPtr pir, 
            ref int bufferLength, 
            byte[] buffer, 
            out int count);

        [DllImport("kernel32")]
        public static extern int ClosePackageInfo(IntPtr pir);

        [DllImport("user32.dll", EntryPoint = "FindWindow", SetLastError = true)]
        public static extern IntPtr FindWindowByCaption(
            IntPtr zeroOnly,
            string lpWindowName);
        
        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool SetWindowPos(
            IntPtr hWnd,
            IntPtr hWndInsertAfter, 
            int x, 
            int y, 
            int cx, 
            int cy,
            SetWindowPosFlags uFlags);
        
        [Flags]
        public enum SetWindowPosFlags : uint
        {
            SynchronousWindowPosition = 0x4000,
            DeferErase = 0x2000,
            DrawFrame = 0x0020,
            FrameChanged = 0x0020,
            HideWindow = 0x0080,
            DoNotActivate = 0x0010,
            DoNotCopyBits = 0x0100,
            IgnoreMove = 0x0002,
            DoNotChangeOwnerZOrder = 0x0200,
            DoNotRedraw = 0x0008,
            DoNotReposition = 0x0200,
            DoNotSendChangingEvent = 0x0400,
            IgnoreResize = 0x0001,
            IgnoreZOrder = 0x0004,
            ShowWindow = 0x0040,
        }
    }
}
