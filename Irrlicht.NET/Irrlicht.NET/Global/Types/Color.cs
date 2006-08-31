using System;

namespace IrrlichtNETCP
{
    public struct Color
    {
        public Color(int a, int r, int g, int b)
        {
            A = a;
            R = r;
            G = g;
            B = b;
        }

        public int A, R, G, B;
        public void Set(int a, int r, int g, int b)
        {
            A = a;
            R = r;
            G = g;
            B = b;
        }

        public static Color From(int a, int r, int g, int b)
        {
            Color c;
            c.A = a;
            c.R = r;
            c.G = g;
            c.B = b;
            return c;
        }

        public int NativeColor
        {
            get
            {
                return ((A & 0xff) << 24) |
                       ((R & 0xff) << 16) |
                       ((G & 0xff) << 8) |
                        (B & 0xff);
            }
            set
            {
                A = (value >> 24) & 0xff;
                R = (value >> 16) & 0xff;
                G = (value >> 8) & 0xff;
                B = (value) & 0xff;
            }
        }

        /// <summary>
        /// Gets an standard .NET color from the current Irrlicht color.
        /// </summary>
        /// <returns>The .NET color</returns>
        public System.Drawing.Color ToBCL()
        {
            return System.Drawing.Color.FromArgb(A, R, G, B);
        }

        /// <summary>
        /// Makes an Irrlicht color from a standard .NET color.
        /// </summary>
        /// <param name="bcl">The standard .NET color</param>
        /// <returns>The Irrlicht color</returns>
        public static Color FromBCL(System.Drawing.Color bcl)
        {
            return new Color(bcl.A, bcl.R, bcl.G, bcl.B);
        }

        public int[] ToUnmanaged() { return new int[] { A, R, G, B }; }
        public float[] ToShader() { return new float[] { A / 255, R / 255, G / 255, B / 255}; }
        public static Color FromUnmanaged(int[] un) { return From(un[0], un[1], un[2], un[3]); }
        public override string ToString()
        {
            return "\"Type = " + GetType() + "; A = " + A + "; R = " + R + "; G = " + G + "; B = " + B + "\"";
        }
        public override bool Equals(object o)
        {
            if (o is Color)
                return GetHashCode() == o.GetHashCode();
            return base.Equals(o);
        }

        public static bool operator ==(Color first, Color other)
        {
            return first.Equals(other);
        }

        public static bool operator !=(Color first, Color other)
        {
            return !first.Equals(other);
        }

        public override int GetHashCode()
        {
            return ToString().GetHashCode();
        }

        #region Premade Colors
        public static Color Red { get { return new Color(255, 255, 0, 0); } }
        public static Color Green { get { return new Color(255, 0, 255, 0); } }
        public static Color Blue { get { return new Color(255, 0, 0, 255); } }
        public static Color Black { get { return new Color(255, 0, 0, 0); } }
        public static Color White { get { return new Color(255, 255, 255, 255); } }
        public static Color Yellow { get { return new Color(255, 255, 255, 0); } }
        public static Color Purple { get { return new Color(255, 255, 0, 255); } }
        public static Color Gray { get { return new Color(255, 100, 100, 100); } }
        public static Color TransparentRed { get { return new Color(0, 255, 0, 0); } }
        public static Color TransparentGreen { get { return new Color(0, 0, 255, 0); } }
        public static Color TransparentBlue { get { return new Color(0, 0, 0, 255); } }
        public static Color TransparentBlack { get { return new Color(0, 0, 0, 0); } }
        public static Color TransparentWhite { get { return new Color(0, 255, 255, 255); } }
        public static Color TransparentYellow { get { return new Color(0, 255, 255, 0); } }
        public static Color TransparentPurple { get { return new Color(0, 255, 0, 255); } }
        public static Color TransparentGray { get { return new Color(0, 100, 100, 100); } }
        #endregion
    }

    public struct Colorf
    {
        public Colorf(float a, float r, float g, float b)
        {
            A = a;
            R = r;
            G = g;
            B = b;
        }

        public float A, R, G, B;
        public void Set(float a, float r, float g, float b)
        {
            A = a;
            R = r;
            G = g;
            B = b;
        }

        public static Colorf From(float a, float r, float g, float b)
        {
            Colorf c;
            c.A = a;
            c.R = r;
            c.G = g;
            c.B = b;
            return c;
        }

        /// <summary>
        /// Gets an standard .NET color from the current Irrlicht color.
        /// </summary>
        /// <returns>The .NET color</returns>
        public System.Drawing.Color ToBCL()
        {
            return System.Drawing.Color.FromArgb((int)A, (int)R, (int)G, (int)B);
        }

        /// <summary>
        /// Makes an Irrlicht color from a standard .NET color.
        /// </summary>
        /// <param name="bcl">The standard .NET color</param>
        /// <returns>The Irrlicht color</returns>
        public static Colorf FromBCL(System.Drawing.Color bcl)
        {
            return new Colorf(bcl.A, bcl.R, bcl.G, bcl.B);
        }

        public float[] ToUnmanaged() { return new float[] { R, G, B, A }; }
        public float[] ToShader() { return new float[] { R / 255, G / 255, B / 255, A / 255 }; }
        public static Colorf FromUnmanaged(float[] un) { return From(un[0], un[1], un[2], un[3]); }
        public override string ToString()
        {
            return "\"Type = " + GetType() + "; A = " + A + "; R = " + R + "; G = " + G + "; B = " + B + "\"";
        }
        public override bool Equals(object o)
        {
            if (o is Colorf)
                return GetHashCode() == o.GetHashCode();
            return base.Equals(o);
        }

        public override int GetHashCode()
        {
            return ToString().GetHashCode();
        }

        #region Premade Colors
        public static Colorf Red { get { return new Colorf(255, 255, 0, 0); } }
        public static Colorf Green { get { return new Colorf(255, 0, 255, 0); } }
        public static Colorf Blue { get { return new Colorf(255, 0, 0, 255); } }
        public static Colorf Black { get { return new Colorf(255, 0, 0, 0); } }
        public static Colorf White { get { return new Colorf(255, 255, 255, 255); } }
        public static Colorf Yellow { get { return new Colorf(255, 255, 255, 0); } }
        public static Colorf Purple { get { return new Colorf(255, 255, 0, 255); } }
        public static Colorf Gray { get { return new Colorf(255, 100, 100, 100); } }
        public static Colorf TransparentRed { get { return new Colorf(0, 255, 0, 0); } }
        public static Colorf TransparentGreen { get { return new Colorf(0, 0, 255, 0); } }
        public static Colorf TransparentBlue { get { return new Colorf(0, 0, 0, 255); } }
        public static Colorf TransparentBlack { get { return new Colorf(0, 0, 0, 0); } }
        public static Colorf TransparentWhite { get { return new Colorf(0, 255, 255, 255); } }
        public static Colorf TransparentYellow { get { return new Colorf(0, 255, 255, 0); } }
        public static Colorf TransparentPurple { get { return new Colorf(0, 255, 0, 255); } }
        public static Colorf TransparentGray { get { return new Colorf(0, 100, 100, 100); } }
        #endregion
    }
}
