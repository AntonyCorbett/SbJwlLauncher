namespace SbJwlLauncher
{
    using System;
    using System.Diagnostics;
    using System.Linq;
    using SbJwlLauncher.EventArgs;
    using SbJwlLauncher.Exceptions;
    using SbJwlLauncher.NativeHelpers;

    //// some very useful code here:
    //// https://github.com/zodiacon/MetroManager/blob/master/MetroManager/MetroLauncher.cs

    internal static class JwlManager
    {
        private const string PackagePrefix = "WatchtowerBibleandTractSo.45909CDBADF3C";
        private const string JwlMainWindowTitle = "JW Library";
        private const string JwlMainWindowClassName = "ApplicationFrameWindow";

        public static event EventHandler<JwLauncherEventArgs> JwLauncherEvent;

        public static uint Launch()
        {
            OnJwLauncherEvent(new JwLauncherEventArgs("Finding JWL package"));

            var packageNames = PackageInfo.GetPackageNamesStartingWith(PackagePrefix);
            if (packageNames.Count != 1)
            {
                var errorMessage = "Could not find JwLibrary package name.";
                throw new JwlManagerException(errorMessage);
            }

            var packageName = packageNames.First();
            OnJwLauncherEvent(new JwLauncherEventArgs($"Found JWL package: {packageName}"));

            OnJwLauncherEvent(new JwLauncherEventArgs("Finding JWL App user model Id"));
            var appUserModelId = PackageInfo.GetAppUserModelId(packageName);

            if (string.IsNullOrWhiteSpace(appUserModelId))
            {
                throw new JwlManagerException("Could not find JwLibrary app user model Id.");
            }

            OnJwLauncherEvent(new JwLauncherEventArgs($"Found JWL App user model Id: {appUserModelId}"));

            var appActiveManager = new ApplicationActivationManager();

            OnJwLauncherEvent(new JwLauncherEventArgs("Activating application"));

            appActiveManager.ActivateApplication(appUserModelId, null, ActivateOptions.None, out var processId);

            OnJwLauncherEvent(new JwLauncherEventArgs($"Activated application, process Id = 0x{processId:X}"));

            return processId;
        }

        public static void SetWindowPosition(int x, int y, int width, int height)
        {
            OnJwLauncherEvent(new JwLauncherEventArgs("Checking JWL is running"));

            CheckIsRunning();

            OnJwLauncherEvent(new JwLauncherEventArgs("JWL is running"));

            OnJwLauncherEvent(new JwLauncherEventArgs("Getting JWL main window handle"));

            var targetHandle = GetMainWindowHandle();

            OnJwLauncherEvent(new JwLauncherEventArgs($"Retrieved JWL main window handle: 0x{(int)targetHandle:X}"));

            OnJwLauncherEvent(new JwLauncherEventArgs($"Setting window position: {x} {y} {width} {height}"));

            // fix-up!
            x -= 8;

            WindowPositionManager.SetWindowPosition(targetHandle, x, y, width, height);
        }

        private static IntPtr GetMainWindowHandle()
        {
            var desktopWindowHandle = NativeMethods.GetDesktopWindow();

            // JWL main window is a direct descendent of desktop
            // and the 1st in z-order having specified caption.
            var targetHandle = NativeMethods.FindWindowEx(
                desktopWindowHandle,
                IntPtr.Zero,
                JwlMainWindowClassName,
                JwlMainWindowTitle);

            if (targetHandle == IntPtr.Zero)
            {
                throw new JwlManagerException("Could not find window");
            }

            return targetHandle;
        }

        private static void CheckIsRunning()
        {
            var process = GetRunningProcess();
            if (process == null)
            {
                throw new JwlManagerException("Could not find JwLibrary running process.");
            }
        }

        private static Process GetRunningProcess()
        {
            var processes = Process.GetProcessesByName("JWLibrary");
            return processes.Length != 1 ? null : processes.First();
        }

        private static void OnJwLauncherEvent(JwLauncherEventArgs e)
        {
            JwLauncherEvent?.Invoke(null, e);
        }
    }
}
