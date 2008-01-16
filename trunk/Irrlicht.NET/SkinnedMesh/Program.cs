// Main.cs
//
// This is a tutorial, how to use SkinnedMesh
//
using System;
using IrrlichtNETCP;
using IrrlichtNETCP.Extensions;

namespace SkinnedMesh
{
	class MainClass
	{
		static IrrlichtDevice device = null;
		
		static BoneSceneNode bone1, bone2, bone3;
		
		public static void Main(string[] args)
		{
			// Do a usuall setup
			DriverType driverType = DriverType.OpenGL;
			device = new IrrlichtDevice(driverType, new Dimension2D(640, 480), 32, false, true, true, true);
            device.FileSystem.WorkingDirectory = "../../medias";			
			
			device.OnEvent += new OnEventDelegate(device_OnEvent);
			
			SceneManager smgr = device.SceneManager;
            VideoDriver driver = device.VideoDriver;
            GUIEnvironment guienv = device.GUIEnvironment;
			
			guienv.Skin.SetColor(GuiDefaultColor.ButtonText, Color.White);
			
            guienv.AddImage(driver.GetTexture("NETCPlogo.png"),
                new Position2D(10, 10), true, null, 0, "");
			
			// Static camera should be enough
            CameraSceneNode camera =
                smgr.AddCameraSceneNode(null);
			
			camera.Position = new Vector3D(0,40,-60);
			camera.Target = new Vector3D(0,40,0);
			
			smgr.AddLightSceneNode(null, new Vector3D(10, 9, 0), Colorf.White, 10.0f, -1);
			
            smgr.AddSkyBoxSceneNode(null, new Texture[] {
                driver.GetTexture("irrlicht2_up.jpg"),
                driver.GetTexture("irrlicht2_dn.jpg"),
                driver.GetTexture("irrlicht2_rt.jpg"),
				driver.GetTexture("irrlicht2_lf.jpg"),
                driver.GetTexture("irrlicht2_ft.jpg"),
                driver.GetTexture("irrlicht2_bk.jpg")}, 0);
			
			// The interesting part
			// First load the mesh with bones.
			AnimatedMesh mesh = smgr.GetMesh("dwarf.x");
			
			// Check, whether our mesh has bones
			if (mesh.MeshType == AnimatedMeshType.Skinned)
			{
			// Create an animatedmesh scene node from it				
			AnimatedMeshSceneNode mesh_node = smgr.AddAnimatedMeshSceneNode(mesh);
			
			
			// set joints to controllable state, or irrlicht will try to animate them
			mesh_node.JointMode = JointUpdateOnRenderMode.Control;
			
			// Get our bones by name
			bone1 = mesh_node.GetJointNode("spine2");
			bone2 = mesh_node.GetJointNode("head");
			bone3 = mesh_node.GetJointNode("rsholda");
			
			// We add three scrollbars to control our bones
			guienv.AddStaticText("Torso", new Rect(new Position2D(40, driver.ViewPort.Height-110), 
			                                                   new Dimension2D(240, 20)), 
				                     false,
				                     false,
				                     null,
				                     -1,
				                     false);
			GUIScrollBar scroll = guienv.AddScrollBar(true, 
			                    new Rect(new Position2D(40, driver.ViewPort.Height-100), 
			                                                   new Dimension2D(240, 20)),
			                    null,
                                1);
			scroll.Max = 180;
			// Initial rotation of each bone is 0, so set the scrollbar position to the middle
			// to make controlling more cool
			scroll.Pos = 90;
			
			guienv.AddStaticText("Head", new Rect(new Position2D(40, driver.ViewPort.Height-80), 
			                                                   new Dimension2D(240, 20)), 
				                     false,
				                     false,
				                     null,
				                     -1,
				                     false);
				
			scroll = guienv.AddScrollBar(true, 
			                    new Rect(new Position2D(40, driver.ViewPort.Height-70), 
			                                                   new Dimension2D(240, 20)),
			                    null,
                                2);
			scroll.Max = 160;
			scroll.Pos = 80;
				
			guienv.AddStaticText("Axe", new Rect(new Position2D(40, driver.ViewPort.Height-50), 
			                                                   new Dimension2D(240, 20)), 
				                     false,
				                     false,
				                     null,
				                     -1,
				                     false);				
			scroll = guienv.AddScrollBar(true, 
			                    new Rect(new Position2D(40, driver.ViewPort.Height-40), 
			                                                   new Dimension2D(240, 20)),
			                    null,
                                3);
			scroll.Max = 90;
			scroll.Pos = 45;		
			}
			
            int lastFPS = -1;

            while (device.Run())
            {
                if (device.WindowActive)
                {
                    driver.BeginScene(true, true, new Color(0, 200, 200, 200));
                    smgr.DrawAll();
                    guienv.DrawAll();
                    driver.EndScene();

                    int fps = device.VideoDriver.FPS;
                    if (lastFPS != fps)
                    {
                        device.WindowCaption = "Skinned Mesh - Irrlicht.NET CP Engine " +
                            "FPS:" + fps.ToString();
                        lastFPS = fps;
                    }
                }
            }			
			
		}

        public static bool device_OnEvent(Event ev)
        {
			if (ev.Type == EventType.GUIEvent)
            {
			    switch (ev.GUIEvent)
                {
					case GUIEventType.ScrollBarChanged:
					GUIScrollBar bar = (GUIScrollBar)ev.Caller;
					if (ev.Caller.ID == 1) // Torso
					{
						// We can control each bone position&rotation
						// like a normal scene node
						bone1.Rotation = new Vector3D(0,90-bar.Pos, 0);
						// This should be called after any modification done with the bone
						// to let it update it's children
						bone1.UpdateAbsolutePositionOfAllChildren();
					} else
					if (ev.Caller.ID == 2) // Head
					{
						bone2.Rotation = new Vector3D(0, 80-bar.Pos, 0);
						bone2.UpdateAbsolutePositionOfAllChildren();						
					} else // Axe
					{
						bone3.Rotation = new Vector3D(bar.Pos-30, 0, 0);
						bone3.UpdateAbsolutePositionOfAllChildren();						
					}
					break;
				default:
					return false;
				}
			}
            return false;
        }
		
	}
}