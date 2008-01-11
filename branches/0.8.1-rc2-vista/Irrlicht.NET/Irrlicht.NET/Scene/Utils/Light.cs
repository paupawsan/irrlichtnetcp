using System;

namespace IrrlichtNETCP
{	
	public struct Light
	{	
		public Colorf AmbientColor;
		public Colorf DiffuseColor;
		public Colorf SpecularColor;
		public Vector3D Position;
		public float Radius;
		public LightType Type;
		public bool CastShadows;
	}
	
	public enum LightType
	{
		Point,
		Directional
	}	
}
