#pragma once
#include <cstdint>
#include <string>

#define EXPORT(rtype) extern "C" __declspec(dllexport) rtype __cdecl

/// \brief tries to open the file with the given filename
/// \param filename absolute or relative path
/// \return returns a non zero integer on success.
/// The error can be retrieved with get_error on failure.
EXPORT(int) image_open(const char* filename);

/// \brief allocates a texture with the given amount of layers and levels
/// \param format dxgi texture format (must be one of the compatible formats, see Image.h)
/// \param width width in pixels
/// \param height height in pixels
/// \param layer number of layers
/// \param mipmaps number of mipmap levels 
EXPORT(int) image_allocate(uint32_t format, int width, int height, int layer, int mipmaps);

/// \brief releases all resources from the file with the given id
EXPORT(void) image_release(int id);

/// \brief retrieves image info
/// \brief format internal texture format, will be one of the compatible formats from Image.h
EXPORT(void) image_info(int id, uint32_t& format, int& nLayer, int& nMipmaps);

/// \brief retrieve info for one mipmap
EXPORT(void) image_info_mipmap(int id, int mipmap, int& width, int& height);

/// \brief get mipmap bytes
/// \return mipmap data. Can also be used to write mipmap data
EXPORT(unsigned char*) image_get_mipmap(int id, int layer, int mipmap, uint32_t& size);

/// \brief saves the image with the given format and extension
/// \param format format that must be compatible with the extension. Can by queried with image_get_export_formats
/// \param quality quality for compressed formats or .jpg. range: [0, 100]
EXPORT(bool) image_save(int id, const char* filename, const char* extension, uint32_t format, int quality);

/// \brief retrieves an array with all supported dxgi formats that are available for export with the extension
EXPORT(const uint32_t*) get_export_formats(const char* extension, int& numFormats);

/// \brief get last error
EXPORT(const char*) get_error(int& length);

/// \brief set current error (for internal use only) 
void set_error(const std::string& str);