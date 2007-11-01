// AnimatedMeshMS3D.cs created with MonoDevelop
// User: lester at 13:51 06.09.2007
//
//
//

using System;
using System.Runtime.InteropServices;
using System.Security;

namespace IrrlichtNETCP
{
	
	
	public class AnimatedMeshMS3D : AnimatedMesh
	{
		
		public AnimatedMeshMS3D(IntPtr raw) : base(raw)
		{
		}
		
		public Matrix4 GetMatrixOfJoint(int jointNumber, int frame)
		{
			if (this.MeshType != AnimatedMeshType.MS3D) return Matrix4.Identity;
			float[] mat = new float[16];
			AnimatedMesh_GetMatrixOfJointMS3D(_raw, mat, jointNumber, frame);
			return Matrix4.FromUnmanaged(mat);
		}
		
		public int JointCount
		{
			get
			{
				if (this.MeshType != AnimatedMeshType.MS3D) return -1;
				return AnimatedMesh_GetJointCountMS3D(_raw);
			}
		}
		
		public string GetJointName(int number)
		{
			if (this.MeshType != AnimatedMeshType.MS3D) return "not_a_MS3D_mesh";
			return AnimatedMesh_GetJointNameMS3D(_raw, number);
		}
		
		public int GetJointNumber(string name)
		{
			if (this.MeshType != AnimatedMeshType.MS3D) return -1;
			return AnimatedMesh_GetJointNumber(_raw, name);
		}
		
#region native imports
		[DllImport(Native.Dll), SuppressUnmanagedCodeSecurity]
		static extern void AnimatedMesh_GetMatrixOfJointMS3D(IntPtr mesh, [MarshalAs(UnmanagedType.LPArray)] float[] matrix, int jointNumber, int frame);

		[DllImport(Native.Dll), SuppressUnmanagedCodeSecurity]		
		static extern int AnimatedMesh_GetJointCountMS3D(IntPtr mesh);
		
		[DllImport(Native.Dll), SuppressUnmanagedCodeSecurity]
		static extern string AnimatedMesh_GetJointNameMS3D(IntPtr mesh, int number);
			
		[DllImport(Native.Dll), SuppressUnmanagedCodeSecurity]			
		static extern int AnimatedMesh_GetJointNumber(IntPtr mesh, string name);			
			
#endregion

		
	}
}
