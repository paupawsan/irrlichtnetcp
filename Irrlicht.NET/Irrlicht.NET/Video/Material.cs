using System;
using System.Runtime.InteropServices;

namespace IrrlichtNETCP
{	
	public class Material : NativeElement
	{
        public Material()
            : base(Material_Create())
        {
        }

		public Material(IntPtr raw) : base(raw)
		{
		}

        public void CopyTo(Material dest)
        {
            dest.AmbientColor = AmbientColor;
            dest.AnisotropicFilter = AnisotropicFilter;
            dest.BackfaceCulling = BackfaceCulling;
            dest.BilinearFilter = BilinearFilter;
            dest.DiffuseColor = DiffuseColor;
            dest.EmissiveColor = EmissiveColor;
            dest.FogEnable = FogEnable;
            dest.GouraudShading = GouraudShading;
            dest.Lighting = Lighting;
            dest.MaterialType = MaterialType;
            dest.MaterialTypeParam = MaterialTypeParam;
            dest.NormalizeNormals = NormalizeNormals;
            dest.Shininess = Shininess;
            dest.SpecularColor = SpecularColor;
            dest.Texture1 = Texture1;
            dest.Texture2 = Texture2;
            dest.Texture3 = Texture3;
            dest.Texture4 = Texture4;
            dest.TrilinearFilter = TrilinearFilter;
            dest.Wireframe = Wireframe;
            dest.ZBuffer = ZBuffer;
            dest.ZWriteEnable = ZWriteEnable;
        }
		
		public Color AmbientColor
		{
			get
			{
				int[] col = new int[4];
				Material_GetAmbientColor(_raw, col);
				return Color.FromUnmanaged(col);
			}
			set
			{
				Material_SetAmbientColor(_raw, value.ToUnmanaged());
			}
		}
		
		public Color DiffuseColor
		{
			get
			{
				int[] col = new int[4];
				Material_GetDiffuseColor(_raw, col);
				return Color.FromUnmanaged(col);
			}
			set
			{
				Material_SetDiffuseColor(_raw, value.ToUnmanaged());
			}
		}
		
		public Color EmissiveColor
		{
			get
			{
				int[] col = new int[4];
				Material_GetEmissiveColor(_raw, col);
				return Color.FromUnmanaged(col);
			}
			set
			{
				Material_SetEmissiveColor(_raw, value.ToUnmanaged());
			}
		}
		
		public Color SpecularColor
		{
			get
			{
				int[] col = new int[4];
				Material_GetSpecularColor(_raw, col);
				return Color.FromUnmanaged(col);
			}
			set
			{
				Material_SetSpecularColor(_raw, value.ToUnmanaged());
			}
		}
		
		public MaterialType MaterialType
		{
			get
			{
				return Material_GetMaterialType(_raw);
			}
			set
			{
				Material_SetMaterialType(_raw, value);
			}
		}
		
		public float MaterialTypeParam
		{
			get
			{
				return Material_GetMaterialTypeParam(_raw);
			}
			set
			{
				Material_SetMaterialTypeParam(_raw, value);
			}
		}
		
		public float Shininess
		{
			get
			{
				return Material_GetShininess(_raw);
			}
			set
			{
				Material_SetShininess(_raw, value);
			}
		}
		
		public Texture Texture1
		{
			get
			{
				return (Texture) NativeElement.GetObject(Material_GetTexture1(_raw), typeof(Texture));
			}
			set
			{
                if (value != null)
                    Material_SetTexture1(_raw, value.Raw);
                else
                    Material_SetTexture1(_raw, IntPtr.Zero);
			}
		}
		
		public Texture Texture2
		{
			get
			{
				return (Texture) NativeElement.GetObject(Material_GetTexture2(_raw), typeof(Texture));
			}
			set
            {
                if (value != null)
                    Material_SetTexture2(_raw, value.Raw);
                else
                    Material_SetTexture2(_raw, IntPtr.Zero);
			}
        }

        public Texture Texture3
        {
            get
            {
                return (Texture)NativeElement.GetObject(Material_GetTexture3(_raw), typeof(Texture));
            }
            set
            {
                if (value != null)
                    Material_SetTexture3(_raw, value.Raw);
                else
                    Material_SetTexture3(_raw, IntPtr.Zero);
            }
        }

