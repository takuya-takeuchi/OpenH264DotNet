using System;
using System.Runtime.InteropServices;

namespace OpenH264DotNet
{

    /// <summary>
    /// Defines the SVC Decoding Parameters. This class cannot be inherited.
    /// </summary>
    public sealed class DecodingParam : OpenH264Object
    {

        #region Constructors

        public DecodingParam()
        {
            this.NativePtr = NativeMethods.openh264_SDecodingParam_new();
        }

        #endregion

        #region Properties

        public uint CpuLoad
        {
            get
            {
                this.ThrowIfDisposed();
                return NativeMethods.openh264_SDecodingParam_get_uiCpuLoad(this.NativePtr);
            }
            set
            {
                this.ThrowIfDisposed();
                NativeMethods.openh264_SDecodingParam_set_uiCpuLoad(this.NativePtr, value);
            }
        }

        public ErrorConIdc EcActiveIdc
        {
            get
            {
                this.ThrowIfDisposed();
                return (ErrorConIdc)NativeMethods.openh264_SDecodingParam_get_eEcActiveIdc(this.NativePtr);
            }
            set
            {
                this.ThrowIfDisposed();
                NativeMethods.openh264_SDecodingParam_set_eEcActiveIdc(this.NativePtr, (NativeMethods.ERROR_CON_IDC)value);
            }
        }

        public bool ParseOnly
        {
            get
            {
                this.ThrowIfDisposed();
                return NativeMethods.openh264_SDecodingParam_get_bParseOnly(this.NativePtr);
            }
            set
            {
                this.ThrowIfDisposed();
                NativeMethods.openh264_SDecodingParam_set_bParseOnly(this.NativePtr, value);
            }
        }

        public uint TargetDqLayer
        {
            get
            {
                this.ThrowIfDisposed();
                return NativeMethods.openh264_SDecodingParam_get_uiTargetDqLayer(this.NativePtr);
            }
            set
            {
                this.ThrowIfDisposed();
                NativeMethods.openh264_SDecodingParam_set_uiTargetDqLayer(this.NativePtr, value);
            }
        }

        public VideoProperty VideoProperty
        {
            get
            {
                this.ThrowIfDisposed();
                var ret = NativeMethods.openh264_SDecodingParam_get_SVideoProperty(this.NativePtr);
                return new VideoProperty(ret, false);
            }
        }

        #endregion

        #region Overrides 

        /// <summary>
        /// Releases all unmanaged resources.
        /// </summary>
        protected override void DisposeUnmanaged()
        {
            base.DisposeUnmanaged();

            if (this.NativePtr == IntPtr.Zero)
                return;

            NativeMethods.openh264_SDecodingParam_delete(this.NativePtr);
        }

        #endregion

    }

}