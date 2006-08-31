using System;

namespace IrrlichtNETCP
{
	public struct Matrix4
	{
		public float[] M;
	
		public static Matrix4 From(float[] m)
		{
			Matrix4 mat;
			mat.M = m;
			return mat;
		}
		
		public Vector3D Translation
		{
			get
			{
				return new Vector3D(M[12], M[13], M[14]);
			}
			set
			{
				M[12] = value.X;
				M[13] = value.Y;
				M[14] = value.Z;
			}
		}
		
		public Vector3D Scale
		{
			get
			{
				return new Vector3D(M[0], M[5], M[10]);
			}
			set
			{
				M[0] = value.X;
				M[5] = value.Y;
				M[10] = value.Z;
			}
		}
		
		public Vector3D RotationRadian
		{
			get
			{
				return (RotationDegrees * 180.0f) / NewMath.PI;
			}
			set
			{
				double cr = Math.Cos( value.X );
				double sr = Math.Sin( value.X );
				double cp = Math.Cos( value.Y );
				double sp = Math.Sin( value.Y );
				double cy = Math.Cos( value.Z );
				double sy = Math.Sin( value.Z );
				
				M[0] = (float)( cp*cy );
				M[1] = (float)( cp*sy );
				M[2] = (float)( -sp );
				
				double srsp = sr*sp;
				double crsp = cr*sp;
				
				M[4] = (float)( srsp*cy-cr*sy );
				M[5] = (float)( srsp*sy+cr*cy );
				M[6] = (float)( sr*cp );
				
				M[8] = (float)( crsp*cy+sr*sy );
				M[9] = (float)( crsp*sy-sr*cy );
				M[10] = (float)( cr*cp );
			}
		}
		
		public Vector3D RotationDegrees
		{
			get
			{
				Matrix4 mat = this;
				
				double Y = -Math.Asin(mat.GetM(2,0));
				double C = Math.Cos(Y);
				Y *= (180.0 / Math.PI);
				
				double rotx, roty, X, Z;
				
				if (Math.Abs(C) > 0.0005f)
				{
					rotx = mat.GetM(2,2) / C;
					roty = mat.GetM(2,1)  / C;
					
					X = Math.Atan2( roty, rotx ) * (180.0 / Math.PI);
					
					rotx = mat.GetM(0,0) / C;
					roty = mat.GetM(1,0) / C;
					Z = Math.Atan2( roty, rotx ) * (180.0 / Math.PI);
				}
				else
				{
					X  = 0.0f;
					rotx = mat.GetM(1,1);
					roty = -mat.GetM	(0,1);
					Z  = Math.Atan2( roty, rotx ) * (180.0 / Math.PI);
				}
				
				// fix values that get below zero
				// before it would set (!) values to 360
				// that where above 360:
				if (X < 0.00) X += 360.00;
				if (Y < 0.00) Y += 360.00;
				if (Z < 0.00) Z += 360.00;
				
				return new Vector3D((float)X, (float)Y, (float)Z);
			}
			set
			{
				RotationRadian = (value * NewMath.PI) / 180.0f;
			}
		}
		
		/// <summary> Set matrix to identity. </summary>
		public void MakeIdentity()
		{
			M = new float[16];
			for (int i=0; i<16; ++i)
				M[i] = 0.0f;
			
			M[0] = M[5] = M[10] = M[15] = 1;
		}
		
		/// <summary> Direct accessing every row and colum of the matrix values </summary>
		public float GetM(int row, int col)
		{
			if (row < 0 || row >= 4 ||
				col < 0 || col >= 4)
				throw new ArgumentOutOfRangeException("Invalid index for accessing matrix members");
			
			return M[col * 4 + row];
		}
		
		public void SetM(int row, int col, float m)
		{
			if (row < 0 || row >= 4 ||
				col < 0 || col >= 4)
				throw new ArgumentOutOfRangeException("Invalid index for accessing matrix members");
			
			M[col * 4 + row] = m;
		}
		
