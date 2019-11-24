namespace SbJwlLauncher
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Microsoft.Win32;
    using SbJwlLauncher.NativeHelpers;

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

                result.AddRange(key.GetSubKeyNames().Where(subKey => subKey.StartsWith(prefix)));
            }

            return result;
        }

        public static string GetAppUserModelId(string fullPackageName)
        {
            NativeMethods.OpenPackageInfoByFullName(fullPackageName, 0, out var packageInfo);

            var length = 0;
            NativeMethods.GetPackageApplicationIds(packageInfo, ref length, null, out var count);

            var buffer = new byte[length];
            NativeMethods.GetPackageApplicationIds(packageInfo, ref length, buffer, out count);

            return Encoding.Unicode.GetString(buffer, IntPtr.Size * count, length - (IntPtr.Size * count));
        }
    }
}
