using System;

namespace OpenH264DotNet
{

    /// <summary>
    /// Defines the decoder. This class cannot be inherited.
    /// </summary>
    public sealed class SVCDecoder : OpenH264Object
    {

        #region Constructors

        internal SVCDecoder(IntPtr ptr)
        {
            this.NativePtr = ptr;
        }

        #endregion

        #region Properties
        #endregion

        #region Methods

        public int Initialize(DecodingParam param)
        {
            if (param == null) 
                throw new ArgumentNullException(nameof(param));

            param.ThrowIfDisposed();

            return NativeMethods.openh264_ISVCDecoder_Initialize(this.NativePtr, param.NativePtr);
        }

        public int Uninitialize()
        {
            return NativeMethods.openh264_ISVCDecoder_Uninitialize(this.NativePtr);
        }

        public DecodingState DecodeFrame2(byte[] src, BufferInfo info)
        {
            if (info == null)
                throw new ArgumentNullException(nameof(info));

            info.ThrowIfDisposed();

            var pData = new IntPtr[3];
            var ret = NativeMethods.openh264_ISVCDecoder_DecodeFrame2(this.NativePtr, src, src.Length, pData, info.NativePtr);
            //dst = pData;

            return (DecodingState)ret;
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

            NativeMethods.WelsDestroyDecoder(this.NativePtr);
        }

        #endregion

    }

}