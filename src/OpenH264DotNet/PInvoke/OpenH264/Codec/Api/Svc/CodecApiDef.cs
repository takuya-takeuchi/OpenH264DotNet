using System;
using System.Runtime.InteropServices;

// ReSharper disable once CheckNamespace
namespace OpenH264DotNet
{

    internal sealed partial class NativeMethods
    {

        [StructLayout(LayoutKind.Sequential)]
        public struct OpenH264Version
        {

            public uint uMajor;                  ///< The major version number
                                                 /// 
            public uint uMinor;                  ///< The minor version number
                                                 /// 
            public uint uRevision;               ///< The revision number
                                                 /// 
            public uint uReserved;               ///< The reserved number, it should be 0.

        }

    }

}