using System;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;

namespace IrrlichtNETCP
{
	public class Texture : NativeElement
	{
		public Texture(IntPtr raw) : base(raw)
		{
		}
		
		public ColorFormat ColorFormat { get { return Texture_GetColorFormat(_raw); } }
		public DriverType DriverType { get { return Texture_GetDriverType(_raw); } }
		public Dimension2D OriginalSize{ get { int[] dim = new int[2]; Texture_GetOriginalSize(_raw, dim); return Dimension2D.FromUnmanaged(dim); } }
		public int Pitch { get { return Texture_GetPitch(_raw); } }
        //EDITED KIWSA
        /*public virtual Matrix4 Transform
        {
            get
            {
                float[] mat = new float[16];
                Texture_GetTransform(_raw, mat);
                return Matrix4.FromUnmanaged(mat);
            }
            set
            {
                Texture_SetTransform(_raw, value.ToUnmanaged());
            }
        }
        //END EDIT*/

        /// <summary>
        /// Call this before any modification/read of the texture via GetPixel/SetPixel
        /// If you try to modify/acces the texture without it, it will still work but really slower 
        /// Because it will lock and unlock the texture each time you access a pixel.
        /// </summary>
        /// <returns>Pointer to the lock result. You can use it in an unsafe context or use GetPixel/SetPixel</returns>
        public IntPtr Lock()
        {
            _lockresult = Texture_Lock(_raw);
            return _lockresult;
        }

        /// <summary>
        /// You must always call this after each lock.
        /// </summary>
        public void Unlock()
        {
            if (_lockresult == IntPtr.Zero)
                return;
            Texture_UnLock(_raw);
            _lockresult = IntPtr.Zero;
        }

        /// <summary>
        /// Sets the current color of a pixel
        /// </summary>
        /// <param name="x">Width of the pixel</param>
        /// <param name="y">Height of the pixel</param>
        /// <param name="color">Color of the pixel</param>
        public void SetPixel(int x, int y, Color color)
        {
            bool unlock = _lockresult == IntPtr.Zero;
            if (unlock)
                Lock();

            LockResult_SetPixel(_lockresult, _raw, x, y, color.ToUnmanaged());

            if (unlock)
                Unlock();
        }

        /// <summary>
        /// Gets the current color of the pixel.
        /// </summary>
        /// <param name="x">Width of the pixel</param>
        /// <param name="y">Height of the pixel</param>
        /// <returns>The color</returns>
        public Color GetPixel(int x, int y)
        {
            bool unlock = _lockresult == IntPtr.Zero;
            if (unlock)
                Lock();

            int[] color = new int[4];
            LockResult_GetPixel(_lockresult, _raw, x, y, color);

            if (unlock)
                Unlock();

            return Color.FromUnmanaged(color);
        }

        /// <summary>
        /// Saves the texture to a file
        /// </summary>
        /// <param name="name">Path to the file in Irrlicht's working directory</param>
        public void Save(string name)
        {
            System.Drawing.Imaging.ImageFormat fmt;
            switch (Path.GetExtension(name).ToLower())
            {
                case ".jpg":
                case ".jpeg":
                    fmt = System.Drawing.Imaging.ImageFormat.Jpeg;
                    break;

                case ".png":
                    fmt = System.Drawing.Imaging.ImageFormat.Png;
                    break;

                case ".gif":
                    fmt = System.Drawing.Imaging.ImageFormat.Gif;
                    break;

                case ".ico":
                    fmt = System.Drawing.Imaging.ImageFormat.Icon;
                    break;

                case ".tiff":
                    fmt = System.Drawing.Imaging.ImageFormat.Tiff;
                    break;

                case ".wmf":
                    fmt = System.Drawing.Imaging.ImageFormat.Wmf;
                    break;

                default:
                case ".bmp":
                    fmt = System.Drawing.Imaging.ImageFormat.Bmp;
                    break;
            }

            System.Drawing.Bitmap img = new System.Drawing.Bitmap(OriginalSize.Width, OriginalSize.Height);

            int w = img.Width;
            int h = img.Height;

            BitmapData bmpData = img.LockBits(new Rectangle(0, 0, w, h), ImageLockMode.ReadWrite, PixelFormat.Format32bppArgb);
            int stride = bmpData.Stride;       
            System.IntPtr Scan0 = bmpData.Scan0;
            Color[,] retrieve = Retrieve();
            unsafe
            {
                byte* p = (byte*)(void*)Scan0;
                int offset = stride - w * 4;
                for (int y = 0; y < h; ++y)
                {
                    for (int x = 0; x < w; ++x)
                    {
                        p[0] = (byte)retrieve[x, y].B;
                        p[1] = (byte)retrieve[x, y].G;
                        p[2] = (byte)retrieve[x, y].R;
                        p[3] = (byte)retrieve[x, y].A;

                        p += 4;
                    }
                    p += offset;
                }
            }
            img.UnlockBits(bmpData);
            img.Save(name, fmt);
        }

