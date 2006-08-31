#include "meshmanipulator.h"

IMeshManipulator *GetMMForIntPtr(IntPtr mm)
{
	return (IMeshManipulator*)mm;
}

IntPtr MeshManipulator_CreateMeshWithTangents(IntPtr mm, IntPtr mesh)
{
	return GetMMForIntPtr(mm)->createMeshWithTangents((IMesh*)mesh);
}

IntPtr MeshManipulator_CreateMeshUniquePrimitives(IntPtr mm, IntPtr mesh)
{
    return GetMMForIntPtr(mm)->createMeshUniquePrimitives((IMesh*)mesh);
}

void MeshManipulator_MakePlanarTextureMapping(IntPtr mm, IntPtr mesh, float resolution)
{
    GetMMForIntPtr(mm)->makePlanarTextureMapping((IMesh*)mesh, resolution);
}

void MeshManipulator_FlipSurfaces(IntPtr mm, IntPtr mesh)
{
	GetMMForIntPtr(mm)->flipSurfaces((IMesh*)mesh);
}

void MeshManipulator_RecalculateNormals(IntPtr mm, IntPtr mesh, bool smooth)
{
	GetMMForIntPtr(mm)->recalculateNormals((IMesh*)mesh, smooth);
}

void MeshManipulator_ScaleMesh(IntPtr mm, IntPtr mesh, M_VECT3DF scale)
{
	GetMMForIntPtr(mm)->scaleMesh((IMesh*)mesh, MU_VECT3DF(scale));
}

void MeshManipulator_SetVertexColorAlpha(IntPtr mm, IntPtr mesh, int alpha)
{
	GetMMForIntPtr(mm)->setVertexColorAlpha((IMesh*)mesh, alpha);
}

void MeshManipulator_SetVertexColors(IntPtr mm, IntPtr mesh, M_SCOLOR alpha)
{
	GetMMForIntPtr(mm)->setVertexColors((IMesh*)mesh, MU_SCOLOR(alpha));
}

int MeshManipulator_GetPolyCount(IntPtr mm, IntPtr mesh)
{
	return GetMMForIntPtr(mm)->getPolyCount((IMesh*)mesh);
}

int MeshManipulator_GetPolyCountA(IntPtr mm, IntPtr amesh)
{
	return GetMMForIntPtr(mm)->getPolyCount((IAnimatedMesh*)amesh);
}