using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Text;

namespace BusinessObjects
{
    public delegate object FastInvokeHandler(object _target, object[] _params);
    /*
So the usage can be in this manner:

a) invoking some method on extrernal dll:


....
// invoke method MyMethod(string) on type MyNamespace.MyClass (MyAssembly.dll)
Assembly a = Assembly.LoadFrom("MyAssembly.dll");
object instance = a.CreateInstance("MyNamespace.MyClass");
Type type = typeof(instance);
object[] args = new object[] { "some_string_argument" };
MethodInfo mi = type.GetMehod("MyMethod");
FastInvokeHandler fastInvoker = FastMethodInvoker.GetMethodInvoker(mi);
object result = fastInvoker(instance, args);
.... 

b) some method on known instance ....
// e.g. invoke GetXml() on DataSet instance
Type type = typeof(System.Data.DataSet);
object instance = Activator.CreateInstance(type);
MethodInfo mi = type.GetMethod("GetXml");
FastInvokeHandler fastInvoker = FastMethodInvoker.GetMethodInvoker(mi);
object result = fastInvoker(instance, null);
....

     
     */
    /// <summary>
    /// A class to invoke methods using System.Reflection.Emit.DynamicMethod (.NET 2.0).
    /// </summary>
    public sealed class FastMethodInvoker
    {
        private FastMethodInvoker() { }

        /// <summary>
        /// Returns DynamicMethod.
        /// </summary>
        /// <param name="_methodInfo">MethodInfo.</param>
        /// <returns>Delegate</returns>
        public static FastInvokeHandler GetMethodInvoker(MethodInfo _methodInfo)
        {
            DynamicMethod dynamicMethod = new DynamicMethod(string.Empty, typeof(object), new Type[] { typeof(object), typeof(object[]) }, _methodInfo.DeclaringType.Module);
            ILGenerator il = dynamicMethod.GetILGenerator();
            ParameterInfo[] ps = _methodInfo.GetParameters();
            Type[] paramTypes = new Type[ps.Length];
            for (int ii = 0; ii < paramTypes.Length; ii++)
            {
                if (ps[ii].ParameterType.IsByRef)
                    paramTypes[ii] = ps[ii].ParameterType.GetElementType();
                else
                    paramTypes[ii] = ps[ii].ParameterType;
            }
            LocalBuilder[] locals = new LocalBuilder[paramTypes.Length];

            for (int ii = 0; ii < paramTypes.Length; ii++)
            {
                locals[ii] = il.DeclareLocal(paramTypes[ii], true);
            }

            for (int ii = 0; ii < paramTypes.Length; ii++)
            {
                il.Emit(OpCodes.Ldarg_1);
                EmitFastInt(il, ii);
                il.Emit(OpCodes.Ldelem_Ref);
                EmitCastToReference(il, paramTypes[ii]);
                il.Emit(OpCodes.Stloc, locals[ii]);
            }
            il.Emit(OpCodes.Ldarg_0);
            for (int ii = 0; ii < paramTypes.Length; ii++)
            {
                if (ps[ii].ParameterType.IsByRef)
                    il.Emit(OpCodes.Ldloca_S, locals[ii]);
                else
                    il.Emit(OpCodes.Ldloc, locals[ii]);
            }
            il.EmitCall(OpCodes.Callvirt, _methodInfo, null);
            if (_methodInfo.ReturnType == typeof(void))
                il.Emit(OpCodes.Ldnull);
            else
                EmitBoxIfNeeded(il, _methodInfo.ReturnType);

            for (int ii = 0; ii < paramTypes.Length; ii++)
            {
                if (ps[ii].ParameterType.IsByRef)
                {
                    il.Emit(OpCodes.Ldarg_1);
                    EmitFastInt(il, ii);
                    il.Emit(OpCodes.Ldloc, locals[ii]);
                    if (locals[ii].LocalType.IsValueType)
                        il.Emit(OpCodes.Box, locals[ii].LocalType);
                    il.Emit(OpCodes.Stelem_Ref);
                }
            }

            il.Emit(OpCodes.Ret);
            FastInvokeHandler invoker = (FastInvokeHandler)dynamicMethod.CreateDelegate(typeof(FastInvokeHandler));
            return invoker;
        }

        /// <summary>
        /// Emits the cast to reference.
        /// </summary>
        /// <param name="il">The il.</param>
        /// <param name="type">The type.</param>
        private static void EmitCastToReference(ILGenerator il, System.Type type)
        {
            if (type.IsValueType)
            {
                il.Emit(OpCodes.Unbox_Any, type);
            }
            else
            {
                il.Emit(OpCodes.Castclass, type);
            }
        }

        /// <summary>
        /// Emits the box if needed.
        /// </summary>
        /// <param name="il">The il.</param>
        /// <param name="type">The type.</param>
        private static void EmitBoxIfNeeded(ILGenerator il, System.Type type)
        {
            if (type.IsValueType)
            {
                il.Emit(OpCodes.Box, type);
            }
        }

        /// <summary>
        /// Emits the fast int.
        /// </summary>
        /// <param name="il">The il.</param>
        /// <param name="value">The value.</param>
        private static void EmitFastInt(ILGenerator il, int value)
        {
            switch (value)
            {
                case -1:
                    il.Emit(OpCodes.Ldc_I4_M1);
                    return;
                case 0:
                    il.Emit(OpCodes.Ldc_I4_0);
                    return;
                case 1:
                    il.Emit(OpCodes.Ldc_I4_1);
                    return;
                case 2:
                    il.Emit(OpCodes.Ldc_I4_2);
                    return;
                case 3:
                    il.Emit(OpCodes.Ldc_I4_3);
                    return;
                case 4:
                    il.Emit(OpCodes.Ldc_I4_4);
                    return;
                case 5:
                    il.Emit(OpCodes.Ldc_I4_5);
                    return;
                case 6:
                    il.Emit(OpCodes.Ldc_I4_6);
                    return;
                case 7:
                    il.Emit(OpCodes.Ldc_I4_7);
                    return;
                case 8:
                    il.Emit(OpCodes.Ldc_I4_8);
                    return;
            }

            if (value > -129 && value < 128)
            {
                il.Emit(OpCodes.Ldc_I4_S, (SByte)value);
            }
            else
            {
                il.Emit(OpCodes.Ldc_I4, value);
            }
        }
    }
}
