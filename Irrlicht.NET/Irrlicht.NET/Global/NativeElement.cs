using System;
using System.Collections;
using System.Collections.Generic;
using System.Security;

namespace IrrlichtNETCP
{
	public abstract class NativeElement : IDisposable
	{
        static Dictionary<IntPtr, NativeElement> Elements = new Dictionary<IntPtr, NativeElement>();
		public static object GetObject(IntPtr raw, Type t)
		{
			if(raw == IntPtr.Zero)
				return null;
			
			if(Elements.ContainsKey(raw))
			{
				//This condition should NEVER BE TRUE but
				//in order to prevent stupid engine crashes I added it
				if(Elements[raw] == null || !t.IsInstanceOfType(Elements[raw]))
					Elements[raw] = (NativeElement)Activator.CreateInstance(t,raw);
				return Elements[raw];
			}
			return Activator.CreateInstance(t,raw);
		}
		
		public NativeElement()
		{}
		
		public NativeElement(IntPtr raw)
		{
			Initialize(raw);
		}
		protected virtual void Initialize(IntPtr raw)
		{
			_raw = raw;
			if(!Elements.ContainsKey(raw))
				Elements.Add(raw, this);
			else
				Elements[raw] = this;
		}
		
		public virtual void Dispose()
		{
            if (Elements.ContainsKey(Raw))
                Elements.Remove(Raw);
            if(_raw != IntPtr.Zero)
                try { Pointer_SafeRelease(_raw); } catch { };
		}
		
		#region .NET Wrapper Native Code
		protected IntPtr _raw = IntPtr.Zero;
		public IntPtr Raw { get { return _raw; } }
		public bool Null() { return _raw == IntPtr.Zero; }
		
		/// TODO: Make sure that SuppressUnmanagedCodeAttribute works
        [System.Runtime.InteropServices.DllImport(Native.Dll), SuppressUnmanagedCodeSecurity]
        static extern void Pointer_SafeRelease(IntPtr pointer);
		#endregion
	}
	
}
