using System;
using System.Text;
using IrrlichtNETCP;
using IrrlichtNETCP.Inheritable;

//This is just a rewrite of http://irrlicht.sourceforge.net/tut003.html so it is not commented

namespace CustomSceneNode
{
	public class CustomAnimator : IAnimator
	{
		public CustomAnimator(Vector3D rotation)
		{
			Rotation = rotation;
		}
		Vector3D Rotation;
		uint StartTime = 0;
		public override void AnimateNode(SceneNode node, uint timeMs)
		{				
			Vector3D NewRotation = node.Rotation; 
			NewRotation += Rotation* ((timeMs-StartTime)/10.0f); 
			node.Rotation = (NewRotation); 
			StartTime = timeMs; 
		}
	}
	
    public class CustomSceneNode : ISceneNode
    {
        Box3D Box;
        Vertex3D[] Vertices = new Vertex3D[4];
        Material Material = new Material();
        SceneManager _mgr;
        VideoDriver _driver;

        public CustomSceneNode(SceneNode parent, SceneManager mgr, int id)
            : base(parent, mgr, id)
        {
            _mgr = mgr;
            _driver = _mgr.VideoDriver;
            Material.Wireframe = false;
            Material.Lighting = false;

            Vertices[0] = new Vertex3D(new Vector3D(0, 0, 10), new Vector3D(1, 1, 0), Color.From(255, 0, 255, 255), new Vector2D(0, 1));
            Vertices[1] = new Vertex3D(new Vector3D(10, 0, -10), new Vector3D(1, 0, 0), Color.From(255, 255, 0, 255), new Vector2D(1, 1));
            Vertices[2] = new Vertex3D(new Vector3D(0, 20, 0), new Vector3D(0, 1, 1), Color.From(255, 255, 255, 0), new Vector2D(1, 0));
            Vertices[3] = new Vertex3D(new Vector3D(-10, 0, -10), new Vector3D(0, 0, 1), Color.From(255, 0, 255, 0), new Vector2D(0, 0));

            Box = new Box3D();
            for (int i = 0; i < Vertices.Length; i++)
                Box.AddInternalPoint(Vertices[i].Position);
        }

        public override void OnPreRender()
        {
            if (Visible)
                _mgr.RegisterNodeForRendering(this);
            base.OnPreRender();
        }

        ushort[] indices = { 0, 2, 3, 2, 1, 3, 1, 0, 3, 2, 0, 1 };
        public override void Render()
        {
            _driver.SetMaterial(Material);
            _driver.SetTransform(TransformationState.World, AbsoluteTransformation);
            _driver.DrawIndexedTriangleList(Vertices, 4, indices, 4);
        }

        public override Box3D BoundingBox
        {
            get
            {
                return Box;
            }
        }

        public override int MaterialCount
        {
            get
            {
                return 1;
            }
        }

        public override Material GetMaterial(int i)
        {
            return Material;
        }
    }

    public class Program
    {
        static SceneManager _scene;
        static VideoDriver _driver;
        static void Main(string[] args)
        {
            IrrlichtDevice device = new IrrlichtDevice(DriverType.OpenGL, new Dimension2D(640, 480),
                                        32, false, false, false, false);

            string basecaption = "Irrlicht .NET CP Examples - Custom Scene Node feature";

            _scene = device.SceneManager;
            _driver = device.VideoDriver;

            CustomSceneNode myNode = new CustomSceneNode(null, _scene, 666);

            _scene.AddCameraSceneNode(null);
            _scene.ActiveCamera.Position = new Vector3D(0, -40, 0);
            _scene.ActiveCamera.Target = new Vector3D();

            myNode.AddAnimator(new CustomAnimator(new Vector3D(0.8f, 0, 0.8f)));
			int lastfps = -1, fps = 0;
            while (device.Run())
            {
                _driver.BeginScene(true, true, Color.Gray);
                _scene.DrawAll();
                _driver.EndScene();
                
                fps = _driver.FPS;
                if(fps != lastfps)
                {
                	device.WindowCaption = basecaption + " - FPS : " + fps;
                	lastfps = fps;
                }
            }
            device.Dispose();
        }
    }
}
