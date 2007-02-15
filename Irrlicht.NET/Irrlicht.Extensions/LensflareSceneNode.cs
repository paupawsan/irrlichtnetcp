// Lens flare scene node by Acki
//
// translated to C# for irrlicht.netcp by lester


using System;
using IrrlichtNETCP;
using IrrlichtNETCP.Inheritable;

namespace IrrlichtNETCP.Extensions
{
	
	public class LensflareSceneNode : ISceneNode
	{
		
		public LensflareSceneNode(SceneNode parent, SceneManager mgr, int id, Vector3D position) : base(parent, mgr, id)
		{
            AutomaticCulling = true;
            
            smgr = mgr;

			indices = new ushort[6];
            indices[0] = 0;
            indices[1] = 2;
            indices[2] = 1;
            indices[3] = 0;
            indices[4] = 3;
            indices[5] = 2;

			vertices = new Vertex3D[4];
			for (int i = 0; i<4; i++)
			{
				vertices[i] = new Vertex3D();
			}
            vertices[0].TCoords = Vector2D.From(0.0f, 1.0f);
            vertices[0].Color = Color.White;
            vertices[1].TCoords = Vector2D.From(0.0f, 0.0f);
            vertices[1].Color = Color.White;
            vertices[2].TCoords = Vector2D.From(1.0f, 0.0f);
            vertices[2].Color = Color.White;
            vertices[3].TCoords = Vector2D.From(1.0f, 1.0f);
            vertices[3].Color = Color.White;

            screensize = mgr.VideoDriver.ScreenSize;


			material = new Material();
			
			material.Lighting = false;

            material.MaterialType = MaterialType.TransparentAddColor;

            material.ZBuffer = false;

            material.ZWriteEnable = false;
            
            bbox = new Box3D();

            bbox.MinEdge = Vector3D.From(-2, -2, -2 );

            bbox.MaxEdge = Vector3D.From( 2,  2,  2 );
		
		}
		
		public Material Material
		{
			set
				{
					material = value;
				}
			get
				{
					return material;
				}
		}
		int getMaterialCount()
        {
            return 1;
        }
        
        public override void OnPreRender()
        {

            if (Visible )
            {

                Position2D pm = smgr.CollisionManager.GetScreenCoordinatesFrom3DPosition(AbsolutePosition,
                												smgr.ActiveCamera );
	            if (Rect.From(Position2D.From(0,0), Position2D.FromUnmanaged(screensize.ToUnmanaged())).IsPointInside(pm) )
                {
                    smgr.RegisterNodeForRendering(this, SceneNodeRenderPass.Transparent);
					base.OnPreRender();

                }
            }
        }        
		
        public override void Render()
        {

            VideoDriver driver = smgr.VideoDriver;
            CameraSceneNode camera = smgr.ActiveCamera;
            if (camera.Raw == IntPtr.Zero || driver.Raw == IntPtr.Zero || material.Texture1.Raw == IntPtr.Zero ) return;
            driver.SetTransform(TransformationState.World, new Matrix4());
            Vector3D campos = camera.AbsolutePosition;
            Dimension2D sz = smgr.VideoDriver.ScreenSize;
            Vector2D mid = Vector2D.From(sz.Width, sz.Height );
            mid /= 2;
       		Position2D lp = smgr.CollisionManager.GetScreenCoordinatesFrom3DPosition(AbsolutePosition, camera );
            Vector2D lightpos = Vector2D.From(lp.X, lp.Y );
            Vector2D ipos;
            int nframes = material.Texture1.OriginalSize.Width / material.Texture1.OriginalSize.Height;
            int imageheight = material.Texture1.OriginalSize.Height;
            int texw = 8;
            float texp = 1.0f / nframes;
            Vector3D target = camera.Target;
            Vector3D up = camera.UpVector;
            Vector3D view = target - campos;
            view.Normalize();
            Vector3D horizontal = up.CrossProduct(view);
            horizontal.Normalize();
            Vector3D vertical;
            vertical = horizontal.CrossProduct(view);
            vertical.Normalize();
            view *= -1.0f;
            for (int i=0; i<4; ++i) vertices[i].Normal = view;
            Vector3D hor;
            Vector3D ver;
            driver.SetMaterial(material );
            Vector3D pos;
            for (int ax = 0; ax < nframes; ax++ )
	        {
	
               if (ax == 0 )
                {

                    pos = AbsolutePosition;
                    texw = imageheight;

                }
                else
                {
                    ipos = mid.GetInterpolated(lightpos, (2.0f / nframes ) * ax );
                    pos = smgr.CollisionManager.GetRayFromScreenCoordinates(
                           Position2D.From((int)ipos.X , (int)ipos.Y ), camera ).End;
                    Vector3D dir = (campos - pos ).Normalize();
                    pos = campos + (dir * -10.0f );
                    texw = 4;
                }
                vertices[0].TCoords = Vector2D.From(   ax * texp,  1.0f);
                vertices[1].TCoords = Vector2D.From(   ax * texp,  0.0f);
                vertices[2].TCoords = Vector2D.From((ax+1) * texp, 0.0f);
                vertices[3].TCoords = Vector2D.From((ax+1) * texp, 1.0f);
                hor = horizontal * (0.5f * texw );
                ver = vertical   * (0.5f * texw );
                vertices[0].Position = pos + hor + ver;
                vertices[1].Position = pos + hor - ver;
                vertices[2].Position = pos - hor - ver;
                vertices[3].Position = pos - hor + ver;
           
                driver.DrawIndexedTriangleList(vertices, 4, indices, 2);

            }
        }		
        
        public override Box3D BoundingBox
        {
            get
            	{
            		return bbox;
            	}
        }
		
		
            Box3D bbox;
            Material material;
            Vertex3D[] vertices;
            ushort[] indices;
            Dimension2D screensize;
            SceneManager smgr;
		
	}
}
