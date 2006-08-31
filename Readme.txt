=============================================================================
===================== Irrlicht .NET CP Readme ===============================
==================== Emmanuel 'DeusXL' Bossiere =============================
=============================================================================

Table of Contents :

	1. Introduction
	2. Requirements
	3. SDK Directories and Files
	4. How to use the SDK
	
=============================================================================
I Introduction
=============================================================================

Here is provided the SDK (Software Development Kit) for the library
"Irrlicht .NET CP".
This library provides a complete, cross-platform and easy-to-use .NET wrapper
for the Irrlicht Engine by Nikolaus Gebhardt (http://irrlicht.sourceforge.net)

This library IS NOT PART OF THE OFFICIAL IRRLICHT SDK.


=============================================================================
II Requirements
=============================================================================

On Microsoft Windows, you need the .NET official framework 2.0
(the wrapper can be compiled under version 1.1 but the binaries provided
are built under .NET 2) that can be found here :
http://www.microsoft.com/downloads/details.aspx?displaylang=en&FamilyID=0856eacb-4362-4b0d-8edd-aab15c5e04f5

On Linux or other Operating Systems, you need the last version 
of the Mono Project (at least the 1.16) that can be downloaded on :
http://www.mono-project.com/Main_Page

=============================================================================
III SDK Directories/Files
=============================================================================

/Readme.txt : This file
/License.txt : License of the engine YOU MUST READ THIS LICENSE BEFORE USING.
/change.log : List of all changes made on the engine since the first version
/bin : Binaries files provided for Windows and Linux.
/medias : Medias files used by the SDK examples.
/Irrlicht.NET : C# part of the wrapper with solutions provided for MonoDevelop
			    and Visual Studio 2005. It can however be compiled on any
			    .NET IDE/compilator
/IrrlichtW : C++/C part of the wrapper with solutions provided for Code::BLocks
			    and Visual Studio 2005. It can however be compiled on any
			    Irrlicht-compatible IDE/compilator.
/Irrlicht SDK : Constains all include and libraries needed to compile IrrlichtW
			    on Linux and Windows (Visual Studio and GCC).
			    Taken from the official Irrlicht SDK.
			    			
=============================================================================  
IV Use the SDK
=============================================================================

If you want to try the examples, navigate to the /bin/Release directory
and start the three examples executables (simply double click on Windows
and start with "mono <name of the file>.exe" on other OS).

If you want to use the wrapper, you need to add a reference to 
/bin/Release/Irrlicht.NET.dll
on your project and copy "IrrlichtW.dll", "Irrlicht.dll" (both for Windows)
and "IrrlichtW.so" (for Linux) on your project's directory.
