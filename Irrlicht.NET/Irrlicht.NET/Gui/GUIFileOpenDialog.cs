using System;
using System.Runtime.InteropServices;

namespace IrrlichtNETCP
{	
	public class GUIFileOpenDialog : GUIElement
	{		
		public GUIFileOpenDialog(IntPtr raw) : base(raw)
		{
        }

        public string Filename
        {
            get
            {
                return GUIFileOpenDialog_GetFilename(_raw);
            }
        }

        #region Native Invokes
        [DllImport(Native.Dll)]
        static extern string GUIFileOpenDialog_GetFilename(IntPtr dialog);
        #endregion
    }	
}
