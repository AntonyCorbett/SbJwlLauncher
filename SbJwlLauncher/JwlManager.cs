namespace SbJwlLauncher
{
    using System;
    using System.Diagnostics;
    using System.Linq;
    using SbJwlLauncher.Exceptions;
    using SbJwlLauncher.NativeMethods;

    //// some very useful code here:
    //// https://github.com/zodiacon/MetroManager/blob/master/MetroManager/MetroLauncher.cs

    internal static class JwlManager
    {
        private const string PackagePrefix = "WatchtowerBibleandTractSo.45909CDBADF3C";

        public static uint Launch()
        {
            var packageNames = PackageInfo.GetPackageNamesStartingWith(PackagePrefix);
            if (packageNames.Count != 1)
            {
                throw new JwlManagerException("Could not find JwLibrary package name.");
            }
            
            var appUserModelId = PackageInfo.GetAppUserModelId(packageNames.First());

            if (string.IsNullOrWhiteSpace(appUserModelId))
            {
                throw new JwlManagerException("Could not find JwLibrary app user model Id.");
            }

            var appActiveManager = new ApplicationActivationManager();

            appActiveManager.ActivateApplication(appUserModelId, null, ActivateOptions.None, out var processId);

            return processId;
        }

        public static void SetWindowPosition(int x, int y, int width, int height)
        {
            CheckIsRunning();

            var targetHandle = NativeHelpers.FindWindowByCaption(IntPtr.Zero, "JW Library");
            if (targetHandle == IntPtr.Zero)
            {
                throw new JwlManagerException("Could not find window");
            }

            // fix-up!
            x -= 8;
            WindowPositionManager.SetWindowPosition(targetHandle, x, y, width, height);
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
            if (processes.Length != 1)
            {
                return null;
            }

            return processes.First();
        }
    }
}
