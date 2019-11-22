namespace SbJwlLauncher
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using Microsoft.Win32;
    using SbJwlLauncher.NativeMethods;

    internal static class PackageInfo
    {
        public static IReadOnlyCollection<string> GetPackageNamesStartingWith(string prefix)
        {
            var result = new List<string>();

            // Computer\HKEY_CURRENT_USER\Software\Classes\ActivatableClasses\Package\WatchtowerBibleandTractSo.45909CDBADF3C_11.4.81.0_x64__5rz59y55nfz3e
            using (var key = Registry.CurrentUser.OpenSubKey(@"Software\Classes\ActivatableClasses\Package"))
            {
                if (key == null)
                {
                    return result;
                }

                foreach (var subKey in key.GetSubKeyNames())
                {
                    if (subKey.StartsWith(prefix))
                    {
                        // jwlPackageName, e.g. "WatchtowerBibleandTractSo.45909CDBADF3C_11.4.81.0_x64__5rz59y55nfz3e"
                        result.Add(subKey);
                    }
                }
            }

            return result;
        }

        public static string GetAppUserModelId(string fullPackageName)
        {
            NativeHelpers.OpenPackageInfoByFullName(fullPackageName, 0, out var packageInfo);

            var length = 0;
            NativeHelpers.GetPackageApplicationIds(packageInfo, ref length, null, out var count);

            var buffer = new byte[length];
            NativeHelpers.GetPackageApplicationIds(packageInfo, ref length, buffer, out count);

            return Encoding.Unicode.GetString(buffer, IntPtr.Size * count, length - (IntPtr.Size * count));
        }
    }
}
