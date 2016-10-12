// G2O_DotNet_LoaderMono.cpp : Defines the exported functions for the DLL application.
//
#pragma once
#include <iostream>

#include "squirrel/sqmodule.h"
#include "squirrel/sq_macro.h"

/*Mono*/
#include <mono/mini/jit.h>
#include <mono/metadata/assembly.h>
#include <mono/metadata/object.h>
#include <mono/metadata/environment.h>
#include <mono/metadata/debug-helpers.h>
/*Own Code*/
#include "defines.h"

#define MODULE G2O_DotNet_LoaderMono

extern "C"
{
	// Module init function
	SQRESULT EXPORT sqmodule_load(HSQUIRRELVM vm, HSQAPI api)
	{
		std::string namespaceName = "GothicOnline.G2.DotNet";
		std::string assemblyName = "G2O_DotNet.dll";
		std::string className = "Entry";
		std::string mainMethod = "Main";

		//create a app domain and load the interface assembly
		MonoDomain*	domain = mono_jit_init(namespaceName.c_str());
		MonoAssembly *	assembly = mono_domain_assembly_open(domain, assemblyName.c_str());
		if (!assembly)
		{
			//assembly not found
			mono_jit_cleanup(domain);
			std::cout << "[DotNetLoader] Failed to load managed interface '" << assemblyName.c_str() << "'!" << std::endl;
			return SQ_ERROR;
		}
		else
		{
			//find the Program class and the Main method
			MonoImage * img = mono_assembly_get_image(assembly);
			MonoClass*  managedClass = mono_class_from_name(img, namespaceName.c_str(), className.c_str());
			MonoMethod*	method = mono_class_get_method_from_name(managedClass, mainMethod.c_str(), 2);

			//method not found 
			if (method == nullptr)
			{
				mono_jit_cleanup(domain);
				std::cout << "[DotNetLoader] Failed to find initialization method '" << mainMethod.c_str() << "'" << std::endl;
				return SQ_ERROR;
			}
			else
			{

				std::cout << "[DotNetLoader]G2O DotNetLoaderMono " << LOADER_VERSION << " loaded" << std::endl;

				//call the main method
				void* args[2];
				args[0] = &vm;
				args[1] = &api;
				mono_runtime_invoke(method, nullptr, args, nullptr);
				return  SQ_OK;
			}
		}
	}
}