        public Texture Texture4
        {
            get
            {
                return (Texture)NativeElement.GetObject(Material_GetTexture4(_raw), typeof(Texture));
            }
            set
            {
                if (value != null)
                    Material_SetTexture4(_raw, value.Raw);
                else
                    Material_SetTexture4(_raw, IntPtr.Zero);
            }
        }
		
		public bool AnisotropicFilter
		{
			get
			{
				return Material_GetAnisotropicFilter(_raw);
			}
			set
			{
				Material_SetAnisotropicFilter(_raw, value);
			}
		}
		
		public bool BackfaceCulling
		{
			get
			{
				return Material_GetBackfaceCulling(_raw);
			}
			set
			{
				Material_SetBackfaceCulling(_raw, value);
			}
		}
		
		public bool BilinearFilter
		{
			get
			{
				return Material_GetBilinearFilter(_raw);
			}
			set
			{
				Material_SetBilinearFilter(_raw, value);
			}
		}
		
		public bool FogEnable
		{
			get
			{
				return Material_GetFogEnable(_raw);
			}
			set
			{
				Material_SetFogEnable(_raw, value);
			}
		}
		
		public bool GouraudShading
		{
			get
			{
				return Material_GetGouraudShading(_raw);
			}
			set
			{
				Material_SetGouraudShading(_raw, value);
			}
		}
		
		public bool Lighting
		{
			get
			{
				return Material_GetLighting(_raw);
			}
			set
			{
				Material_SetLighting(_raw, value);
			}
		}
		
		public bool NormalizeNormals
		{
			get
			{
				return Material_GetNormalizeNormals(_raw);
			}
			set
			{
				Material_SetNormalizeNormals(_raw, value);
			}
		}
		
		public bool TrilinearFilter
		{
			get
			{
				return Material_GetTrilinearFilter(_raw);
			}
			set
			{
				Material_SetTrilinearFilter(_raw, value);
			}
		}
		
		public bool Wireframe
		{
			get
			{
				return Material_GetWireframe(_raw);
			}
			set
			{
				Material_SetWireframe(_raw, value);
			}
		}

        public uint ZBuffer
		{
			get
			{
				return Material_GetZBuffer(_raw);
			}
			set
			{
				Material_SetZBuffer(_raw, value);
			}
		}
		
		public bool ZWriteEnable
		{
			get
			{
				return Material_GetZWriteEnable(_raw);
			}
			set
			{
				Material_SetZWriteEnable(_raw, value);
			}
		}
		
		#region Native Invokes
        [DllImport(Native.Dll)]
        static extern IntPtr Material_Create();

	    [DllImport(Native.Dll)]
		static extern void Material_GetAmbientColor(IntPtr material, [MarshalAs(UnmanagedType.LPArray)] int[] color);
	    
	    [DllImport(Native.Dll)]
		static extern void Material_GetDiffuseColor(IntPtr material, [MarshalAs(UnmanagedType.LPArray)] int[] color);
	    
	    [DllImport(Native.Dll)]
		static extern void Material_GetEmissiveColor(IntPtr material, [MarshalAs(UnmanagedType.LPArray)] int[] color);
		
	    [DllImport(Native.Dll)]
		static extern MaterialType Material_GetMaterialType(IntPtr material);
	    
	    [DllImport(Native.Dll)]
		static extern float Material_GetMaterialTypeParam(IntPtr material);
	    
	    [DllImport(Native.Dll)]
		static extern float Material_GetShininess(IntPtr material);
	    
	    [DllImport(Native.Dll)]
		static extern void Material_GetSpecularColor(IntPtr material, [MarshalAs(UnmanagedType.LPArray)] int[] color);
	    
	    [DllImport(Native.Dll)]
		static extern IntPtr Material_GetTexture1(IntPtr material);
	    
	    [DllImport(Native.Dll)]
        static extern IntPtr Material_GetTexture2(IntPtr material);

        [DllImport(Native.Dll)]
        static extern IntPtr Material_GetTexture3(IntPtr material);

