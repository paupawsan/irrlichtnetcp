using System;
using System.Runtime.InteropServices;

namespace IrrlichtNETCP
{	
	public class GUIEditBox : GUIElement
	{		
		public GUIEditBox(IntPtr raw) : base(raw)
		{
        }

        public bool OverrideColorEnabled
        {
            set
            {
                GUIEditBox_EnableOverrideColor(_raw, value);
            }
        }

        public int Max
        {
            get
            {
                return GUIEditBox_GetMax(_raw);
            }
            set
            {
                GUIEditBox_SetMax(_raw, value);
            }
        }

        public Color OverrideColor
        {
            set
            {
                GUIEditBox_SetOverrideColor(_raw, value.ToUnmanaged());
            }
        }

        public GUIFont OverrideFont
        {
            set
            {
                GUIEditBox_SetOverrideFont(_raw, value.Raw);
            }
        }

        #region Native Invokes
        [DllImport(Native.Dll)]
        static extern void GUIEditBox_EnableOverrideColor(IntPtr edit, bool enabled);

        [DllImport(Native.Dll)]
        static extern int GUIEditBox_GetMax(IntPtr edit);

        [DllImport(Native.Dll)]
        static extern void GUIEditBox_SetMax(IntPtr edit, int max);

        [DllImport(Native.Dll)]
        static extern void GUIEditBox_SetOverrideColor(IntPtr edit, int[] color);

        [DllImport(Native.Dll)]
	    static extern void GUIEditBox_SetOverrideFont(IntPtr edit, IntPtr font);
        #endregion
    }	
}
