using System;
using System.Runtime.InteropServices;
using System.Security;

namespace IrrlichtNETCP
{
	public class ViewFrustum : NativeElement
	{		
		public ViewFrustum(IntPtr raw) : base(raw)
		{
		}
		
		public Box3D BoundingBox
		{
			get
			{
				float[] box = new float[6];
				VF_GetBoundingBox(_raw, box);
				return Box3D.FromUnmanaged(box);
			}
		}
		
		public Vector3D FarLeftUp
		{
			get
			{
				float[] v = new float[3];
				VF_GetFarLeftUp(_raw, v);
				return Vector3D.FromUnmanaged(v);
			}
		}
		public Vector3D FarLeftDown
		{
			get
			{
				float[] v = new float[3];
				VF_GetFarLeftDown(_raw, v);
				return Vector3D.FromUnmanaged(v);
			}
		}
		public Vector3D FarRightUp
		{
			get
			{
				float[] v = new float[3];
				VF_GetFarRightUp(_raw, v);
				return Vector3D.FromUnmanaged(v);
			}
		}
		public Vector3D FarRightDown
		{
			get
			{
				float[] v = new float[3];
				VF_GetFarRightDown(_raw, v);
				return Vector3D.FromUnmanaged(v);
			}
		}
		
		public void RecalculateBoundingBox()
		{
			VF_RecalculateBoundingBox(_raw);
		}
		
		public void Transform(Matrix4 mat)
		{
			VF_Transform(_raw, mat.ToUnmanaged());
		}
		
		#region Native Invokes
		 [DllImport(Native.Dll), SuppressUnmanagedCodeSecurity]
	    static extern void VF_GetBoundingBox(IntPtr vf, [MarshalAs(UnmanagedType.LPArray)] float[] box);
	    
		 [DllImport(Native.Dll), SuppressUnmanagedCodeSecurity]
        static extern void VF_GetFarLeftUp(IntPtr vf, [MarshalAs(UnmanagedType.LPArray)] float[] pf);
	    
		 [DllImport(Native.Dll), SuppressUnmanagedCodeSecurity]
        static extern void VF_GetFarLeftDown(IntPtr vf, [MarshalAs(UnmanagedType.LPArray)] float[] pf);
	    
		 [DllImport(Native.Dll), SuppressUnmanagedCodeSecurity]
        static extern void VF_GetFarRightDown(IntPtr vf, [MarshalAs(UnmanagedType.LPArray)] float[] pf);
	    
		 [DllImport(Native.Dll), SuppressUnmanagedCodeSecurity]
        static extern void VF_GetFarRightUp(IntPtr vf, [MarshalAs(UnmanagedType.LPArray)] float[] pf);
	    
		 [DllImport(Native.Dll), SuppressUnmanagedCodeSecurity]
        static extern void VF_RecalculateBoundingBox(IntPtr v);
	    
		 [DllImport(Native.Dll), SuppressUnmanagedCodeSecurity]
        static extern void VF_Transform(IntPtr vf, [MarshalAs(UnmanagedType.LPArray)] float[] mat);
        #endregion
	}
}
