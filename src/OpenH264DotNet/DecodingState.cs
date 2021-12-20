namespace OpenH264DotNet
{

    /// <summary>
    /// Enumerate the type of video bitstream which is provided to decoder.
    /// </summary>
    public enum DecodingState
    {

        ErrorFree = NativeMethods.DECODING_STATE.dsErrorFree,

        FramePending = NativeMethods.DECODING_STATE.dsFramePending,

        RefLost = NativeMethods.DECODING_STATE.dsRefLost,

        BitstreamError = NativeMethods.DECODING_STATE.dsBitstreamError,

        DepLayerLost = NativeMethods.DECODING_STATE.dsDepLayerLost,

        NoParamSets = NativeMethods.DECODING_STATE.dsNoParamSets,

        DataErrorConcealed = NativeMethods.DECODING_STATE.dsDataErrorConcealed,

        RefListNullPtrs = NativeMethods.DECODING_STATE.dsRefListNullPtrs,

        InvalidArgument = NativeMethods.DECODING_STATE.dsInvalidArgument,

        InitialOptExpected = NativeMethods.DECODING_STATE.dsInitialOptExpected,

        OutOfMemory = NativeMethods.DECODING_STATE.dsOutOfMemory,

        DstBufNeedExpan = NativeMethods.DECODING_STATE.dsDstBufNeedExpan

    }

}