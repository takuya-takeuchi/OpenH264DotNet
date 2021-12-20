using System;
using System.Runtime.InteropServices;

using int32_t = System.Int32;
using uint64_t = System.UInt64;

// ReSharper disable once CheckNamespace
namespace OpenH264DotNet
{

    internal sealed partial class NativeMethods
    {

        #region SBufferInfo

        [DllImport(NativeWrapperLibrary, CallingConvention = CallingConvention)]
        public static extern IntPtr openh264_SBufferInfo_new();

        [DllImport(NativeWrapperLibrary, CallingConvention = CallingConvention)]
        public static extern int openh264_SBufferInfo_delete(IntPtr info);

        [DllImport(NativeWrapperLibrary, CallingConvention = CallingConvention)]
        public static extern int32_t openh264_SBufferInfo_get_iBufferStatus(IntPtr buffer);

        [DllImport(NativeWrapperLibrary, CallingConvention = CallingConvention)]
        public static extern uint64_t openh264_SBufferInfo_get_uiInBsTimeStamp(IntPtr buffer);

        [DllImport(NativeWrapperLibrary, CallingConvention = CallingConvention)]
        public static extern uint64_t openh264_SBufferInfo_get_uiOutYuvTimeStamp(IntPtr buffer);

        [DllImport(NativeWrapperLibrary, CallingConvention = CallingConvention)]
        public static extern IntPtr openh264_SBufferInfo_get_UsrData(IntPtr buffer);

        [DllImport(NativeWrapperLibrary, CallingConvention = CallingConvention)]
        public static extern IntPtr openh264_SBufferInfo_get_UsrData_sSystemBuffer(IntPtr buffer);

        [DllImport(NativeWrapperLibrary, CallingConvention = CallingConvention)]
        public static extern void openh264_SBufferInfo_get_pDst(IntPtr buffer, IntPtr[] pDst, int pDstLen);

        #endregion

    }

}