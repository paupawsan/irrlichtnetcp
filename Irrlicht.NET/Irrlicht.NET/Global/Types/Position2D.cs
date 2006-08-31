using System;

namespace IrrlichtNETCP
{
	public struct Position2D
	{
		public int X, Y;
		public Position2D(int x, int y)
		{
			X = Y = 0;
			Set(x, y);
		}
		
		public void Set(int x, int y)
		{
			X = x;
			Y = y;
		}
		
		public override string ToString()
		{
			return GetType() + "; X = " + X + "; Y = " + Y;
		}
		
		public override int GetHashCode()
		{
			return ToString().GetHashCode();
		}
		
		public override bool Equals(object o)
		{
			if(o is Position2D)
				return GetHashCode() == o.GetHashCode();
			return base.Equals(o);
		}
		public static Position2D From(int X, int Y)
		{
			Position2D toR = new Position2D();
			toR.Set(X, Y);
			return toR;
		}
		public int[]ToUnmanaged() {return new int[] { X, Y }; }
		public static Position2D FromUnmanaged(int[] un){ return From(un[0], un[1]); }
	}
	
	public struct Position2Df
	{
		public float X, Y;
		public Position2Df(float x, float y)
		{
			X = Y = 0f;
			Set(x, y);
		}
		
		public void Set(float x, float y)
		{
			X = x;
			Y = y;
		}
		
		public override string ToString()
		{
			return GetType() + "; X = " + X + "; Y = " + Y;
		}
		
		public override int GetHashCode()
		{
			return ToString().GetHashCode();
		}
		
		public override bool Equals(object o)
		{
			if(o is Position2Df)
				return GetHashCode() == o.GetHashCode();
			return base.Equals(o);
		}
		
		public static Position2Df From(float X, float Y)
		{
			Position2Df toR = new Position2Df();
			toR.Set(X, Y);
			return toR;
		}
		public float[]ToUnmanaged() {return new float[] { X, Y }; }
		public static Position2Df FromUnmanaged(float[] un){ return From(un[0], un[1]); }
	}
}
