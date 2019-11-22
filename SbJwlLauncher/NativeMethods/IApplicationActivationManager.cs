namespace SbJwlLauncher.NativeMethods
{
    using System;
    using System.Runtime.InteropServices;

    [ComImport]
    [Guid("2e941141-7f97-4756-ba1d-9decde894a3d")]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    internal interface IApplicationActivationManager
    {
        IntPtr ActivateApplication(
            [In] string appUserModelId, 
            [In] string arguments, 
            [In] ActivateOptions options, 
            [Out] out uint processId);

        IntPtr ActivateForFile(
            [In] string appUserModelId, 
            [In] IntPtr itemArray, 
            [In] string verb, 
            [Out] out uint processId);

        IntPtr ActivateForProtocol(
            [In] string appUserModelId, 
            [In] IntPtr itemArray, 
            [Out] out uint processId);
    }
}
