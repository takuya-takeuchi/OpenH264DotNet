namespace OpenH264DotNet
{

    /// <summary>
    /// Enumerate the type of error concealment methods.
    /// </summary>
    public enum ErrorConIdc
    {

        Disable = 0,

        FrameCopy,

        SliceCopy,

        FrameCopyCrossIdr,

        SliceCopyCrossIdr,

        SliceCopyCrossIdrFreezeResChange,

        SliceMvCopyCrossIdr,

        SliceMvCopyCrossIdrFreezeResChange

    }

}