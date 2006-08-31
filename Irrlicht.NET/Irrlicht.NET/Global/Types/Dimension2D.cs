using System;

namespace IrrlichtNETCP
{
	public struct Dimension2D
	{
		public Dimension2D(int w, int h)
		{
			Width = w;
			Height = h;
		}
		
		public int Width;
		public int Height;
		
		public void Set(int w, int h)
		{
			Width = w;
			Height = h;
		}
		
		public static Dimension2D From(int w, int h)
		{
			Dimension2D d;
			d.Width = w;
			d.Height = h;
			return d;
		}
		
		public int[]ToUnmanaged() { return new int[] {Width, Height}; }
		public static Dimension2D FromUnmanaged(int[] un){ return From(un[0], un[1]); }
		public override string ToString()
		{
			return "\"Type = " + GetType() + "; Width = " + Width + "; Height = " + Height + "\"";
		}
		public override bool Equals(object o)
		{
			if(o is Dimension2D)
				return GetHashCode() == o.GetHashCode();
			return base.Equals(o);
		}
		
		public override int GetHashCode()
		{
			return ToString().GetHashCode();
		}
	}
	
	public struct Dimension2Df
	{
		public Dimension2Df(float w, float h)
		{
			Width = w;
			Height = h;
		}
		
		public float Width;
		public float Height;
		
		public void Set(float w, float h)
		{
			Width = w;
			Height = h;
		}
		
		public static Dimension2Df From(float w, float h)
		{
			Dimension2Df d;
			d.Width = w;
			d.Height = h;
			return d;
		}
		
		public float[]ToUnmanaged() { return new float[] {Width, Height}; }
		public static Dimension2Df FromUnmanaged(float[] un){ return From(un[0], un[1]); }
		public override string ToString()
		{
			return "\"Type = " + GetType() + "; Width = " + Width + "; Height = " + Height + "\"";
		}
		public override bool Equals(object o)
		{
			if(o is Dimension2Df)
				return GetHashCode() == o.GetHashCode();
			return base.Equals(o);
		}
		
		public override int GetHashCode()
		{
			return ToString().GetHashCode();
		}
	}
}
