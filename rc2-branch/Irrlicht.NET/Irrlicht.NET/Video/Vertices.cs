using System;
using System.Runtime.InteropServices;
using System.Text;
using System.Security;

namespace IrrlichtNETCP
{
    public class Vertex3D : NativeElement
    {
        public Vertex3D() : base(Vertices_CreateVertex())
        {
        }
        public Vertex3D(IntPtr raw) : base(raw)
        {
        }
        public Vertex3D(Vector3D position, Vector3D normal, Color color, Vector2D tcoord)
            : this()
        {
            Position = position;
            Normal = normal;
            Color = color;
            TCoords = tcoord;
        }
        const bool VertOr2T = true;

        public Color Color
        {
            get
            {
                int[] color = new int[4];
                Vertices_GetColor(_raw, color, VertOr2T);
                return Color.FromUnmanaged(color);
            }
            set
            {
                Vertices_SetColor(_raw, value.ToUnmanaged(), VertOr2T);
            }
        }

        public Vector3D Normal
        {
            get
            {
                float[] norm = new float[3];
                Vertices_GetNormal(_raw, norm, VertOr2T);
                return Vector3D.FromUnmanaged(norm);
            }
            set
            {
                Vertices_SetNormal(_raw, value.ToUnmanaged(), VertOr2T);
            }
        }

        public Vector3D Position
        {
            get
            {
                float[] norm = new float[3];
                Vertices_GetPos(_raw, norm, VertOr2T);
                return Vector3D.FromUnmanaged(norm);
            }
            set
            {
                Vertices_SetPos(_raw, value.ToUnmanaged(), VertOr2T);
            }
        }

        public Vector2D TCoords
        {
            get
            {
                float[] coords = new float[2];
                Vertices_GetTCoords(_raw, coords, VertOr2T);
                return Vector2D.FromUnmanaged(coords);
            }
            set 
            {
                Vertices_SetTCoords(_raw, value.ToUnmanaged(), VertOr2T);
            }
        }

        #region Native Invokes
         [DllImport(Native.Dll), SuppressUnmanagedCodeSecurity]
        static extern IntPtr Vertices_CreateVertex();
        
         [DllImport(Native.Dll), SuppressUnmanagedCodeSecurity]
        static extern void Vertices_GetColor(IntPtr vertex, [MarshalAs(UnmanagedType.LPArray)] int[] color, bool vertor2t);

         [DllImport(Native.Dll), SuppressUnmanagedCodeSecurity]
        static extern void Vertices_GetNormal(IntPtr vertex, [MarshalAs(UnmanagedType.LPArray)] float[] normal, bool vertor2t);

         [DllImport(Native.Dll), SuppressUnmanagedCodeSecurity]
        static extern void Vertices_GetPos(IntPtr vertex, [MarshalAs(UnmanagedType.LPArray)] float[] pos, bool vertor2t);

         [DllImport(Native.Dll), SuppressUnmanagedCodeSecurity]
        static extern void Vertices_GetTCoords(IntPtr vertex, [MarshalAs(UnmanagedType.LPArray)] float[] tcoords, bool vertor2t);

         [DllImport(Native.Dll), SuppressUnmanagedCodeSecurity]
        static extern void Vertices_SetColor(IntPtr vertex, int[] color, bool vertor2t);

         [DllImport(Native.Dll), SuppressUnmanagedCodeSecurity]
        static extern void Vertices_SetNormal(IntPtr vertex, float[] normal, bool vertor2t);

         [DllImport(Native.Dll), SuppressUnmanagedCodeSecurity]
        static extern void Vertices_SetPos(IntPtr vertex, float[] pos, bool vertor2t);

         [DllImport(Native.Dll), SuppressUnmanagedCodeSecurity]
        static extern void Vertices_SetTCoords(IntPtr vertex, float[] tcoords, bool vertor2t);
        #endregion
    }

    public class Vertex3DT2 : NativeElement
    {
        public Vertex3DT2()
            : base(Vertices_CreateVertex2TCoords())
        {
        }
        public Vertex3DT2(IntPtr raw)
            : base(raw)
        {
        }
        public Vertex3DT2(Vector3D position, Vector3D normal, Color color, Vector2D tcoord, Vector2D tcoord2)
            : this()
        {
            Position = position;
            Normal = normal;
            Color = color;
            TCoords = tcoord;
            TCoords2 = tcoord2;
        }
        const bool VertOr2T = false;

