// AnimatedMeshB3D.cs created with MonoDevelop
// User: lester at 7:16 06.09.2007
//
//
//

using System;
using System.Runtime.InteropServices;
using System.Security;

namespace IrrlichtNETCP
{
	
	
	public class AnimatedMeshB3D : AnimatedMesh 
	{
		
		public AnimatedMeshB3D(IntPtr raw) : base(raw)
		{
		}
		
		public Matrix4 GetMatrixOfJoint(int jointNumber, int frame)
		{
			if (this.MeshType != AnimatedMeshType.B3D) return Matrix4.Identity;
			float[] mat = new float[16];
			AnimatedMesh_GetMatrixOfJointB3D(_raw, mat, jointNumber, frame);
			return Matrix4.FromUnmanaged(mat);
		}
		
		public Matrix4 GetMatrixOfJoint(int jointNumber)
		{
			if (this.MeshType != AnimatedMeshType.B3D) return Matrix4.Identity;
			float[] mat = new float[16];
			AnimatedMesh_GetLocalMatrixOfJointB3D(_raw, mat, jointNumber);
			return Matrix4.FromUnmanaged(mat);
		}
		
		public Matrix4 GetMatrixOfJointUnanimated(int jointNumber)
		{
			if (this.MeshType != AnimatedMeshType.B3D) return Matrix4.Identity;
			float[] mat = new float[16];
			AnimatedMesh_GetMatrixOfJointUnanimatedB3D(_raw, mat, jointNumber);
			return Matrix4.FromUnmanaged(mat);
		}
		
		public void SetJointAnimation(int jointNumber, bool On)
		{
			if (this.MeshType != AnimatedMeshType.B3D) return;
			AnimatedMesh_SetJointAnimationB3D(_raw, jointNumber, On);
		}
		
		public int JointCount
		{
			get {
				if (this.MeshType != AnimatedMeshType.B3D) return 0;
				return AnimatedMesh_GetJointCountB3D(_raw);
			}
		}
		
		public string GetJointName(int number)
		{
			if (this.MeshType != AnimatedMeshType.B3D) return "";
			return AnimatedMesh_GetJointNameB3D(_raw, number);
		}
		
		public int GetJointNumber(string name)
		{
			if (this.MeshType != AnimatedMeshType.B3D) return -1;
			return AnimatedMesh_GetJointNumberB3D(_raw, name);	
		}
		
		public bool UpdateNormalsWhenAnimating
		{
			set
			{
				if (this.MeshType != AnimatedMeshType.B3D) return;
				AnimatedMesh_UpdateNormalsWhenAnimatingB3D(_raw, value);
			}
		}
		
		public int Interpolation
		{
			set
			{
				if (this.MeshType != AnimatedMeshType.B3D) return;
				AnimatedMesh_SetInterpolationModeB3D(_raw, value);
			}
		}
		
		public int AnimationMode
		{
			set
			{
				if (this.MeshType != AnimatedMeshType.B3D) return;
				AnimatedMesh_SetAnimateModeB3D(_raw, value);
			}
		}
		
		public void ConvertToTangents()
		{
			if (this.MeshType != AnimatedMeshType.B3D) return;
			AnimatedMesh_convertToTangentsB3D(_raw);	
		}
		
		
#region native imports
		[DllImport(Native.Dll), SuppressUnmanagedCodeSecurity]
		static extern void AnimatedMesh_GetMatrixOfJointB3D(IntPtr mesh, [MarshalAs(UnmanagedType.LPArray)] float[] matrix, int jointNumber, int frame);
			
		[DllImport(Native.Dll), SuppressUnmanagedCodeSecurity]	
		static extern void AnimatedMesh_GetLocalMatrixOfJointB3D(IntPtr mesh, [MarshalAs(UnmanagedType.LPArray)] float[] matrix, int jointNumber);		
			
		[DllImport(Native.Dll), SuppressUnmanagedCodeSecurity]
		static extern void AnimatedMesh_GetMatrixOfJointUnanimatedB3D(IntPtr mesh, [MarshalAs(UnmanagedType.LPArray)] float[] matrix, int jointNumber);
				
		[DllImport(Native.Dll), SuppressUnmanagedCodeSecurity]		
		static extern void AnimatedMesh_SetJointAnimationB3D(IntPtr mesh, int jointNumber, bool On);
		
		[DllImport(Native.Dll), SuppressUnmanagedCodeSecurity]		
		static extern int AnimatedMesh_GetJointCountB3D(IntPtr mesh);		
		
		[DllImport(Native.Dll), SuppressUnmanagedCodeSecurity]		
		static extern string AnimatedMesh_GetJointNameB3D(IntPtr mesh, int number);
		
		[DllImport(Native.Dll), SuppressUnmanagedCodeSecurity]		
		static extern int AnimatedMesh_GetJointNumberB3D(IntPtr mesh, string name);
		
		[DllImport(Native.Dll), SuppressUnmanagedCodeSecurity]		
		static extern void AnimatedMesh_UpdateNormalsWhenAnimatingB3D(IntPtr mesh, bool on);		
		
		[DllImport(Native.Dll), SuppressUnmanagedCodeSecurity]
		static extern void AnimatedMesh_SetInterpolationModeB3D(IntPtr mesh, int mode);

		[DllImport(Native.Dll), SuppressUnmanagedCodeSecurity]
		static extern void AnimatedMesh_SetAnimateModeB3D (IntPtr mesh, int mode);
				
		[DllImport(Native.Dll), SuppressUnmanagedCodeSecurity]
		static extern void AnimatedMesh_convertToTangentsB3D(IntPtr mesh);				
#endregion
	 		
	}
}
