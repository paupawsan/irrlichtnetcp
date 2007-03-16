#include "main.h"

extern "C"
{
    EXPORT void Mesh_GetBoundingBox(IntPtr mesh, M_BOX3D box);
    EXPORT void Mesh_SetMaterialFlag(IntPtr mesh, E_MATERIAL_FLAG flag, bool newValue);
	EXPORT int Mesh_GetMeshBufferCount(IntPtr mesh);
	EXPORT IntPtr Mesh_GetMeshBuffer(IntPtr mesh, int nr);

    EXPORT void AnimatedMesh_GetBoundingBox(IntPtr mesh, M_BOX3D box);
    EXPORT IntPtr AnimatedMesh_GetMesh(IntPtr mesh, int frame, int detailLevel, int startFrameloop, int endFrameloop);
    EXPORT E_ANIMATED_MESH_TYPE AnimatedMesh_GetMeshType(IntPtr mesh);

	EXPORT IntPtr MeshBuffer_Create(int type);
	EXPORT void MeshBuffer_GetBoundingBox(IntPtr meshb, M_BOX3D bb);
	EXPORT void MeshBuffer_SetBoundingBox(IntPtr meshb, M_BOX3D bb);
	EXPORT int MeshBuffer_GetIndexCount(IntPtr meshb);
	EXPORT void MeshBuffer_GetIndices(IntPtr meshb, unsigned short* indices);
	EXPORT void MeshBuffer_SetIndices(IntPtr meshb, unsigned short* indices, int count);
	EXPORT unsigned short MeshBuffer_GetIndex(IntPtr meshb, unsigned int nr);
	EXPORT void MeshBuffer_SetIndex(IntPtr meshb, unsigned int nr, unsigned short val);
	EXPORT IntPtr MeshBuffer_GetMaterial(IntPtr meshb);
	EXPORT void MeshBuffer_SetMaterial(IntPtr meshb, IntPtr material);
	EXPORT int MeshBuffer_GetVertexCount(IntPtr meshb);
	EXPORT E_VERTEX_TYPE MeshBuffer_GetVertexType(IntPtr meshb);
	EXPORT IntPtr MeshBuffer_GetVertex(IntPtr meshb, unsigned int nr);
	EXPORT void MeshBuffer_SetVertex(IntPtr meshb, unsigned int nr, IntPtr vert);
	EXPORT IntPtr MeshBuffer_GetVertex2T(IntPtr meshb, unsigned int nr);
	EXPORT void MeshBuffer_SetVertex2T(IntPtr meshb, unsigned int nr, IntPtr vert);
}
