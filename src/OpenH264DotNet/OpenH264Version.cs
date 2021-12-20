namespace OpenH264DotNet
{

    /// <summary>
    /// A class represents OpenH264 version.
    /// </summary>
    public sealed class OpenH264Version
    {

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="OpenH264Object"/> class with the structure of OpenH264 version.
        /// </summary>
        internal OpenH264Version(NativeMethods.OpenH264Version version)
        {
            this.Major = version.uMajor;
            this.Minor = version.uMinor;
            this.Revision = version.uRevision;
            this.Reserved = version.uReserved;
        }

        #endregion

        #region Finalizer

        // IMPORTANT
        //      OpenH264DotNet passes and get native pointer to unmanaged domain and from.
        //      Sometimes, OpenH264DotNet create OpenH264Object from native pointer.
        //      There may be some OpenH264Object has same native pointer .
        //      It means that developer may dispose objects has same one.
        //      To avoid this, OpenH264Object has IsEnableDispose property.
        //      OpenH264Object checks this property when Dispose method is called.
        //      However, if OpenH264Object implements finalizer and developer forgets to dispose, OpenH264Object may
        //      be disposed by GC and native pointer will be corrupted.
        //      
        //      If user add OpenH264Object to StdVector<T> and set null to passed OpenH264Object.
        //      Generally, OpenH264Object means pointer and it should not disposed automatically.
        //      If it's disposed automatically, element passed to StdVector<T> also be corrupt.
        //      This problem is only occured on C# rather than C++ because GC and finalizer.
        //
        //      In conclusion, OpenH264DotNet doesn't implement finalizer.
        //
        ///// <summary>
        ///// Allows an object to try to free resources and perform other cleanup operations before it is reclaimed by garbage collection.
        ///// </summary>
        //~OpenH264Object()
        //{
        //    this.Dispose(false);
        //}

        #endregion

        #region Properties

        /// <summary>
        /// Gets a value indicating major version number.
        /// </summary>
        public uint Major
        {
            get;
        }

        /// <summary>
        /// Gets a value indicating minor version number.
        /// </summary>
        public uint Minor
        {
            get;
        }

        /// <summary>
        /// Gets a value indicating revision number.
        /// </summary>
        public uint Revision
        {
            get;
        }

        /// <summary>
        /// Gets a value indicating reserved number.
        /// </summary>
        public uint Reserved
        {
            get;
        }

        #endregion

    }

}
