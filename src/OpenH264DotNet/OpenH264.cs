using System;

namespace OpenH264DotNet
{

    /// <summary>
    /// Provides the methods of openh264.
    /// </summary>
    public static partial class OpenH264
    {

        #region Methods

        /// <summary>
        /// Create decoder.
        /// </summary>
        public static SVCDecoder WelsCreateDecoder()
        {
            var ret = NativeMethods.WelsCreateDecoder(out var ppDecoder);
            return new SVCDecoder(ppDecoder);
        }

        /// <summary>
        /// Create encoder.
        /// </summary>
        public static SVCEncoder WelsCreateSVCEncoder()
        {
            var ret = NativeMethods.WelsCreateSVCEncoder(out var ppEncoder);
            return new SVCEncoder(ppEncoder);
        }

        /// <summary>
        /// Destroy decoder.
        /// </summary>
        public static void WelsDestroyDecoder(SVCDecoder decoder)
        {
            if (decoder == null)
                throw new ArgumentNullException(nameof(decoder));
            if (decoder.IsDisposed)
                return;

            decoder.Dispose();
        }

        /// <summary>
        /// Destroy encoder.
        /// </summary>
        public static void WelsDestroySVCEncoder(SVCEncoder encoder)
        {
            if (encoder == null) 
                throw new ArgumentNullException(nameof(encoder));
            if (encoder.IsDisposed)
                return;

            encoder.Dispose();
        }

        /// <summary>
        /// Get the version of OpenH264.
        /// </summary>
        /// <returns><see cref="OpenH264Version"/>.</returns>
        public static OpenH264Version WelsGetCodecVersion()
        {
            var ret = NativeMethods.WelsGetCodecVersion();
            return new OpenH264Version(ret);
        }

        /// <summary>
        /// Get the version of OpenH264.
        /// </summary>
        /// <returns><see cref="OpenH264Version"/>.</returns>
        public static void WelsGetCodecVersionEx(out OpenH264Version version)
        {
            unsafe
            {
                NativeMethods.OpenH264Version tmp;
                NativeMethods.WelsGetCodecVersionEx(&tmp);
                version = new OpenH264Version(tmp);
            }
        }

        #endregion

    }

}