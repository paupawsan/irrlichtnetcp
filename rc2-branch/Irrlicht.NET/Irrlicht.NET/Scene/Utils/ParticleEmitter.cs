using System;

namespace IrrlichtNETCP
{	
	public class ParticleEmitter : NativeElement
	{		
		public ParticleEmitter(IntPtr raw) : base(raw)
		{
		}
	}
}

namespace IrrlichtNETCP.Inheritable
{
    public interface IParticleEmitter
    {
        void Emit(uint now, uint timeSinceLastCall, out Particle[] Particles);
    }
}