        [DllImport(Native.Dll)]
        static extern IntPtr Material_GetTexture4(IntPtr material);
	    
	    [DllImport(Native.Dll)]
		static extern bool Material_GetAnisotropicFilter(IntPtr material);
	    
	    [DllImport(Native.Dll)]
		static extern bool Material_GetBackfaceCulling(IntPtr material);
	    
	    [DllImport(Native.Dll)]
		static extern bool Material_GetBilinearFilter(IntPtr material);
	    
	    [DllImport(Native.Dll)]
		static extern bool Material_GetFogEnable(IntPtr material);
	    
	    [DllImport(Native.Dll)]
		static extern bool Material_GetGouraudShading(IntPtr material);
	    
	    [DllImport(Native.Dll)]
		static extern bool Material_GetLighting(IntPtr material);
	    
	    [DllImport(Native.Dll)]
		static extern bool Material_GetNormalizeNormals(IntPtr material);
	    
	    [DllImport(Native.Dll)]
		static extern bool Material_GetTrilinearFilter(IntPtr material);
	    
	    [DllImport(Native.Dll)]
		static extern bool Material_GetWireframe(IntPtr material);
	    		
	    [DllImport(Native.Dll)]
        static extern uint Material_GetZBuffer(IntPtr material);
	    
	    [DllImport(Native.Dll)]
		static extern bool Material_GetZWriteEnable(IntPtr material);
	    		
	    [DllImport(Native.Dll)]
		static extern void Material_SetAmbientColor(IntPtr material, int[] color);
	    
	    [DllImport(Native.Dll)]
		static extern void Material_SetDiffuseColor(IntPtr material, int[] color);
	    		
	    [DllImport(Native.Dll)]
		static extern void Material_SetEmissiveColor(IntPtr material, int[] color);
	    
	    [DllImport(Native.Dll)]
		static extern void Material_SetMaterialType(IntPtr material, MaterialType val);
	    
	    [DllImport(Native.Dll)]
		static extern void Material_SetMaterialTypeParam(IntPtr material, float val);
	    
	    [DllImport(Native.Dll)]
		static extern void Material_SetShininess(IntPtr material, float val);
	    		
	    [DllImport(Native.Dll)]
		static extern void Material_SetSpecularColor(IntPtr material, int[] color);
	    
	    [DllImport(Native.Dll)]
		static extern void Material_SetTexture1(IntPtr material, IntPtr text);
	    
	    [DllImport(Native.Dll)]
        static extern void Material_SetTexture2(IntPtr material, IntPtr text);

        [DllImport(Native.Dll)]
        static extern void Material_SetTexture3(IntPtr material, IntPtr text);

        [DllImport(Native.Dll)]
        static extern void Material_SetTexture4(IntPtr material, IntPtr text);
	    
	    [DllImport(Native.Dll)]
		static extern void Material_SetAnisotropicFilter(IntPtr material, bool val);
	    
	    [DllImport(Native.Dll)]
		static extern void Material_SetBackfaceCulling(IntPtr material, bool val);
	    		
	    [DllImport(Native.Dll)]
		static extern void Material_SetBilinearFilter(IntPtr material, bool val);
	    		
	    [DllImport(Native.Dll)]
		static extern void Material_SetFogEnable(IntPtr material, bool val);
	    
	    [DllImport(Native.Dll)]
		static extern void Material_SetGouraudShading(IntPtr material, bool val);
	    
	    [DllImport(Native.Dll)]
		static extern void Material_SetLighting(IntPtr material, bool val);
	    
	    [DllImport(Native.Dll)]
		static extern void Material_SetNormalizeNormals(IntPtr material, bool val);
	    
	    [DllImport(Native.Dll)]
		static extern void Material_SetTrilinearFilter(IntPtr material, bool val);
	    
	    [DllImport(Native.Dll)]
		static extern void Material_SetWireframe(IntPtr material, bool val);
	    
	    [DllImport(Native.Dll)]
		static extern void Material_SetZBuffer(IntPtr material, uint val);
	    
	    [DllImport(Native.Dll)]
		static extern void Material_SetZWriteEnable(IntPtr material, bool val);
		#endregion
	}
	
}
