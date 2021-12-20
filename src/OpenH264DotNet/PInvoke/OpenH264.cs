﻿using System;
using System.Runtime.InteropServices;
using uint8_t = System.Byte;
using uint16_t = System.UInt16;
using uint32_t = System.UInt32;
using int64_t = System.Int64;
using int8_t = System.SByte;
using int16_t = System.Int16;
using int32_t = System.Int32;

// ReSharper disable once CheckNamespace
namespace OpenH264DotNet
{

    internal sealed partial class NativeMethods
    {

        internal enum ErrorType
        {

            OK = 0x00000000,

            #region General

            GeneralError = 0x76000000,

            GeneralFileIOError      = -(GeneralError | 0x00000001),

            GeneralOutOfRange       = -(GeneralError | 0x00000002),

            #endregion

            #region Image

            ImageError = 0x77000000,

            ImageFileInvalid        = -(ImageError | 0x00000001),

            ImageFileWrongExtension = -(ImageError | 0x00000002)

            #endregion

        }

    }

}