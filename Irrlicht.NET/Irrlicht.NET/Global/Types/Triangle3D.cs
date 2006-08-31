using System;
using System.Text;

namespace IrrlichtNETCP
{
    public struct Triangle3D
    {
        public Vector3D PointA, PointB, PointC;

        public Triangle3D(Vector3D A, Vector3D B, Vector3D C)
        {
            PointA = A;
            PointB = B;
            PointC = C;
        }
        public Triangle3D(float AX, float AY, float AZ, float BX, float BY, float BZ, float CX, float CY, float CZ)
            : this(new Vector3D(AX, AY, AZ), new Vector3D(BX, BY, BZ), new Vector3D(CX, CY, CZ))
        {
        }

        public float[] ToUnmanaged()
        {
            return new float[]
                { PointA.X, PointA.Y, PointA.Z,
                  PointB.X, PointB.Y, PointB.Z,
                  PointC.X, PointC.Y, PointC.Z };
        }
        public static Triangle3D FromUnmanaged(float[] un)
        {
            return new Triangle3D(un[0], un[1], un[2],
                                  un[3], un[4], un[5],
                                  un[6], un[7], un[8]);
        }

        public override string ToString()
        {
            return GetType() + "; A = " + PointA + "; B = " + PointB + "; C = " + PointC;
        }
        public override int GetHashCode()
        {
            return ToString().GetHashCode();
        }
        public override bool Equals(object obj)
        {
            if (obj is Triangle3D)
                return obj.GetHashCode() == GetHashCode();
            return base.Equals(obj);
        }
    }
}
