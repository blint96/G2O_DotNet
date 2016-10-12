#ifndef _SQMACRO_H_
#define _SQMACRO_H_

// Get params, with checking value
#define SQ_CHECK_PARAM_COUNT(vm, count) \
	int stack_size = SQAPI(gettop)(vm); \
if (stack_size <= count) \
{ \
	char buffor[255]; \
	sprintf(buffor, "(%s) wrong number of parameters, expecting %d params", __FUNCTION__, count); \
	SQAPI(sqerror)(vm, buffor); \
	return SQ_ERROR; \
} \

#define SQ_CHECK_PARAM_INT(vm, ref, index) \
	int ref; \
if (SQAPI(gettype)(vm, -stack_size + index + 1) == OT_INTEGER || SQAPI(gettype)(vm, -stack_size + index + 1) == OT_FLOAT) \
	SQAPI(getinteger)(vm, -stack_size + index + 1, &ref); \
else \
{ \
	char buffor[255]; \
	sprintf(buffor, "(%s) invalid argument at index %d, expecting 'int'", __FUNCTION__, index); \
	SQAPI(sqerror)(vm, buffor); \
	return SQ_ERROR; \
} \

#define SQ_CHECK_PARAM_BOOL(vm, ref, index) \
	SQBool ref; \
if (SQAPI(gettype)(vm, -stack_size + index + 1) == OT_BOOL || SQAPI(gettype)(vm, -stack_size + index + 1) == OT_INTEGER) \
	SQAPI(getbool)(vm, -stack_size + index + 1, &ref); \
else \
{ \
	char buffor[255]; \
	sprintf(buffor, "(%s) invalid argument at index %d, expecting 'bool'", __FUNCTION__, index); \
	SQAPI(sqerror)(vm, buffor); \
	return SQ_ERROR; \
} \

#define SQ_CHECK_PARAM_FLOAT(vm, ref, index) \
	float ref; \
if (SQAPI(gettype)(vm, -stack_size + index + 1) == OT_FLOAT || SQAPI(gettype)(vm, -stack_size + index + 1) == OT_INTEGER) \
	SQAPI(getfloat)(vm, -stack_size + index + 1, &ref); \
else \
{ \
	char buffor[255]; \
	sprintf(buffor, "(%s) invalid argument at index %d, expecting 'float'", __FUNCTION__, index); \
	SQAPI(sqerror)(vm, buffor); \
	return SQ_ERROR; \
} \

#define SQ_CHECK_PARAM_STRING(vm, ref, index) \
	const SQChar *ref; \
if (SQAPI(gettype)(vm, -stack_size + index + 1) == OT_STRING) \
	SQAPI(getstring)(vm, -stack_size + index + 1, &ref); \
else \
{ \
	char buffor[255]; \
	sprintf(buffor, "(%s) invalid argument at index %d, expecting 'string'", __FUNCTION__, index); \
	SQAPI(sqerror)(vm, buffor); \
	return SQ_ERROR; \
} \

#define SQ_CHECK_PARAM_TABLE(vm, ref, index) \
	HSQOBJECT ref; \
	SQAPI(resetobject)(&ref); \
if (SQAPI(gettype)(vm, -stack_size + index + 1) == OT_TABLE) \
{ \
	SQAPI(getstackobj)(vm, -stack_size + index + 1, &ref); \
	SQAPI(addref)(vm, &ref); \
} \
else \
{ \
	char buffor[255]; \
	sprintf(buffor, "(%s) invalid argument at index %d, expecting 'table'", __FUNCTION__, index); \
	SQAPI(sqerror)(vm, buffor); \
	return SQ_ERROR; \
} \

#define SQ_CHECK_PARAM_FUNC(vm, ref, index) \
	HSQOBJECT ref; \
	SQAPI(resetobject)(&ref); \
if (SQAPI(gettype)(vm, -stack_size + index + 1) == OT_CLOSURE) \
{ \
	SQAPI(getstackobj)(vm, -stack_size + index + 1, &ref); \
	SQAPI(addref)(vm, &ref); \
} \
else \
{ \
	char buffor[255]; \
	sprintf(buffor, "(%s) invalid argument at index %d, expecting 'function'", __FUNCTION__, index); \
	SQAPI(sqerror)(vm, buffor); \
	return SQ_ERROR; \
} \

#define SQ_CHECK_PARAM_POINTER(vm, ref, index) \
	SQUserPointer ref = NULL; \
if (SQAPI(gettype)(vm, -stack_size + index + 1) == OT_USERPOINTER) \
{ \
	SQAPI(getuserpointer)(vm, -stack_size + index + 1, &ref); \
} \
else \
{ \
	char buffor[255]; \
	sprintf(buffor, "(%s) invalid argument at index %d, expecting 'userpointer'", __FUNCTION__, index); \
	SQAPI(sqerror)(vm, buffor); \
	return SQ_ERROR; \
} \

// Get params, with default value
#define SQ_PARAM_INT(vm, ref, value, index) \
	int ref = value; \
if (SQAPI(gettype)(vm, -stack_size + index + 1) == OT_INTEGER) \
	SQAPI(getinteger)(vm, -stack_size + index + 1, &ref); \

#define SQ_PARAM_BOOL(vm, ref, value, index) \
	SQBool ref = value; \