		private float getMInsecure(int row, int col)
		{
			if (row < 0 || row >= 4 ||
				col < 0 || col >= 4)
				throw new ArgumentOutOfRangeException("Invalid index for accessing matrix members");
			
			return M[col * 4 + row];
		}
		
		private void setMInsecure(int row, int col, float m)
		{
			if (row < 0 || row >= 4 ||
				col < 0 || col >= 4)
				throw new ArgumentOutOfRangeException("Invalid index for accessing matrix members");
			
			M[col * 4 + row] = m;
		}

		/// <summary> Returns true if the matrix is the identity matrix. </summary>
		public bool IsIdentity()
		{
			for (int i=0; i<4; ++i)
				for (int j=0; j<4; ++j)
					if (j != i)
					{
						if (GetM(i,j) < -0.0000001f ||
							GetM(i,j) >  0.0000001f)
							return false;
					}
					else
					{
						if (GetM(i,j) < 0.9999999f ||
							GetM(i,j) > 1.0000001f)
							return false;
					}
			
			return true;
		}

        public Matrix4 GetTransposed()
        {
            Matrix4 t = new Matrix4();
            t.MakeIdentity();

            for (int r = 0; r < 4; ++r)
                for (int c = 0; c < 4; ++c)
                    t.SetM(r, c, this.GetM(c, r));

            return t;
        }
		
		public static Matrix4 operator*(Matrix4 a, Matrix4 b)
		{
			Matrix4 tmtrx = new Matrix4();
			tmtrx.MakeIdentity();
						
			tmtrx.M[0] = a.M[0]*b.M[0] + a.M[4]*b.M[1] + a.M[8]*b.M[2] + a.M[12]*b.M[3];
			tmtrx.M[1] = a.M[1]*b.M[0] + a.M[5]*b.M[1] + a.M[9]*b.M[2] + a.M[13]*b.M[3];
			tmtrx.M[2] = a.M[2]*b.M[0] + a.M[6]*b.M[1] + a.M[10]*b.M[2] + a.M[14]*b.M[3];
			tmtrx.M[3] = a.M[3]*b.M[0] + a.M[7]*b.M[1] + a.M[11]*b.M[2] + a.M[15]*b.M[3];
			
			tmtrx.M[4] = a.M[0]*b.M[4] + a.M[4]*b.M[5] + a.M[8]*b.M[6] + a.M[12]*b.M[7];
			tmtrx.M[5] = a.M[1]*b.M[4] + a.M[5]*b.M[5] + a.M[9]*b.M[6] + a.M[13]*b.M[7];
			tmtrx.M[6] = a.M[2]*b.M[4] + a.M[6]*b.M[5] + a.M[10]*b.M[6] + a.M[14]*b.M[7];
			tmtrx.M[7] = a.M[3]*b.M[4] + a.M[7]*b.M[5] + a.M[11]*b.M[6] + a.M[15]*b.M[7];
			
			tmtrx.M[8] = a.M[0]*b.M[8] + a.M[4]*b.M[9] + a.M[8]*b.M[10] + a.M[12]*b.M[11];
			tmtrx.M[9] = a.M[1]*b.M[8] + a.M[5]*b.M[9] + a.M[9]*b.M[10] + a.M[13]*b.M[11];
			tmtrx.M[10] = a.M[2]*b.M[8] + a.M[6]*b.M[9] + a.M[10]*b.M[10] + a.M[14]*b.M[11];
			tmtrx.M[11] = a.M[3]*b.M[8] + a.M[7]*b.M[9] + a.M[11]*b.M[10] + a.M[15]*b.M[11];
			
			tmtrx.M[12] = a.M[0]*b.M[12] + a.M[4]*b.M[13] + a.M[8]*b.M[14] + a.M[12]*b.M[15];
			tmtrx.M[13] = a.M[1]*b.M[12] + a.M[5]*b.M[13] + a.M[9]*b.M[14] + a.M[13]*b.M[15];
			tmtrx.M[14] = a.M[2]*b.M[12] + a.M[6]*b.M[13] + a.M[10]*b.M[14] + a.M[14]*b.M[15];
			tmtrx.M[15] = a.M[3]*b.M[12] + a.M[7]*b.M[13] + a.M[11]*b.M[14] + a.M[15]*b.M[15];
					
			return tmtrx;
		}
		
