using System;
using IrrlichtNETCP;
using IrrlichtNETCP.Inheritable;

namespace IrrlichtNETCP.Extensions
{
	public class WaterSceneNode : ISceneNode
	{		
		static int _current = 0;
		
		VideoDriver _driver;
		SceneManager _scene;
		SceneNode _waternode;
		Texture _rt;
		CameraSceneNode _fixedcam;
				
		public WaterSceneNode(SceneNode parent, SceneManager mgr, Dimension2Df tileSize, Dimension2D tileCount) : 
			this(parent, mgr, tileSize, tileCount, new Dimension2D(256, 256), -1)
		{}
		
		public WaterSceneNode(SceneNode parent, SceneManager mgr, Dimension2Df tileSize, Dimension2D tileCount, Dimension2D precision) : 
			this(parent, mgr, tileSize, tileCount, precision, -1)
		{}
		
		public WaterSceneNode(SceneNode parent, SceneManager mgr, Dimension2Df tileSize,
		                      Dimension2D tileCount, Dimension2D precision, int id) : 
			base(parent, mgr, id)
		{
			_scene = mgr;
			_driver = mgr.VideoDriver;
			
			AnimatedMesh wmesh =  _scene.AddHillPlaneMesh("watermesh" + _current,
                tileSize,
                tileCount, 0,
                new Dimension2Df(0, 0),
                new Dimension2Df(1, 1));
           	_current++; 
           	 
            int dmat = _driver.GPUProgrammingServices.AddHighLevelShaderMaterial(
                 VERTEX_GLSL, "main", VertexShaderType._1_1, FRAGMENT_GLSL,
                 "main", PixelShaderType._1_1, OnShaderSet, MaterialType.Solid, 0); 
                 
            ClampShader = _driver.GPUProgrammingServices.AddHighLevelShaderMaterial(
                 CLAMP_VERTEX_GLSL, "main", VertexShaderType._1_1, CLAMP_FRAGMENT_GLSL,
                 "main", PixelShaderType._1_1, OnShaderSet, MaterialType.TransparentAlphaChannel, 1);
                 
           	_waternode = _scene.AddMeshSceneNode(wmesh.GetMesh(0), this, -1);  
            _waternode.SetMaterialType(dmat);
            _waternode.SetMaterialFlag(MaterialFlag.BackFaceCulling, false);
            
            _rt = _driver.CreateRenderTargetTexture(precision);
            _waternode.SetMaterialTexture(0, _rt); 
            
            CameraSceneNode oldcam = _scene.ActiveCamera;
            _fixedcam = _scene.AddCameraSceneNode(null);
            if(oldcam != null)
            	_scene.ActiveCamera = oldcam;
		}
		
		public SceneNode WaterNode
		{
			get
			{
				return _waternode;
			}
		}
		
		public void Update()
		{                 
			if(!Visible)
				return;
           foreach(ClampedTerrain terr in clampList)
               terr.terrain.SetMaterialType(ClampShader);     
               
       	    _waternode.Visible = false;        
			CameraSceneNode camera = _scene.ActiveCamera;
			
			_scene.ActiveCamera = _fixedcam;
            _fixedcam.FarValue = camera.FarValue;        
           	if(camera.Position.Y >= Position.Y)
           	{                      	
                	_fixedcam.Position = new Vector3D(camera.Position.X,
                	                                 2 * Position.Y - camera.Position.Y,
                	                                 camera.Position.Z);
                	Vector3D target = ((camera.Target - camera.Position).Normalize());
                	target.Y *= -1;
                	_fixedcam.Target = _fixedcam.Position + target * 20000;
                	_fixedcam.UpVector = camera.UpVector;
           }
           else
           {
	           	_fixedcam.Position = camera.Position;
	               
	           	Vector3D target = ((camera.Target - camera.Position).Normalize()) * 200000;
	           	_fixedcam.Target = _fixedcam.Position + target;
	           	_fixedcam.UpVector = camera.UpVector;
           }
           _driver.SetRenderTarget(_rt, true, true, Color.TransparentGray);      	
           _scene.DrawAll();
               
           foreach(ClampedTerrain terr in clampList)
               terr.terrain.SetMaterialType(MaterialType.DetailMap);
           _driver.SetRenderTarget(null, true, true, Color.Gray);
           _scene.ActiveCamera = camera;
           _waternode.Visible = true;       
		}
		static int ClampShader;
		System.Collections.ArrayList clampList = new System.Collections.ArrayList();
		public void ApplyClampingOnTerrain(TerrainSceneNode terrain)
		{
			ClampedTerrain clamp;
			clamp.terrain = terrain;
			clamp.oldmat = terrain.GetMaterial(0);
			clampList.Add(clamp);
		}
		
