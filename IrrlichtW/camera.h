#include "main.h"

extern "C"
{
    EXPORT float Camera_GetAspectRation(IntPtr camera);
    EXPORT float Camera_GetFarValue(IntPtr camera);
    EXPORT float Camera_GetFOV(IntPtr camera);
    EXPORT float Camera_GetNearValue(IntPtr camera);
    EXPORT void Camera_GetProjectionMatrix(IntPtr camera, M_MAT4 toR);
    EXPORT void Camera_GetTarget(IntPtr camera, M_VECT3DF toR);
    EXPORT void Camera_GetUpVector(IntPtr camera, M_VECT3DF toR);
    EXPORT void Camera_GetViewMatrix(IntPtr camera, M_MAT4 toR);
    EXPORT IntPtr Camera_GetViewFrustrum(IntPtr camera);
    EXPORT bool Camera_IsInputReceiverEnabled(IntPtr camera);
    EXPORT bool Camera_IsOrthogonal(IntPtr camera);
    EXPORT bool Camera_OnEvent(IntPtr camera, IntPtr event);
    EXPORT void Camera_SetAspectRatio(IntPtr camera, float aspect);
    EXPORT void Camera_SetFarValue(IntPtr camera, float far);
    EXPORT void Camera_SetFOV(IntPtr camera, float FOV);
    EXPORT void Camera_SetInputReceiverEnabled(IntPtr camera, bool enabled);
    EXPORT void Camera_SetIsOrthogonal(IntPtr camera, bool orthogonal);
    EXPORT void Camera_SetNearValue(IntPtr camera, float near);
    EXPORT void Camera_SetProjectionMatrix(IntPtr camera, M_MAT4 projection);
    EXPORT void Camera_SetTarget(IntPtr camera, M_VECT3DF target);
    EXPORT void Camera_SetUpVector(IntPtr camera, M_VECT3DF upvector);

    EXPORT void VF_GetBoundingBox(IntPtr vf, M_BOX3D box);
    EXPORT void VF_GetFarLeftUp(IntPtr vf, M_VECT3DF pf);
    EXPORT void VF_GetFarLeftDown(IntPtr vf, M_VECT3DF pf);
    EXPORT void VF_GetFarRightDown(IntPtr vf, M_VECT3DF pf);
    EXPORT void VF_GetFarRightUp(IntPtr vf, M_VECT3DF pf);
    EXPORT void VF_RecalculateBoundingBox(IntPtr v);
    EXPORT void VF_Transform(IntPtr vf, M_MAT4 mat);
}
