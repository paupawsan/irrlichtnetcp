using System;
using System.Text;
using System.Runtime.InteropServices;

namespace IrrlichtNETCP
{
    public class MeshBuffer : NativeElement
    {
        public MeshBuffer(IntPtr raw) : base(raw)
        {
        }

        public MeshBuffer(VertexType type)
            : this(MeshBuffer_Create((int)type))
        {
        }

        public Box3D BoundingBox
        {
            get
            {
                float[] box = new float[6];
                MeshBuffer_GetBoundingBox(_raw, box);
                return Box3D.FromUnmanaged(box);
            }
            set
            {
                MeshBuffer_SetBoundingBox(_raw, value.ToUnmanaged());
            }
        }

        public int IndexCount
        {
            get
            {
                return MeshBuffer_GetIndexCount(_raw);
            }
        }
        
        public int VertexCount
        {
        	get
        	{
        		return MeshBuffer_GetVertexCount(_raw);
        	}
        }

        public ushort[] Indices
        {
            get
            {
                ushort[] indices = new ushort[IndexCount];
                MeshBuffer_GetIndices(_raw, indices);
                return indices;
            }
        }

        public ushort GetIndex(int nr)
        {
            return MeshBuffer_GetIndex(_raw, nr);
        }

        public void SetIndex(int nr, ushort val)
        {
            MeshBuffer_SetIndex(_raw, nr, val);
        }

        public Material Material
        {
            get
            {
                return (Material)NativeElement.GetObject(MeshBuffer_GetMaterial(_raw), typeof(Material));
            }
            set
            {
                MeshBuffer_SetMaterial(_raw, value.Raw);
                Material.MaterialType = value.MaterialType;
            }
        }

        public VertexType VertexType
        {
            get
            {
                return MeshBuffer_GetVertexType(_raw);
            }
        }

        public Vertex3D GetVertex(int nr)
        {
            return (Vertex3D)NativeElement.GetObject(MeshBuffer_GetVertex(_raw, nr), typeof(Vertex3D));
        }

        public Vertex3DT2 GetVertexT2(int nr)
        {
            return (Vertex3DT2)NativeElement.GetObject(MeshBuffer_GetVertex2T(_raw, nr), typeof(Vertex3DT2));
        }

        public void SetVertex(int nr, Vertex3D vert)
        {
            MeshBuffer_SetVertex(_raw, nr, vert.Raw);
        }

        public void SetVertexT2(int nr, Vertex3DT2 vert)
        {
            MeshBuffer_SetVertex2T(_raw, nr, vert.Raw);
        }

        #region Native Invokes
        [DllImport(Native.Dll)]
        static extern IntPtr MeshBuffer_Create(int type);

        [DllImport(Native.Dll)]
        static extern void MeshBuffer_GetBoundingBox(IntPtr meshb, [MarshalAs(UnmanagedType.LPArray)] float[] bb);
        
        [DllImport(Native.Dll)]
        static extern void MeshBuffer_SetBoundingBox(IntPtr meshb, float[] bb);

        [DllImport(Native.Dll)]
        static extern int MeshBuffer_GetIndexCount(IntPtr meshb);

        [DllImport(Native.Dll)]
        static extern void MeshBuffer_GetIndices(IntPtr meshb, [MarshalAs(UnmanagedType.LPArray)] ushort[] indices);

        [DllImport(Native.Dll)]
        static extern ushort MeshBuffer_GetIndex(IntPtr meshb, int nr);

        [DllImport(Native.Dll)]
        static extern void MeshBuffer_SetIndex(IntPtr meshb, int nr, ushort val);

        [DllImport(Native.Dll)]
        static extern IntPtr MeshBuffer_GetMaterial(IntPtr meshb);

        [DllImport(Native.Dll)]
        static extern void MeshBuffer_SetMaterial(IntPtr meshb, IntPtr material);

        [DllImport(Native.Dll)]
        static extern int MeshBuffer_GetVertexCount(IntPtr meshb);

        [DllImport(Native.Dll)]
        static extern VertexType MeshBuffer_GetVertexType(IntPtr meshb);

        [DllImport(Native.Dll)]
        static extern IntPtr MeshBuffer_GetVertex(IntPtr meshb, int nr);

        [DllImport(Native.Dll)]
        static extern void MeshBuffer_SetVertex(IntPtr meshb, int nr, IntPtr vert);

        [DllImport(Native.Dll)]
        static extern IntPtr MeshBuffer_GetVertex2T(IntPtr meshb, int nr);

        [DllImport(Native.Dll)]
        static extern void MeshBuffer_SetVertex2T(IntPtr meshb, int nr, IntPtr vert);
        #endregion
    }
    public enum VertexType
    {
        Standard,
        T2Coords,
        Tangents
    }
}