        public void RegenerateMipMapLevels()
        {
            Texture_RegenerateMipMapLevels(_raw);
        }

        #region Modify
        /// <summary>
        /// Modifies the texture using a simple callback called on each pixel's modification.
        /// </summary>
        /// <param name="callback">Callback called for each pixel</param>
        public void Modify(ModifyPixel callback)
        {
            switch (ColorFormat)
            {
                case ColorFormat.A1R5G5B5:
                    ModifyA1R5G5B5(callback);
                    break;

                case ColorFormat.R5G6B5:
                    ModifyR5G6B5(callback);
                    break;
                    
                case ColorFormat.A8R8G8B8:
                    ModifyA8R8G8B8(callback);
                    break;

                default:
                    throw new NotImplementedException(ColorFormat + " modifying options are not implemented. Consider using unsafe acess or Get/SetPixel instead.");
            }
        }

        unsafe protected void ModifyA8R8G8B8(ModifyPixel callback)
        {
            int w = OriginalSize.Width;
            int h = OriginalSize.Height;
            IntPtr lockresult = Lock();
            int* directacces = (int*)(void*)lockresult;
            int pitch = Pitch / 4;
            for (int x = 0; x < w; ++x)
                for (int y = 0; y < h; ++y)
                {
                    Color col;
                    if (callback(x, y, out col))
                        directacces[x + y * pitch] = col.NativeColor;
                }
            Unlock();
        }

        unsafe protected void ModifyR5G6B5(ModifyPixel callback)
        {
            int w = OriginalSize.Width;
            int h = OriginalSize.Height;
            IntPtr lockresult = Lock();
            short* directacces = (short*)(void*)lockresult;
            int pitch = Pitch / 2;
            for (int x = 0; x < w; ++x)
                for (int y = 0; y < h; ++y)
                {
                    Color col;
                    if (callback(x, y, out col))
                    {
                        uint color = (uint)col.NativeColor;
                        directacces[x + y * pitch] = (short)(0x8000 | (color & 0x1F) | ((color >> 1) & 0x7FE0));
                    }
                }
            Unlock();
        }

        unsafe protected void ModifyA1R5G5B5(ModifyPixel callback)
        {
            int w = OriginalSize.Width;
            int h = OriginalSize.Height;
            IntPtr lockresult = Lock();
            short* directacces = (short*)(void*)lockresult;
            int pitch = Pitch / 2;
            for (int x = 0; x < w; ++x)
                for (int y = 0; y < h; ++y)
                {
                    Color col;
                    if (callback(x, y, out col))
                    {
                        uint color = (uint)col.NativeColor;
                        directacces[x + y * pitch] = (short)((color & 0x80000000) >> 16 |
                                                             (color & 0x00F80000) >> 9 |
                                                             (color & 0x0000F800) >> 6 |
                                                             (color & 0x000000F8) >> 3);
                    }
                }
            Unlock();
        }
        #endregion

        #region Retrieve
        /// <summary>
        /// Safe and fast way to retrieve all pixels of the texture.
        /// </summary>
        /// <returns>A two-dimension array with all pixels [x,y]</returns>
        public Color[,] Retrieve()
        {
            switch (ColorFormat)
            {
                case ColorFormat.A1R5G5B5:
                    return RetrieveA1R5G5B5();

                case ColorFormat.R5G6B5:
                    return RetrieveR5G6B5();

                case ColorFormat.A8R8G8B8:
                    return RetrieveA8R8G8B8();

                default:
                    throw new NotImplementedException(ColorFormat + " retrieving options are not implemented. Consider using unsafe acess or Get/SetPixel instead.");
            }
        }

