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
#if DEBUG
				System.Diagnostics.Debug.WriteLine("Got pointer to "+t.ToString()+" as "+raw);
#endif				                                   
				return Elements[raw];

			}
			return Activator.CreateInstance(t,raw);
		}
		
		public NativeElement()
		{}
		
		
		~NativeElement()
		{
			Dispose(false);
		} 

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
			{
				GC.SuppressFinalize(Elements[raw]);//Don't let this object get disposed
				userdata=Elements[raw].Userdata;//Pass userdata
				Elements[raw] = this; 
			}
		}
		
		public virtual void Dispose(bool disposing)
		{
			if(!disposed)
			{
				if(disposing)
					userdata=null;
				if (Elements.ContainsKey(Raw))
					Elements.Remove(Raw);
				if(_raw != IntPtr.Zero)
					CloseHandle(_raw);
				_raw=IntPtr.Zero;
				disposed=true;
			}
		}
		
		protected virtual void CloseHandle(IntPtr _raw)
		{
			Pointer_SafeRelease(_raw);
		} 
		
		public virtual void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this); 
		}
		
		
		public object Userdata
		{
			get { return userdata; }
			set { userdata = value; }
		} 
		
		#region .NET Wrapper Native Code
        bool disposed=false;
        object userdata;		
		protected IntPtr _raw = IntPtr.Zero;
		public IntPtr Raw { get { return _raw; } }
		public bool Null() { return _raw == IntPtr.Zero; }
		
		/// TODO: Make sure that SuppressUnmanagedCodeAttribute works
        [System.Runtime.InteropServices.DllImport(Native.Dll), SuppressUnmanagedCodeSecurity]
        static extern void Pointer_SafeRelease(IntPtr pointer);
		#endregion
	}
	
}
