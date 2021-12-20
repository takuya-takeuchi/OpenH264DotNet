using System.Runtime.InteropServices;

// ReSharper disable once CheckNamespace
namespace OpenH264DotNet
{

    internal sealed partial class NativeMethods
    {

        #region Fields

        private const string Version = "2.1.1";

        // Native library file name.
        // If Linux, it will be converted to  libOpenH264DotNetNative.so
        // If MacOSX, it will be converted to  libOpenH264DotNetNative.dylib
        // If Windows, it will be available after call LoadLibrary.
        // And this file name must not contain period. If it does,
        // CLR does not add extension (.dll) and CLR fails to load library
#if WINDOWS_X86
        private const string Platform = "win32";
#elif WINDOWS_X64
        private const string Platform = "win64";
#elif OSX_X86
        private const string Platform = "osx32";
#elif OSX_X64
        private const string Platform = "osx64";
#elif LINUX_X86
        private const string Platform = "linux32.6";
#elif LINUX_X64
        private const string Platform = "linux64.6";
#elif LINUX_ARM
        private const string Platform = "linux-arm.6";
#elif LINUX_ARM64
        private const string Platform = "linux-arm64.6";
#else
        private const string Platform = "win64";
#endif

        internal const string NativeLibrary = $"openh264-{Version}-{Platform}";

        internal const string NativeWrapperLibrary = "OpenH264DotNetNative";

        public const CallingConvention CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl;

        private static readonly WindowsLibraryLoader WindowsLibraryLoader = new WindowsLibraryLoader();

#endregion

#region Constructors

        static NativeMethods()
        {
            WindowsLibraryLoader.LoadLibraries(new[]
            {
                $"{NativeLibrary}"
            });
        }

#endregion

    }

}