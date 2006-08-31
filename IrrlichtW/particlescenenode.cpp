#include "particlescenenode.h"

IParticleSystemSceneNode *GetPSSFromIntPtr(IntPtr part)
{
    return ((IParticleSystemSceneNode*)part);
}


void Particle_AddAffector(IntPtr part, IntPtr affector)
{
    GetPSSFromIntPtr(part)->addAffector((IParticleAffector*)affector);
}

class Affector : public IParticleAffector
{
    public:
    void SetCallBack(AFFECTORCALLBACK call)
    {
		_callback = call;
    }

    void affect(u32 now, SParticle* particlearray, u32 count)
    {
		IntPtr *intptrarr = new IntPtr[count];
		for(unsigned int i = 0; i < count; i++)
			intptrarr[i] = &particlearray[i];
		_callback(now, intptrarr, count);
		delete[] intptrarr;
    }

	E_PARTICLE_AFFECTOR_TYPE getType()
	{
		return EPAT_NONE;
	}

    protected:
    AFFECTORCALLBACK _callback;
};

void Particle_AddAffectorA(IntPtr part, AFFECTORCALLBACK affector)
{
	Affector *af = new Affector();
	af->SetCallBack(affector);
	GetPSSFromIntPtr(part)->addAffector(af);
}

IntPtr Particle_CreateBoxEmitter(IntPtr part,M_BOX3D box, M_VECT3DF direction, unsigned int minPPS, unsigned int maxPPS, M_SCOLOR minSC, M_SCOLOR maxSC, unsigned int minLT, unsigned int maxLT, int maxAngleDegrees)
{
    return GetPSSFromIntPtr(part)->createBoxEmitter(MU_BOX3D(box), MU_VECT3DF(direction), minPPS, maxPPS, MU_SCOLOR(minSC), MU_SCOLOR(maxSC), minLT, maxLT, maxAngleDegrees);
}

IntPtr Particle_CreateFadeOutParticleAffector(IntPtr part, M_SCOLOR tgtColor, unsigned int timeNeeded)
{
    return GetPSSFromIntPtr(part)->createFadeOutParticleAffector(MU_SCOLOR(tgtColor), timeNeeded);
}

IntPtr Particle_CreateGravityAffector(IntPtr part, M_VECT3DF gravity, unsigned int timeForceLost)
{
    return GetPSSFromIntPtr(part)->createGravityAffector(MU_VECT3DF(gravity), timeForceLost);
}

IntPtr Particle_CreatePointEmitter(IntPtr part, M_VECT3DF dir, unsigned int minPPS, unsigned int maxPPS, M_SCOLOR minSC, M_SCOLOR maxSC, unsigned int minLT, unsigned int maxLT, int maxAngleDegrees)
{
    return GetPSSFromIntPtr(part)->createPointEmitter(MU_VECT3DF(dir), minPPS, maxPPS, MU_SCOLOR(minSC), MU_SCOLOR(maxSC), minLT, maxLT, maxAngleDegrees);
}

void Particle_RemoveAllAffectors(IntPtr part)
{
    GetPSSFromIntPtr(part)->removeAllAffectors();
}

void Particle_SetEmitter(IntPtr part, IntPtr emitter)
{
    GetPSSFromIntPtr(part)->setEmitter((IParticleEmitter*)emitter);
}

class Emitter : public IParticleEmitter
{
    public:
    void SetCallBack(EMITTERCALLBACK call)
    {
		_callback = call;
    }

	core::array<SParticle> Particles;
    int emitt(u32 now, u32 timeSinceLastCall, SParticle*& outArray)
    {
        Particles.clear();
		_callback(now, timeSinceLastCall, this);
		outArray = Particles.pointer();
		int tor = Particles.size();
		return tor;
    }

    void AddParticles(IntPtr *part, int count)
    {
        for(int i = 0; i < count; i++)
            Particles.push_back(*(SParticle*)part[i]);
    }

	E_PARTICLE_EMITTER_TYPE getType()
	{
		return EPET_POINT;
	}
    protected:
    EMITTERCALLBACK _callback;
};
 void Emitter_AddParticle(IntPtr emitter, IntPtr *part, int count)
 {
     ((Emitter*)emitter)->AddParticles(part, count);
 }


void Particle_SetEmitterA(IntPtr part, EMITTERCALLBACK callback)
{
	Emitter *em = new Emitter();
	em->SetCallBack(callback);
    GetPSSFromIntPtr(part)->setEmitter(em);
}

void Particle_SetParticlesAreGlobal(IntPtr part, bool global)
{
    GetPSSFromIntPtr(part)->setParticlesAreGlobal(global);
}

void Particle_SetParticleSize(IntPtr part, M_DIM2DF size)
{
    GetPSSFromIntPtr(part)->setParticleSize(MU_DIM2DF(size));
}


IntPtr SParticle_Create()
{
	return new SParticle();
}

void SParticle_GetPos(IntPtr particle, M_VECT3DF vect)
{
	SParticle *part = ((SParticle*)particle);
	UM_VECT3DF(part->pos, vect);
}

void SParticle_SetPos(IntPtr particle, M_VECT3DF vect)
{
	SParticle *part = ((SParticle*)particle);
	part->pos = MU_VECT3DF(vect);
}

void SParticle_GetVect(IntPtr particle, M_VECT3DF vect)
{
	SParticle *part = ((SParticle*)particle);
	UM_VECT3DF(part->vector, vect);
}

void SParticle_SetVect(IntPtr particle, M_VECT3DF vect)
{
	SParticle *part = ((SParticle*)particle);
	part->vector = MU_VECT3DF(vect);
}

void SParticle_GetStartVect(IntPtr particle, M_VECT3DF vect)
{
	SParticle *part = ((SParticle*)particle);
	UM_VECT3DF(part->startVector, vect);
}

void SParticle_SetStartVect(IntPtr particle, M_VECT3DF vect)
{
	SParticle *part = ((SParticle*)particle);
	part->startVector = MU_VECT3DF(vect);
}

void SParticle_GetColor(IntPtr particle, M_SCOLOR color)
{
	SParticle *part = ((SParticle*)particle);
	UM_SCOLOR(part->color, color);
}

void SParticle_SetColor(IntPtr particle, M_SCOLOR color)
{
	SParticle *part = ((SParticle*)particle);
	part->color = MU_SCOLOR(color);
}

void SParticle_GetStartColor(IntPtr particle, M_SCOLOR color)
{
	SParticle *part = ((SParticle*)particle);
	UM_SCOLOR(part->startColor, color);
}

void SParticle_SetStartColor(IntPtr particle, M_SCOLOR color)
{
	SParticle *part = ((SParticle*)particle);
	part->startColor = MU_SCOLOR(color);
}

unsigned int SParticle_GetStartTime(IntPtr particle)
{
	SParticle *part = ((SParticle*)particle);
	return part->startTime;
}

void SParticle_SetStartTime(IntPtr particle, unsigned int time)
{
	SParticle *part = ((SParticle*)particle);
	part->startTime = time;
}

unsigned int SParticle_GetEndTime(IntPtr particle)
{
	SParticle *part = ((SParticle*)particle);
	return part->endTime;
}

void SParticle_SetEndTime(IntPtr particle, unsigned int time)
{
	SParticle *part = ((SParticle*)particle);
	part->endTime = time;
}