        public Color Color
        {
            get
            {
                int[] color = new int[4];
                Vertices_GetColor(_raw, color, VertOr2T);
                return Color.FromUnmanaged(color);
            }
            set
            {
                Vertices_SetColor(_raw, value.ToUnmanaged(), VertOr2T);
            }
        }

        public Vector3D Normal
        {
            get
            {
                float[] norm = new float[3];
                Vertices_GetNormal(_raw, norm, VertOr2T);
                return Vector3D.FromUnmanaged(norm);
            }
            set
            {
                Vertices_SetNormal(_raw, value.ToUnmanaged(), VertOr2T);
            }
        }

        public Vector3D Position
        {
            get
            {
                float[] norm = new float[3];
                Vertices_GetPos(_raw, norm, VertOr2T);
                return Vector3D.FromUnmanaged(norm);
            }
            set
            {
                Vertices_SetPos(_raw, value.ToUnmanaged(), VertOr2T);
            }
        }

        public Vector2D TCoords
        {
            get
            {
                float[] coords = new float[2];
                Vertices_GetTCoords(_raw, coords, VertOr2T);
                return Vector2D.FromUnmanaged(coords);
            }
            set
            {
                Vertices_SetTCoords(_raw, value.ToUnmanaged(), VertOr2T);
            }
        }

        public Vector2D TCoords2
        {
            get
            {
                float[] coords = new float[2];
                Vertices_GetTCoords2(_raw, coords);
                return Vector2D.FromUnmanaged(coords);
            }
            set
            {
                Vertices_SetTCoords2(_raw, value.ToUnmanaged());
            }
        }

        #region Native Invokes
         [DllImport(Native.Dll), SuppressUnmanagedCodeSecurity]
        static extern IntPtr Vertices_CreateVertex2TCoords();

         [DllImport(Native.Dll), SuppressUnmanagedCodeSecurity]
        static extern void Vertices_GetColor(IntPtr vertex, [MarshalAs(UnmanagedType.LPArray)] int[] color, bool vertor2t);

         [DllImport(Native.Dll), SuppressUnmanagedCodeSecurity]
        static extern void Vertices_GetNormal(IntPtr vertex, [MarshalAs(UnmanagedType.LPArray)] float[] normal, bool vertor2t);

         [DllImport(Native.Dll), SuppressUnmanagedCodeSecurity]
        static extern void Vertices_GetPos(IntPtr vertex, [MarshalAs(UnmanagedType.LPArray)] float[] pos, bool vertor2t);

         [DllImport(Native.Dll), SuppressUnmanagedCodeSecurity]
        static extern void Vertices_GetTCoords(IntPtr vertex, [MarshalAs(UnmanagedType.LPArray)] float[] tcoords, bool vertor2t);

         [DllImport(Native.Dll), SuppressUnmanagedCodeSecurity]
        static extern void Vertices_GetTCoords2(IntPtr vertex, [MarshalAs(UnmanagedType.LPArray)] float[] tcoords);

         [DllImport(Native.Dll), SuppressUnmanagedCodeSecurity]
        static extern void Vertices_SetColor(IntPtr vertex, int[] color, bool vertor2t);

         [DllImport(Native.Dll), SuppressUnmanagedCodeSecurity]
        static extern void Vertices_SetNormal(IntPtr vertex, float[] normal, bool vertor2t);

         [DllImport(Native.Dll), SuppressUnmanagedCodeSecurity]
        static extern void Vertices_SetPos(IntPtr vertex, float[] pos, bool vertor2t);

         [DllImport(Native.Dll), SuppressUnmanagedCodeSecurity]
        static extern void Vertices_SetTCoords(IntPtr vertex, float[] tcoords, bool vertor2t);

         [DllImport(Native.Dll), SuppressUnmanagedCodeSecurity]
        static extern void Vertices_SetTCoords2(IntPtr vertex, float[] tcoords);

        #endregion
    }
}
