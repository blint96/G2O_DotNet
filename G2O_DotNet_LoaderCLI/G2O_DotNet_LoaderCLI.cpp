// G2O_DotNet_LoaderCLI.cpp : Defines the exported functions for the DLL application.
#pragma once
/*Own Code*/
#include "defines.h"

using namespace System;
using namespace System::IO;
using namespace System::Reflection;

extern "C"
{
	// Module init function
	int EXPORT sqmodule_load(void* vm, void* api)
	{
		Console::WriteLine("[" + LOADER_NAME + "] Trying to load G2O_DotNet");
		String^ file = Path::Combine(Environment::CurrentDirectory, "G2O_DotNet.dll");
		if (File::Exists(file))
		{
			try
			{
				//Load assembly
				Assembly^ loadedAsm = Assembly::LoadFrom(file);
				//Find static Entry class.
				for each (Type^ type in loadedAsm->GetTypes())
				{
					if (type->Name == "Entry")
					{
						for each (MethodInfo^ method in type->GetMethods(BindingFlags::Static | BindingFlags::NonPublic))
						{
							//Find Main method.
							if (method->Name == "Main")
							{
								array<Object^>^ paramterArray = gcnew array<Object^>(2);
								paramterArray[0] = gcnew IntPtr(vm);
								paramterArray[1] = gcnew IntPtr(api);

								method->Invoke(nullptr, paramterArray);
								return 0;
							}
						}
					}
				}
				Console::WriteLine("[" + LOADER_NAME + "] no G2O_DotNet entry point found.");
				return -1;
			}
			catch (Exception^ ex)
			{
				Console::WriteLine("[" + LOADER_NAME + "] G2O_DotNet could not be loaded");
				return -1;
			}
		}
		else
		{
			Console::WriteLine("[" + LOADER_NAME + "] G2O_DotNet.dll could not be found!!");
			return -1;
		}
		return -1;
	}
}