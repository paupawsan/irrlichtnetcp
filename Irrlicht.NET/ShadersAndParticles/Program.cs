using System;
using System.Text;
using IrrlichtNETCP;
//Here is a new namespace... It contains all inheritable interfaces for Particle Emitters or Affectors for instance.
using IrrlichtNETCP.Inheritable;

namespace ShadersAndParticles
{
    class Program
    {
        static VideoDriver Driver;
        static SceneManager Scene;
        static void Main(string[] args)
        {
            //We choosed OpenGL because it is cross-platform and we only have the openGL shader
            //So please do not change this unless you change the shader !
            IrrlichtDevice device = new IrrlichtDevice(DriverType.OpenGL,
                                                    new Dimension2D(640, 480),
                                                    32, false, true, true, true);
            //We set a new working directory
            device.FileSystem.WorkingDirectory = "../../medias";
            device.OnEvent += new OnEventDelegate(device_OnEvent);

            //We set a basic caption
            string caption = "Irrlicht .NET CP Shaders and Particles Example";

            //We set our handlers
            Driver = device.VideoDriver;
            Scene = device.SceneManager;

            //We have already seen that... The only special thing is the pointless emitter (what a funny name isn't it ?)
            //Which is detailed just down.
            Texture fire = Driver.GetTexture("fire.bmp");
            ParticleSystemSceneNode particles = Scene.AddParticleSystemSceneNode(false, null, -1);
            particles.SetEmitter(new PointlessEmitter());
            //particles.AddAffector(new PointlessAffector());
            particles.SetMaterialTexture(0, fire);
            particles.SetMaterialType(MaterialType.TransparentAddColor);
            particles.SetMaterialFlag(MaterialFlag.Lighting, false);
            particles.ParticleSize = new Dimension2Df(50, 50);
            particles.ParticlesAreGlobal = false;

            particles = Scene.AddParticleSystemSceneNode(false, null, -1);
            particles.SetEmitter(new PointlessEmitter());
            //particles.AddAffector(new PointlessAffector());
            particles.SetMaterialTexture(0, fire);
            particles.SetMaterialType(MaterialType.TransparentAddColor);
            particles.SetMaterialFlag(MaterialFlag.Lighting, false);
            particles.ParticleSize = new Dimension2Df(50, 50);
            particles.Position = new Vector3D(0, 400, 0);
            particles.ParticlesAreGlobal = false;

            particles = Scene.AddParticleSystemSceneNode(false, null, -1);
            particles.SetEmitter(new PointlessEmitter());
            //particles.AddAffector(new PointlessAffector());
            particles.SetMaterialTexture(0, fire);
            particles.SetMaterialType(MaterialType.TransparentAddColor);
            particles.SetMaterialFlag(MaterialFlag.Lighting, false);
            particles.ParticleSize = new Dimension2Df(50, 50);
            particles.Position = new Vector3D(0, -400, 0);
            particles.ParticlesAreGlobal = false;

            //Here we only create 3 cubes and add a texture... Nothing exciting
            SceneNode cube1, cube2, cube3;
            cube1 = Scene.AddCubeSceneNode(40f, null, -1);
            cube2 = Scene.AddCubeSceneNode(40f, null, -1);
            cube3 = Scene.AddCubeSceneNode(40f, null, -1);
            cube1.SetMaterialTexture(0, Driver.GetTexture("rockwall.bmp"));
            cube2.SetMaterialTexture(0, Driver.GetTexture("rockwall.bmp"));
            cube3.SetMaterialTexture(0, Driver.GetTexture("rockwall.bmp"));

            //Here comes the fun... We create two low level shaders (taken from Irrlich's shader Example) and we set the base material as 
            //Solid for the first one and transparent for the second
            int mat = Driver.GPUProgrammingServices.AddShaderMaterialFromFiles("opengl.vsh", "opengl.psh", OnShaderSet, MaterialType.Solid, 0);
            int mat2 = Driver.GPUProgrammingServices.AddShaderMaterialFromFiles("opengl.vsh", "opengl.psh", OnShaderSet, MaterialType.TransparentAddColor, 0);
            //And now we add both materials... Notice that no cast is needed because SetMaterialType
            //Has an overload especially made for shaders !
            cube2.SetMaterialType(mat);
            cube3.SetMaterialType(mat2);

            cube2.Position = new Vector3D(0, -40, 0);
            cube3.Position = new Vector3D(0, 40, 0);

            //We create a fixed cam for our render target scene
            CameraSceneNode fixedcam = Scene.AddCameraSceneNode(null);
            fixedcam.Position = new Vector3D(50, 0, -1000);
            fixedcam.FarValue = 10000f;
            fixedcam.AddAnimator(Scene.CreateFlyCircleAnimator(fixedcam.Target, 5000f, 0.001f));

            //And a fps cam for our main scene
            CameraSceneNode fpscam = Scene.AddCameraSceneNodeFPS(null, 100f, 400f, false);
            Scene.ActiveCamera.Position = new Vector3D(50, 0, -1000);
            Scene.ActiveCamera.FarValue = 10000f;

            Texture mask = Driver.AddTexture(new Dimension2D(128, 128), "", ColorFormat.A8R8G8B8);  

            //Here is another cool feature from .NET CP
            //Direct access to textures via Texture.Lock/Unlock and Texture.SetPixel/GetPixel
            //Uncomment these lines to see the effect :)
            /*int w = mask.OriginalSize.Width;
            int h = mask.OriginalSize.Height;
            double maxdistance = Math.Sqrt(Math.Pow(w, 2) + Math.Pow(h, 2));
            mask.Lock();
            for (int x = 0; x < w; x++)
                for (int y = 0; y < h; y++)
                {
                    double distance = Math.Sqrt(Math.Pow((w - x), 2) + Math.Pow((h - y), 2));
                    double coeff = distance / maxdistance;
                    int color = (int)(255 - (255 * coeff));
                    Color pixel = new Color((int)(color / 1.5), (int)(color / 1.5), (int)(color / 2), (int)(color / 2));
                    mask.SetPixel(x, y, pixel);
                }
            mask.Unlock();
            //Again a useful feature... Direct texture saving to common formats such as png,jpg,bmp or gif...
            //Without coding anything, your texture is directly saved in Irrlicht's working directory !
            mask.Save("image.png");*/

            //You may have noticed that GetPixel/SetPixel is very slow even if you lock the texture.
            //Another insecure but quite faster way to proceed is to convert our lock to a pointer.
            //It needs unsafe code but works very fast...
            //However it is very unsecure and you must know perfectly what you are doing...
            //That's why Modify/Retrieve exists (look down)
            /*int w = mask.OriginalSize.Width;
            int h = mask.OriginalSize.Height;
            double maxdistance = Math.Sqrt(Math.Pow(w, 2) + Math.Pow(h, 2));
            IntPtr lockresult = mask.Lock();
            unsafe
            {
                int* directacces = (int*)(void*)lockresult;
                int pitch = mask.Pitch / 4;
                for (int x = 0; x < w; ++x)
                    for (int y = 0; y < h; ++y)
                    {
                        double distance = Math.Sqrt(Math.Pow((w - x), 2) + Math.Pow((h - y), 2));
                        double coeff = distance / maxdistance;
                        int color = (int)(255 - (255 * coeff));
                        Color pixel = new Color((int)(color / 1.5), (int)(color / 1.5), (int)(color / 2), (int)(color / 2));
                        directacces[x + y * pitch] = pixel.NativeColor;
                    }
            }
            mask.Unlock();*/

            //Here is the SAFEST AND FASTEST way to modify our texture.
            //A simple delegate which takes the coords of the pixel and returns the color
            //It is called on each pixel and you can even return false if you don't want to change the pixel.
            //We can now create our mask without speed or compatibility issue !
            int w = mask.OriginalSize.Width;
            int h = mask.OriginalSize.Height;
            double maxdistance = Math.Sqrt(Math.Pow(w, 2) + Math.Pow(h, 2));
            //We create a delegate to modify each pixel
            ModifyPixel del = delegate(int x, int y, out Color col)
            {
                //Here is our formula... You can modify it as you wish, this is just an example !
                double distance = Math.Sqrt(Math.Pow((w - x), 2) + Math.Pow((h - y), 2));
                double coeff = distance / maxdistance;
                int color = (int)(255 - (255 * coeff));
                //We now set the out color
                col = new Color((int)(color / 1.5), (int)(color / 1.5), (int)(color / 2), (int)(color / 2)); 
                //And we return true since we created a color.
                //If you return false, the current pixel won't be modified !
                return true;                       
            };
            //And we modify our texture with our delegate
            mask.Modify(del);
            //Uncomment this line to save our mask on a portable network graphic file !
            //You do not need to know jpg, bmp, gif, png... formats since they are converted automatically
            //mask.Save("mask.png");
            
            Console.WriteLine();
            Console.WriteLine("============================================");
            Console.WriteLine("Features List :");
            Console.WriteLine();
            for (VideoDriverFeature feat = 0; feat < VideoDriverFeature.Count; feat++)
                Console.WriteLine(feat + " = " + Driver.QueryFeature(feat));
            Console.WriteLine("============================================");

            //Here is our logo
            Texture logo = Driver.GetTexture("NETCPlogo.png");

            //Here is another feature, RenderTarget
            Texture renderTarget = Driver.CreateRenderTargetTexture(new Dimension2D(320, 240));
			int lastfps = -1, fps = 0;
            while (device.Run() && !Exit)
            {
                Driver.BeginScene(true, true, Color.Gray);

                Driver.SetRenderTarget(renderTarget, true, true, Color.TransparentBlue);
                Scene.ActiveCamera = fixedcam;
                Scene.DrawAll();
                Driver.SetRenderTarget(null, true, true, Color.Gray);

                Scene.ActiveCamera = fpscam;
                Scene.DrawAll();
                Driver.Draw2DImage(renderTarget, new Position2D(0, 0), true);
                Driver.Draw2DImage(mask, new Rect(new Position2D(), Driver.ScreenSize), new Rect(new Position2D(), mask.OriginalSize), Color.White, true);

                //And finally our logo is painted 
                Driver.Draw2DImage(logo, new Position2D(0, Driver.ScreenSize.Height - logo.OriginalSize.Height),
                                   new Rect(new Position2D(0, 0),
                                   logo.OriginalSize), Color.White, true);
                Driver.EndScene();

                fps = Driver.FPS;
                if(fps != lastfps)
                {
                	device.WindowCaption = caption + " - FPS : " + fps;
                	lastfps = fps;
                }
            }
            device.Dispose();
        }
        static bool Exit = false;

