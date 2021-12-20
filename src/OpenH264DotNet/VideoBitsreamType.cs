namespace OpenH264DotNet
{

    /// <summary>
    /// Enumerate the type of video bitstream which is provided to decoder.
    /// </summary>
    public enum VideoBitstreamType
    {

        Avc = NativeMethods.VIDEO_BITSTREAM_TYPE.VIDEO_BITSTREAM_AVC,

        Svc = NativeMethods.VIDEO_BITSTREAM_TYPE.VIDEO_BITSTREAM_SVC,

        Default = NativeMethods.VIDEO_BITSTREAM_TYPE.VIDEO_BITSTREAM_DEFAULT

    }

}