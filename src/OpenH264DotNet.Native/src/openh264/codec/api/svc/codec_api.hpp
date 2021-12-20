#ifndef _CPP_OPENH264_CODEC_API_SVC_CODEC_API_H_
#define _CPP_OPENH264_CODEC_API_SVC_CODEC_API_H_

#include "../../../../export.hpp"
#include "../../../../shared.hpp"
#include <codec/api/svc/codec_api.h>

#pragma region ISVCDecoder

DLLEXPORT const long openh264_ISVCDecoder_Initialize(ISVCDecoder* const decoder,
                                                     const SDecodingParam* pParam)
{
    return decoder->Initialize(pParam);
}

DLLEXPORT const long openh264_ISVCDecoder_Uninitialize(ISVCDecoder* const decoder)
{
    return decoder->Uninitialize();
}

DLLEXPORT const DECODING_STATE openh264_ISVCDecoder_DecodeFrame2(ISVCDecoder* const decoder,
                                                                 const unsigned char* pSrc,
                                                                 const int iSrcLen,
                                                                 unsigned char** ppDst,
                                                                 SBufferInfo* pDstInfo)
{
    return decoder->DecodeFrame2(pSrc, iSrcLen, ppDst, pDstInfo);
}

#pragma endregion ISVCDecoder

#endif // _CPP_OPENH264_CODEC_API_SVC_CODEC_API_H_