﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageFramework.ImageLoader
{
    public enum GliFormat
    {
        UNDEFINED = 0,

        FORMAT_FIRST, RG4_UNORM = FORMAT_FIRST,
        RGBA4_UNORM,
        BGRA4_UNORM,
        R5G6B5_UNORM,
        B5G6R5_UNORM,
        RGB5A1_UNORM,
        BGR5A1_UNORM,
        A1RGB5_UNORM,

        R8_UNORM,
        R8_SNORM,
        R8_USCALED,
        R8_SSCALED,
        R8_UINT,
        R8_SINT,
        R8_SRGB,

        RG8_UNORM,
        RG8_SNORM,
        RG8_USCALED,
        RG8_SSCALED,
        RG8_UINT,
        RG8_SINT,
        RG8_SRGB,

        RGB8_UNORM,
        RGB8_SNORM,
        RGB8_USCALED,
        RGB8_SSCALED,
        RGB8_UINT,
        RGB8_SINT,
        RGB8_SRGB,

        BGR8_UNORM,
        BGR8_SNORM,
        BGR8_USCALED,
        BGR8_SSCALED,
        BGR8_UINT,
        BGR8_SINT,
        BGR8_SRGB,

        RGBA8_UNORM,
        RGBA8_SNORM,
        RGBA8_USCALED,
        RGBA8_SSCALED,
        RGBA8_UINT,
        RGBA8_SINT,
        RGBA8_SRGB,

        BGRA8_UNORM,
        BGRA8_SNORM,
        BGRA8_USCALED,
        BGRA8_SSCALED,
        BGRA8_UINT,
        BGRA8_SINT,
        BGRA8_SRGB,

        RGBA8_UNORM_PACK32,
        RGBA8_SNORM_PACK32,
        RGBA8_USCALED_PACK32,
        RGBA8_SSCALED_PACK32,
        RGBA8_UINT_PACK32,
        RGBA8_SINT_PACK32,
        RGBA8_SRGB_PACK32,

        RGB10A2_UNORM,
        RGB10A2_SNORM,
        RGB10A2_USCALED,
        RGB10A2_SSCALED,
        RGB10A2_UINT,
        RGB10A2_SINT,

        BGR10A2_UNORM,
        BGR10A2_SNORM,
        BGR10A2_USCALED,
        BGR10A2_SSCALED,
        BGR10A2_UINT,
        BGR10A2_SINT,

        R16_UNORM,
        R16_SNORM,
        R16_USCALED,
        R16_SSCALED,
        R16_UINT,
        R16_SINT,
        R16_SFLOAT,

        RG16_UNORM,
        RG16_SNORM,
        RG16_USCALED,
        RG16_SSCALED,
        RG16_UINT,
        RG16_SINT,
        RG16_SFLOAT,

        RGB16_UNORM,
        RGB16_SNORM,
        RGB16_USCALED,
        RGB16_SSCALED,
        RGB16_UINT,
        RGB16_SINT,
        RGB16_SFLOAT,

        RGBA16_UNORM,
        RGBA16_SNORM,
        RGBA16_USCALED,
        RGBA16_SSCALED,
        RGBA16_UINT,
        RGBA16_SINT,
        RGBA16_SFLOAT,

        R32_UINT,
        R32_SINT,
        R32_SFLOAT,

        RG32_UINT,
        RG32_SINT,
        RG32_SFLOAT,

        RGB32_UINT,
        RGB32_SINT,
        RGB32_SFLOAT,

        RGBA32_UINT,
        RGBA32_SINT,
        RGBA32_SFLOAT,

        R64_UINT,
        R64_SINT,
        R64_SFLOAT,

        RG64_UINT,
        RG64_SINT,
        RG64_SFLOAT,

        RGB64_UINT,
        RGB64_SINT,
        RGB64_SFLOAT,

        RGBA64_UINT,
        RGBA64_SINT,
        RGBA64_SFLOAT,

        RG11B10_UFLOAT,
        RGB9E5_UFLOAT,

        D16_UNORM,
        D24_UNORM_PACK32,
        D32_SFLOAT,
        S8_UINT,
        D16_UNORM_S8_UINT_PACK32,
        D24_UNORM_S8_UINT,
        D32_SFLOAT_S8_UINT_PACK64,

        RGB_DXT1_UNORM_BLOCK8,
        RGB_DXT1_SRGB_BLOCK8,
        RGBA_DXT1_UNORM_BLOCK8,
        RGBA_DXT1_SRGB_BLOCK8,
        RGBA_DXT3_UNORM_BLOCK16,
        RGBA_DXT3_SRGB_BLOCK16,
        RGBA_DXT5_UNORM_BLOCK16,
        RGBA_DXT5_SRGB_BLOCK16,
        R_ATI1N_UNORM_BLOCK8,
        R_ATI1N_SNORM_BLOCK8,
        RG_ATI2N_UNORM_BLOCK16,
        RG_ATI2N_SNORM_BLOCK16,
        RGB_BP_UFLOAT_BLOCK16,
        RGB_BP_SFLOAT_BLOCK16,
        RGBA_BP_UNORM_BLOCK16,
        RGBA_BP_SRGB_BLOCK16,

        RGB_ETC2_UNORM_BLOCK8,
        RGB_ETC2_SRGB_BLOCK8,
        RGBA_ETC2_UNORM_BLOCK8,
        RGBA_ETC2_SRGB_BLOCK8,
        RGBA_ETC2_UNORM_BLOCK16,
        RGBA_ETC2_SRGB_BLOCK16,
        R_EAC_UNORM_BLOCK8,
        R_EAC_SNORM_BLOCK8,
        RG_EAC_UNORM_BLOCK16,
        RG_EAC_SNORM_BLOCK16,

        RGBA_ASTC_4X4_UNORM_BLOCK16,
        RGBA_ASTC_4X4_SRGB_BLOCK16,
        RGBA_ASTC_5X4_UNORM_BLOCK16,
        RGBA_ASTC_5X4_SRGB_BLOCK16,
        RGBA_ASTC_5X5_UNORM_BLOCK16,
        RGBA_ASTC_5X5_SRGB_BLOCK16,
        RGBA_ASTC_6X5_UNORM_BLOCK16,
        RGBA_ASTC_6X5_SRGB_BLOCK16,
        RGBA_ASTC_6X6_UNORM_BLOCK16,
        RGBA_ASTC_6X6_SRGB_BLOCK16,
        RGBA_ASTC_8X5_UNORM_BLOCK16,
        RGBA_ASTC_8X5_SRGB_BLOCK16,
        RGBA_ASTC_8X6_UNORM_BLOCK16,
        RGBA_ASTC_8X6_SRGB_BLOCK16,
        RGBA_ASTC_8X8_UNORM_BLOCK16,
        RGBA_ASTC_8X8_SRGB_BLOCK16,
        RGBA_ASTC_10X5_UNORM_BLOCK16,
        RGBA_ASTC_10X5_SRGB_BLOCK16,
        RGBA_ASTC_10X6_UNORM_BLOCK16,
        RGBA_ASTC_10X6_SRGB_BLOCK16,
        RGBA_ASTC_10X8_UNORM_BLOCK16,
        RGBA_ASTC_10X8_SRGB_BLOCK16,
        RGBA_ASTC_10X10_UNORM_BLOCK16,
        RGBA_ASTC_10X10_SRGB_BLOCK16,
        RGBA_ASTC_12X10_UNORM_BLOCK16,
        RGBA_ASTC_12X10_SRGB_BLOCK16,
        RGBA_ASTC_12X12_UNORM_BLOCK16,
        RGBA_ASTC_12X12_SRGB_BLOCK16,

        RGB_PVRTC1_8X8_UNORM_BLOCK32,
        RGB_PVRTC1_8X8_SRGB_BLOCK32,
        RGB_PVRTC1_16X8_UNORM_BLOCK32,
        RGB_PVRTC1_16X8_SRGB_BLOCK32,
        RGBA_PVRTC1_8X8_UNORM_BLOCK32,
        RGBA_PVRTC1_8X8_SRGB_BLOCK32,
        RGBA_PVRTC1_16X8_UNORM_BLOCK32,
        RGBA_PVRTC1_16X8_SRGB_BLOCK32,
        RGBA_PVRTC2_4X4_UNORM_BLOCK8,
        RGBA_PVRTC2_4X4_SRGB_BLOCK8,
        RGBA_PVRTC2_8X4_UNORM_BLOCK8,
        RGBA_PVRTC2_8X4_SRGB_BLOCK8,

        RGB_ETC_UNORM_BLOCK8,
        RGB_ATC_UNORM_BLOCK8,
        RGBA_ATCA_UNORM_BLOCK16,
        RGBA_ATCI_UNORM_BLOCK16,

        L8_UNORM,
        A8_UNORM,
        LA8_UNORM,
        L16_UNORM,
        A16_UNORM,
        LA16_UNORM,

        BGR8_UNORM_PACK32,
        BGR8_SRGB_PACK32,

        RG3B2_UNORM, LAST = RG3B2_UNORM
    };

    public static class GliFormatExtensions
    {
        public static bool IsCompressed(this GliFormat format)
        {
            switch (format)
            {
                case GliFormat.RGB_DXT1_UNORM_BLOCK8:                    
                case GliFormat.RGB_DXT1_SRGB_BLOCK8:                    
                case GliFormat.RGBA_DXT1_UNORM_BLOCK8:                    
                case GliFormat.RGBA_DXT1_SRGB_BLOCK8:                    
                case GliFormat.RGBA_DXT3_UNORM_BLOCK16:                    
                case GliFormat.RGBA_DXT3_SRGB_BLOCK16:                    
                case GliFormat.RGBA_DXT5_UNORM_BLOCK16:                    
                case GliFormat.RGBA_DXT5_SRGB_BLOCK16:                    
                case GliFormat.R_ATI1N_UNORM_BLOCK8:                    
                case GliFormat.R_ATI1N_SNORM_BLOCK8:                    
                case GliFormat.RG_ATI2N_UNORM_BLOCK16:                    
                case GliFormat.RG_ATI2N_SNORM_BLOCK16:                    
                case GliFormat.RGB_BP_UFLOAT_BLOCK16:                    
                case GliFormat.RGB_BP_SFLOAT_BLOCK16:                    
                case GliFormat.RGBA_BP_UNORM_BLOCK16:                    
                case GliFormat.RGBA_BP_SRGB_BLOCK16:                    
                case GliFormat.RGB_ETC2_UNORM_BLOCK8:                   
                case GliFormat.RGB_ETC2_SRGB_BLOCK8:                    
                case GliFormat.RGBA_ETC2_UNORM_BLOCK8:                    
                case GliFormat.RGBA_ETC2_SRGB_BLOCK8:                    
                case GliFormat.RGBA_ETC2_UNORM_BLOCK16:                    
                case GliFormat.RGBA_ETC2_SRGB_BLOCK16:                    
                case GliFormat.R_EAC_UNORM_BLOCK8:                    
                case GliFormat.R_EAC_SNORM_BLOCK8:                    
                case GliFormat.RG_EAC_UNORM_BLOCK16:                    
                case GliFormat.RG_EAC_SNORM_BLOCK16:                    
                case GliFormat.RGBA_ASTC_4X4_UNORM_BLOCK16:                    
                case GliFormat.RGBA_ASTC_4X4_SRGB_BLOCK16:                    
                case GliFormat.RGBA_ASTC_5X4_UNORM_BLOCK16:                    
                case GliFormat.RGBA_ASTC_5X4_SRGB_BLOCK16:                    
                case GliFormat.RGBA_ASTC_5X5_UNORM_BLOCK16:                    
                case GliFormat.RGBA_ASTC_5X5_SRGB_BLOCK16:                    
                case GliFormat.RGBA_ASTC_6X5_UNORM_BLOCK16:                    
                case GliFormat.RGBA_ASTC_6X5_SRGB_BLOCK16:                    
                case GliFormat.RGBA_ASTC_6X6_UNORM_BLOCK16:                    
                case GliFormat.RGBA_ASTC_6X6_SRGB_BLOCK16:                    
                case GliFormat.RGBA_ASTC_8X5_UNORM_BLOCK16:                    
                case GliFormat.RGBA_ASTC_8X5_SRGB_BLOCK16:                    
                case GliFormat.RGBA_ASTC_8X6_UNORM_BLOCK16:                    
                case GliFormat.RGBA_ASTC_8X6_SRGB_BLOCK16:                    
                case GliFormat.RGBA_ASTC_8X8_UNORM_BLOCK16:                    
                case GliFormat.RGBA_ASTC_8X8_SRGB_BLOCK16:                    
                case GliFormat.RGBA_ASTC_10X5_UNORM_BLOCK16:                    
                case GliFormat.RGBA_ASTC_10X5_SRGB_BLOCK16:                    
                case GliFormat.RGBA_ASTC_10X6_UNORM_BLOCK16:                    
                case GliFormat.RGBA_ASTC_10X6_SRGB_BLOCK16:                    
                case GliFormat.RGBA_ASTC_10X8_UNORM_BLOCK16:                    
                case GliFormat.RGBA_ASTC_10X8_SRGB_BLOCK16:                    
                case GliFormat.RGBA_ASTC_10X10_UNORM_BLOCK16:                    
                case GliFormat.RGBA_ASTC_10X10_SRGB_BLOCK16:                    
                case GliFormat.RGBA_ASTC_12X10_UNORM_BLOCK16:                    
                case GliFormat.RGBA_ASTC_12X10_SRGB_BLOCK16:                    
                case GliFormat.RGBA_ASTC_12X12_UNORM_BLOCK16:                    
                case GliFormat.RGBA_ASTC_12X12_SRGB_BLOCK16:                    
                case GliFormat.RGB_PVRTC1_8X8_UNORM_BLOCK32:                    
                case GliFormat.RGB_PVRTC1_8X8_SRGB_BLOCK32:                    
                case GliFormat.RGB_PVRTC1_16X8_UNORM_BLOCK32:                    
                case GliFormat.RGB_PVRTC1_16X8_SRGB_BLOCK32:                    
                case GliFormat.RGBA_PVRTC1_8X8_UNORM_BLOCK32:                    
                case GliFormat.RGBA_PVRTC1_8X8_SRGB_BLOCK32:                    
                case GliFormat.RGBA_PVRTC1_16X8_UNORM_BLOCK32:                    
                case GliFormat.RGBA_PVRTC1_16X8_SRGB_BLOCK32:                    
                case GliFormat.RGBA_PVRTC2_4X4_UNORM_BLOCK8:                    
                case GliFormat.RGBA_PVRTC2_4X4_SRGB_BLOCK8:                    
                case GliFormat.RGBA_PVRTC2_8X4_UNORM_BLOCK8:
                case GliFormat.RGBA_PVRTC2_8X4_SRGB_BLOCK8:
                case GliFormat.RGB_ETC_UNORM_BLOCK8:
                case GliFormat.RGB_ATC_UNORM_BLOCK8:
                case GliFormat.RGBA_ATCA_UNORM_BLOCK16:
                case GliFormat.RGBA_ATCI_UNORM_BLOCK16:
                    return true;
            }

            return false;
        }
    }
}
