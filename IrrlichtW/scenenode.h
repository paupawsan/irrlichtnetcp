#include "main.h"

extern "C"
{
    EXPORT void SceneNode_AddAnimator(IntPtr scenenode, IntPtr animator);
    EXPORT void SceneNode_AddChild(IntPtr scenenode, IntPtr childnode);
	EXPORT void SceneNode_GetChildren(IntPtr scenenode, IntPtr *list);
	EXPORT unsigned int SceneNode_GetChildrenCount(IntPtr scenenode); //Only used by SceneNode.Children on C#
    EXPORT void SceneNode_GetAbsolutePosition(IntPtr scenenode, M_VECT3DF toR);
    EXPORT void SceneNode_GetAbsoluteTransformation(IntPtr scenenode, M_MAT4 toR);
    EXPORT bool SceneNode_GetAutomaticCulling(IntPtr scenenode);
    EXPORT void SceneNode_GetBoundingBox(IntPtr scenenode, M_BOX3D toR);
    EXPORT int SceneNode_GetID(IntPtr scenenode);
    EXPORT int SceneNode_GetMaterialCount(IntPtr scenenode);
    EXPORT IntPtr SceneNode_GetMaterial(IntPtr scenenode, int i);
    EXPORT M_STRING SceneNode_GetName(IntPtr scenenode);
    EXPORT IntPtr SceneNode_GetParent(IntPtr scenenode);
    EXPORT void SceneNode_GetPosition(IntPtr scenenode, M_VECT3DF toR);
    EXPORT void SceneNode_GetRelativeTransformation(IntPtr scenenode, M_MAT4 toR);
    EXPORT void SceneNode_GetRotation(IntPtr scenenode, M_VECT3DF toR);
    EXPORT void SceneNode_GetScale(IntPtr scenenode, M_VECT3DF toR);
    EXPORT void SceneNode_GetTransformedBoundingBox(IntPtr scenenode, M_BOX3D toR);
    EXPORT IntPtr SceneNode_GetTriangleSelector(IntPtr scenenode);
    EXPORT ESCENE_NODE_TYPE SceneNode_GetType(IntPtr scenenode);
    EXPORT bool SceneNode_IsDebugDataVisible(IntPtr scenenode);
    EXPORT bool SceneNode_IsDebugObject(IntPtr scenenode);
    EXPORT bool SceneNode_IsVisible(IntPtr scenenode);
    EXPORT void SceneNode_OnPostRender(IntPtr scenenode, unsigned int timeMS);
    EXPORT void SceneNode_OnPreRender(IntPtr scenenode);
    EXPORT void SceneNode_Remove(IntPtr scenenode);
    EXPORT void SceneNode_RemoveAll(IntPtr scenenode);
    EXPORT void SceneNode_RemoveAnimator(IntPtr scenenode, IntPtr animator);
    EXPORT void SceneNode_RemoveAnimators(IntPtr scenenode);
    EXPORT bool SceneNode_RemoveChild(IntPtr scenenode, IntPtr childscenenode);
    EXPORT void SceneNode_Render(IntPtr scenenode);
    EXPORT void SceneNode_SetAutomaticCulling(IntPtr scenenode, bool enabled);
    EXPORT void SceneNode_SetDebugDataVisible(IntPtr scenenode, bool visible);
    EXPORT void SceneNode_SetID(IntPtr scenenode, int id);
    EXPORT void SceneNode_SetIsDebugObject(IntPtr scenenode, bool debugObject);
    EXPORT void SceneNode_SetMaterialFlag(IntPtr scenenode, E_MATERIAL_FLAG flag, bool newvalue);
    EXPORT void SceneNode_SetMaterialTexture(IntPtr scenenode, int layer, IntPtr texture);
    EXPORT void SceneNode_SetMaterialType(IntPtr scenenode, E_MATERIAL_TYPE newtype);
    EXPORT void SceneNode_SetName(IntPtr scenenode, M_STRING name);
    EXPORT void SceneNode_SetParent(IntPtr scenenode, IntPtr parent);
    EXPORT void SceneNode_SetPosition(IntPtr scenenode, M_VECT3DF pos);
    EXPORT void SceneNode_SetRotation(IntPtr scenenode, M_VECT3DF rot);
    EXPORT void SceneNode_SetScale(IntPtr scenenode, M_VECT3DF scale);
    EXPORT void SceneNode_SetTriangleSelector(IntPtr scenenode, IntPtr triangleselector);
    EXPORT void SceneNode_SetVisible(IntPtr scenenode, bool visible);
    EXPORT void SceneNode_UpdateAbsolutePosition(IntPtr scenenode);

    //Other little scene nodes
    EXPORT void BillboardSceneNode_GetSize(IntPtr billboard, M_DIM2DF dim);
    EXPORT void BillboardSceneNode_SetSize(IntPtr billboard, M_DIM2DF size);

    EXPORT void TextSceneNode_SetText(IntPtr text, M_STRING ctext);
    EXPORT void TextSceneNode_SetTextColor(IntPtr text, M_SCOLOR color);

    EXPORT void TerrainSceneNode_GetBoundingBox(IntPtr terrain, int patchX, int patchZ, M_BOX3D box);
    EXPORT void TerrainSceneNode_GetBoundingBoxA(IntPtr terrain, M_BOX3D box);
    EXPORT int TerrainSceneNode_GetIndexCount(IntPtr terrain);
    EXPORT IntPtr TerrainSceneNode_GetMesh(IntPtr terrain);
    EXPORT void TerrainSceneNode_GetTerrainCenter(IntPtr terrain, M_VECT3DF center);
    EXPORT void TerrainSceneNode_OverrideLODDistance(IntPtr terrain, int lod, double newDistance);
    EXPORT void TerrainSceneNode_ScaleTexture(IntPtr terrain, float scale, float scale2);
    EXPORT void TerrainSceneNode_SetCameraMovementDelta(IntPtr terrain, float delta);
    EXPORT void TerrainSceneNode_SetCameraRotationDelta(IntPtr terrain, float delta);
    EXPORT void TerrainSceneNode_SetDynamicSelectorUpdate(IntPtr terrain, bool bVal);
    EXPORT void TerrainSceneNode_SetLODOfPatch(IntPtr terrain, int patchX, int patchZ, int LOD);

    EXPORT void LightSceneNode_GetLight(IntPtr light, M_SCOLORF ambient, M_SCOLORF diffuse, M_SCOLORF specular, M_VECT3DF pos, float *radius, bool *castshadows, E_LIGHT_TYPE* type);
    EXPORT void LightSceneNode_SetLight(IntPtr light, M_SCOLORF ambient, M_SCOLORF diffuse, M_SCOLORF specular, M_VECT3DF pos, float radius, bool castshadows, E_LIGHT_TYPE type);
}
