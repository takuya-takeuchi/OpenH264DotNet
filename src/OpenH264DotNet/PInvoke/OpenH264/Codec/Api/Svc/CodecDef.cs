using System;
using System.Runtime.InteropServices;

using int32_t = System.Int32;
using uint32_t = System.UInt32;

// ReSharper disable once CheckNamespace
namespace OpenH264DotNet
{

    internal sealed partial class NativeMethods
    {

        /// <summary>
        /// Structure for decoder memery
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        public struct SSysMEMBuffer
        {

            public int iWidth;                    ///< width of decoded pic for display

            public int iHeight;                   ///< height of decoded pic for display

            public int iFormat;                   ///< type is "EVideoFormatType"

            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
            public int[] iStride;                 ///< stride of 2 component

        }

        #region SSysMEMBuffer

        [DllImport(NativeWrapperLibrary, CallingConvention = CallingConvention)]
        public static extern int32_t openh264_SSysMEMBuffer_get_iWidth(IntPtr buffer);

        [DllImport(NativeWrapperLibrary, CallingConvention = CallingConvention)]
        public static extern int32_t openh264_SSysMEMBuffer_get_iHeight(IntPtr buffer);

        [DllImport(NativeWrapperLibrary, CallingConvention = CallingConvention)]
        public static extern int32_t openh264_SSysMEMBuffer_get_iFormat(IntPtr buffer);

        [DllImport(NativeWrapperLibrary, CallingConvention = CallingConvention)]
        public static extern IntPtr openh264_SSysMEMBuffer_get_iStride(IntPtr buffer);

        #endregion

        #region ISVCDecoder

        [DllImport(NativeWrapperLibrary, CallingConvention = CallingConvention)]
        public static extern int openh264_ISVCDecoder_Initialize(IntPtr decoder,
                                                                 IntPtr pParam);

        [DllImport(NativeWrapperLibrary, CallingConvention = CallingConvention)]
        public static extern int openh264_ISVCDecoder_Uninitialize(IntPtr decoder);

        [DllImport(NativeWrapperLibrary, CallingConvention = CallingConvention)]
        public static extern DECODING_STATE openh264_ISVCDecoder_DecodeFrame2(IntPtr decoder,
                                                                              byte[] pSrc,
                                                                              int iSrcLen,
                                                                              IntPtr[] ppDst,
                                                                              IntPtr pDstInfo);

        #endregion

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern int WelsCreateDecoder(out IntPtr ppDecoder);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern void WelsDestroyDecoder(IntPtr pDecoder);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern int WelsCreateSVCEncoder(out IntPtr ppEncoder);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern void WelsDestroySVCEncoder(IntPtr pEncoder);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern OpenH264Version WelsGetCodecVersion();

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern unsafe void WelsGetCodecVersionEx(OpenH264Version* pVersion);

    }

}