using Common.Constants;
using System;
using System.Collections;
using System.Collections.Generic;

namespace Common.Extensions
{
    /// <summary>
    /// Conditional extension methods
    /// </summary>
    public static class ConditionalExtensions
    {
        /// <summary>
        /// Determines whether an element is null or not.
        /// </summary>
        /// <typeparam name="T">T</typeparam>
        /// <param name="source">The source.</param>
        /// <returns>IsNull</returns>
        /// <example>
        /// <code>
        /// string[] arr = null;
        /// trace(arr.IsNull()); //=> true
        ///
        /// var list = new ArrayList();
        /// trace(list.IsNull()); //=> false
        ///
        /// var list = new ArrayList();
        /// trace(list.IsEmpty()); //=> true, because array is empty.
        ///
        /// var x = "string";
        /// trace(x.IsNull()); //=> false
        ///
        /// x = "";
        /// trace(x.IsNull()); //=> false
        /// </code>
        /// </example>
        public static bool IsNull<T>(this T source)
        {
            return source == null || source is DBNull;
        }

        /// <summary>
        /// Determines whether an element is empty or not.
        /// </summary>
        /// <typeparam name="T">T</typeparam>
        /// <param name="source">The source.</param>
        /// <returns>boolean</returns>
        /// <example>
        /// <code>
        /// string[] arr = null;
        /// trace(arr.IsEmpty()); //=> true
        ///
        /// var list = new ArrayList();
        /// trace(list.IsEmpty()); //=> true, because array is empty.
        ///
        /// var x = "string";
        /// trace(x.IsEmpty()); //=> false
        ///
        /// x = "";
        /// trace(x.IsEmpty()); //=> true, because string is empty.
        /// </code>
        /// </example>
        public static bool IsEmpty<T>(this T source)
            where T : class
        {
            return source.IsNull() || CheckEmptyValue(source);
        }

        /// <summary>
        /// Determines whether an element is null or not.
        /// </summary>
        /// <typeparam name="T">T</typeparam>
        /// <param name="source">The source.</param>
        /// <returns>True/False</returns>
        /// <example>
        /// <code>
        /// string[] arr = null;
        /// trace(arr.IsNotEmpty()); //=> false
        ///
        /// var list = new ArrayList();
        /// trace(list.IsNotEmpty()); //=> false, because array is empty.
        ///
        /// var x = "string";
        /// trace(x.IsNotEmpty()); //=> true
        ///
        /// x = "";
        /// trace(x.IsNotEmpty()); //=> false, because string is empty.
        /// </code>
        /// </example>
        public static bool IsNotEmpty<T>(this T source)
            where T : class
        {
            return !source.IsEmpty();
        }

        /// <summary>
        /// Determines whether an element is default value or not.
        /// </summary>
        /// <typeparam name="T">T</typeparam>
        /// <param name="source">The source.</param>
        /// <returns>True/False</returns>
        public static bool IsDefault<T>(this T source)
        {
            if (source.IsNull())
            {
                return true;
            }

            Type type = Nullable.GetUnderlyingType(typeof(T));
            if (type != null)
            {
                object value = source; // To convert nullable type into real value type
                if (value.Equals(type.GetDefaultValue()))
                {
                    return true;
                }
            }
            else if (source.Equals(default(T)))
            {
                return true;
            }

            return false;
        }

        ///// <summary>
        ///// Determines whether an element is default value or not.
        ///// </summary>
        ///// <typeparam name="T">T</typeparam>
        ///// <param name="source">source</param>
        ///// <returns>True/False</returns>
        //public static bool IsDefault<T>(this T? source)
        //    where T : struct
        //{
        //    return source == null || source.Value.Equals(default(T));
        //}

        /// <summary>
        /// Determines whether an element is default value or empty.
        /// </summary>
        /// <typeparam name="T">T</typeparam>
        /// <param name="source">The source.</param>
        /// <returns>True/False</returns>
        public static bool IsEmptyOrDefault<T>(this T source)
        {
            return source.IsDefault() || CheckEmptyValue(source);
        }

