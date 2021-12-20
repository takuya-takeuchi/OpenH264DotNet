using System;
using System.Runtime.InteropServices;

namespace OpenH264DotNet
{

    /// <summary>
    /// Define a output buffer info. This class cannot be inherited.
    /// </summary>
    public sealed class UsrData : OpenH264Object
    {

        #region Constructors

        internal UsrData(IntPtr parent, bool isEnabledDispose) :
            base(isEnabledDispose)
        {
            this.NativePtr = parent;
        }

        #endregion

        #region Properties

        public SystemBuffer SystemBuffer
        {
            get
            {
                this.ThrowIfDisposed();
                var ret = NativeMethods.openh264_SBufferInfo_get_UsrData_sSystemBuffer(this.NativePtr);
                return new SystemBuffer(ret, false);
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