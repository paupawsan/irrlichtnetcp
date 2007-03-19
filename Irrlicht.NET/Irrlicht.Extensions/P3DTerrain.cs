/*
	Perlin simplex noise terrain by cube3 
*/

using System;
using IrrlichtNETCP;
using IrrlichtNETCP.Extensions.Other;
using IrrlichtNETCP.Inheritable;

namespace IrrlichtNETCP.Extensions
{

    public class P3DSimplexNoiseTerrain : ISceneNode
    {
    

        int _numVertsPerRow;
        int _numVertsPerCol;
        int _cellSpacing;
        int _numCellsPerRow;
        int _numCellsPerCol;
        int _width;
        int _depth;
        public int NumVertices;
        int _numIndices;
        int _numTriangles;
        float _heightScale;
        ushort[] _indices;

        SceneManager _mgr;
        VideoDriver _driver;
        public Material Material;
        public Vertex3D[] v;

        Box3D Box3D;

        public float[] Vertices
        {
            get
            {
                float[] ret = new float[NumVertices * 3];
                for (int i = 0; i < NumVertices; i++)
                {
                    ret[(i * 3)] = v[i].Position.X;
                    ret[(i * 3) + 1] = v[i].Position.Y;
                    ret[(i * 3) + 2] = v[i].Position.Z;
                }
                return ret;
            }
        }

        public P3DSimplexNoiseTerrain(SceneNode parent, SceneManager mgr, int id, int vertsPerRow, int vertsPerCol, int cellSpacing, float heightScale)
            : base(parent, mgr, id)
        {
            _mgr = mgr;
            _driver = _mgr.VideoDriver;

            Material = new Material();
            Material.Lighting = false;

            _numVertsPerRow = vertsPerRow;
            _numVertsPerCol = vertsPerCol;
            _cellSpacing = cellSpacing;
            _heightScale = heightScale;

            _numCellsPerRow = _numVertsPerRow - 1;
            _numCellsPerCol = _numVertsPerCol - 1;
            _width = _numCellsPerRow * _cellSpacing;
            _depth = _numCellsPerCol * _cellSpacing;
            NumVertices = _numVertsPerRow * _numVertsPerCol;
            _numIndices = (_numCellsPerRow * _numCellsPerCol) * 6;
            _numTriangles = (_numCellsPerRow * _numCellsPerCol) * 2;
            _indices = new ushort[_numIndices];

            int startX = -_width / 2;
            int startZ = _depth / 2;

            int endX = _width / 2;
            int endZ = -_depth / 2;

            Box3D = new Box3D(new Vector3D(startX, - _heightScale, startZ), new Vector3D(endX, _heightScale, endZ));

            float uCoordIncrementSize = 1.0f;
            float vCoordIncrementSize = 1.0f;

            v = new Vertex3D[NumVertices];



            int i = 0;
            for (int z = startZ; z >= endZ; z -= _cellSpacing)
            {
                int j = 0;
                for (int x = startX; x <= endX; x += _cellSpacing)
                {

                    int index = i * _numVertsPerRow + j;
                    float height;// = noise.PerlinNoise1F(x, 100*heightScale, 0.001f);
                    // large noise.
                    height = PerlinSimplexNoise.noise(x * 0.0001f, z * 0.0001f) * _heightScale;
                    // detail noise.
                    height += PerlinSimplexNoise.noise(x * 0.001f, z * 0.001f) * _heightScale / 10;
                    v[index] = new Vertex3D(
                        new Vector3D(x, height, z),
                        new Vector3D(0, 1, 0),
                        Color.Black,
                        new Vector2D(j * uCoordIncrementSize, i * vCoordIncrementSize)
                        );
                    j++;
                }
                i++;
            }

            int baseIndex = 0;

            for (int k = 0; k < _numCellsPerRow; k++)
            {
                for (int j = 0; j < _numCellsPerCol; j++)
                {
                    _indices[baseIndex + 0] = (ushort)(k * _numVertsPerRow + j);
                    _indices[baseIndex + 1] = (ushort)(k * _numVertsPerRow + j + 1);
                    _indices[baseIndex + 2] = (ushort)((k + 1) * _numVertsPerRow + j);
                    _indices[baseIndex + 3] = (ushort)((k + 1) * _numVertsPerRow + j);
                    _indices[baseIndex + 4] = (ushort)(k * _numVertsPerRow + j + 1);
                    _indices[baseIndex + 5] = (ushort)((k + 1) * _numVertsPerRow + j + 1);

                    baseIndex += 6;
                }
            }
        }

		public override void OnRegisterSceneNode ()
		{
            if (Visible)
                _mgr.RegisterNodeForRendering(this);
            base.OnRegisterSceneNode();
		}


        public override Box3D BoundingBox
        {
            get
            {
                return Box3D;
            }
        }

        public override void Render()
        {
            _driver.SetMaterial(Material);
            _driver.SetTransform(TransformationState.World, AbsoluteTransformation);
            _driver.DrawIndexedTriangleList(v, v.Length, _indices, _numTriangles);
        }
    }
}