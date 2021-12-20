using System;

namespace OpenH264DotNet
{

    /// <summary>
    /// Define a new struct to show the property of video bitstream. This class cannot be inherited.
    /// </summary>
    public sealed class VideoProperty : OpenH264Object
    {

        #region Constructors

        internal VideoProperty(IntPtr ptr, bool isEnabledDispose) :
            base(isEnabledDispose)
        {
            this.NativePtr = ptr;
        }

        #endregion

        #region Properties

        public uint Size
        {
            get
            {
                this.ThrowIfDisposed();
                return NativeMethods.openh264_SVideoProperty_get_size(this.NativePtr);
            }
            set
            {
                this.ThrowIfDisposed();
                NativeMethods.openh264_SVideoProperty_set_size(this.NativePtr, value);
            }
        }

        public VideoBitstreamType VideoBsType
        {
            get
            {
                this.ThrowIfDisposed();
                return (VideoBitstreamType)NativeMethods.openh264_SVideoProperty_get_eVideoBsType(this.NativePtr);
            }
            set
            {
                this.ThrowIfDisposed();
                NativeMethods.openh264_SVideoProperty_set_eVideoBsType(this.NativePtr, (NativeMethods.VIDEO_BITSTREAM_TYPE)value);
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

            // nothing to do
        }

        #endregion

    }

}