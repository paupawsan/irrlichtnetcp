// project created on 25/06/2006 at 5:40 P
using IrrlichtNETCP;
using System;
using System.Runtime.InteropServices;

namespace IrrlichtNetExample
{
    class MainClass
    {
        static SceneManager scene;
        static VideoDriver driver;
        static GUIEnvironment guienv;
        public static void Main(string[] args)
        {
            //Shall we fullscreen ? (new verb I just invented)
            bool fullscreen = false;

            //We check the optimal video mode (resolution and screen depth)
            //As a default we choose 800x600x32
            VideoMode optimalmode;
            optimalmode.Resolution = new Dimension2D(800, 600);
            optimalmode.Depth = 32;

            //OpenGL is platform independent whereas DirectX isn't
            //As a default we choose OpenGL
            DriverType drivertype = DriverType.OpenGL;

            //We set a cool caption for our example
            string caption = "Irrlicht.NET CP Example on ";
            //Is the application started on a Windows platform ?
            //You may wonder why I did not wrote "Environment.OSVersion.Platform == PlatformID.Unix"
            //The reason is simple : We do not need to know if we are on an unix platform,
            //We need to know if we ARE NOT ON A WINDOWS platform, that's different indeed.
            if (Environment.OSVersion.Platform != PlatformID.Win32Windows &&
                Environment.OSVersion.Platform != PlatformID.Win32NT &&
                Environment.OSVersion.Platform != PlatformID.Win32S)
            {
                caption += "Linux/Unix System";
                //If the script is compiled on debug mode, we set 1024x768x32 with fullscreen.
                //I guess most of users will have at least a 15' screen...
#if !DEBUG
	            fullscreen = true;
				optimalmode.Resolution = new Dimension2D(1024, 768);
				optimalmode.Depth = 32;
#endif
            }
            else //We ARE on a Windows Platform
            {
                caption += "Windows";

                //Uncomment this line and the engine will use DirectX 9 on a Windows system
                //I don't like Direct3D 9 but well the choice is yours ;)
                //drivertype = DriverType.Direct3D9;

                //Here we are, if the engine is compiled with release settings,
                //We can play with a funny toy : fakedevice.
                //It does not seem to work on Linux (perhaps one of those bugs that only appear on some cursed computers like mine)
                //With this device, we will determine the ideal video mode by forcing
                //The application to launch on the desktop video mode
                //If a bug occurs (meaning video mode is null for instance)
                //We set the same video mode as on Linux : 1024x768x32 which is the most common video mode.
#if !DEBUG
	            IrrlichtDevice fakedevice = new IrrlichtDevice(DriverType.Null, new Dimension2D(), 16, false, false, false, false);
	            optimalmode = fakedevice.DesktopVideoMode;
				if(optimalmode.Resolution.Width == 0 || optimalmode.Resolution.Height == 0)
				{
					optimalmode.Resolution = new Dimension2D(1024, 768);
					optimalmode.Depth = 32;
				}
	            fakedevice.Dispose();
	            fullscreen = true;
#endif
            }

            //We add a cool caption which says which renderer (OpenGL or Direct3D9 for instance) we use
            caption += " With " + drivertype + " - ";

            //Here we are, we create the device with settings we determined before
            IrrlichtDevice device = new IrrlichtDevice(drivertype,
                                                       optimalmode.Resolution,
                                                       optimalmode.Depth, fullscreen,
                                                       true, //Stencil Buffer (for shadow)
                                                       false, //Vertical Synchronisation (use it if you want your application not to go over 70 FPS)
                                                       false); //Anti Aliasing

            scene = device.SceneManager; //We get some object such as the scene manager
            driver = device.VideoDriver;
            guienv = device.GUIEnvironment;

            device.FileSystem.WorkingDirectory = "../../medias"; //We set Irrlicht's current directory to %application directory%/media
            device.CursorControl.Visible = false; //Let's hide the cursor

            device.OnEvent += new OnEventDelegate(device_OnEvent); //We had a simple delegate that will handle every event
            device.WindowCaption = caption; //And we set a basic caption

            //We create a floor mesh which is actually an hill plane mesh without height
            AnimatedMesh floormesh = scene.AddHillPlaneMesh("_MyHill_", new Dimension2Df(250, 250),
                                                            new Dimension2D(5, 5),
                                                            0f, new Dimension2Df(0, 0),
                                                            new Dimension2Df(10, 10));

            //We make the planar texture mapping to set the texture resolution     
            scene.MeshManipulator.MakePlanarTextureMapping(floormesh.GetMesh(0), 0.006f);

            //We create a new mesh with tangents. It is needed for Parallax mapping
            Mesh tFloor = scene.MeshManipulator.CreateMeshWithTangents(floormesh.GetMesh(0));
            Texture heightmap = driver.GetTexture("rockwall_height.bmp"); //Our normal map
            driver.MakeNormalMapTexture(heightmap, 10f); //We make a quite exagerated normal map (just to show off)

            //And here we are, we finally create the scene node
            SceneNode floor = scene.AddMeshSceneNode(tFloor, null, -1);

            //We get the material
            Material mat = floor.GetMaterial(0);
            mat.Texture1 = driver.GetTexture("rockwall.bmp"); //Diffuse texture
            mat.Texture2 = heightmap; //Normal map
            mat.MaterialType = MaterialType.ParallaxMapSolid; //The beautiful Parallax Mapping
            mat.MaterialTypeParam = 0.035f; //Parameter for the height of parallax mapping

            //We create a FPS camera which is a basic camera controlled by 
            //Arrow keys and mouse such as the camera in Quake or Doom
            CameraSceneNode cam = scene.AddCameraSceneNodeFPS(null, 50f, 100f, true);
            cam.Position = new Vector3D(0, 100, -100);
            cam.FarValue = 10000f; //We want to see all the scene 

            //We add the sword (don't tell me that it is not a katana, I already know it !)
            SceneNode katana = scene.AddMeshSceneNode(scene.GetMesh("katana.x").GetMesh(0), cam, -1);
            Vector3D KInitialRotation = new Vector3D(0, 20, -90);
            Vector3D KInitialPosition = new Vector3D(23, -3, 40);
            //We set our materials (the n 1 is the sword and the 0 the stick)
            katana.Scale = new Vector3D(100f, 100f, 100f);
            mat = katana.GetMaterial(1);
            mat.DiffuseColor = new Color(255, 90, 90, 90);
            mat.AmbientColor = new Color(255, 90, 90, 90);
            mat.EmissiveColor = new Color(255, 90, 90, 90);
            mat.SpecularColor = new Color(255, 90, 90, 90);
            Material mat2 = katana.GetMaterial(0);
            mat2.DiffuseColor = new Color(255, 10, 10, 10);
            mat2.EmissiveColor = new Color(255, 120, 80, 0);

            //We add a little shining effect with particles on the sword
            ParticleSystemSceneNode particles = scene.AddParticleSystemSceneNode(false, katana, -1);
            particles.SetEmitter(particles.CreateBoxEmitter(scene.GetMesh("katana.x").GetMesh(0).GetMeshBuffer(1).BoundingBox,
                                                              new Vector3D(0, 0.002f, 0),
                                                              1000, 1000,
                                                              new Color(0, 255, 255, 255),
                                                              new Color(0, 255, 255, 255),
                                                              200, 200, 0));
            particles.ParticleSize = new Dimension2Df(1f, 3f); //A funny size for our funny effect
            particles.AddAffector(particles.CreateFadeOutParticleAffector(new Color(0, 0, 0, 0), 100));
            particles.SetMaterialTexture(0, driver.GetTexture("fire.bmp"));
            particles.SetMaterialType(MaterialType.TransparentAddColor);
            particles.SetMaterialFlag(MaterialFlag.Lighting, false);

            //We create three dwarves with shadows
            AnimatedMeshSceneNode dwarf = scene.AddAnimatedMeshSceneNode(scene.GetMesh("dwarf.x"));
            dwarf.AnimationSpeed = 15;
            dwarf.Position = new Vector3D(0, 0, 100);
            dwarf.AddShadowVolumeSceneNode(-1, true, 10000f); //Wow... It was really hard to create it !
            dwarf.Scale = new Vector3D(1.5f, 1.5f, 1.5f);

            //Here we have our light that will simply rotate around the dwarf
            LightSceneNode dwarflight = scene.AddLightSceneNode(dwarf, new Vector3D(0, 0, 0), Colorf.White, 10000f, -1);
            dwarflight.AddAnimator(scene.CreateFlyCircleAnimator(new Vector3D(0, 100, 0), 100f, 0.001f));

            dwarf = scene.AddAnimatedMeshSceneNode(scene.GetMesh("dwarf.x"));
            dwarf.AnimationSpeed = 10;
            dwarf.Position = new Vector3D(-100, 0, 100);
            dwarf.AddShadowVolumeSceneNode(-1, true, 10000f);
            dwarf.Scale = new Vector3D(1.5f, 1.5f, 1.5f);

            dwarf = scene.AddAnimatedMeshSceneNode(scene.GetMesh("dwarf.x"));
            dwarf.AnimationSpeed = 20;
            dwarf.Position = new Vector3D(100, 0, 100);
            dwarf.AddShadowVolumeSceneNode(-1, true, 10000f);
            dwarf.Scale = new Vector3D(1.5f, 1.5f, 1.5f);

            //We set the shadow color.
            //I reduced a lot the opacity since our light is not supposed to be the sun
            //And the scene is at night... Thus a shadow is not something very visible.
            scene.ShadowColor = new Color(100, 0, 0, 0);

            //We had a simple billboard to represent physically the light
            BillboardSceneNode lightbill = scene.AddBillboardSceneNode(dwarflight, new Dimension2Df(20, 20), -1);
            Texture fire = driver.GetTexture("fire.bmp");
            lightbill.SetMaterialTexture(0, fire);
            lightbill.SetMaterialType(MaterialType.TransparentAddColor);
            lightbill.SetMaterialFlag(MaterialFlag.Lighting, false);

            //We add a simple skybox
            scene.AddSkyBoxSceneNode(null, new Texture[] { driver.GetTexture("irrlicht2_up.jpg"), 
                                                           driver.GetTexture("irrlicht2_dn.jpg"), 
                                                           driver.GetTexture("irrlicht2_lf.jpg"), 
                                                           driver.GetTexture("irrlicht2_rt.jpg"), 
                                                           driver.GetTexture("irrlicht2_ft.jpg"),
                                                           driver.GetTexture("irrlicht2_bk.jpg") },
                                                           -1);

            //We had a simple texture that will be rendered each time
            Texture matthias = driver.GetTexture("matthias.png");

            //Here is our logo
            Texture logo = driver.GetTexture("NETCPlogo.png");

            //Just a simple demonstration of a feature which is not on Irrlicht .NET :
            //Listing children of the node as a simple Array.
            foreach (SceneNode node in cam.Children)
                Console.WriteLine("Child : " + node.ToString() + " Parent : " + node.Parent.ToString());

            //Another cool new feature :
            //FileSystem class with features like listing every file that Irrlicht can load
            //(including files on zip archives for instance)
            //foreach (FileListItem item in device.FileSystem.FileList)
            //    Console.WriteLine(item.ToString());

            double random = 0;
            Random chance = new Random();

			int lastfps = -1;
            while (device.Run() && !Exit)
            {
                //We get the FPS to synchronize all movements
                //Notice that I advice NOT TO USE THE FPS to synchronize
                //Because it is updated every second whereas it can change at every time
                //Use the time between each loop instead
                //(We use it here because it is not very important but be very careful)
                int FPS = driver.FPS;
                
                if(FPS != lastfps)
	            {
	                //And here we are, we set the caption of the main window
	                device.WindowCaption = caption + "FPS : " + FPS;
	                lastfps = FPS;
	            }
	            
                if (FPS < 10)
                    FPS = 70;
                random += ((2000f / FPS) + chance.Next(-10, 10)) / 1000f;
                //If someone has played the demo about thirty years...
                if (random >= double.MaxValue)
                    random = 0;
                //Sinuses and Cosines are very useful because we all know that
                //they are between -1 and 1. Thus we don't need to add some "if"
                //And we can control the domain easilly
                //We had a realstic move of the sword because try to hold one during 10 minutes
                //You won't be static... Tested !
                //A nice thing would be to generate it using the camera movement speed or things like that...
                katana.Position = KInitialPosition +
                                    new Vector3D((float)(Math.Sin(random) / 6.0f),
                                                 (float)(Math.Sin(random) / 10f),
                                                 (float)(Math.Cos(random) / 6.0f));
                katana.Rotation = KInitialRotation +
                                    new Vector3D((float)(Math.Cos(random) / 2f),
                                                 (float)(Math.Cos(random)),
                                                 (float)(Math.Sin(random) / 2f));

                //We clear the back buffer and begin the scene
                driver.BeginScene(true, true, Color.From(255, 50, 50, 50));
                //First we draw all 3D Objects such as our sword or the dwarves
                //Notice that the order is very important... Try to move this line just before the
                //driver.EndScene and you will see the difference
                scene.DrawAll();

                //3 rectangles that could for instance represent Health, Mana and Endurance points...
                driver.Draw2DRectangle(new Rect(new Position2D(10, 5), new Dimension2D(300, 15)), Color.Red);
                driver.Draw2DRectangle(new Rect(new Position2D(10, 25), new Dimension2D(300, 15)), Color.Blue);
                driver.Draw2DRectangle(new Rect(new Position2D(10, 45), new Dimension2D(300, 15)), Color.Green);
                //A little image loaded before that could represent for instance our character
                driver.Draw2DImage(matthias, new Position2D(driver.ScreenSize.Width - matthias.OriginalSize.Width, 0),
                                   new Rect(new Position2D(0, 0),
                                   matthias.OriginalSize), Color.White, true);
                //And finally our logo is painted 
                driver.Draw2DImage(logo, new Position2D(0, driver.ScreenSize.Height - logo.OriginalSize.Height),
                                   new Rect(new Position2D(0, 0),
                                   logo.OriginalSize), Color.White, true);

                //Finally we draw everything in the GUI Environment... CF the GUI example
                guienv.DrawAll();
                //End of the scene, the back buffer is displayed
                driver.EndScene();                
            }
            //ALWAYS DISPOSE THE DEVICE AT THE END
            //It is REQUIRED on Linux because if you don't, XWindow will stay at the old video mode
            //And you can't imagine how ugly it is not to get back to the original video mode 
            device.Close();
        }
        static bool Exit = false;

