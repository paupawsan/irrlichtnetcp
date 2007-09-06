#include "main.h"

extern "C"
{
	EXPORT IntPtr Mesh_Create(void);
    EXPORT void Mesh_GetBoundingBox(IntPtr mesh, M_BOX3D box);
    EXPORT void Mesh_SetMaterialFlag(IntPtr mesh, E_MATERIAL_FLAG flag, bool newValue);
	EXPORT int Mesh_GetMeshBufferCount(IntPtr mesh);
	EXPORT IntPtr Mesh_GetMeshBuffer(IntPtr mesh, int nr);
	EXPORT void Mesh_AddMeshBuffer(IntPtr mesh, IntPtr meshbuf);

    EXPORT void AnimatedMesh_GetBoundingBox(IntPtr mesh, M_BOX3D box);
    EXPORT IntPtr AnimatedMesh_GetMesh(IntPtr mesh, int frame, int detailLevel, int startFrameloop, int endFrameloop);
    EXPORT E_ANIMATED_MESH_TYPE AnimatedMesh_GetMeshType(IntPtr mesh);
    // B3D functions
    EXPORT void AnimatedMesh_GetMatrixOfJointB3D(IntPtr mesh, M_MAT4 matrix, int jointNumber, int frame);
    EXPORT void AnimatedMesh_GetLocalMatrixOfJointB3D(IntPtr mesh, M_MAT4 matrix, int jointNumber);
    EXPORT void AnimatedMesh_GetMatrixOfJointUnanimatedB3D(IntPtr mesh, M_MAT4 matrix, int jointNumber);
    EXPORT void AnimatedMesh_SetJointAnimationB3D(IntPtr mesh, int jointNumber, bool On);
    EXPORT int AnimatedMesh_GetJointCountB3D(IntPtr mesh);
    EXPORT M_STRING AnimatedMesh_GetJointNameB3D(IntPtr mesh, int number);
    EXPORT int AnimatedMesh_GetJointNumberB3D(IntPtr mesh, M_STRING name);
    EXPORT void AnimatedMesh_UpdateNormalsWhenAnimatingB3D(IntPtr mesh, bool on);
    EXPORT void AnimatedMesh_SetInterpolationModeB3D(IntPtr mesh, int mode);
    EXPORT void AnimatedMesh_SetAnimateModeB3D (IntPtr mesh, s32 mode);
    EXPORT void AnimatedMesh_convertToTangentsB3D(IntPtr mesh);
    // MD2 specific
    EXPORT void AnimatedMesh_GetFrameLoopMD2 (IntPtr mesh, EMD2_ANIMATION_TYPE l, int* outBegin, int* outEnd, int* outFPS);
    EXPORT void AnimatedMesh_GetFrameLoopMD2a (IntPtr mesh, M_STRING name, int *outBegin, int *outEnd, int *outFPS);
    EXPORT int AnimationMesh_GetAnimationCountMD2(IntPtr mesh);
    EXPORT M_STRING AnimationMesh_GetAnimationNameMD2(IntPtr mesh, int nr);
    // MS3D specific
    EXPORT void AnimatedMesh_GetMatrixOfJointMS3D(IntPtr mesh, M_MAT4 matrix, int jointNumber, int frame);
    EXPORT int AnimatedMesh_GetJointCountMS3D(IntPtr mesh);
    EXPORT M_STRING AnimatedMesh_GetJointNameMS3D(IntPtr mesh, int number);
    EXPORT int AnimatedMesh_GetJointNumber(IntPtr mesh, M_STRING name);

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