if (SQAPI(gettype)(vm, -stack_size + index + 1) == OT_BOOL || SQAPI(gettype)(vm, -stack_size + index + 1) == OT_INTEGER) \
	SQAPI(getbool)(vm, -stack_size + index + 1, &ref); \

#define SQ_PARAM_FLOAT(vm, ref, value, index) \
	float ref = value; \
if (SQAPI(gettype)(vm, -stack_size + index + 1) == OT_FLOAT) \
	SQAPI(getfloat)(vm, -stack_size + index + 1, &ref); \

#define SQ_PARAM_STRING(vm, ref, value, index) \
	const SQChar *ref = value; \
if (SQAPI(gettype)(vm, -stack_size + index + 1) == OT_STRING) \
	SQAPI(getstring)(vm, -stack_size + index + 1, &ref); \

#define SQ_PARAM_ANY(vm, ref, index) \
	HSQOBJECT ref; \
	SQAPI(resetobject)(&ref);  \
	SQAPI(getstackobj)(vm, -stack_size + index + 1, &ref); \
	SQAPI(addref)(vm, &ref); \

// Register globals
#define SQ_REGISTER_GLOBAL_FUNC(vm, name, func) \
{ \
	SQAPI(pushroottable)(vm); \
	SQAPI(pushstring)(vm, name, -1); \
	SQAPI(newclosure)(vm, func, NULL); \
	SQAPI(newslot)(vm, -3, SQTrue); \
	SQAPI(pop)(vm, 1); \
} \

#define SQ_REGISTER_GLOBAL_CONST_INT(vm, name, value) \
{ \
	SQAPI(pushconsttable)(vm); \
	SQAPI(pushstring)(vm, name, -1); \
	SQAPI(pushinteger)(vm, value); \
	SQAPI(newslot)(vm, -3, SQTrue); \
	SQAPI(pop)(vm, 1); \
} \

#define SQ_REGISTER_GLOBAL_CONST_BOOL(vm, name, value) \
{ \
	SQAPI(pushconsttable)(vm); \
	SQAPI(pushstring)(vm, name, -1); \
	SQAPI(pushbool)(vm, value); \
	SQAPI(newslot)(vm, -3, SQTrue); \
	SQAPI(pop)(vm, 1); \
} \

#define SQ_REGISTER_GLOBAL_CONST_FLOAT(vm, name, value) \
{ \
	SQAPI(pushconsttable)(vm); \
	SQAPI(pushstring)(vm, name, -1); \
	SQAPI(pushfloat)(vm, value); \
	SQAPI(newslot)(vm, -3, SQTrue); \
	SQAPI(pop)(vm, 1); \
} \

#define SQ_REGISTER_GLOBAL_CONST_STRING(vm, name, value) \
{ \
	SQAPI(pushconsttable(vm); \
	SQAPI(pushstring)(vm, name, -1); \
	SQAPI(pushstring)(vm, value, -1); \
	SQAPI(newslot)(vm, -3, SQTrue); \
	SQAPI(pop)(vm, 1); \
} \

// Register callback
#define SQ_FUNCTION_BEGIN(vm, name) \
	SQInteger topStackSize = SQAPI(gettop)(vm); \
	SQAPI(pushroottable)(vm); \
	SQAPI(pushstring)(vm, _SC(name), -1); \
if (SQ_SUCCEEDED(SQAPI(get(vm, -2))) \
	SQAPI(pushroottable)(vm); \

#define SQ_FUNCTION_END(vm) \
SQAPI(settop)(vm, topStackSize); \

#define SQ_FUNCTION_CALL(vm, arg_count) SQAPI(call(vm, arg_count + 1, SQFalse, SQTrue);
#define SQ_FUNCTION_CALL_RETURN_INT(vm, arg_count, returnVar) \
if (SQ_SUCCEEDED(SQAPI(call)(vm, arg_count + 1, SQTrue, SQTrue))) \
	if (SQ_FAILED(SQAPI(getinteger)(vm, -1, &returnVar))) \
		returnVar = 0; \

// Register table
#define SQ_TABLE_SET_INT(vm, index, value) \
{ \
	SQAPI(pushstring)(vm, index, -1); \
	SQAPI(pushinteger)(vm, value); \
	SQAPI(newslot)(vm, -3, SQFalse); \
} \

#define SQ_TABLE_SET_FLOAT(vm, index, value) \
{ \
	SQAPI(pushstring)(vm, index, -1); \
	SQAPI(pushfloat)(vm, value); \
	SQAPI(newslot)(vm, -3, SQFalse); \
} \

#define SQ_TABLE_SET_STRING(vm, index, value) \
{ \
	SQAPI(pushstring)(vm, index, -1); \
	SQAPI(pushstring)(vm, value, -1); \
	SQAPI(newslot)(vm, -3, SQFalse); \
} \

// Register array
#define SQ_ARRAY_INT(vm, value) \
{ \
	SQAPI(pushinteger)(vm, value); \
	SQAPI(arrayappend)(vm, -2); \
} \

#define SQ_ARRAY_FLOAT(vm, value) \
{ \
	SQAPI(pushfloat)(vm, value); \
	SQAPI(arrayappend)(vm, -2); \
} \

#define SQ_ARRAY_STRING(vm, value) \
{ \
	SQAPI(pushstring)(vm, value, -1); \
	SQAPI(arrayappend)(vm, -2); \
} \

#endif //_SQMACRO_H_
