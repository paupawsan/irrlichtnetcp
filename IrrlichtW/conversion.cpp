#include "conversion.h"

void Pointer_SafeRelease(IntPtr pointer)
{
    #ifdef WIN32
    if(!pointer)
        return;
    irr::IUnknown* i = ( irr::IUnknown*)pointer;
    if(i)
    {
        i->drop();
        return;
    }
    delete pointer;
	#endif
}

#ifdef _MSC_VER
bool fixmarshal(bool val)
{
	__asm mov eax, 100;
	return val;
}
#endif

wchar_t *MU_WCHAR(const M_STRING base)
{
    size_t size = mbstowcs(NULL, base, 0);
    wchar_t *tor = new wchar_t[size + 1];
    for(unsigned int i = 0; i < size; i++)
        tor[i] = base[i];
    tor[size] = '\0';
    return tor;
}

M_STRING UM_STRING(const wchar_t* base)
{
    size_t size = wcstombs(NULL, base, 0);
    M_STRING tor = new char[size + 1];
    for(unsigned int i = 0; i < size; i++)
        tor[i] = (char)base[i];
    tor[size] = '\0';
    return tor;
}

M_STRING UM_STRING(const M_STRING base)
{
    // Bug found by Copland, to be tested, it seems that a C-style cast is enough (and needed on Windows ?) because mbstowcs is deprecated.
    #ifdef _MSC_VER
    return (M_STRING)base;
    #else
    size_t size = mbstowcs(NULL, base, 0);
    M_STRING tor = new char[size + 1];
    for(unsigned int i = 0; i < size; i++)
        tor[i] = base[i];
    tor[size] = '\0';
    return tor;
    #endif
}

void UM_DIM2DS(irr::core::dimension2d<int> base, M_DIM2DS t)
{
    t[0] = base.Width;
    t[1] = base.Height;
}

void UM_DIM2DF(irr::core::dimension2d<float> base, M_DIM2DF t)
{
    t[0] = base.Width;
    t[1] = base.Height;
}

void UM_POS2DS(irr::core::position2d<int> base, M_POS2DS t)
{
    t[0] = base.X;
    t[1] = base.Y;
}

void UM_POS2DF(irr::core::position2d<float> base, M_POS2DF t)
{
    t[0] = base.X;
    t[1] = base.Y;
}

void UM_SCOLOR(irr::video::SColor color, M_SCOLOR t)
{
    t[0] = color.getAlpha();
    t[1] = color.getRed();
    t[2] = color.getGreen();
    t[3] = color.getBlue();
}

void UM_SCOLORF(irr::video::SColorf color, M_SCOLORF t)
{
    t[0] = color.a;
    t[1] = color.r;
    t[2] = color.g;
    t[3] = color.b;
}

void UM_VECT3DF(irr::core::vector3df base, M_VECT3DF t)
{
    t[0] = base.X;
    t[1] = base.Y;
    t[2] = base.Z;
}

irr::core::matrix4 MU_MAT4(M_MAT4 val)
{
    irr::core::matrix4 mat;
    for(int row = 0; row < 4; row++)
        for(int col = 0; col < 4; col++)
            mat.M[col * 4 + row] = val[col * 4 + row];
    return mat;
}

void UM_MAT4(irr::core::matrix4 val, M_MAT4 mat)
{
    for(int row = 0; row < 4; row++)
        for(int col = 0; col < 4; col++)
            mat[col * 4 + row] = val.M[col * 4 + row];
}

void UM_BOX3D(irr::core::aabbox3d<float> base, M_BOX3D t)
{
    t[0] = base.MinEdge.X;
    t[1] = base.MinEdge.Y;
    t[2] = base.MinEdge.Z;
    t[3] = base.MaxEdge.X;
    t[4] = base.MaxEdge.Y;
    t[5] = base.MaxEdge.Z;
}

void UM_RECT(irr::core::rect<int> base, M_RECT t)
{
	t[0] = base.UpperLeftCorner.X;
    t[1] = base.UpperLeftCorner.Y;
	t[2] = base.LowerRightCorner.X;
    t[3] = base.LowerRightCorner.Y;
}

irr::core::triangle3df MU_TRIANGLE3DF(float *val)
{
	irr::core::triangle3df tri;
	tri.set(irr::core::vector3df(val[0], val[1], val[2]),
		    irr::core::vector3df(val[3], val[4], val[5]),
			irr::core::vector3df(val[6], val[7], val[8]));
	return tri;
}

void UM_TRIANGLE3DF(irr::core::triangle3d<float> base, M_TRIANGLE3DF t)
{
	t[0] = base.pointA.X;
	t[1] = base.pointA.Y;
	t[2] = base.pointA.Z;
	t[3] = base.pointB.X;
	t[4] = base.pointB.Y;
	t[5] = base.pointB.Z;
	t[6] = base.pointC.X;
	t[7] = base.pointC.Y;
	t[8] = base.pointC.Z;
}

void UM_LINE3D(irr::core::line3d<float> base, M_LINE3D t)
{
	t[0] = base.start.X;
	t[1] = base.start.Y;
	t[2] = base.start.Z;
	t[3] = base.end.X;
	t[4] = base.end.Y;
	t[5] = base.end.Z;
}

