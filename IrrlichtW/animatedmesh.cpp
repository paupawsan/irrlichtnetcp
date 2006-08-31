#include "animatedhmesh.h"

//Shadow Volume Scene Node
void ShadowVolume_SetMeshToRenderFrom(IntPtr shadow, IntPtr mesh)
{
    ((IShadowVolumeSceneNode*)shadow)->setMeshToRenderFrom((IMesh*)mesh);
}

//Animated Mesh Scene Node
IAnimatedMeshSceneNode *GetNodeFromIntPtr(IntPtr node)
{
    return (IAnimatedMeshSceneNode*)node;
}

class AnimationEnd : public IAnimationEndCallBack
{
    public:
	AnimationEnd()
	{
		isCallbackDefined = false;
	}

    virtual void OnAnimationEnd(IAnimatedMeshSceneNode *node)
    {
		if(isCallbackDefined)
			_callback(node);
    }
    void setCallback(ANIMATIONENDCALLBACK call)
    {
		isCallbackDefined = true;
        _callback = *call;
    }

    protected:
	bool isCallbackDefined;
    ANIMATIONENDCALLBACK _callback;
};

IntPtr AnimatedMeshSceneNode_AddShadowVolumeSceneNode(IntPtr node, int ID, bool zfail, float infinity)
{
    return GetNodeFromIntPtr(node)->addShadowVolumeSceneNode(ID, zfail, infinity);
}

int AnimatedMeshSceneNode_GetFrameNr(IntPtr node)
{
    return GetNodeFromIntPtr(node)->getFrameNr();
}

IntPtr AnimatedMeshSceneNode_GetMS3DJointNode(IntPtr node, M_STRING jointName)
{
    return GetNodeFromIntPtr(node)->getMS3DJointNode(jointName);
}

IntPtr AnimatedMeshSceneNode_GetXJointNode(IntPtr node, M_STRING jointName)
{
    return GetNodeFromIntPtr(node)->getXJointNode(jointName);
}

void AnimatedMeshSceneNode_SetAnimationEndCallback(IntPtr node, ANIMATIONENDCALLBACK callback)
{
    AnimationEnd *end = new AnimationEnd();
    end->setCallback(callback);
    GetNodeFromIntPtr(node)->setAnimationEndCallback(end);
}

void AnimatedMeshSceneNode_SetAnimationSpeed(IntPtr node, int framePS)
{
    GetNodeFromIntPtr(node)->setAnimationSpeed(framePS);
}

void AnimatedMeshSceneNode_SetCurrentFrame(IntPtr node, int cf)
{
    GetNodeFromIntPtr(node)->setCurrentFrame(cf);
}

void AnimatedMeshSceneNode_SetFrameLoop(IntPtr node, int start, int end)
{
    GetNodeFromIntPtr(node)->setFrameLoop(start, end);
}

void AnimatedMeshSceneNode_SetLoopMode(IntPtr node, bool animationLooped)
{
    GetNodeFromIntPtr(node)->setLoopMode(animationLooped);
}

void AnimatedMeshSceneNode_SetMD2Animation(IntPtr node, M_STRING animationname)
{
    GetNodeFromIntPtr(node)->setMD2Animation(animationname);
}

void AnimatedMeshSceneNode_SetMD2AnimationA(IntPtr node, EMD2_ANIMATION_TYPE anim)
{
    GetNodeFromIntPtr(node)->setMD2Animation(anim);
}

