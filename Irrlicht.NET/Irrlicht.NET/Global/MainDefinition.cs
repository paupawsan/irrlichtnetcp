 using System;
 
 namespace IrrlichtNETCP
 {
 	public class Native
 	{
        public const string Dll = @"IrrlichtW";
 	}
 	
 	public enum DriverType
 	{
 		Null,
 		Software,
 		Software2,
 		Direct3D8,
 		Direct3D9,
 		OpenGL
 	}
 	
 	public enum ColorFormat
 	{
 		A1R5G5B5,
 		R5G6B5,
 		R8G8B8,
 		A8R8G8B8
 	}
 	
 	public enum MaterialType
 	{
 		Solid,
 		Solid2Layer,
 		Lightmap,
 		LightmapAdd,
 		LightmapM2,
 		LightmapM4,
 		LightmapLighting,
 		LightmapLightingM2,
 		LightmapLightingM4,
 		DetailMap,
 		SphereMap,
 		Reflection2Layer,
 		TransparentAddColor,
 		TransparentAlphaChannel,
 		TransparentAlphaChannelRef,
 		TransparentVertexAlpha,
 		TransparentReflection2Layer,
 		NormalMapSolid,
 		NormalMapTransparentAddColor,
 		NormalMapTransparentVertexAlpha,
 		ParallaxMapSolid,
 		ParallaxMapTransparentAddColor,
 		ParallaxMapTransparentVertexAlpha
 	}
 	
 	public enum MaterialFlag
 	{
 		Wireframe,
        PointCloud,
 		GouraudShading,
 		Lighting,
 		ZBuffer,
 		ZWriteEnable,
 		BackFaceCulling,
 		BilinearFilter,
 		TrilinearFilter,
 		AnisotropicFilter,
 		FogEnable,
 		NormalizeNormals,
 		MaterialFlagCount //Do not use
 	}
 	
 	public enum SceneNodeRenderPass
 	{
 		LightAndCamera, //Deprecated, use Light or Camera instead
        Light,
        Camera,
 		SkyBox,
 		Automatic,
 		Solid,
 		Shadow,
 		Transparent,
 		Count //Do not use
 	}
 	
 	public enum SceneNodeType
 	{
 		Cube,
		Sphere,
 		Text,
 		WaterSurface,
 		Terrain,
 		SkyBox,
 		ShadowVolume,
 		OctTree,
 		Mesh,
 		Light,
 		Empty,
 		DummyTransformation,
 		Camera,
 		CameraMaya,
 		CameraFPS,
 		Billboard,
 		AnimatedMesh,
 		ParticleSystem,
 		Count,
 		Unknown
 	}
 	
 	public enum TerrainPatchSize
 	{
 		TPS9 = 9,
 		TPS17 = 17,
 		TPS33 = 33,
 		TPS65 = 65,
 		TPS129 = 129
 	}
 }