        static bool device_OnEvent(Event ev)
        {
            if (ev.KeyPressedDown && ev.KeyCode == KeyCode.Escape)
                Exit = true;
            return false;
        }

        static void OnShaderSet(MaterialRendererServices services, int userData)
        {
            //This is called when we need to set shader's constants
            //It is very simple and taken from Irrlicht's original shader example.
            //Please notice that many types already has a "ToShader" func made especially
            //For exporting to shader floats !
            //If the structure you want has no such function, then simply use "ToUnmanaged" instead
            Matrix4 invWorld = Driver.GetTransform(TransformationState.World);
            invWorld.MakeInverse();

            services.SetVertexShaderConstant(invWorld.ToShader(), 0, 4);

            Matrix4 worldviewproj;
            worldviewproj = Driver.GetTransform(TransformationState.Projection);
            worldviewproj *= Driver.GetTransform(TransformationState.View);
            worldviewproj *= Driver.GetTransform(TransformationState.World);

            services.SetVertexShaderConstant(worldviewproj.ToShader(), 4, 4);

            services.SetVertexShaderConstant(Scene.ActiveCamera.Position.ToShader(), 8, 1);

            Colorf col = Colorf.Blue;
            services.SetVertexShaderConstant(col.ToShader(), 9, 1);

            Matrix4 world = Driver.GetTransform(TransformationState.World);
            world = world.GetTransposed();
            services.SetVertexShaderConstant(world.ToShader(), 10, 4);
        }
    }

