using System;
using System.Runtime.InteropServices;

namespace IrrlichtNETCP
{	
	public class GUIToolBar : GUIElement
	{		
		public GUIToolBar(IntPtr raw) : base(raw)
		{
        }

        public GUIButton AddButton(int id, string text, Texture img, Texture pressedimg, bool isPushButton, bool useAlphaChannel)
        {
            return (GUIButton)NativeElement.GetObject(
                GUIToolBar_AddButton(_raw, id, text, img == null ? IntPtr.Zero : img.Raw, pressedimg == null ? IntPtr.Zero : pressedimg.Raw, isPushButton, useAlphaChannel),
                typeof(GUIButton));
        }

        #region Native Invokes (you must be tired now... Actually I am... This must be the... hundreth time I write "region Native Invokes")
        [DllImport(Native.Dll)]
        static extern IntPtr GUIToolBar_AddButton(IntPtr toolbar, int id, string text, IntPtr img, IntPtr pressedimg, bool isPushButton, bool useAlphaChannel);
        #endregion
    }	
}
