
using System;
using IrrlichtNETCP;
using IrrlichtNETCP.Extensions;

namespace ATMOSkytest
{
	
	
	public class ATMOSkytest
	{
		static VideoDriver driver;
        static SceneManager scene;
        static ATMOSphere atmo;
        static TerrainSceneNode terrain;        
        
        IrrlichtDevice device;
        static bool Exit = false;
        
		public ATMOSkytest()
		{
		}
		
		public static void Main()
		{
			IrrlichtDevice device = new IrrlichtDevice(DriverType.OpenGL,
                                                    new Dimension2D(640, 480),
                                                    32, false, true, true, true);
                                                    
            device.FileSystem.WorkingDirectory = "../../medias";                                        
            device.OnEvent += new OnEventDelegate(device_OnEvent);
            
            string caption = "Irrlicht .NET CP ATMOSpere test";
            
            driver = device.VideoDriver;
            scene = device.SceneManager;
            
            terrain = scene.AddTerrainSceneNode(
                "terrain-heightmap.bmp", null, -1,
                new Vector3D(0,0,0), new Vector3D(1,1,1), new Vector3D(40, 4.4f, 40), new Color(255, 255, 255, 255),5,TerrainPatchSize.TPS17);

            terrain.SetMaterialFlag(MaterialFlag.Lighting, false);
            terrain.SetMaterialType(MaterialType.DetailMap);
            terrain.SetMaterialTexture(0, driver.GetTexture("terrain-texture.jpg"));
            terrain.SetMaterialTexture(1, driver.GetTexture("detail2.tga"));

            terrain.ScaleTexture(1.0f, 20.0f);			
			
           	atmo = new ATMOSphere(device.Timer, null, scene, -1);
           	atmo.SkyTexture = driver.GetTexture("sky2.tga");
           	atmo.SunTexture = driver.GetTexture("sun.tga");
           	atmo.StarsTexture = driver.GetTexture("stars.bmp");
           	atmo.CreateSkyPalette();
			atmo.Speed = 600.0f;

            			        KeyMap keyMap = new KeyMap();
			        keyMap.AssignAction(KeyAction.MoveForward,KeyCode.Key_W);
			        keyMap.AssignAction(KeyAction.MoveBackward,KeyCode.Key_S);
			        keyMap.AssignAction(KeyAction.StrafeLeft,KeyCode.Key_A);
			        keyMap.AssignAction(KeyAction.StrafeRight,KeyCode.Key_D);
		
			CameraSceneNode fpsCam = scene.AddCameraSceneNodeFPS(null, 50, 200, false, keyMap);
			fpsCam.Position = Vector3D.From(2200,440,2000);
			
			device.CursorControl.Visible = false;
			driver.SetTextureFlag(TextureCreationFlag.Always32Bit, true);
			Timer timer = device.Timer;
			
			new ATMOSkytest();
            
            while (device.Run() && !Exit)
            {
            	driver.BeginScene(true, true, Color.Gray);
       			atmo.Update(device.Timer.RealTime);
				scene.DrawAll();
            	driver.EndScene();
            	
			}
		}
		
		static bool device_OnEvent(Event ev)
        {
            if (ev.KeyPressedDown && ev.KeyCode == KeyCode.Escape)
                Exit = true;
            return false;
        }
	}
}