		public Colorf AddedColor = Colorf.From(255, 1, 1, 5);
		public Colorf MultiColor = Colorf.From(255, 190, 190, 210);
		public float WaveHeight = 3f;
		public float WaveLength = 50f;
		public float WaveSpeed = 10f;
		public float WaveDisplacement = 10f;
        void OnShaderSet(MaterialRendererServices services, int userData) 
        {
        	if(userData == 1)
        	{
        		services.SetPixelShaderConstant("DiffuseMap", 0f);
        		services.SetPixelShaderConstant("DetailMap", 1f);
        		Vector3D pos = _waternode.Position;
        		services.SetPixelShaderConstant("WaterPosition", pos.ToShader());
        		return;
        	}
        	services.SetVertexShaderConstant("Time", (float)((DateTime.Now.TimeOfDay.TotalMilliseconds)));
        	services.SetVertexShaderConstant("WaveHeight", WaveHeight);
        	services.SetVertexShaderConstant("WaveLength", WaveLength);
        	services.SetVertexShaderConstant("WaveSpeed", WaveSpeed);
        	
        	services.SetPixelShaderConstant("AddedColor", AddedColor.ToShader());
        	services.SetPixelShaderConstant("MultiColor", MultiColor.ToShader());
        	services.SetPixelShaderConstant("WaveDisplacement", WaveDisplacement);
        	services.SetPixelShaderConstant("UnderWater", _scene.ActiveCamera.Position.Y < Position.Y ? 1.0f : 0.0f);
        }
        
        #region Shaders
        static string VERTEX_GLSL = 
        				"uniform float Time;\n" +
						"uniform float WaveHeight, WaveLength, WaveSpeed;\n" +
						"varying vec4 waterpos;\n" +
						"varying float addition;\n" +
						"void main()\n" +
						"{\n" +
						"	waterpos = ftransform();\n" +
						"	addition = (sin((gl_Vertex.x/WaveLength) + (Time * WaveSpeed / 10000.0)) * WaveHeight) +\n" +
						"                   (cos((gl_Vertex.z/WaveLength) + (Time * WaveSpeed / 10000.0)) * WaveHeight);\n" +
						"	addition += sin(gl_Vertex.x) / 10.0 + cos(gl_Vertex.z) / 10.0;\n" +
						"	waterpos.y += addition;\n" +
						"	gl_Position = waterpos;\n" +
						"}\n";
        static string FRAGMENT_GLSL = 
        				"uniform sampler2D ReflectionTexture;\n" +
						"uniform vec4 AddedColor, MultiColor;\n" +
						"uniform float UnderWater, WaveDisplacement;\n" +
						"varying vec4 waterpos;\n" +
						"varying float addition;\n" +
						"void main()\n" +
						"{\n" +
						"	vec4 projCoord = waterpos * vec4(1.0 / waterpos.w);\n" +
						"	projCoord += vec4(1.0);\n" +
						"	projCoord *= vec4(0.5);\n" +
						"	projCoord.x += sin(addition) * (WaveDisplacement / 1000.0);\n" +
						"	projCoord.y += cos(addition) * (WaveDisplacement / 1000.0);\n" +
						"	projCoord = clamp(projCoord, 0.001, 0.999);\n" +
						"	if(UnderWater == 0.0)\n" +
						"		projCoord.y = 1.0 - projCoord.y;\n" +
						"	vec4 refTex = texture2D(ReflectionTexture, vec2(projCoord));\n" +
						"	refTex = (refTex + AddedColor) * MultiColor;\n" +
						"	gl_FragColor = refTex * refTex;\n" +
                        "	gl_FragColor += refTex;\n" +
                        "	if(UnderWater == 1.0)\n" +
                        "	    gl_FragColor *= (MultiColor / 1.3);\n" +
						"}\n";
		static string CLAMP_VERTEX_GLSL = 
						"varying float cutoff;\n" + 
						"void main()\n" + 
						"{\n" + 
						"	cutoff = gl_Vertex.y;\n" + 
						"	gl_Position = ftransform();\n" + 
						"	gl_TexCoord[0] = gl_MultiTexCoord0;\n" + 
						"}\n";
		static string CLAMP_FRAGMENT_GLSL = 
						"uniform sampler2D DiffuseMap, DetailMap;\n" +
						"uniform vec3 WaterPosition;\n" +
						"varying float cutoff;\n" +
						"void main()\n" +
						"{\n" +	
						"	vec4 color = texture2D(DiffuseMap, gl_TexCoord[0].st) * 3.0 *\n" + 
						"                     texture2D(DetailMap, vec2(gl_TexCoord[0].x * 100.0, gl_TexCoord[0].y * 100.0));\n" +
						"	if(cutoff <= (WaterPosition.y - 10.0))\n" +
						"		color.a = 0.0;\n" +
						"	else\n" +
						"		color.a = 1.0;\n" +
						"	gl_FragColor = color; \n" +
						"}\n";
        #endregion
	}
	internal struct ClampedTerrain
	{
		public TerrainSceneNode terrain;
		public Material oldmat;
	}
}
