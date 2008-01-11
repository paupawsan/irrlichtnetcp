// TextureLayer.cs created with MonoDevelop
// User: lester at 21:42 06.10.2007
//
//
//

using System;
using System.Runtime.InteropServices;
using System.Security;

namespace IrrlichtNETCP
{
	
	public class MaterialLayer : NativeElement
	{
		
		public MaterialLayer(IntPtr raw) : base (raw)
		{
		}
		
        public MaterialLayer()
            : base(MaterialLayer_Create())
        {
        }		
		
		public bool TrilinearFilter
		{
			get
			{
				return MaterialLayer_GetTrilinearFilter(_raw);
			}
			set
			{
				MaterialLayer_SetTrilinearFilter(_raw, value);
			}
		}
		
		public bool AnisotropicFilter
		{
			get
			{
				return MaterialLayer_GetAnisotropicFilter(_raw);
			}
			set
			{
				MaterialLayer_SetAnisotropicFilter(_raw, value);
			}
		}

		public bool BilinearFilter
		{
			get
			{
				return MaterialLayer_GetBilinearFilter(_raw);
			}
			set
			{
				MaterialLayer_SetBilinearFilter(_raw, value);
			}
		}
		
		public Texture Texture
		{
			get
			{
				return (Texture)NativeElement.GetObject(MaterialLayer_GetTexture(_raw), typeof(Texture));
			}
			set
			{
				MaterialLayer_SetTexture(_raw, value.Raw);
			}
		}
		
#region Native Imports
         [DllImport(Native.Dll), SuppressUnmanagedCodeSecurity]
        static extern IntPtr MaterialLayer_Create();		
		
	     [DllImport(Native.Dll), SuppressUnmanagedCodeSecurity]
		static extern bool MaterialLayer_GetAnisotropicFilter(IntPtr material);
		
	     [DllImport(Native.Dll), SuppressUnmanagedCodeSecurity]
		static extern bool MaterialLayer_GetBilinearFilter(IntPtr material);
		
	     [DllImport(Native.Dll), SuppressUnmanagedCodeSecurity]
		static extern bool MaterialLayer_GetTrilinearFilter(IntPtr material);
		
  	     [DllImport(Native.Dll), SuppressUnmanagedCodeSecurity]
		static extern void MaterialLayer_SetAnisotropicFilter(IntPtr material, bool val);
		
	     [DllImport(Native.Dll), SuppressUnmanagedCodeSecurity]
		static extern void MaterialLayer_SetBilinearFilter(IntPtr material, bool val);
		
	     [DllImport(Native.Dll), SuppressUnmanagedCodeSecurity]
		static extern void MaterialLayer_SetTrilinearFilter(IntPtr material, bool val);
		
		[DllImport(Native.Dll), SuppressUnmanagedCodeSecurity]
		static extern void MaterialLayer_SetTexture(IntPtr material, IntPtr text);
		
		[DllImport(Native.Dll), SuppressUnmanagedCodeSecurity]
		static extern IntPtr MaterialLayer_GetTexture(IntPtr material);		
#endregion
		
	}
}
