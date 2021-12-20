using System;
using System.Runtime.InteropServices;

namespace OpenH264DotNet
{

    /// <summary>
    /// Define a memory info for one picture. This class cannot be inherited.
    /// </summary>
    public sealed class SystemBuffer : OpenH264Object
    {

        #region Constructors

        internal SystemBuffer(IntPtr ptr, bool isEnabledDispose) :
            base(isEnabledDispose)
        {
            this.NativePtr = ptr;
        }

        #endregion

        #region Properties

        public int Width
        {
            get
            {
                this.ThrowIfDisposed();
                return NativeMethods.openh264_SSysMEMBuffer_get_iWidth(this.NativePtr);
            }
        }

        public int Height
        {
            get
            {
                this.ThrowIfDisposed();
                return NativeMethods.openh264_SSysMEMBuffer_get_iHeight(this.NativePtr);
            }
        }

        public int Format
        {
            get
            {
                this.ThrowIfDisposed();
                return NativeMethods.openh264_SSysMEMBuffer_get_iFormat(this.NativePtr);
            }
        }

        public int[] Stride
        {
            get
            {
                this.ThrowIfDisposed();
                var ret = NativeMethods.openh264_SSysMEMBuffer_get_iStride(this.NativePtr);
                var element1 = Marshal.ReadInt32(ret, 0);
                var element2 = Marshal.ReadInt32(ret, 1);
                return new[] { element1, element2 };
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