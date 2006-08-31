using System;
using System.Collections;
using System.Runtime.InteropServices;

namespace IrrlichtNETCP
{
	public class FileSystem : NativeElement
	{
		public FileSystem(IntPtr raw) : base(raw)
		{}
		/// <summary>
		/// Adds to the current File System a zip/zlib-compatible archive
		/// </summary>
		/// <param name="name">Path to the archive</param>
		/// <param name="ignoreCase">Will future archive-based name loading ignore case ?</param>
		/// <param name="ignorePaths">Will future archive-based name loading ignore paths ?</param>
		public void AddZipFileArchive(string name, bool ignoreCase, bool ignorePaths)
		{
			FileSystem_AddZipFileArchive(_raw, name, ignoreCase, ignorePaths);
		}
		
		public string WorkingDirectory
		{
			get
			{
				return FileSystem_GetWorkingDirectory(_raw);
			}
			set
			{
				FileSystem_ChangeWorkingDirectory(_raw, value);
			}
		}
		
		public bool FileExist(string name)
		{
			return FileSystem_ExistsFile(_raw, name);
		}
		
		public FileListItem[] FileList
		{
			get
			{
				IntPtr raw = FileSystem_GetFileList(_raw);
				int count = FileList_GetFileCount(raw);
				ArrayList itemlist = new ArrayList();
				for(int i = 0; i < count; i++)
				{
					FileListItem item = new FileListItem();
					item.FullName = FileList_GetFullFileName(raw, i);
					item.Name = FileList_GetFileName(raw, i);
					item.IsDirectory = FileList_IsDirectory(raw, i);
					itemlist.Add(item);
				}
				return (FileListItem[])itemlist.ToArray(typeof(FileListItem));
			}
		}
		#region Native Invoke
		[DllImport(Native.Dll)]
		static extern void FileSystem_AddZipFileArchive(IntPtr system,string filename, bool ignoreCase, bool ignorePaths);
		
		[DllImport(Native.Dll)]
		static extern bool FileSystem_ChangeWorkingDirectory(IntPtr system, string workingdirectory);
		
		[DllImport(Native.Dll)]
		static extern IntPtr FileSystem_GetFileList(IntPtr system);
		
		[DllImport(Native.Dll)]
		static extern bool FileSystem_ExistsFile(IntPtr system, string filename);
		
		[DllImport(Native.Dll)]
		static extern string FileSystem_GetWorkingDirectory(IntPtr system);
		
		
		[DllImport(Native.Dll)]
		static extern int FileList_GetFileCount(IntPtr list);
		
		[DllImport(Native.Dll)]
		static extern string FileList_GetFileName(IntPtr list, int index);
		
		[DllImport(Native.Dll)]
		static extern string FileList_GetFullFileName(IntPtr list, int index);
		
		[DllImport(Native.Dll)]
		static extern bool FileList_IsDirectory(IntPtr list, int index);
		#endregion
	}
	
	public struct FileListItem
	{
		public string Name;
		public string FullName;
		public bool IsDirectory;
		public override string ToString()
		{
			return "Name = " + Name + "; FullName = " + FullName + "; IsDirectory = " + IsDirectory;
		}
	}
}
