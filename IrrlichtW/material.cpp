#include "material.h"

SMaterial *GetMatFromIntPtr(IntPtr material)
{
    return ((SMaterial*)material);
}

void Material_GetAmbientColor(IntPtr material, M_SCOLOR color)
{
    UM_SCOLOR(GetMatFromIntPtr(material)->AmbientColor, color);
}
void Material_GetDiffuseColor(IntPtr material, M_SCOLOR color)
{
    UM_SCOLOR(GetMatFromIntPtr(material)->DiffuseColor, color);
}
void Material_GetEmissiveColor(IntPtr material, M_SCOLOR color)
{
    UM_SCOLOR(GetMatFromIntPtr(material)->EmissiveColor, color);
}
E_MATERIAL_TYPE Material_GetMaterialType(IntPtr material)
{
    return GetMatFromIntPtr(material)->MaterialType;
}
float Material_GetMaterialTypeParam(IntPtr material)
{
    return GetMatFromIntPtr(material)->MaterialTypeParam;
}
float Material_GetShininess(IntPtr material)
{
    return GetMatFromIntPtr(material)->Shininess;
}
void Material_GetSpecularColor(IntPtr material, M_SCOLOR color)
{
    UM_SCOLOR(GetMatFromIntPtr(material)->SpecularColor, color);
}
IntPtr Material_GetTexture1(IntPtr material)
{
    return GetMatFromIntPtr(material)->Texture1;
}
IntPtr Material_GetTexture2(IntPtr material)
{
    return GetMatFromIntPtr(material)->Texture2;
}
IntPtr Material_GetTexture3(IntPtr material)
{
    return GetMatFromIntPtr(material)->Texture3;
}
IntPtr Material_GetTexture4(IntPtr material)
{
    return GetMatFromIntPtr(material)->Texture4;
}
bool Material_GetAnisotropicFilter(IntPtr material)
{
    _FIX_BOOL_MARSHAL_BUG(GetMatFromIntPtr(material)->AnisotropicFilter);
}
bool Material_GetBackfaceCulling(IntPtr material)
{
    _FIX_BOOL_MARSHAL_BUG(GetMatFromIntPtr(material)->BackfaceCulling);
}
bool Material_GetBilinearFilter(IntPtr material)
{
    _FIX_BOOL_MARSHAL_BUG(GetMatFromIntPtr(material)->BilinearFilter);
}
bool Material_GetFogEnable(IntPtr material)
{
    _FIX_BOOL_MARSHAL_BUG(GetMatFromIntPtr(material)->FogEnable);
}
bool Material_GetGouraudShading(IntPtr material)
{
    _FIX_BOOL_MARSHAL_BUG(GetMatFromIntPtr(material)->GouraudShading);
}
bool Material_GetLighting(IntPtr material)
{
    _FIX_BOOL_MARSHAL_BUG(GetMatFromIntPtr(material)->Lighting);
}
bool Material_GetNormalizeNormals(IntPtr material)
{
    _FIX_BOOL_MARSHAL_BUG(GetMatFromIntPtr(material)->NormalizeNormals);
}
bool Material_GetTrilinearFilter(IntPtr material)
{
    _FIX_BOOL_MARSHAL_BUG(GetMatFromIntPtr(material)->TrilinearFilter);
}
bool Material_GetWireframe(IntPtr material)
{
    _FIX_BOOL_MARSHAL_BUG(GetMatFromIntPtr(material)->Wireframe);
}
bool Material_GetZBuffer(IntPtr material)
{
    _FIX_BOOL_MARSHAL_BUG(GetMatFromIntPtr(material)->ZBuffer);
}
bool Material_GetZWriteEnable(IntPtr material)
{
    _FIX_BOOL_MARSHAL_BUG(GetMatFromIntPtr(material)->ZWriteEnable);
}
void Material_SetAmbientColor(IntPtr material, M_SCOLOR color)
{
    GetMatFromIntPtr(material)->AmbientColor = MU_SCOLOR(color);
}
void Material_SetDiffuseColor(IntPtr material, M_SCOLOR color)
{
    GetMatFromIntPtr(material)->DiffuseColor = MU_SCOLOR(color);
}
void Material_SetEmissiveColor(IntPtr material, M_SCOLOR color)
{
    GetMatFromIntPtr(material)->EmissiveColor = MU_SCOLOR(color);
}
void Material_SetMaterialType(IntPtr material, E_MATERIAL_TYPE val)
{
    GetMatFromIntPtr(material)->MaterialType = val;
}
void Material_SetMaterialTypeParam(IntPtr material, float val)
{
    GetMatFromIntPtr(material)->MaterialTypeParam = val;
}
void Material_SetShininess(IntPtr material, float val)
{
    GetMatFromIntPtr(material)->Shininess = val;
}
void Material_SetSpecularColor(IntPtr material, M_SCOLOR color)
{
    GetMatFromIntPtr(material)->SpecularColor = MU_SCOLOR(color);
}
void Material_SetTexture1(IntPtr material, IntPtr text)
{
    GetMatFromIntPtr(material)->Texture1 = (ITexture*)text;
}
void Material_SetTexture2(IntPtr material, IntPtr text)
{
    GetMatFromIntPtr(material)->Texture2 = (ITexture*)text;
}
void Material_SetTexture3(IntPtr material, IntPtr text)
{
    GetMatFromIntPtr(material)->Texture3 = (ITexture*)text;
}
void Material_SetTexture4(IntPtr material, IntPtr text)
{
    GetMatFromIntPtr(material)->Texture4 = (ITexture*)text;
}
void Material_SetAnisotropicFilter(IntPtr material, bool val)
{
    GetMatFromIntPtr(material)->AnisotropicFilter = val;
}
void Material_SetBackfaceCulling(IntPtr material, bool val)
{
    GetMatFromIntPtr(material)->BackfaceCulling = val;
}
void Material_SetBilinearFilter(IntPtr material, bool val)
{
    GetMatFromIntPtr(material)->BilinearFilter = val;
}
void Material_SetFogEnable(IntPtr material, bool val)
{
    GetMatFromIntPtr(material)->FogEnable = val;
}
void Material_SetGouraudShading(IntPtr material, bool val)
{
    GetMatFromIntPtr(material)->GouraudShading = val;
}
void Material_SetLighting(IntPtr material, bool val)
{
    GetMatFromIntPtr(material)->Lighting = val;
}
void Material_SetNormalizeNormals(IntPtr material, bool val)
{
    GetMatFromIntPtr(material)->NormalizeNormals = val;
}
void Material_SetTrilinearFilter(IntPtr material, bool val)
{
    GetMatFromIntPtr(material)->TrilinearFilter = val;
}
void Material_SetWireframe(IntPtr material, bool val)
{
    GetMatFromIntPtr(material)->Wireframe = val;
}
void Material_SetZBuffer(IntPtr material, bool val)
{
    GetMatFromIntPtr(material)->ZBuffer = val;
}
void Material_SetZWriteEnable(IntPtr material, bool val)
{
    GetMatFromIntPtr(material)->ZWriteEnable = val;
}
