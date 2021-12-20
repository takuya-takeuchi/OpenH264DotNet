using System;
using System.Runtime.InteropServices;

using uint32_t = System.UInt32;

// ReSharper disable once CheckNamespace
namespace OpenH264DotNet
{

    internal sealed partial class NativeMethods
    {

        /// <summary>
        /// Decoding status
        /// </summary>
        public enum DECODING_STATE
        {

            dsErrorFree = 0x00,   ///< bit stream error-free
            
            dsFramePending = 0x01,   ///< need more throughput to generate a frame output,
            
            dsRefLost = 0x02,   ///< layer lost at reference frame with temporal id 0
            
            dsBitstreamError = 0x04,   ///< error bitstreams(maybe broken internal frame) the decoder cared
            
            dsDepLayerLost = 0x08,   ///< dependented layer is ever lost
            
            dsNoParamSets = 0x10,   ///< no parameter set NALs involved
            
            dsDataErrorConcealed = 0x20,   ///< current data error concealed specified
            
            dsRefListNullPtrs = 0x40, ///<ref picure list contains null ptrs within uiRefCount range

            /**
            * Errors derived from logic level
            */
            dsInvalidArgument = 0x1000, ///< invalid argument specified
            
            dsInitialOptExpected = 0x2000, ///< initializing operation is expected
            
            dsOutOfMemory = 0x4000, ///< out of memory due to new request
            
            /**
            * ANY OTHERS?
            */
            dsDstBufNeedExpan = 0x8000  ///< actual picture size exceeds size of dst pBuffer feed in decoder, so need expand its size

        }

        /// <summary>
        /// Enumerate the type of error concealment methods
        /// </summary>
        public enum ERROR_CON_IDC
        {

            ERROR_CON_DISABLE = 0,

            ERROR_CON_FRAME_COPY,

            ERROR_CON_SLICE_COPY,

            ERROR_CON_FRAME_COPY_CROSS_IDR,

            ERROR_CON_SLICE_COPY_CROSS_IDR,

            ERROR_CON_SLICE_COPY_CROSS_IDR_FREEZE_RES_CHANGE,

            ERROR_CON_SLICE_MV_COPY_CROSS_IDR,

            ERROR_CON_SLICE_MV_COPY_CROSS_IDR_FREEZE_RES_CHANGE

        }

        /// <summary>
        /// Define a new struct to show the property of video bitstream.
        /// </summary>
        public enum VIDEO_BITSTREAM_TYPE
        {

            VIDEO_BITSTREAM_AVC = 0,

            VIDEO_BITSTREAM_SVC = 1,

            VIDEO_BITSTREAM_DEFAULT = VIDEO_BITSTREAM_SVC

        }

        /// <summary>
        /// Define a new struct to show the property of video bitstream.
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        public struct SVideoProperty
        {

            public uint size;                          ///< size of the struct

            public VIDEO_BITSTREAM_TYPE eVideoBsType;  ///< video stream type (AVC/SVC)

        }

        /// <summary>
        /// SVC Decoding Parameters, reserved here and potential applicable in the future
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        public struct SDecodingParam
        {

            public unsafe sbyte* pFileNameRestructed;       ///< file name of reconstructed frame used for PSNR calculation based debug

            public uint uiCpuLoad;                          ///< CPU load

            public byte uiTargetDqLayer;                    ///< setting target dq layer id

            public ERROR_CON_IDC eEcActiveIdc;              ///< whether active error concealment feature in decoder

            public bool bParseOnly;                         ///< decoder for parse only, no reconstruction. When it is true, SPS/PPS size should not exceed SPS_PPS_BS_SIZE (128). Otherwise, it will return error info

            public SVideoProperty sVideoProperty;           ///< video stream property

        }

        #region SDecodingParam

        [DllImport(NativeWrapperLibrary, CallingConvention = CallingConvention)]
        public static extern IntPtr openh264_SDecodingParam_new();

        [DllImport(NativeWrapperLibrary, CallingConvention = CallingConvention)]
        public static extern int openh264_SDecodingParam_delete(IntPtr param);

        [DllImport(NativeWrapperLibrary, CallingConvention = CallingConvention)]
        public static extern void openh264_SDecodingParam_set_uiCpuLoad(IntPtr param,
                                                                        uint32_t value);

        [DllImport(NativeWrapperLibrary, CallingConvention = CallingConvention)]
        public static extern uint32_t openh264_SDecodingParam_get_uiCpuLoad(IntPtr param);

        [DllImport(NativeWrapperLibrary, CallingConvention = CallingConvention)]
        public static extern void openh264_SDecodingParam_set_uiTargetDqLayer(IntPtr param,
                                                                              uint32_t value);

        [DllImport(NativeWrapperLibrary, CallingConvention = CallingConvention)]
        public static extern uint32_t openh264_SDecodingParam_get_uiTargetDqLayer(IntPtr param);

        [DllImport(NativeWrapperLibrary, CallingConvention = CallingConvention)]
        public static extern void openh264_SDecodingParam_set_eEcActiveIdc(IntPtr param,
                                                                           ERROR_CON_IDC value);

        [DllImport(NativeWrapperLibrary, CallingConvention = CallingConvention)]
        public static extern ERROR_CON_IDC openh264_SDecodingParam_get_eEcActiveIdc(IntPtr param);

        [DllImport(NativeWrapperLibrary, CallingConvention = CallingConvention)]
        public static extern void openh264_SDecodingParam_set_bParseOnly(IntPtr param,
                                                                         bool value);

        [DllImport(NativeWrapperLibrary, CallingConvention = CallingConvention)]
        [return: MarshalAs(UnmanagedType.U1)]
        public static extern bool openh264_SDecodingParam_get_bParseOnly(IntPtr param);

        [DllImport(NativeWrapperLibrary, CallingConvention = CallingConvention)]
        public static extern IntPtr openh264_SDecodingParam_get_SVideoProperty(IntPtr param);

        #endregion

        #region SVideoProperty

        [DllImport(NativeWrapperLibrary, CallingConvention = CallingConvention)]
        public static extern void openh264_SVideoProperty_set_size(IntPtr property,
                                                                   uint32_t value);

        [DllImport(NativeWrapperLibrary, CallingConvention = CallingConvention)]
        public static extern uint32_t openh264_SVideoProperty_get_size(IntPtr property);

        [DllImport(NativeWrapperLibrary, CallingConvention = CallingConvention)]
        public static extern void openh264_SVideoProperty_set_eVideoBsType(IntPtr property,
                                                                           VIDEO_BITSTREAM_TYPE value);

        [DllImport(NativeWrapperLibrary, CallingConvention = CallingConvention)]
        public static extern VIDEO_BITSTREAM_TYPE openh264_SVideoProperty_get_eVideoBsType(IntPtr property);

        #endregion

    }

}