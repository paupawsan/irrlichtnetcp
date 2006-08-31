using System;
using System.Runtime.InteropServices;

namespace IrrlichtNETCP
{
	public class LightSceneNode : SceneNode
	{
		public LightSceneNode(IntPtr raw) : base(raw)
		{
		}
		
		public Light LightData
		{
			get
			{
				Light l;
				float[] ambient = new float[4];
				float[] diffuse = new float[4];
				float[] specular = new float[4];
				float[] pos = new float[3];
				float radius = 0f;
				bool cast = false;
				LightType t = LightType.Point;	
				LightSceneNode_GetLight(_raw, ambient, diffuse, specular, pos, ref radius, ref cast, ref t);
				l.AmbientColor = Colorf.FromUnmanaged(ambient);
				l.DiffuseColor = Colorf.FromUnmanaged(diffuse);
				l.SpecularColor = Colorf.FromUnmanaged(diffuse);
				l.Position = Vector3D.FromUnmanaged(pos);
				l.Radius = radius;
				l.CastShadows = cast;
				l.Type = t;
				return l;	
			}
			set
			{
				LightSceneNode_SetLight(_raw, value.AmbientColor.ToUnmanaged(), value.DiffuseColor.ToUnmanaged(), value.SpecularColor.ToUnmanaged(), value.Position.ToUnmanaged(), value.Radius, value.CastShadows, value.Type);
			}
		}
		
		#region Native Invokes
		[DllImport(Native.Dll)]
	    static extern void LightSceneNode_GetLight(IntPtr light, [MarshalAs(UnmanagedType.LPArray)] float[] ambient, [MarshalAs(UnmanagedType.LPArray)] float[] diffuse, [MarshalAs(UnmanagedType.LPArray)] float[] specular, [MarshalAs(UnmanagedType.LPArray)] float[] pos, ref float radius, ref bool castshadows, ref LightType type);
	    
	    [DllImport(Native.Dll)]
	    static extern void LightSceneNode_SetLight(IntPtr light, float[] ambient, float[] diffuse, float[] specular, float[] pos, float radius, bool castshadows, LightType type);
	    #endregion
	}
}