		public void BuildProjectionMatrixPerspectiveFovRH(float fieldOfViewRadians, float aspectRatio, float zNear, float zFar)
		{
			float h = (float)(Math.Cos(fieldOfViewRadians/2) / Math.Sin(fieldOfViewRadians/2));
			float w = h / aspectRatio;
			
			setMInsecure(0,0,2*zNear/w);
			setMInsecure(1,0,0);
			setMInsecure(2,0,0);
			setMInsecure(3,0,0);
			
			setMInsecure(0,1,0);
			setMInsecure(1,1,2*zNear/h);
			setMInsecure(2,1,0);
			setMInsecure(3,1,0);
			
			setMInsecure(0,2,0);
			setMInsecure(1,2,0);
			setMInsecure(2,2,zFar/(zFar-zNear));
			setMInsecure(3,2,-1);
			
			setMInsecure(0,3,0);
			setMInsecure(1,3,0);
			setMInsecure(2,3,zNear*zFar/(zNear-zFar));
			setMInsecure(3,3,0);
		}
		
		/// <summary> Builds a left-handed perspective projection matrix based on a field of view</summary>
		public void BuildProjectionMatrixPerspectiveFovLH(float fieldOfViewRadians, float aspectRatio, float zNear, float zFar)
		{
			float h = (float)(Math.Cos(fieldOfViewRadians/2) / Math.Sin(fieldOfViewRadians/2));
			float w = h / aspectRatio;
			
			setMInsecure(0,0,2*zNear/w);
			setMInsecure(1,0,0);
			setMInsecure(2,0,0);
			setMInsecure(3,0,0);
			
			setMInsecure(0,1,0);
			setMInsecure(1,1,2*zNear/h);
			setMInsecure(2,1,0);
			setMInsecure(3,1,0);
			
			setMInsecure(0,2,0);
			setMInsecure(1,2,0);
			setMInsecure(2,2,zFar/(zFar-zNear));
			setMInsecure(3,2,1);
			
			setMInsecure(0,3,0);
			setMInsecure(1,3,0);
			setMInsecure(2,3,zNear*zFar/(zNear-zFar));
			setMInsecure(3,3,0);
		}
		
		/// <summary> Builds a right-handed perspective projection matrix.</summary>
		public void BuildProjectionMatrixPerspectiveRH(float widthOfViewVolume, float heightOfViewVolume, float zNear, float zFar)
		{
			setMInsecure(0,0,2*zNear/widthOfViewVolume);
			setMInsecure(1,0,0);
			setMInsecure(2,0,0);
			setMInsecure(3,0,0);
			
			setMInsecure(0,1,0);
			setMInsecure(1,1,2*zNear/heightOfViewVolume);
			setMInsecure(2,1,0);
			setMInsecure(3,1,0);
			
			setMInsecure(0,2,0);
			setMInsecure(1,2,0);
			setMInsecure(2,2,zFar/(zNear-zFar));
			setMInsecure(3,2,-1);
			
			setMInsecure(0,3,0);
			setMInsecure(1,3,0);
			setMInsecure(2,3,zNear*zFar/(zNear-zFar));
			setMInsecure(3,3,0);
		}
		
		/// <summary> Builds a left-handed perspective projection matrix.</summary>
		public void BuildProjectionMatrixPerspectiveLH(float widthOfViewVolume, float heightOfViewVolume, float zNear, float zFar)
		{
			setMInsecure(0,0,2*zNear/widthOfViewVolume);
			setMInsecure(1,0,0);
			setMInsecure(2,0,0);
			setMInsecure(3,0,0);
			
			setMInsecure(0,1,0);
			setMInsecure(1,1,2*zNear/heightOfViewVolume);
			setMInsecure(2,1,0);
			setMInsecure(3,1,0);
			
			setMInsecure(0,2,0);
			setMInsecure(1,2,0);
			setMInsecure(2,2,zFar/(zNear-zFar));
			setMInsecure(3,2,1);
			
			setMInsecure(0,3,0);
			setMInsecure(1,3,0);
			setMInsecure(2,3,zNear*zFar/(zNear-zFar));
			setMInsecure(3,3,0);
		}
		
