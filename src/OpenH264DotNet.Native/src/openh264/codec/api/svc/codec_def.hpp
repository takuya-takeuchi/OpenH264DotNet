#ifndef _CPP_OPENH264_CODEC_API_SVC_CODEC_DEF_H_
#define _CPP_OPENH264_CODEC_API_SVC_CODEC_DEF_H_

#include "../../../../export.hpp"
#include "../../../../shared.hpp"
#include <codec/api/svc/codec_def.h>

#pragma region SBufferInfo

DLLEXPORT SBufferInfo* openh264_SBufferInfo_new()
{
    auto ret = new SBufferInfo();
    memset(ret, 0, sizeof(SBufferInfo));
    return ret;
}

DLLEXPORT void openh264_SBufferInfo_delete(const SBufferInfo* info)
{
    delete info;
}

DLLEXPORT const int32_t openh264_SBufferInfo_get_iBufferStatus(const SBufferInfo* info)
{
    return info->iBufferStatus;
}

DLLEXPORT const uint64_t openh264_SBufferInfo_get_uiInBsTimeStamp(const SBufferInfo* info)
{
    return info->uiInBsTimeStamp;
}

DLLEXPORT const uint64_t openh264_SBufferInfo_get_uiOutYuvTimeStamp(const SBufferInfo* info)
{
    return info->uiOutYuvTimeStamp;
}

DLLEXPORT void* openh264_SBufferInfo_get_UsrData(SBufferInfo* const info)
{
    return &info->UsrData;
}

DLLEXPORT const SSysMEMBuffer* openh264_SBufferInfo_get_UsrData_sSystemBuffer(SBufferInfo* const info)
{
    return &info->UsrData.sSystemBuffer;
}

DLLEXPORT void openh264_SBufferInfo_get_pDst(SBufferInfo* const info,
                                             unsigned char** pDst,
                                             int pDstLen)
{
    const int max = std::min(3, pDstLen);
    for (auto i = 0; i < max; i++)
        pDst[i] = info->pDst[i];
}

#pragma endregion SBufferInfo

#pragma region SSysMEMBuffer

DLLEXPORT const int32_t openh264_SSysMEMBuffer_get_iWidth(const SSysMEMBuffer* buffer)
{
    return buffer->iWidth;
}

DLLEXPORT const int32_t openh264_SSysMEMBuffer_get_iHeight(const SSysMEMBuffer* buffer)
{
    return buffer->iHeight;
}

DLLEXPORT const int32_t openh264_SSysMEMBuffer_get_iFormat(const SSysMEMBuffer* buffer)
{
    return buffer->iFormat;
}

DLLEXPORT int32_t* openh264_SSysMEMBuffer_get_iStride(SSysMEMBuffer* const buffer)
{
    return &buffer->iStride[0];
}

#pragma endregion SSysMEMBuffer

#endif // _CPP_OPENH264_CODEC_API_SVC_CODEC_DEF_H_