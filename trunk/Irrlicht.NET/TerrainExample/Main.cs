using System;
using System.IO;
using System.Xml;
using System.Collections.Generic;
using System.Text;
using IrrlichtNETCP;
using IrrlichtNETCP.Extensions;
using IrrlichtNETCP.Inheritable;

namespace Tutorial12
{
    class Tutorial12
    {
        static IrrlichtDevice device;
        static string StartUpModelFile = string.Empty;
        static string MessageText = string.Empty;
        static string Caption = string.Empty;
        static TerrainSceneNode terrain;
        static bool isWireframe = false;

        static void Main(string[] args)
        {
            // ask user for driver
            DriverType driverType = DriverType.OpenGL;

            // Ask user to select driver:
            // Create device and exit if creation fails:
            device = new IrrlichtDevice(driverType, new Dimension2D(640, 480), 32, false, true, true, true);
            device.FileSystem.WorkingDirectory = "../../medias"; //We set Irrlicht's current directory to %application directory%/media
            //MyEventReceiver receiver(room, env, driver);
            device.OnEvent += new OnEventDelegate(device_OnEvent); //We had a simple delegate that will handle every event


            /*************************************************/
            /* First, we add standard stuff to the scene: A nice irrlicht engine logo,
               a small help text, a user controlled camera, and we disable the mouse
               cursor.*/
            SceneManager smgr = device.SceneManager;
            VideoDriver driver = device.VideoDriver;
            GUIEnvironment env = device.GUIEnvironment;

            driver.SetTextureFlag(TextureCreationFlag.Always32Bit, true);

            // add irrlicht logo
            env.AddImage(driver.GetTexture("NETCPlogo.png"),
                new Position2D(10, 10), true, null, 0, "");

            // add some help text
            GUIStaticText text = env.AddStaticText(
                "Press 'W' to change wireframe mode\nPress 'D' to toggle detail map",
                new Rect(new Position2D(10, 453), new Position2D(200, 475)), true, true, null, -1,true);

            // add camera
            CameraSceneNode camera =
                smgr.AddCameraSceneNodeFPS(null, 100.0f, 1200.0f, false);
            camera.Position = new Vector3D(1900 * 2, 255 * 2, 3700 * 2);
            camera.Target = new Vector3D(2397 * 2, 343 * 2, 2700 * 2);
            camera.FarValue = 120000.0f;

            // disable mouse cursor
            device.CursorControl.Visible = false;
            /* Here comes the terrain renderer scene node: We add it just like any other scene
               node to the scene using ISceneManager::addTerrainSceneNode(). The only parameter
               we use is a file name to the heightmap we use. A heightmap is simply a gray
               scale texture. The terrain renderer loads it and creates the 3D terrain
               from it.
               To make the terrain look more big, we change the scale factor of it to
               (40, 4.4, 40). Because we don't have any dynamic lights in the scene, we
               switch off the lighting, and we set the file terrain-texture.jpg as texture
               for the terrain and detailmap3.jpg as second texture, called detail map.
               At last, we set the scale values for the texture: The first texture will be
               repeated only one time over the whole terrain, and the second one (detail map)
               20 times.
             */
            // add terrain scene node
            terrain = smgr.AddTerrainSceneNode(
                "terrain-heightmap.bmp", null, -1,
                new Vector3D(0,0,0), new Vector3D(1,1,1), new Vector3D(40, 4.4f, 40), new Color(255, 255, 255, 255),3,TerrainPatchSize.TPS9);

            terrain.SetMaterialFlag(MaterialFlag.Lighting, false);
            terrain.SetMaterialType(MaterialType.DetailMap);
            terrain.SetMaterialTexture(0, driver.GetTexture("terrain-texture.jpg"));
            terrain.SetMaterialTexture(1, driver.GetTexture("detailmap3.jpg"));


            terrain.ScaleTexture(1.0f, 20.0f);
			
			/*WaterSceneNode water = new WaterSceneNode(null, smgr, new Dimension2Df(64,64), new Dimension2D(100,100));
			water.Position = new Vector3D(1900 * 2, 255 * 2, 3700 * 2);*/
			
	
            /* To be able to do collision with the terrain, we create a triangle selector.
               If you want to know what triangle selectors do, just take a look into the
               collision tutorial. The terrain triangle selector works together with the
               terrain. To demonstrate this, we create a collision response animator and
               attach it to the camera, so that the camera will not be able to fly through
               the terrain.*/
            // create triangle selector for the terrain   
            TriangleSelector selector =
                smgr.CreateTerrainTriangleSelector(terrain, 0);

            // create collision response animator and attach it to the camera
            Animator anim = smgr.CreateCollisionResponseAnimator(
                selector, camera, new Vector3D(60, 100, 60),
                new Vector3D(0, 0, 0),
                new Vector3D(0, 50, 0), 0.0005f);
            camera.AddAnimator(anim);

            //we add the skybox which we already used in lots of Irrlicht examples.
            driver.SetTextureFlag(TextureCreationFlag.CreateMipMaps, false);

            smgr.AddSkyBoxSceneNode(null, new Texture[] {
                driver.GetTexture("irrlicht2_up.jpg"),
                driver.GetTexture("irrlicht2_dn.jpg"),
                driver.GetTexture("irrlicht2_lf.jpg"),
                driver.GetTexture("irrlicht2_rt.jpg"),
                driver.GetTexture("irrlicht2_ft.jpg"),
                driver.GetTexture("irrlicht2_bk.jpg")}, 0);

            driver.SetTextureFlag(TextureCreationFlag.CreateMipMaps, true);



            /* That's it, draw everything. Now you know how to use terrain
               in Irrlicht.
            */
            int lastFPS = -1;

            while (device.Run())
            {
                if (device.WindowActive)
                {
                    driver.BeginScene(true, true, new Color(0, 200, 200, 200));
                    smgr.DrawAll();
                    env.DrawAll();
                    driver.EndScene();

                    int fps = device.VideoDriver.FPS;
                    if (lastFPS != fps)
                    {
                        device.WindowCaption = "Terrain Renderer - Irrlicht Engine " +
                            "FPS:" + fps.ToString();
                        lastFPS = fps;
                    }
                }
            }


            /*
            In the end, delete the Irrlicht device.
            */
            device.Dispose();


        }

        public static bool device_OnEvent(Event p_event)
        {
            // check if user presses the key 'W' or 'D'
            if (p_event.Type == EventType.KeyInputEvent &&
                !p_event.KeyPressedDown)
            {
                switch (p_event.KeyCode)
                {
                    case KeyCode.Key_W:
                        isWireframe = !isWireframe;
                        terrain.SetMaterialFlag(MaterialFlag.Wireframe, isWireframe);
                        break;
                    case KeyCode.Key_D:
                        terrain.SetMaterialType(
                            terrain.GetMaterial(0).MaterialType== MaterialType.Solid ?
                            MaterialType.DetailMap : MaterialType.Solid);
                        return true;
                }
            }
            return false;
        }
    }
} 