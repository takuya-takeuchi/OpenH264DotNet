using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;

namespace OpenH264DotNet
{

    /// <summary>
    /// Defines the buffer info. This class cannot be inherited.
    /// </summary>
    public sealed class BufferInfo : OpenH264Object
    {

        #region Constructors

        public BufferInfo()
        {
            this.NativePtr = NativeMethods.openh264_SBufferInfo_new();
        }

        #endregion

        #region Properties

        public int BufferStatus
        {
            get
            {
                this.ThrowIfDisposed();
                return NativeMethods.openh264_SBufferInfo_get_iBufferStatus(this.NativePtr);
            }
        }

        public ulong InBsTimeStamp
        {
            get
            {
                this.ThrowIfDisposed();
                return NativeMethods.openh264_SBufferInfo_get_uiInBsTimeStamp(this.NativePtr);
            }
        }

        public ulong OutYuvTimeStamp
        {
            get
            {
                this.ThrowIfDisposed();
                return NativeMethods.openh264_SBufferInfo_get_uiOutYuvTimeStamp(this.NativePtr);
            }
        }

        public UsrData UsrData
        {
            get
            {
                this.ThrowIfDisposed();
                //var ret =  NativeMethods.openh264_SBufferInfo_get_UsrData(this.NativePtr);
                return new UsrData(this.NativePtr, false);
            }
        }

        public IntPtr[] Dst
        {
            get
            {
                this.ThrowIfDisposed();
                var dst = new IntPtr[3];
                NativeMethods.openh264_SBufferInfo_get_pDst(this.NativePtr, dst, dst.Length);
                return dst;
            }
        }

        #endregion

        #region Methods

        public Bitmap ToBitmap()
        {
            this.ThrowIfDisposed();

            if (this.BufferStatus != 1)
                return null;

            var dst = this.Dst;
            var usrData = this.UsrData;
            var systemBuffer = usrData.SystemBuffer;

            unsafe
            {
                var yPlane = (byte*)dst[0].ToPointer();
                var yWidth = systemBuffer.Width;
                var yHeight = systemBuffer.Height;
                var yStride = systemBuffer.Stride[0];

                var uPlane = (byte*)dst[1].ToPointer();
                var uWidth = systemBuffer.Width / 2;
                var uHeight = systemBuffer.Height / 2;
                var uStride = systemBuffer.Stride[1];

                var vPlane = (byte*)dst[2].ToPointer();
                var vWidth = systemBuffer.Width / 2;
                var vHeight = systemBuffer.Height / 2;
                var vStride = systemBuffer.Stride[1];

                var width = yWidth;
                var height = yHeight;
                var stride = yStride;

                var rgb = Yuv420PtoRgb(yPlane, uPlane, vPlane, width, height, stride);
                fixed (byte* pRgb = &rgb[0])
                    return RgbToBitmap(pRgb, width, height);
            }
        }

        #region Helpers
        
        private static unsafe byte[] Yuv420PtoRgb(byte* yPlane, byte* uPlane, byte* vPlane, int width, int height, int stride)
        {
            // https://www.ite.or.jp/contents/keywords/FILE-20120103130828.pdf
            // https://msdn.microsoft.com/ja-jp/library/windows/desktop/dd206750(v=vs.85).aspx
            // https://stackoverflow.com/questions/16107165/convert-from-yuv-420-to-imagebgr-byte/16108293
            // https://gist.github.com/RicardoRodriguezPina/b90c4cef9c1646c0a9fe7faea8e06d63

            var result = new byte[width * height * 3];
            fixed (byte* pResult = &result[0])
            {
                var rgb = pResult;

                for (var y = 0; y < height; y++)
                {
                    var rowIdx = (stride * y);
                    var uvpIdx = (stride / 2) * (y / 2);

                    var pYp = yPlane + rowIdx;
                    var pUp = uPlane + uvpIdx;
                    var pVp = vPlane + uvpIdx;
                    
                    for (var x = 0; x < width; x += 2)
                    {
                        var c1 = pYp[0] - 16;
                        var c2 = pYp[1] - 16;
                        var d = *pUp - 128;
                        var e = *pVp - 128;

                        var r1 = (298 * c1 + 409 * e + 128) >> 8;
                        var g1 = (298 * c1 - 100 * d - 208 * e + 128) >> 8;
                        var b1 = (298 * c1 + 516 * d + 128) >> 8;

                        var r2 = (298 * c2 + 409 * e + 128) >> 8;
                        var g2 = (298 * c2 - 100 * d - 208 * e + 128) >> 8;
                        var b2 = (298 * c2 + 516 * d + 128) >> 8;

                        rgb[0] = (byte)(b1 < 0 ? 0 : b1 > 255 ? 255 : b1);
                        rgb[1] = (byte)(g1 < 0 ? 0 : g1 > 255 ? 255 : g1);
                        rgb[2] = (byte)(r1 < 0 ? 0 : r1 > 255 ? 255 : r1);

                        rgb[3] = (byte)(b2 < 0 ? 0 : b2 > 255 ? 255 : b2);
                        rgb[4] = (byte)(g2 < 0 ? 0 : g2 > 255 ? 255 : g2);
                        rgb[5] = (byte)(r2 < 0 ? 0 : r2 > 255 ? 255 : r2);

                        rgb += 6;
                        pYp += 2;
                        pUp += 1;
                        pVp += 1;
                    }
                }
            }

            return result;
        }

        private static unsafe Bitmap RgbToBitmap(byte* rgb, int width, int height)
        {
            const int pixelSize = 3;
            var bmp = new Bitmap(width, height, PixelFormat.Format24bppRgb);
            var bmpData = bmp.LockBits(new Rectangle(0, 0, width, height),ImageLockMode.WriteOnly, bmp.PixelFormat);
            var ptr = (byte*)bmpData.Scan0.ToPointer();
            var stride = bmpData.Stride;

            var cnt = 0;
            for (var y = 0; y < height; y++)
            {
                var xStartPos = y * stride;
                for (var x = 0; x < width; x++)
                {
                    var pos = xStartPos + x * pixelSize;
                    ptr[pos + 0] = rgb[cnt + 0];
                    ptr[pos + 1] = rgb[cnt + 1];
                    ptr[pos + 2] = rgb[cnt + 2];
                    cnt += pixelSize;
                }
            }
            
            bmp.UnlockBits(bmpData);

            return bmp;
        }

        #endregion

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

            NativeMethods.openh264_SBufferInfo_delete(this.NativePtr);
        }

        #endregion

    }

}