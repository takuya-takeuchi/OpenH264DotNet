using System;

namespace OpenH264DotNet
{

    /// <summary>
    /// Defines the encoder. This class cannot be inherited.
    /// </summary>
    public sealed class SVCEncoder : OpenH264Object
    {

        #region Constructors

        internal SVCEncoder(IntPtr ptr)
        {
            this.NativePtr = ptr;
        }

        #endregion

        #region Properties
        #endregion

        #region Methods
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

            NativeMethods.WelsDestroySVCEncoder(this.NativePtr);
        }

        #endregion

    }

}