    //The class you have already seen... Basically it is just based on IParticleEmitter
    public class PointlessEmitter : IParticleEmitter
    {
        uint lastParticleCreation = 0;
        //The function called each time we need to emit something.
        //It takes as argument "now" which represent the actual time, 
        //"timeSinceLastCall" which represent... you know what
        //And an "out" argument (which means that IT MUST BE INITIALIZED INTO THIS FUNCTION),
        //"Particles" which represents every newly created particle.
        public void Emit(uint now, uint timeSinceLastCall, out Particle[] Particles)
        {
            //YOU ALWAYS MUST INITIALIZE Particles
            Particles = new Particle[0];
            if (now - lastParticleCreation < 100)
                return;
            Particles = new Particle[36];
            lastParticleCreation = now;
            //A little offset for the particles to go up and down.
            Vector3D offset = new Vector3D(0, (float)Math.Cos(now) / 10f, 0);
            //For every particle in our array
            for (int i = 0; i < 36; i++)
            {
                double angle = (Math.PI * 10 * i) / 180.0;
                //We create the particle
                Particles[i] = new Particle();
                //We set the postion as null...
                Particles[i].Position = new Vector3D(0, 0, 0);
                //Particle.Vector represents the direction and speed of the particle.
                //As you may have seen, I love cosines and sinus and here again I used
                //it to create a "circle" effect
                Particles[i].Vector = new Vector3D((float)Math.Cos(angle) / 5f, 0, (float)Math.Sin(angle) / 5f) + offset;
                //Start Vector is the same as the vector... It is useless here but serves for the affector.
                Particles[i].StartVector = Particles[0].Vector;
                //Start Time is now and End Time is now + 10 seconds
                Particles[i].StartTime = now;
                Particles[i].EndTime = now + 10000;
                //Color is White and again Start Color is the same... For the same reason as Start Vector !
                Particles[i].Color = Color.White;
                Particles[i].StartColor = Particles[0].Color;
            }
        }
    }

    //And here we are with our affector
    //An affector is called at each frame and modify every particle of the system.
    //It is a VERY HEAVY OPERATION and even more with .NET so be very very careful
    public class PointlessAffector : IParticleAffector
    {
        uint lastParticleAffect = 0;
        public void Affect(uint now, Particle[] Particles)
        {
            if (now - lastParticleAffect < 200)
                return;
            lastParticleAffect = now;
            //Our color changes every second... Again cosines and sinuses !
            Color col = new Color(0, (int)(Math.Cos(now) * 255),
                                  (int)(Math.Sin(now) + Math.Cos(now) * 255 / 2),
                                  (int)(Math.Sin(now) * 255));
            //Here it is very heavy on big particle systems.
            foreach (Particle part in Particles)
                //We set the color
                part.Color = col;
        }
    }
}