        //Our function that will handle every event irrlicht casts
        //If we handled the event we must return true, else false !
        static bool device_OnEvent(Event ev)
        {
            //A log has been sent, we just draw it on the window...
            //Here you could for instance store this on a text file or send it to your girlfriend...
            if (ev.Type == EventType.LogTextEvent)
            {
                Console.WriteLine(ev.LogText);
                return true;
            }
            //If the user has pressed Escape, we end the application.
            //I advice to add such a line on EVERY CROSS-PLATFORM APPLICATION
            //Because on many Linux systems, you won't be able to leave the application if you started
            //it as fullscreen... Your friend is CTRL + ALT + BACKSPACE.
            if (ev.Type == EventType.KeyInputEvent && ev.KeyPressedDown && ev.KeyCode == KeyCode.Escape)
            {
                Exit = true;
                return true;
            }
            //And another great features added in 0.4 (Irrlicht 1.1) :
            //Making screenshots and saving them !
            //For now, Irrlicht only supports saving to bmp but Irrlicht .NET CP
            //Supports a lot of format such as jpeg, gif, png...
            if (ev.Type == EventType.KeyInputEvent && ev.KeyPressedDown && ev.KeyCode == KeyCode.Return)
            {
                driver.WriteImageIntoFile(driver.CreateScreenShot(), "screenshot.jpg");
                return true;
            }
            //The event wasn't handled.
            return false;
        }
    }
}
