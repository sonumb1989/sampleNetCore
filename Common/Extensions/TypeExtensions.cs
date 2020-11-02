using Common.Constants;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;

namespace Common.Extensions
{
    /// <summary>
    /// Define utilities of <see cref=" System.Type" />
    /// </summary>
    public static class TypeExtensions
    {
        private static ConcurrentDictionary<Type, object> _defaultValueMap = new ConcurrentDictionary<Type, object>();
        private static Type _assemblyBuilderType = typeof(AssemblyBuilder);

        #region Validations

        /// <summary>
        /// IsDynamic
        /// </summary>
        /// <typeparam name="T">The T</typeparam>
        /// <param name="source">The source</param>
        /// <returns>True if the object is a dynamic instance</returns>
        /// <example>
        /// <code>
        /// var t = typeof(int);
        /// var x = new {name='john', id=123};
        /// dynamic d = x;
        ///
        /// trace(t.vIsDynamic()); //=> false
        /// trace(x.vIsDynamic());  //=> false
        /// trace(d.vIsDynamic()); //=> true
        /// </code>
        /// </example>
        public static bool IsDynamic<T>(this T source)
            where T : class
        {
            if (source.VIsInterfaceOfAny(TypeConst.DynamicProviderType, _assemblyBuilderType))
            {
                return true;
            }

            //Consider to check the isAnok
            return false;
        }