        ///// <summary>
        ///// Determines whether is default value.
        ///// </summary>
        ///// <param name="source">The source.</param>
        ///// <returns>True/False</returns>
        //public static bool IsDefaultValue(this object source)
        //{
        //    if (source.IsNull())
        //    {
        //        return true;
        //    }

        //    Type srcType = source.GetType();
        //    Type type = Nullable.GetUnderlyingType(srcType);
        //    if (type != null)
        //    {
        //        object value = source; // To convert nullable type into real value type
        //        if (value.Equals(type.GetDefaultValue()))
        //        {
        //            return true;
        //        }
        //    }
        //    else if (source.Equals(srcType.GetDefaultValue()))
        //    {
        //        return true;
        //    }

        //    return false;
        //}

        /// <summary>
        /// Determines whether an element is null or not.
        /// </summary>
        /// <typeparam name="T">T</typeparam>
        /// <param name="source">The source.</param>
        /// <returns>return <c>true</c> if source is not null, otherwise <c>false</c></returns>
        /// <example>
        /// <code>
        /// string[] arr = null;
        /// trace(arr.IsNotNull()); //=> false
        ///
        /// var list = new ArrayList();
        /// trace(list.IsNotNull()); //=> true
        ///
        /// var x = "string";
        /// trace(x.IsNotNull()); //=> true
        ///
        /// x = "";
        /// trace(x.IsNotNull()); //=> true.
        /// </code>
        /// </example>
        public static bool IsNotNull<T>(this T source)
        {
            return !source.IsNull();
        }

        /// <summary>
        /// Return true if the type is a System.Nullable wrapper of a value type
        /// </summary>
        /// <param name="source">The source.</param>
        /// <returns>True if the object is a System.Nullable instance</returns>
        /// <example>
        /// <code>
        /// Type intx = typeof(int);
        /// Type strx = ECConst.StringType;
        /// int? ixx = 10; //=> nullable variable when it declares with '?'
        ///
        /// trace(intx.IsNullable()); //=> false
        /// trace(strx.IsNullable());  //=> true
        /// trace(itx.IsNullable()); //=> true
        /// </code>
        /// </example>
        public static bool IsNullable(this object source)
        {
            Type type = source.GetType();
            if (type == TypeConst.DBNullType)
            {
                return true;
            }

            if (type.IsGenericType && type.GetGenericTypeDefinition() == TypeConst.NullableType)
            {
                return true;
            }

            return !type.IsValueType;
        }