		/// <summary> Builds a left-handed orthogonal projection matrix.</summary>
		public void BuildProjectionMatrixOrthoLH(float widthOfViewVolume, float heightOfViewVolume, float zNear, float zFar)
		{
			setMInsecure(0,0,2/widthOfViewVolume);
			setMInsecure(1,0,0);
			setMInsecure(2,0,0);
			setMInsecure(3,0,0);
			
			setMInsecure(0,1,0);
			setMInsecure(1,1,2/heightOfViewVolume);
			setMInsecure(2,1,0);
			setMInsecure(3,1,0);
			
			setMInsecure(0,2,0);
			setMInsecure(1,2,0);
			setMInsecure(2,2,1/(zNear-zFar));
			setMInsecure(3,2,0);
			
			setMInsecure(0,3,0);
			setMInsecure(1,3,0);
			setMInsecure(2,3,zNear/(zNear-zFar));
			setMInsecure(3,3,1);
		}
		
		/// <summary> Builds a right-handed orthogonal projection matrix.</summary>
		public void BuildProjectionMatrixOrthoRH(float widthOfViewVolume, float heightOfViewVolume, float zNear, float zFar)
		{
			setMInsecure(0,0,2/widthOfViewVolume);
			setMInsecure(1,0,0);
			setMInsecure(2,0,0);
			setMInsecure(3,0,0);
			
			setMInsecure(0,1,0);
			setMInsecure(1,1,2/heightOfViewVolume);
			setMInsecure(2,1,0);
			setMInsecure(3,1,0);
			
			setMInsecure(0,2,0);
			setMInsecure(1,2,0);
			setMInsecure(2,2,1/(zNear-zFar));
			setMInsecure(3,2,0);
			
			setMInsecure(0,3,0);
			setMInsecure(1,3,0);
			setMInsecure(2,3,zNear/(zNear-zFar));
			setMInsecure(3,3,-1);
		}
		
		/// <summary> Builds a left-handed look-at matrix.</summary>
		public void BuildCameraLookAtMatrixLH(Vector3D position, Vector3D target, Vector3D upVector)
		{
			Vector3D zaxis = target - position;
			zaxis.Normalize();
			
			Vector3D xaxis = upVector.CrossProduct(zaxis);
			xaxis.Normalize();
			
			Vector3D yaxis = zaxis.CrossProduct(xaxis);
			
			setMInsecure(0,0,xaxis.X);
			setMInsecure(1,0,yaxis.X);
			setMInsecure(2,0,zaxis.X);
			setMInsecure(3,0,0);
			
			setMInsecure(0,1,xaxis.Y);
			setMInsecure(1,1,yaxis.Y);
			setMInsecure(2,1,zaxis.Y);
			setMInsecure(3,1,0);
			
			setMInsecure(0,2,xaxis.Z);
			setMInsecure(1,2,yaxis.Z);
			setMInsecure(2,2,zaxis.Z);
			setMInsecure(3,2,0);
			
			setMInsecure(0,3,-xaxis.DotProduct(position));
			setMInsecure(1,3,-yaxis.DotProduct(position));
			setMInsecure(2,3,-zaxis.DotProduct(position));
			setMInsecure(3,3,1.0f);
		}
		
		/// <summary> Builds a right-handed look-at matrix.</summary>
		public void BuildCameraLookAtMatrixRH(Vector3D position, Vector3D target, Vector3D upVector)
		{
			Vector3D zaxis = position - target;
			zaxis.Normalize();
			
			Vector3D xaxis = upVector.CrossProduct(zaxis);
			xaxis.Normalize();
			
			Vector3D yaxis = zaxis.CrossProduct(xaxis);
			
			setMInsecure(0,0,xaxis.X);
			setMInsecure(1,0,yaxis.X);
			setMInsecure(2,0,zaxis.X);
			setMInsecure(3,0,0);
			
			setMInsecure(0,1,xaxis.Y);
			setMInsecure(1,1,yaxis.Y);
			setMInsecure(2,1,zaxis.Y);
			setMInsecure(3,1,0);
			
			setMInsecure(0,2,xaxis.Z);
			setMInsecure(1,2,yaxis.Z);
			setMInsecure(2,2,zaxis.Z);
			setMInsecure(3,2,0);
			
			setMInsecure(0,3,-xaxis.DotProduct(position));
			setMInsecure(1,3,-yaxis.DotProduct(position));
			setMInsecure(2,3,-zaxis.DotProduct(position));
			setMInsecure(3,3,1.0f);
		}
		
