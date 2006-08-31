using System;
using System.Runtime.InteropServices;

namespace IrrlichtNETCP
{
	
	public class TextSceneNode : SceneNode
	{
		public TextSceneNode(IntPtr raw) : base(raw)
		{
		}
		
		public string Text
		{
			set
			{
				TextSceneNode_SetText(_raw, value);
			}
		}
		
		public Color TextColor
		{
			set
			{
				TextSceneNode_SetTextColor(_raw, value.ToUnmanaged());
			}
		}
		
		#region Native Invokes
		[DllImport(Native.Dll)]
		static extern void TextSceneNode_SetText(IntPtr text, string ctext);
		
		[DllImport(Native.Dll)]
		static extern void TextSceneNode_SetTextColor(IntPtr text, int[] color);
		#endregion
	}
	
}