        /// <summary>
        /// Determines whether an element is true or not.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <returns>True/False</returns>
        /// <example>
        /// <code>
        /// string[] arr = null;
        /// trace(arr.IsTrue(); //=> false, because null or empty array
        ///
        /// string[] arr = new string[] {"1", "2"};
        /// trace(arr.IsTrue(); //=> true
        ///
        /// string s1 = "";
        /// trace(arr.IsTrue(); //=> false
        ///
        /// string s2 = "false";
        /// trace(arr.IsTrue(); //=> false
        ///
        /// string s3 = "no";
        /// trace(arr.IsTrue(); //=> false
        ///
        /// string s4 = "0";
        /// trace(arr.IsTrue(); //=> false
        ///
        /// string s2 = "some value";
        /// trace(arr.IsTrue(); //=> true
        ///
        /// string s2 = "true";
        /// trace(arr.IsTrue(); //=> true
        ///
        /// bool b1 = true;
        /// trace(b1.IsTrue(); //=> true
        /// </code>
        /// </example>
        public static bool IsTrue(this object source)
        {
            if (source.IsNull())
            {
                return false;
            }
            else if (source is bool)
            {
                return (bool)source;
            }
            else
            {
                Type sourceType = source.GetType();
                if (sourceType.IsEnum)
                {
                    source = Convert.ToChar(source);
                }

                if (source.IsEquals(StringComparer.OrdinalIgnoreCase, "false", "n", "0", string.Empty))
                {
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// Check source is false
        /// </summary>
        /// <param name="source">source</param>
        /// <returns>bool</returns>
        public static bool IsFalse(this object source)
        {
            return !source.IsTrue();
        }

        /// <summary>
        /// Determines whether an object is equal or not.
        /// </summary>
        /// <param name="source">source</param>
        /// <param name="target">target</param>
        /// <returns>True/False</returns>
        public static bool IsEquals(this object source, object target)
        {
            if (source.IsNull() && target.IsNull())
                return true;

            if (source.IsNull() || target.IsNull())
                return false;
            return source.Equals(target);
        }

        /// <summary>
        /// Determines whether an object is in the list or not.
        /// </summary>
        /// <typeparam name="T">The type of the elements in the list</typeparam>
        /// <param name="source">The source string.</param>
        /// <param name="values">The values.</param>
        /// <returns>True/False</returns>
        /// <example>
        ///   <code>
        /// string s = "John";
        /// s.IsEquals("m", "f", "male", "female"); //=&gt; false
        /// s.IsEquals("mfcpro", "john"); //=&gt; true, becase case insenstive
        /// string s1 = Request["isadmin"];
        /// if (s1.IsEquals("false", "no", "0")) {
        ///     return false;
        /// }
        /// var s = 5;
        /// s.IsEquals(1, 2, 3); //=&gt; false
        /// s.IsEquals(51, 5, 2); //=&gt; true
        ///   </code>
        ///   </example>
        public static bool IsEquals<T>(this object source, params T[] values)
        {
            return IsEquals(source, (IComparer)null, (IEnumerable<T>)values);
        }

        /// <summary>
        /// IsEquals
        /// </summary>
        /// <typeparam name="T">The T</typeparam>
        /// <param name="source">The source</param>
        /// <param name="comparer">The comparer</param>
        /// <param name="values">The values</param>
        /// <returns>True/False</returns>
        public static bool IsEquals<T>(this object source, IComparer comparer, params T[] values)
        {
            return IsEquals(source, comparer, (IEnumerable<T>)values);
        }

        /// <summary>
        /// Determines whether an object is in the list or not.
        /// </summary>
        /// <typeparam name="T">The type of the elements in the list</typeparam>
        /// <param name="source">The source string.</param>
        /// <param name="values">The values.</param>
        /// <returns>True/False</returns>
        /// <example>
        ///   <code>
        /// string s = "John";
        /// s.IsEquals("m", "f", "male", "female"); //=&gt; false
        /// s.IsEquals("mfcpro", "john"); //=&gt; true, becase case insenstive
        /// string s1 = Request["isadmin"];
        /// if (s1.IsEquals("false", "no", "0")) {
        ///     return false;
        /// }
        /// var s = 5;
        /// s.IsEquals(1, 2, 3); //=&gt; false
        /// s.IsEquals(51, 5, 2); //=&gt; true
        ///   </code>
        ///   </example>
        public static bool IsEquals<T>(this object source, IEnumerable<T> values)
        {
            return IsEquals(source, (IComparer)null, values);
        }

        /// <summary>
        /// IsEquals
        /// </summary>
        /// <typeparam name="T">The T</typeparam>
        /// <param name="source">The source</param>
        /// <param name="comparer">The comparer</param>
        /// <param name="values">The value</param>
        /// <returns>True/False</returns>
        public static bool IsEquals<T>(this object source, IComparer comparer, IEnumerable<T> values)
        {
            T tsource = source.Safe<T>();

            if (comparer == null)
            {
                bool isStringType = typeof(T) == TypeConst.StringType;
                comparer = isStringType ? (IComparer)StringComparer.OrdinalIgnoreCase : Comparer<T>.Default;
            }

            foreach (T local in values.Safe())
            {
                if (comparer.Compare(tsource, local) == 0)
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Determines whether an element is equal or not.
        /// Equals method that lets you specify on what property or field value you want to compare an object on.
        /// Also compares the object types. Useful to use in an overridden Equals method of an object.
        /// </summary>
        /// <typeparam name="T">The type for check</typeparam>
        /// <typeparam name="TValue">TValue</typeparam>
        /// <param name="source">The source.</param>
        /// <param name="selector">The comapre function for return current object type.</param>
        /// <param name="targets">The targets.</param>
        /// <returns>True/False</returns>
        /// <example>
        /// <code>
        /// var obj1 = new {id="John", title="manager", age: 43};
        /// var obj2 = new {id="Bok", title="developer", age: 21};
        /// var obj3 = new {id="Joo", title="developer", age: 43};
        ///
        /// obj1.IsEquals(x => x.id, obj2.id, obj3.id); //=> false
        /// obj1.IsEquals(x => x.age, obj3.age); //=> true
        /// obj1.IsEquals(x => x.id, "John", "Bok"); //=> true
        /// obj1.IsEquals(x => x.age, "Joo"); //=> false
        /// </code>
        /// </example>
        public static bool IsEquals<T, TValue>(this T source, Func<T, TValue> selector, params TValue[] targets)
        {
            return selector(source).IsEquals((IEnumerable<TValue>)targets);
        }

        /// <summary>
        /// Object equals
        /// </summary>
        /// <typeparam name="T">T</typeparam>
        /// <param name="source">source</param>
        /// <param name="equalsFunc">equalsFunc</param>
        /// <returns>True/False</returns>
        public static bool IsEquals<T>(this object source, Func<T, bool> equalsFunc)
            where T : class
        {
            T compareObj = source as T;
            if (compareObj == null)
            {
                return false;
            }

            return equalsFunc(compareObj);
        }

        #region Null

        /// <summary>
        /// allows an action to be taken on an object if it is null, with no return value.
        /// if the target is null, does nothing
        /// </summary>
        /// <typeparam name="T">The type for check</typeparam>
        /// <param name="source">The source.</param>
        /// <param name="action">delegate that defines action for the element to do something.</param>
        /// <returns>True/False</returns>
        /// <example>
        /// <code>
        /// item.vIfNull(x => {
        ///     // do something...
        /// });
        ///
        /// var Identity = HttpContext.Current.User.vIfNull(x => new User(), HttpContext.Current.User);
        /// </code>
        /// </example>
        public static bool IfNull<T>(this T source, Action<T> action)
        {
            if (source.IsNull())
            {
                action(source);
                return true;
            }

            return false;
        }

        /// <summary>
        /// allows an action to be taken on an object if it is not null, with no return value.
        /// if the target is null, does nothing
        /// </summary>
        /// <typeparam name="T">The type for check</typeparam>
        /// <param name="source">The source.</param>
        /// <param name="action">delegate that defines action for the element to do something.</param>
        /// <returns>True/False</returns>
        /// <example>
        /// <code>
        /// item.vIfNotNull(x => {
        ///     // do something...
        /// });
        ///
        /// var Identity = HttpContext.Current.vIfNotNull(ctx => ctx.User.vIfNotNull(user => user.Identity));
        /// </code>
        /// </example>
        public static bool IfNotNull<T>(this T source, Action<T> action)
        {
            if (!source.IsNull())
            {
                action(source);
                return true;
            }

            return false;
        }

        /// <summary>
        /// if the object this method is called on is not null, runs the given function and returns the value.
        /// if the object is null, returns <c>default(TResult)</c>
        /// </summary>
        /// <typeparam name="T">any object type</typeparam>
        /// <typeparam name="TResult">return object type</typeparam>
        /// <param name="source">The object</param>
        /// <param name="valueSelector">delegate that defines the element to return for. </param>
        /// <param name="defRetVal">Default value to return if it does not succeed the test</param>
        /// <returns>TResult</returns>
        /// <example>
        /// <code>
        /// item.vIfNotNull(x => {
        ///     // do something...
        /// });
        ///
        /// var Identity = HttpContext.Current.vIfNotNull(ctx => ctx.User.vIfNotNull(user => user.Identity));
        /// </code>
        /// </example>
        public static TResult IfNotNull<T, TResult>(this T source, Func<T, TResult> valueSelector, TResult defRetVal = default(TResult))
        {
            if (!source.IsNull())
            {
                return valueSelector(source).Safe(defRetVal);
            }

            return defRetVal;
        }

        #endregion Null

        #region Empty

        /// <summary>
        /// allows an action to be taken on an object if it is null or empty, with no return value.
        /// if the target is null, does nothing
        /// </summary>
        /// <typeparam name="T">The type for check</typeparam>
        /// <param name="source">The source.</param>
        /// <param name="action">delegate that defines action for the element to do something.</param>
        /// <returns>True/False</returns>
        /// <example>
        /// <code>
        /// item.vIfEmpty(x => {
        ///     // do something...
        /// });
        ///
        /// var Identity = ViewData["id"].vIfEmpty(ctx => ECUtil.MakeKey());
        /// </code>
        /// </example>
        public static bool IfEmpty<T>(this T source, Action<T> action)
            where T : class
        {
            if (source.IsEmpty())
            {
                action(source);
                return true;
            }

            return false;
        }

        /// <summary>
        /// allows an action to be taken on an object if it is not null, with no return value.
        /// if the target is null, does nothing
        /// </summary>
        /// <typeparam name="T">The type for check</typeparam>
        /// <param name="source">The source.</param>
        /// <param name="action">delegate that defines action for the element to do something.</param>
        /// <returns>True/False</returns>
        /// <example>
        /// <code>
        /// item.vIfNotEmpty(x => {
        ///     // do something...
        /// });
        ///
        /// var Identity = HttpContext.Current.vIfNotEmpty(ctx => ctx.User.vIfNotEmpty(user => user.Identity));
        /// </code>
        /// </example>
        public static bool IfNotEmpty<T>(this T source, Action<T> action)
            where T : class
        {
            if (!source.IsEmpty())
            {
                action(source);
                return true;
            }

            return false;
        }

        /// <summary>
        /// if the object this method is called on is not null, runs the given function and returns the value.
        /// if the object is null, returns <c>default(TResult)</c>
        /// </summary>
        /// <typeparam name="T">any object type</typeparam>
        /// <typeparam name="TResult">return object type</typeparam>
        /// <param name="source">The object</param>
        /// <param name="valueSelector">delegate that defines the element to return for. </param>
        /// <param name="defRetVal">Default value to return if it does not succeed the test</param>
        /// <returns>TResult</returns>
        /// <example>
        /// <code>
        /// item.vIfNotEmpty(x => {
        ///     // do something...
        /// });
        ///
        /// var Identity = HttpContext.Current.vIfNotEmpty(ctx => ctx.User.vIfNotEmpty(user => user.Identity));
        /// </code>
        /// </example>
        public static TResult IfNotEmpty<T, TResult>(this T source, Func<T, TResult> valueSelector, TResult defRetVal = default(TResult))
            where T : class
        {
            if (!source.IsEmpty())
            {
                return valueSelector(source).Safe(defRetVal);
            }

            return defRetVal;
        }

        #endregion Empty

        /// <summary>
        /// Check empty value
        /// </summary>
        /// <typeparam name="T">T</typeparam>
        /// <param name="source">source</param>
        /// <returns>bool</returns>
        internal static bool CheckEmptyValue<T>(T source)
        {
            var sourceType = source.GetType();

            switch (Type.GetTypeCode(sourceType))
            {
                case TypeCode.String:
                    if (string.IsNullOrWhiteSpace(source as string))
                    {
                        return true;
                    }

                    break;

                case TypeCode.Object:
                    IEnumerable enumer = source as IEnumerable;
                    if (enumer != null)
                    {
                        IEnumerator enumerator = enumer.GetEnumerator();
                        if (!enumerator.MoveNext() || enumerator.Current.IsNull())
                        {
                            return true;
                        }
                    }

                    break;
            }

            return false;
        }
    }
}
