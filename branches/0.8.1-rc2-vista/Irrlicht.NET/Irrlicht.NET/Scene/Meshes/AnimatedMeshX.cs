// AnimatedMeshX.cs created with MonoDevelop
// User: lester at 14:44 09.09.2007
//
//
//

using System;
using System.Runtime.InteropServices;
using System.Security;

namespace IrrlichtNETCP
{
	
	
	public class AnimatedMeshX : AnimatedMesh
	{
		
		public AnimatedMeshX(IntPtr raw) : base(raw)
		{
		}
		
		public Matrix4 GetMatrixOfJoint(int jointNumber, int frame)
		{
			if (this.MeshType != AnimatedMeshType.X) return Matrix4.Identity;
			float[] mat = new float[16];			
			AnimatedMesh_GetMatrixOfJointX(_raw, mat, jointNumber, frame);
			return Matrix4.FromUnmanaged(mat);
		}
		
		public int JointCount
		{
			get{
				if (this.MeshType != AnimatedMeshType.X) return -1;
				return AnimatedMesh_GetJointCountX(_raw);
			}
		}
		
		public string GetJointName(int number)
		{
			if (this.MeshType != AnimatedMeshType.X) return "not_a_X_mesh";
			return AnimatedMesh_GetJointNameX(_raw, number);
		}
		
		public int GetJointNumber(string name)
		{
			if (this.MeshType != AnimatedMeshType.X) return -1;
			return AnimatedMesh_GetJointNumberX(_raw, name);
		}
		
		public int AnimationCount
		{
			get{
			if (this.MeshType != AnimatedMeshType.X) return -1;
			return AnimatedMesh_GetAnimationCountX(_raw);
			}
		}
		
		public string GetAnimationName(int idx)
		{
			if (this.MeshType != AnimatedMeshType.X) return "not_a_X_mesh";
			return AnimatedMesh_GetAnimationNameX(_raw, idx);
		}
		
		public void SetCurrentAnimation(int idx)
		{
				if (this.MeshType != AnimatedMeshType.X) return;				
				AnimatedMesh_SetCurrentAnimationX(_raw, idx);
		}
		
		public void SetCurrentAnimation(string name)
		{
			if (this.MeshType != AnimatedMeshType.X) return;
			AnimatedMesh_SetCurrentAnimationXa(_raw, name);
		}
		
		
#region native imports
		[DllImport(Native.Dll), SuppressUnmanagedCodeSecurity]
		static extern void AnimatedMesh_GetMatrixOfJointX(IntPtr mesh, [MarshalAs(UnmanagedType.LPArray)] float[] matrix, int jointNumber, int frame);
		
		[DllImport(Native.Dll), SuppressUnmanagedCodeSecurity]
		static extern int AnimatedMesh_GetJointCountX(IntPtr mesh);
		
		[DllImport(Native.Dll), SuppressUnmanagedCodeSecurity]
		static extern string AnimatedMesh_GetJointNameX(IntPtr mesh, int number);
		
		[DllImport(Native.Dll), SuppressUnmanagedCodeSecurity]
		static extern int AnimatedMesh_GetJointNumberX(IntPtr mesh, string name);		
		
		[DllImport(Native.Dll), SuppressUnmanagedCodeSecurity]
		static extern int AnimatedMesh_GetAnimationCountX(IntPtr mesh);		
		
		[DllImport(Native.Dll), SuppressUnmanagedCodeSecurity]
		static extern string AnimatedMesh_GetAnimationNameX(IntPtr mesh, int idx);
		
		[DllImport(Native.Dll), SuppressUnmanagedCodeSecurity]
		static extern void AnimatedMesh_SetCurrentAnimationX(IntPtr mesh, int idx);
		
		[DllImport(Native.Dll), SuppressUnmanagedCodeSecurity]
		static extern void AnimatedMesh_SetCurrentAnimationXa(IntPtr mesh, string name);		
		
#endregion
		
	}
}
