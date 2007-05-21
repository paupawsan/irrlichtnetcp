using System;

namespace IrrlichtNETCP
{
	public struct Rect
	{
		public Position2D UpperLeftCorner, LowerRightCorner;
		public Rect(Position2D upperLeft, Position2D lowerRight)
		{
			UpperLeftCorner = new Position2D();
			LowerRightCorner = new Position2D();
			Set(upperLeft, lowerRight);
		}
		
		public Rect(int x1, int y1, int x2, int y2)
		{
			UpperLeftCorner = new Position2D(x1,y1);
			LowerRightCorner = new Position2D(x2,y2);
		}
		
		public Rect(Position2D pos, Dimension2D size)
		{
			UpperLeftCorner = new Position2D();
			LowerRightCorner = new Position2D();
			Set(pos, size);
		}
		
		public void Set(Position2D upperLeft, Position2D lowerRight)
		{
			UpperLeftCorner = upperLeft;
			LowerRightCorner = lowerRight;
		}
		
		public void Set(Position2D pos, Dimension2D size)
		{
			UpperLeftCorner = pos;
			LowerRightCorner = new Position2D(pos.X + size.Width, pos.Y + size.Height);
		}
		
		/// <summary>
		/// Returns if a 2d point is within this rectangle.
		/// </summary>
		/// <param name="pos"> Position to test if it lies within this rectangle.</param>
		/// <returns> Returns true if the position is within the rectangle, false if not.</returns>
		public bool IsPointInside(Position2D pos)
		{
			return UpperLeftCorner.X <= pos.X && UpperLeftCorner.Y <= pos.Y &&
				LowerRightCorner.X > pos.X && LowerRightCorner.Y > pos.Y;
		}
		
		/// <summary>
		/// Returns if the rectangle collides with an other rectangle.
		/// </summary>
		public bool IsRectCollided(Rect other)
		{
			return (LowerRightCorner.Y > other.UpperLeftCorner.Y && UpperLeftCorner.Y < other.LowerRightCorner.Y &&
				LowerRightCorner.X > other.UpperLeftCorner.X && UpperLeftCorner.X < other.LowerRightCorner.X);
		}
		
		/// <summary>
		/// Clips this rectangle with another one.
		/// </summary>
		public void ClipAgainst(Rect other)
		{
			if (other.LowerRightCorner.X < LowerRightCorner.X)
				LowerRightCorner.X = other.LowerRightCorner.X;
			if (other.LowerRightCorner.Y < LowerRightCorner.Y)
				LowerRightCorner.Y = other.LowerRightCorner.Y;
			
			if (other.UpperLeftCorner.X > UpperLeftCorner.X)
				UpperLeftCorner.X = other.UpperLeftCorner.X;
			if (other.UpperLeftCorner.Y > UpperLeftCorner.Y)
				UpperLeftCorner.Y = other.UpperLeftCorner.Y;
		}
		
		public int Width
		{
			get
			{
				return LowerRightCorner.X - UpperLeftCorner.X;
			}
			set
			{
				LowerRightCorner.X = UpperLeftCorner.X + value;
			}
		}
		
		public int Height
		{
			get
			{
				return LowerRightCorner.Y - UpperLeftCorner.Y;
			}
			set
			{
				LowerRightCorner.Y = UpperLeftCorner.Y + value;
			}
		}
		
		/// <summary>
		/// If the lower right corner of the rect is smaller then the upper left,
		/// the points are swapped.
		/// </summary>
		public void Repair()
		{
			if (LowerRightCorner.X < UpperLeftCorner.X)
			{
				int t = LowerRightCorner.X;
				LowerRightCorner.X = UpperLeftCorner.X;
				UpperLeftCorner.X = t;
			}
			
			if (LowerRightCorner.Y < UpperLeftCorner.Y)
			{
				int t = LowerRightCorner.Y;
				LowerRightCorner.Y = UpperLeftCorner.Y;
				UpperLeftCorner.Y = t;
			}
		}
		
		/// <summary>
		/// Returns if the rect is valid to draw. It could be invalid, if
		/// The UpperLeftCorner is lower or more right than the LowerRightCorner,
		/// or if the area described by the rect is 0.
		/// </summary>
		public bool IsValid()
		{
			return ((LowerRightCorner.X - UpperLeftCorner.X) *
				(LowerRightCorner.Y - UpperLeftCorner.Y) >= 0);
		}
		
		public Position2D Center
		{
			get
			{
				return new Position2D((UpperLeftCorner.X + LowerRightCorner.X) / 2,
								      (UpperLeftCorner.Y + LowerRightCorner.Y) / 2);
			}
		}
		
		public static Rect From(Position2D upperL, Position2D lowerR)
		{
			Rect toR = new Rect();
			toR.Set(upperL, lowerR);
			return toR;
		}

        /// <summary>
        /// Returns a standard .NET Rectangle from the current Irrlicht rectangle
        /// </summary>
        /// <returns>The standard .NET Rectangle</returns>
        public System.Drawing.Rectangle ToBCL()
        {
            return new System.Drawing.Rectangle(UpperLeftCorner.X, UpperLeftCorner.Y,
                                                Width, Height);
        }

        /// <summary>
        /// Makes an Irrlicht rectangle from the standard .NET rectangle.
        /// </summary>
        /// <returns>The Irrlicht rectangle</returns>
        public static Rect FromBCL(System.Drawing.Rectangle bcl)
        {
            return new Rect(new Position2D(bcl.X, bcl.Y),
                            new Dimension2D(bcl.Width, bcl.Height));
        }

		public int[]ToUnmanaged() {return new int[] { UpperLeftCorner.X, UpperLeftCorner.Y, LowerRightCorner.X, LowerRightCorner.Y}; }
		public static Rect FromUnmanaged(int[] un){ return From(Position2D.From(un[0], un[1]), Position2D.From(un[2], un[3])); }
	}
}