		public void MakeInverse()
		{
			Matrix4 temp;
			if(GetInverse(out temp))
				this = temp;
		}
		
		public bool GetInverse(out Matrix4 outM)
		{
			outM = new Matrix4();
			outM.MakeIdentity();
			
			float d = (getMInsecure(0, 0) * getMInsecure(1, 1) - getMInsecure(1, 0) * getMInsecure(0, 1)) * (getMInsecure(2, 2) * getMInsecure(3, 3) - getMInsecure(3, 2) * getMInsecure(2, 3))	- (getMInsecure(0, 0) * getMInsecure(2, 1) - getMInsecure(2, 0) * getMInsecure(0, 1)) * (getMInsecure(1, 2) * getMInsecure(3, 3) - getMInsecure(3, 2) * getMInsecure(1, 3))
					+ (getMInsecure(0, 0) * getMInsecure(3, 1) - getMInsecure(3, 0) * getMInsecure(0, 1)) * (getMInsecure(1, 2) * getMInsecure(2, 3) - getMInsecure(2, 2) * getMInsecure(1, 3))	+ (getMInsecure(1, 0) * getMInsecure(2, 1) - getMInsecure(2, 0) * getMInsecure(1, 1)) * (getMInsecure(0, 2) * getMInsecure(3, 3) - getMInsecure(3, 2) * getMInsecure(0, 3))
			        - (getMInsecure(1, 0) * getMInsecure(3, 1) - getMInsecure(3, 0) * getMInsecure(1, 1)) * (getMInsecure(0, 2) * getMInsecure(2, 3) - getMInsecure(2, 2) * getMInsecure(0, 3))	+ (getMInsecure(2, 0) * getMInsecure(3, 1) - getMInsecure(3, 0) * getMInsecure(2, 1)) * (getMInsecure(0, 2) * getMInsecure(1, 3) - getMInsecure(1, 2) * getMInsecure(0, 3));
			
			if (d == 0f)
			return false;
			
			d = 1f / d;
			
			outM.setMInsecure(0, 0, d * (getMInsecure(1, 1) * (getMInsecure(2, 2) * getMInsecure(3, 3) - getMInsecure(3, 2) * getMInsecure(2, 3)) + getMInsecure(2, 1) * (getMInsecure(3, 2) * getMInsecure(1, 3) - getMInsecure(1, 2) * getMInsecure(3, 3)) + getMInsecure(3, 1) * (getMInsecure(1, 2) * getMInsecure(2, 3) - getMInsecure(2, 2) * getMInsecure(1, 3))));
			outM.setMInsecure(1, 0, d * (getMInsecure(1, 2) * (getMInsecure(2, 0) * getMInsecure(3, 3) - getMInsecure(3, 0) * getMInsecure(2, 3)) + getMInsecure(2, 2) * (getMInsecure(3, 0) * getMInsecure(1, 3) - getMInsecure(1, 0) * getMInsecure(3, 3)) + getMInsecure(3, 2) * (getMInsecure(1, 0) * getMInsecure(2, 3) - getMInsecure(2, 0) * getMInsecure(1, 3))));
			outM.setMInsecure(2, 0, d * (getMInsecure(1, 3) * (getMInsecure(2, 0) * getMInsecure(3, 1) - getMInsecure(3, 0) * getMInsecure(2, 1)) + getMInsecure(2, 3) * (getMInsecure(3, 0) * getMInsecure(1, 1) - getMInsecure(1, 0) * getMInsecure(3, 1)) + getMInsecure(3, 3) * (getMInsecure(1, 0) * getMInsecure(2, 1) - getMInsecure(2, 0) * getMInsecure(1, 1))));
			outM.setMInsecure(3, 0, d * (getMInsecure(1, 0) * (getMInsecure(3, 1) * getMInsecure(2, 2) - getMInsecure(2, 1) * getMInsecure(3, 2)) + getMInsecure(2, 0) * (getMInsecure(1, 1) * getMInsecure(3, 2) - getMInsecure(3, 1) * getMInsecure(1, 2)) + getMInsecure(3, 0) * (getMInsecure(2, 1) * getMInsecure(1, 2) - getMInsecure(1, 1) * getMInsecure(2, 2))));
			outM.setMInsecure(0, 1, d * (getMInsecure(2, 1) * (getMInsecure(0, 2) * getMInsecure(3, 3) - getMInsecure(3, 2) * getMInsecure(0, 3)) + getMInsecure(3, 1) * (getMInsecure(2, 2) * getMInsecure(0, 3) - getMInsecure(0, 2) * getMInsecure(2, 3)) + getMInsecure(0, 1) * (getMInsecure(3, 2) * getMInsecure(2, 3) - getMInsecure(2, 2) * getMInsecure(3, 3))));
			outM.setMInsecure(1, 1, d * (getMInsecure(2, 2) * (getMInsecure(0, 0) * getMInsecure(3, 3) - getMInsecure(3, 0) * getMInsecure(0, 3)) + getMInsecure(3, 2) * (getMInsecure(2, 0) * getMInsecure(0, 3) - getMInsecure(0, 0) * getMInsecure(2, 3)) + getMInsecure(0, 2) * (getMInsecure(3, 0) * getMInsecure(2, 3) - getMInsecure(2, 0) * getMInsecure(3, 3))));
			outM.setMInsecure(2, 1, d * (getMInsecure(2, 3) * (getMInsecure(0, 0) * getMInsecure(3, 1) - getMInsecure(3, 0) * getMInsecure(0, 1)) + getMInsecure(3, 3) * (getMInsecure(2, 0) * getMInsecure(0, 1) - getMInsecure(0, 0) * getMInsecure(2, 1)) + getMInsecure(0, 3) * (getMInsecure(3, 0) * getMInsecure(2, 1) - getMInsecure(2, 0) * getMInsecure(3, 1))));
			outM.setMInsecure(3, 1, d * (getMInsecure(2, 0) * (getMInsecure(3, 1) * getMInsecure(0, 2) - getMInsecure(0, 1) * getMInsecure(3, 2)) + getMInsecure(3, 0) * (getMInsecure(0, 1) * getMInsecure(2, 2) - getMInsecure(2, 1) * getMInsecure(0, 2)) + getMInsecure(0, 0) * (getMInsecure(2, 1) * getMInsecure(3, 2) - getMInsecure(3, 1) * getMInsecure(2, 2))));
			outM.setMInsecure(0, 2, d * (getMInsecure(3, 1) * (getMInsecure(0, 2) * getMInsecure(1, 3) - getMInsecure(1, 2) * getMInsecure(0, 3)) + getMInsecure(0, 1) * (getMInsecure(1, 2) * getMInsecure(3, 3) - getMInsecure(3, 2) * getMInsecure(1, 3)) + getMInsecure(1, 1) * (getMInsecure(3, 2) * getMInsecure(0, 3) - getMInsecure(0, 2) * getMInsecure(3, 3))));
			outM.setMInsecure(1, 2, d * (getMInsecure(3, 2) * (getMInsecure(0, 0) * getMInsecure(1, 3) - getMInsecure(1, 0) * getMInsecure(0, 3)) + getMInsecure(0, 2) * (getMInsecure(1, 0) * getMInsecure(3, 3) - getMInsecure(3, 0) * getMInsecure(1, 3)) + getMInsecure(1, 2) * (getMInsecure(3, 0) * getMInsecure(0, 3) - getMInsecure(0, 0) * getMInsecure(3, 3))));
			outM.setMInsecure(2, 2, d * (getMInsecure(3, 3) * (getMInsecure(0, 0) * getMInsecure(1, 1) - getMInsecure(1, 0) * getMInsecure(0, 1)) + getMInsecure(0, 3) * (getMInsecure(1, 0) * getMInsecure(3, 1) - getMInsecure(3, 0) * getMInsecure(1, 1)) + getMInsecure(1, 3) * (getMInsecure(3, 0) * getMInsecure(0, 1) - getMInsecure(0, 0) * getMInsecure(3, 1))));
			outM.setMInsecure(3, 2, d * (getMInsecure(3, 0) * (getMInsecure(1, 1) * getMInsecure(0, 2) - getMInsecure(0, 1) * getMInsecure(1, 2)) + getMInsecure(0, 0) * (getMInsecure(3, 1) * getMInsecure(1, 2) - getMInsecure(1, 1) * getMInsecure(3, 2)) + getMInsecure(1, 0) * (getMInsecure(0, 1) * getMInsecure(3, 2) - getMInsecure(3, 1) * getMInsecure(0, 2))));
			outM.setMInsecure(0, 3, d * (getMInsecure(0, 1) * (getMInsecure(2, 2) * getMInsecure(1, 3) - getMInsecure(1, 2) * getMInsecure(2, 3)) + getMInsecure(1, 1) * (getMInsecure(0, 2) * getMInsecure(2, 3) - getMInsecure(2, 2) * getMInsecure(0, 3)) + getMInsecure(2, 1) * (getMInsecure(1, 2) * getMInsecure(0, 3) - getMInsecure(0, 2) * getMInsecure(1, 3))));
			outM.setMInsecure(1, 3, d * (getMInsecure(0, 2) * (getMInsecure(2, 0) * getMInsecure(1, 3) - getMInsecure(1, 0) * getMInsecure(2, 3)) + getMInsecure(1, 2) * (getMInsecure(0, 0) * getMInsecure(2, 3) - getMInsecure(2, 0) * getMInsecure(0, 3)) + getMInsecure(2, 2) * (getMInsecure(1, 0) * getMInsecure(0, 3) - getMInsecure(0, 0) * getMInsecure(1, 3))));
			outM.setMInsecure(2, 3, d * (getMInsecure(0, 3) * (getMInsecure(2, 0) * getMInsecure(1, 1) - getMInsecure(1, 0) * getMInsecure(2, 1)) + getMInsecure(1, 3) * (getMInsecure(0, 0) * getMInsecure(2, 1) - getMInsecure(2, 0) * getMInsecure(0, 1)) + getMInsecure(2, 3) * (getMInsecure(1, 0) * getMInsecure(0, 1) - getMInsecure(0, 0) * getMInsecure(1, 1))));
			outM.setMInsecure(3, 3, d * (getMInsecure(0, 0) * (getMInsecure(1, 1) * getMInsecure(2, 2) - getMInsecure(2, 1) * getMInsecure(1, 2)) + getMInsecure(1, 0) * (getMInsecure(2, 1) * getMInsecure(0, 2) - getMInsecure(0, 1) * getMInsecure(2, 2)) + getMInsecure(2, 0) * (getMInsecure(0, 1) * getMInsecure(1, 2) - getMInsecure(1, 1) * getMInsecure(0, 2))));
			
			return true;
		}
		
		public float[] ToUnmanaged() { return M; }
		public static Matrix4 FromUnmanaged(float[] m) { return From(m); }
        public float[] ToShader() { return M; }
		
		public override string ToString()
		{
			string t = GetType().ToString();
			for(int i = 0; i < 4; i++)
				for(int j = 0; j < 4; j++)
					t += "; (" + i + "," + j + ") = " + GetM(i, j);
			return t;
		}
		public override bool Equals(object o)
		{
			if(o is Matrix4)
				return GetHashCode() == o.GetHashCode();
			return base.Equals(o);
		}
		
		public override int GetHashCode()
		{
			return ToString().GetHashCode();
		}

        public static Matrix4 Identity { get { Matrix4 mat = new Matrix4(); mat.MakeIdentity(); return mat; } }
	}
}