        /// <summary>
        /// Determines whether an element is date or not.
        /// </summary>
        /// <typeparam name="T">The T</typeparam>
        /// <param name="source">The source.</param>
        /// <returns>boolean</returns>
        /// <example>
        /// <code>
        /// string[] arr = null;
        /// trace(arr.vIsArray()); //=> true
        ///
        /// var list = new ArrayList();
        /// trace(list.vIsArray()); //=> true
        ///
        /// var x = "string";
        /// trace(x.vIsArray()); //=> false
        ///
        /// var e = x.Select(c => Char.IsDigit(c));
        /// trace(e.vIsArray()); //=> false, because Enumerable is not array.
        /// </code>
        /// </example>
        public static bool IsArray<T>(this T source)
            where T : class
        {
            if (source != null)
            {
                Type type = source.GetType(typeof(T));

                if (type.IsArray)
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Determines whether the specified type is static.
        /// </summary>
        /// <param name="source">The source type.</param>
        /// <returns>
        ///   <c>true</c> if the specified source is static; otherwise, <c>false</c>.
        /// </returns>
        /// <example>
        /// <code>
        /// Type env = typeof(Environment);
        /// Type str = ECConstans.StringType;
        ///
        /// trace(env.vIsStatic()); //=> true
        /// trace(str.IsStatic());  //=> false
        /// </code>
        /// </example>
        public static bool IsStatic(this Type source)
        {
            var c = source.GetConstructors();
            return source.IsAbstract && source.IsSealed && c.Length == 0;
        }

        /// <summary>
        /// IsNullableType
        /// </summary>
        /// <param name="source">object</param>
        /// <returns>bool</returns>
        public static bool IsNullableType(this object source)
        {
            Type type = source.GetType();
            return type.IsGenericType && type.GetGenericTypeDefinition() == TypeConst.NullableType;
        }

        /// <summary>
        /// Returns true if the supplied implements the given interface.
        /// </summary>
        /// <typeparam name="T">The type (interface) to check for.</typeparam>
        /// <param name="source">object</param>
        /// <returns>True if the given type implements the specified interface.</returns>
        /// <remarks>This method is for interfaces only. </remarks>
        public static bool IsInterfaceOf<T>(this object source)
        {
            return source.IsInterfaceOf(typeof(T));
        }

        /// <summary>
        /// Returns true if the supplied implements the given interface.
        /// </summary>
        /// <param name="source">object</param>
        /// <param name="interfaceType">The type to check.</param>
        /// <returns>True if the given type implements the specified interface.</returns>
        /// <remarks>This method is for interfaces only. </remarks>
        public static bool IsInterfaceOf(this object source, Type interfaceType)
        {
            Type st = source.GetType();
            if (st == null)
            {
                return false;
            }

            if (st.IsInterface && interfaceType.Equals(st))
            {
                return true;
            }

            // find through interfaces
            return st.GetInterfaces().Any(it => interfaceType.Equals(it));
        }

        /// <summary>
        /// Returns true of the supplied implements the given interface If the given
        /// interface type is a generic type definition this method will use the generic type definition of any implemented interfaces
        /// to determine the result.
        /// </summary>
        /// <param name="source">source.</param>
        /// <param name="interfaceTypes">The interface type to check for.</param>
        /// <returns>True if the given type implements the specified interface.</returns>
        /// <remarks>This method is for interfaces only. </remarks>
        public static bool VIsInterfaceOfAny(this object source, params Type[] interfaceTypes)
        {
            Type st = source.GetType();
            if (st == null)
            {
                return false;
            }

            if (st.IsInterface && interfaceTypes.Contains(st))
            {
                return true;
            }

            // find through interfaces
            return st.GetInterfaces().Any(it => interfaceTypes.Contains(it));
        }

        /// <summary>
        /// Determines whether an element is same type or not.
        /// </summary>
        /// <typeparam name="T">T</typeparam>
        /// <param name="source">The source.</param>
        /// <returns>boolean</returns>
        /// <example>
        ///   <code>
        /// string[] arr = null;
        /// trace(arr.vIsTypeOf&lt;string[]&gt;(); //=&gt; true
        /// trace(arr.vIsTypeOf&lt;IEnumerable&lt;string&gt;&gt;(); //=&gt; true
        /// trace(arr.vIsTypeOf&lt;IEnumerable&lt;char&gt;&gt;(); //=&gt; false
        ///   </code>
        ///   </example>
        public static bool IsTypeOf<T>(this object source)
        {
            return source.IsTypeOf(typeof(T));
        }

        /// <summary>
        /// Determines whether an element is same type or not.
        /// </summary>
        /// <param name="source">source</param>
        /// <param name="typeToCheck">type</param>
        /// <returns>boolean</returns>
        public static bool IsTypeOf(this object source, Type typeToCheck)
        {
            Type st = source.GetType();
            if (st == null)
            {
                return false;
            }

            if (st.IsInterface)
            {
                return typeToCheck.Equals(st) || typeToCheck.GetInterfaces().Contains(st);
            }

            return typeToCheck.IsAssignableFrom(st);
        }

        /// <summary>
        /// Determines whether an element is same type or not.
        /// The difference between <c>vIsTypeOf</c> and <c>vIsConcreteTypeOf</c> is <c>vIsConcreteTypeOf</c> can check open generic type,
        /// such as IDictionary&lt; &gt;, IDictionary&lt;string, &gt;, ICollection&lt; &gt;, and etc...
        /// </summary>
        /// <param name="source">The source.</param>
        /// <param name="typeToCheck">The type which you want check for.</param>
        /// <returns>boolean</returns>
        /// <example>
        /// <code>
        /// string[] arr = null;
        /// trace(arr.vIsTypeOf(typeof(string[]))); //=> true
        /// trace(arr.vIsTypeOf(typeof(IEnumerable&lt;string&gt;))); //=> true
        /// trace(arr.vIsTypeOf(typeof(IEnumerable&lt;char&gt;))); //=> false
        ///
        /// </code>
        /// </example>
        public static bool IsTypeOfAny(this object source, params Type[] typeToCheck)
        {
            Type st = source.GetType();

            if (st == null)
            {
                return false;
            }

            return typeToCheck.Any(t =>
            {
                return st.IsInterface ? t.Equals(st) || t.GetInterfaces().Contains(st) : t.IsAssignableFrom(st);
            });
        }

        /// <summary>
        /// Checks the open generic type.
        /// </summary>
        /// <typeparam name="T">The T</typeparam>
        /// <param name="source">The source</param>
        /// <returns>True/False</returns>
        public static bool IsConcreteTypeOf<T>(this object source)
        {
            return source.VIsConcreteTypeOf(typeof(T));
        }

        /// <summary>
        /// Checks the open generic type.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <param name="typeToCheck">Type of the target.</param>
        /// <returns>bool</returns>
        public static bool VIsConcreteTypeOf(this object source, Type typeToCheck)
        {
            Type type = source.GetType();

            while (type != null && type != TypeConst.ObjectType)
            {
                if (type.IsGenericType && typeToCheck.Equals(type.GetGenericTypeDefinition()))
                {
                    return true;
                }

                type = type.BaseType;
            }

            return false;
        }

        /// <summary>
        /// Checks the open generic type.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <param name="typeToCheck">Type of the target.</param>
        /// <returns>boolean</returns>
        public static bool VIsConcreteTypeOfAny(this object source, params Type[] typeToCheck)
        {
            Type type = source.GetType();

            while (type != null && type != TypeConst.ObjectType)
            {
                if (type.IsGenericType && typeToCheck.Contains(type.GetGenericTypeDefinition()))
                {
                    return true;
                }

                type = type.BaseType;
            }

            return false;
        }

        /// <summary>
        /// Checks the number type.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <returns>bool</returns>
        public static bool IsTypeOfNumber(this Type source)
        {
            Type type = source;

            if (source.IsNullableType())
            {
                type = Nullable.GetUnderlyingType(source);
            }

            switch (Type.GetTypeCode(type))
            {
                case TypeCode.Byte:
                case TypeCode.Decimal:
                case TypeCode.Double:
                case TypeCode.Int16:
                case TypeCode.Int32:
                case TypeCode.Int64:
                case TypeCode.SByte:
                case TypeCode.Single:
                case TypeCode.UInt16:
                case TypeCode.UInt32:
                case TypeCode.UInt64:
                    return true;
            }

            return false;
        }

        #endregion Validations

        #region Type

        /// <summary>
        /// Detect convertion from <paramref name="sourceType"/> to <paramref name="destType"/>
        /// </summary>
        /// <param name="sourceType">Source Type</param>
        /// <param name="destType">Destination Type</param>
        /// <returns>Boolean</returns>
        public static bool CanConvert(this Type sourceType, Type destType)
        {
            bool hasChangedType = false;

            if (sourceType == null || destType == null)
            {
                return false;
            }

            if (sourceType == destType || destType.IsAssignableFrom(sourceType))
            {
                return true;
            }

            if (sourceType.IsNullableType())
            {
                sourceType = Nullable.GetUnderlyingType(sourceType);
                hasChangedType = true;
            }
            else if (sourceType.IsEnum)
            {
                sourceType = Enum.GetUnderlyingType(sourceType);
                hasChangedType = true;
            }

            if (destType.IsNullableType())
            {
                destType = Nullable.GetUnderlyingType(destType);
                hasChangedType = true;
            }
            else if (destType.IsEnum)
            {
                destType = Enum.GetUnderlyingType(destType);
                hasChangedType = true;
            }

            // if source and target are same type
            if (hasChangedType && (sourceType == destType || destType.IsAssignableFrom(sourceType)))
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// Return the type of source. if the source is Type object, this method just bypass the argument.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <returns>Type object</returns>
        /// <example>
        /// <code>
        /// var arr = new string[] {"1", "3"};
        /// var type = arr.vGetType(); //=> typeof(string[])
        /// </code>
        /// </example>
        public static Type GetType(this object source)
        {
            return GetType(source, TypeConst.ObjectType);
        }

        /// <summary>
        /// Return the type of source. if the source is Type object, this method just bypass the argument.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <param name="defaultType">The default type.</param>
        /// <returns>GetType</returns>
        public static Type GetType(this object source, Type defaultType)
        {
            if (source == null)
            {
                return defaultType;
            }

            return source as Type ?? source.GetType();
        }

        /// <summary>
        /// Returns the type of element in a sequence.
        /// </summary>
        /// <param name="source">The list</param>
        /// <returns>GetElementType</returns>
        public static Type GetElementType(this Type source)
        {
            if (source.IsArray)
            {
                return source.GetElementType();
            }

            foreach (Type implementedInterface in source.GetInterfaces())
            {
                if (implementedInterface.IsGenericType && implementedInterface.GetGenericTypeDefinition() == typeof(IEnumerable<>))
                {
                    return implementedInterface.GetGenericArguments()[0];
                }
            }

            return TypeConst.ObjectType;
        }

        /// <summary>
        /// Get the default value of Type.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <returns>GetDefaultValue</returns>
        public static object GetDefaultValue(this Type source)
        {
            if (source.IsNullable())
            {
                return null;
            }

            return _defaultValueMap.GetOrAdd(source, (key) =>
            {
                return Activator.CreateInstance(key);
            });
        }

        /// <summary>
        /// Vs the get assembly path.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <returns>GetAssemblyPath</returns>
        public static string GetAssemblyPath(this Type source)
        {
            var assemblyUri = new Uri(source.Assembly.EscapedCodeBase);
            return assemblyUri.LocalPath;
        }

        /// <summary>
        /// Creates an instance of the type
        /// </summary>
        /// <param name="source">Type to create an instance of</param>
        /// <param name="args">Arguments sent into the constructor</param>
        /// <returns>The newly created instance of the type</returns>
        public static object CreateInstance(this Type source, params object[] args)
        {
            if (source == null)
            {
                return null;
            }

            if (args.IsEmpty())
            {
                return Activator.CreateInstance(source);
            }

            return Activator.CreateInstance(source, args);
        }

        /// <summary>
        /// Creates an instance of the type
        /// </summary>
        /// <param name="source">Type to create an instance of</param>
        /// <param name="typeArgs">Type Argument sent into the constructor</param>
        /// <param name="args">Arguments sent into the constructor</param>
        /// <returns>The newly created instance of the type</returns>
        public static object CreateGenericInstance(this Type source, Type[] typeArgs, params object[] args)
        {
            if (source == null)
            {
                return null;
            }

            if (source.IsGenericType && typeArgs.IsNotNull())
            {
                source = source.MakeGenericType(typeArgs);
            }

            return source.CreateInstance(args);
        }

        #endregion Type
    }
}
