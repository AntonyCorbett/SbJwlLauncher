namespace SbJwlLauncher.NativeHelpers
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;

    [ComImport]
    [Guid("45BA127D-10A8-46EA-8AB7-56EA9078943C")]
    internal class ApplicationActivationManager : IApplicationActivationManager
    {
        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        public extern IntPtr ActivateApplication(
            [In] string appUserModelId, 
            [In] string arguments, 
            [In] ActivateOptions options, 
            [Out] out uint processId);

        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        public extern IntPtr ActivateForFile(
            [In] string appUserModelId, 
            [In] IntPtr itemArray, 
            [In] string verb, 
            [Out] out uint processId);

        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        public extern IntPtr ActivateForProtocol(
            [In] string appUserModelId, 
            [In] IntPtr itemArray, 
            [Out] out uint processId);
    }
}
