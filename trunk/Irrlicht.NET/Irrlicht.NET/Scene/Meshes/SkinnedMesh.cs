// SkinnedMesh.cs
//

using System;
using System.Runtime.InteropServices;
using System.Security;

namespace IrrlichtNETCP
{

	public class SkinnedMesh : AnimatedMesh
	{
		
		public SkinnedMesh(IntPtr raw) : base (raw)
		{
		}
		
#region Native Imports
		[DllImport(Native.Dll), SuppressUnmanagedCodeSecurity]
		static extern void SkinnedMesh_AnimateMesh(IntPtr mesh, float frame, float blend);
#endregion
		
	}
}
