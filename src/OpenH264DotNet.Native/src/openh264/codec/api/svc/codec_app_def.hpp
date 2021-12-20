#ifndef _CPP_OPENH264_CODEC_API_SVC_CODEC_APP_DEF_H_
#define _CPP_OPENH264_CODEC_API_SVC_CODEC_APP_DEF_H_

#include "../../../../export.hpp"
#include "../../../../shared.hpp"
#include <codec/api/svc/codec_app_def.h>

#pragma region SDecodingParam

DLLEXPORT SDecodingParam* openh264_SDecodingParam_new()
{
    auto ret = new SDecodingParam();
    memset(ret, 0, sizeof(SDecodingParam));
    return ret;
}

DLLEXPORT void openh264_SDecodingParam_delete(const SDecodingParam* param)
{
    delete param;
}

DLLEXPORT void openh264_SDecodingParam_set_uiCpuLoad(SDecodingParam* const param,
                                                     const uint32_t value)
{
    param->uiCpuLoad = value;
}

DLLEXPORT const uint32_t openh264_SDecodingParam_get_uiCpuLoad(const SDecodingParam* param)
{
    return param->uiCpuLoad;
}

DLLEXPORT void openh264_SDecodingParam_set_uiTargetDqLayer(SDecodingParam* const param,
                                                           const uint32_t value)
{
    param->uiTargetDqLayer = value;
}

DLLEXPORT const uint32_t openh264_SDecodingParam_get_uiTargetDqLayer(const SDecodingParam* param)
{
    return param->uiTargetDqLayer;
}

DLLEXPORT void openh264_SDecodingParam_set_eEcActiveIdc(SDecodingParam* const param,
                                                        const ERROR_CON_IDC value)
{
    param->eEcActiveIdc = value;
}

DLLEXPORT const ERROR_CON_IDC openh264_SDecodingParam_get_eEcActiveIdc(const SDecodingParam* param)
{
    return param->eEcActiveIdc;
}

DLLEXPORT void openh264_SDecodingParam_set_bParseOnly(SDecodingParam* const param,
                                                      const bool value)
{
    param->bParseOnly = value;
}

DLLEXPORT const bool openh264_SDecodingParam_get_bParseOnly(const SDecodingParam* param)
{
    return param->bParseOnly;
}

DLLEXPORT const SVideoProperty* openh264_SDecodingParam_get_SVideoProperty(SDecodingParam* const param)
{
    return &param->sVideoProperty;
}

#pragma endregion SDecodingParam

#pragma region SVideoProperty

DLLEXPORT void openh264_SVideoProperty_set_size(SVideoProperty* const property,
                                                const uint32_t value)
{
    property->size = value;
}

DLLEXPORT const uint32_t openh264_SVideoProperty_get_size(const SVideoProperty* property)
{
    return property->size;
}

DLLEXPORT void openh264_SVideoProperty_set_eVideoBsType(SVideoProperty* const property,
                                                        const VIDEO_BITSTREAM_TYPE value)
{
    property->eVideoBsType = value;
}

DLLEXPORT const VIDEO_BITSTREAM_TYPE openh264_SVideoProperty_get_eVideoBsType(const SVideoProperty* property)
{
    return property->eVideoBsType;
}

#pragma endregion SVideoProperty

#endif // _CPP_OPENH264_CODEC_API_SVC_CODEC_APP_DEF_H_