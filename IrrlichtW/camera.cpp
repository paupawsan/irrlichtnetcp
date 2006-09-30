#include "camera.h"

ICameraSceneNode *GetCameraFromIntPtr(IntPtr camera)
{
    return (ICameraSceneNode*)camera;
}
float Camera_GetAspectRation(IntPtr camera)
{
    return GetCameraFromIntPtr(camera)->getAspectRatio();
}

float Camera_GetFarValue(IntPtr camera)
{
    return GetCameraFromIntPtr(camera)->getFarValue();
}

float Camera_GetFOV(IntPtr camera)
{
    return GetCameraFromIntPtr(camera)->getFOV();
}

float Camera_GetNearValue(IntPtr camera)
{
    return GetCameraFromIntPtr(camera)->getNearValue();
}

void Camera_GetProjectionMatrix(IntPtr camera, M_MAT4 toR)
{
    UM_MAT4(GetCameraFromIntPtr(camera)->getProjectionMatrix(), toR);
}

void Camera_GetTarget(IntPtr camera, M_VECT3DF toR)
{
    UM_VECT3DF(GetCameraFromIntPtr(camera)->getTarget(), toR);
}

void Camera_GetUpVector(IntPtr camera, M_VECT3DF toR)
{
    UM_VECT3DF(GetCameraFromIntPtr(camera)->getUpVector(), toR);
}

void Camera_GetViewMatrix(IntPtr camera, M_MAT4 toR)
{
    UM_MAT4(GetCameraFromIntPtr(camera)->getViewMatrix(), toR);
}

IntPtr Camera_GetViewFrustrum(IntPtr camera)
{
    return (IntPtr)(GetCameraFromIntPtr(camera)->getViewFrustrum());
}

bool Camera_IsInputReceiverEnabled(IntPtr camera)
{
    _FIX_BOOL_MARSHAL_BUG(GetCameraFromIntPtr(camera)->isInputReceiverEnabled());
}

bool Camera_IsOrthogonal(IntPtr camera)
{
    _FIX_BOOL_MARSHAL_BUG(GetCameraFromIntPtr(camera)->isOrthogonal());
}

bool Camera_OnEvent(IntPtr camera, IntPtr event)
{
    return GetCameraFromIntPtr(camera)->OnEvent(*((SEvent*)event));
}

void Camera_SetAspectRatio(IntPtr camera, float aspect)
{
    GetCameraFromIntPtr(camera)->setAspectRatio(aspect);
}

void Camera_SetFarValue(IntPtr camera, float far)
{
    GetCameraFromIntPtr(camera)->setFarValue(far);
}

void Camera_SetFOV(IntPtr camera, float FOV)
{
    GetCameraFromIntPtr(camera)->setFOV(FOV);
}

void Camera_SetInputReceiverEnabled(IntPtr camera, bool enabled)
{
    GetCameraFromIntPtr(camera)->setInputReceiverEnabled(enabled);
}

void Camera_SetIsOrthogonal(IntPtr camera, bool orthogonal)
{
    GetCameraFromIntPtr(camera)->setIsOrthogonal(orthogonal);
}

void Camera_SetNearValue(IntPtr camera, float near)
{
    GetCameraFromIntPtr(camera)->setNearValue(near);
}

void Camera_SetProjectionMatrix(IntPtr camera, M_MAT4 projection)
{
    GetCameraFromIntPtr(camera)->setProjectionMatrix(MU_MAT4(projection));
}

void Camera_SetTarget(IntPtr camera, M_VECT3DF target)
{
    GetCameraFromIntPtr(camera)->setTarget(MU_VECT3DF(target));
}

void Camera_SetUpVector(IntPtr camera, M_VECT3DF upvector)
{
    GetCameraFromIntPtr(camera)->setUpVector(MU_VECT3DF(upvector));
}

void VF_GetBoundingBox(IntPtr vf, M_BOX3D box)
{
    UM_BOX3D(((SViewFrustrum*)vf)->getBoundingBox(), box);
}

void VF_GetFarLeftUp(IntPtr vf, M_VECT3DF pf)
{
    UM_VECT3DF(((SViewFrustrum*)vf)->getFarLeftUp(), pf);
}

void VF_GetFarLeftDown(IntPtr vf, M_VECT3DF pf)
{
    UM_VECT3DF(((SViewFrustrum*)vf)->getFarLeftDown(), pf);
}

void VF_GetFarRightDown(IntPtr vf, M_VECT3DF pf)
{
    UM_VECT3DF(((SViewFrustrum*)vf)->getFarRightDown(), pf);
}

void VF_GetFarRightUp(IntPtr vf, M_VECT3DF pf)
{
    UM_VECT3DF(((SViewFrustrum*)vf)->getFarRightUp(), pf);
}

void VF_RecalculateBoundingBox(IntPtr vf)
{
    ((SViewFrustrum*)vf)->recalculateBoundingBox();
}

void VF_Transform(IntPtr vf, M_MAT4 mat)
{
    ((SViewFrustrum*)vf)->transform(MU_MAT4(mat));
}

