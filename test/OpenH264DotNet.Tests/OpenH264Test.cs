using Xunit;

// ReSharper disable once CheckNamespace
namespace OpenH264DotNet.Tests
{

    public sealed class OpenH264Test : TestBase
    {

        #region Fields

        private const string Version = "2.1.1.2005";

        #endregion

        [Fact]
        public void WelsCreateDecoder()
        {
            var decoder = OpenH264.WelsCreateDecoder();
            Assert.NotNull(decoder);
            decoder.Dispose();
        }

        [Fact]
        public void WelsCreateSVCEncoder()
        {
            var encoder = OpenH264.WelsCreateSVCEncoder();
            Assert.NotNull(encoder);
            encoder.Dispose();
        }

        [Fact]
        public void WelsDestroyDecoder()
        {
            var decoder = OpenH264.WelsCreateDecoder();
            OpenH264.WelsDestroyDecoder(decoder);
            Assert.True(decoder.IsDisposed);
        }

        [Fact]
        public void WelsDestroySVCEncoder()
        {
            var encoder = OpenH264.WelsCreateSVCEncoder();
            OpenH264.WelsDestroySVCEncoder(encoder);
            Assert.True(encoder.IsDisposed);
        }

        [Fact]
        public void WelsGetCodecVersion()
        {
            var version = OpenH264.WelsGetCodecVersion();
            Assert.Equal(Version, $"{version.Major}.{version.Minor}.{version.Revision}.{version.Reserved}");
        }

        [Fact]
        public void WelsGetCodecVersionEx()
        {
            OpenH264.WelsGetCodecVersionEx(out var version);
            Assert.Equal(Version, $"{version.Major}.{version.Minor}.{version.Revision}.{version.Reserved}");
        }

    }

}