        unsafe protected Color[,] RetrieveA8R8G8B8()
        {
            int w = OriginalSize.Width;
            int h = OriginalSize.Height;
            Color[,] tor = new Color[w, h];
            IntPtr lockresult = Lock();
            int* directacces = (int*)(void*)lockresult;
            int pitch = Pitch / 4;
            for (int x = 0; x < w; ++x)
                for (int y = 0; y < h; ++y)
                {
                    tor[x, y] = new Color();
                    tor[x, y].NativeColor = directacces[x + y * pitch];
                }
            Unlock();
            return tor;
        }

        unsafe protected Color[,] RetrieveR5G6B5()
        {
            int w = OriginalSize.Width;
            int h = OriginalSize.Height;
            Color[,] tor = new Color[w, h];
            IntPtr lockresult = Lock();
            short* directacces = (short*)(void*)lockresult;
            int pitch = Pitch / 2;
            for (int x = 0; x < w; ++x)
                for (int y = 0; y < h; ++y)
                {
                   uint color = (uint)directacces[x + y * pitch];
                   tor[x, y] = new Color();
                   tor[x, y].NativeColor = (int)((0xFF000000 | ((color & 0xF800) << 8) |
                                                 ((color & 0x07E0) << 5) | (color & 0x1F) << 3));
                }
            Unlock();
            return tor;
        }

        unsafe protected Color[,] RetrieveA1R5G5B5()
        {
            int w = OriginalSize.Width;
            int h = OriginalSize.Height;
            Color[,] tor = new Color[w, h];
            IntPtr lockresult = Lock();
            short* directacces = (short*)(void*)lockresult;
            int pitch = Pitch / 2;
            for (int x = 0; x < w; ++x)
                for (int y = 0; y < h; ++y)
                {
                        short color = directacces[x + y * pitch];
                        tor[x,y] = new Color();
                        tor[x, y].NativeColor = (int)((color & 0x8000) << 16 |
                                                      (color & 0x7C00) << 9 |
                                                      (color & 0x03E0) << 6 |
                                                      (color & 0x001F) << 3);
                }
            Unlock();
            return tor;
        }
        #endregion

        #region .NET Wrapper Native Code
        IntPtr _lockresult = IntPtr.Zero;
		[DllImport(Native.Dll)]
		static extern ColorFormat Texture_GetColorFormat(IntPtr raw);
		
		[DllImport(Native.Dll)]
		static extern DriverType Texture_GetDriverType(IntPtr raw);
		
		[DllImport(Native.Dll)]
        static extern void Texture_GetOriginalSize(IntPtr raw, [MarshalAs(UnmanagedType.LPArray)] int[] toR);
		
		[DllImport(Native.Dll)]
		static extern int Texture_GetPitch(IntPtr raw);
		
		[DllImport(Native.Dll)]
		static extern void Texture_RegenerateMipMapLevels(IntPtr raw);

        [DllImport(Native.Dll)]
        static extern void Texture_GetTransform(IntPtr texture, [MarshalAs(UnmanagedType.LPArray)] float[] TxT);

        [DllImport(Native.Dll)]
        static extern void Texture_SetTransform(IntPtr texture, float[] TxT);

        [DllImport(Native.Dll)]
        static extern IntPtr Texture_Lock(IntPtr texture);

        [DllImport(Native.Dll)]
	    static extern void Texture_UnLock(IntPtr texture);


        [DllImport(Native.Dll)]
        static extern void LockResult_GetPixel(IntPtr lockr, IntPtr texture, int x, int y, [MarshalAs(UnmanagedType.LPArray)] int[] color);

        [DllImport(Native.Dll)]
	    static extern void LockResult_SetPixel(IntPtr lockr, IntPtr texture, int x, int y, int[] color);
		#endregion
	}
	public delegate bool ModifyPixel(int x, int y, out Color result);
}
