using System;

namespace IrrlichtNETCP
{	
	public class ParticleAffector : NativeElement
	{		
		public ParticleAffector(IntPtr raw) : base(raw)
		{
		}
	}
}

namespace IrrlichtNETCP.Inheritable
{
    public interface IParticleAffector
    {
        void Affect(uint now, Particle[] Particles);
    }
}
