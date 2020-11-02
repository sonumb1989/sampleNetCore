using Common.Constants;
using System;
using System.Collections;
using System.Collections.Generic;

namespace Common.Extensions
{
    /// <summary>
    /// SafeValueExtensions
    /// </summary>
    public static class SafeValueExtensions
    {
        #region object safe

        /// <summary>
        /// This method return safe type of instance if the object is null.
        /// </summary>
        /// <param name="source">The source</param>
        /// <returns>string value</returns>
        /// <example>
        /// <code>
        /// string name = null;
        /// trace(name.Length); //=> error occured because name is null
        /// trace(name.Safe().Length); //=> we are ok.
        ///
        /// DateTime dt = ViewData["now"];
        /// dt.AddDays(1); //=> error occured because names is null
        ///
        /// DateTime dt2 = ViewData["now"].Safe(dt, dt1, DateTime.Now); // multiple argument
        /// dt2.AddDays(1); //=> we are ok.
        ///
        ///
        /// // The two sentenses as belows are equivalent.
        /// return Properties[strName].Safe(string.Empty);
        ///
        /// // old schools style coding
        /// string strProp = Properties[strName];
        /// if (strProp == null)
        ///     return string.Empty;
        ///
        /// return strProp;
        ///
        /// </code>
        /// </example>
        public static string Safe(this string source)
        {
            return source ?? string.Empty;
        }

        /// <summary>
        /// Safe
        /// </summary>
        /// <param name="source">The source</param>
        /// <returns>etring value</returns>
        public static string Safe(this object source)
        {
            if (!source.IsNull())
            {
                return source.ToString();
            }

            return string.Empty;
        }

        /// <summary>
        /// This method return safe type of instance if the object is null.
        /// </summary>
        /// <typeparam name="T">T</typeparam>
        /// <param name="source">The source.</param>
        /// <returns>T Value</returns>
        public static T Safe<T>(this T? source)
            where T : struct
        {
            if (!source.IsNull())
            {
                return source.Value;
            }

            return default(T);
        }

        /// <summary>
        /// This method return safe type of instance if the object is null.
        /// </summary>
        /// <param name="source">The list</param>
        /// <returns>IEnumerable</returns>
        /// <example>
        /// <code>
        /// string[] names = null;
        /// foreach (var item in names) { //=> error occured because names is null
        /// }
        /// foreach (var item in names.Safe()) { //=> we are ok.
        /// }
        /// </code>
        /// </example>
        public static IEnumerable Safe(this IEnumerable source)
        {
            if (source == null)
            {
                source = TypeConst.EmptyObjectArray;
            }

            return source;
        }

        /// <summary>
        /// This method return safe type of instance if the object is null.
        /// </summary>
        /// <typeparam name="T">The type of the elements of source</typeparam>
        /// <param name="source">The list</param>
        /// <returns>IEnumerable</returns>
        /// <example>
        /// <code>
        /// string[] names = null;
        /// foreach (var item in names) { //=> error occured because names is null
        /// }
        /// foreach (var item in names.Safe()) { //=> we are ok.
        /// }
        /// </code>
        /// </example>
        public static IEnumerable<T> Safe<T>(this IEnumerable<T> source)
        {
            if (source == null)
            {
                source = new T[] { };
            }

            return source;
        }

        /// <summary>
        /// This method return safe type of instance if the object is null.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <param name="defval">defval</param>
        /// <returns>byte</returns>
        public static byte[] Safe(this byte[] source, byte[] defval = null)
        {
            if (source == null)
            {
                return defval ?? TypeConst.EmptyByteArray;
            }

            if (source.Length <= 0 && defval != null)
            {
                return defval;
            }

            return source;
        }

        /// <summary>
        /// This method return safe type of instance if the object is null.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <param name="valueSelector">valueSelector function callback.</param>
        /// <returns>byte value</returns>
        public static byte[] Safe(this byte[] source, Func<byte[]> valueSelector)
        {
            if (source == null || source.Length <= 0)
            {
                return valueSelector();
            }

            return source;
        }

        /// <summary>
        /// This method return safe type of instance if the object is null.
        /// If source is empty or if no element passes the test specified by predicate;
        /// otherwise the first element in defvals that passes the test specified by predicate.
        /// </summary>
        /// <typeparam name="T">The object type</typeparam>
        /// <param name="source">The source</param>
        /// <param name="defvals">The default value which will be used if source is null</param>
        /// <returns> T value</returns>
        /// <example>
        /// <code>
        /// string name = null;
        /// trace(name.Length); //=> error occured because name is null
        /// trace(name.Safe().Length); //=> we are ok.
        ///
        /// DateTime dt = ViewData["now"];
        /// dt.AddDays(1); //=> error occured because names is null
        ///
        /// DateTime dt2 = ViewData["now"].Safe(dt, dt1, DateTime.Now); // multiple argument
        /// dt2.AddDays(1); //=> we are ok.
        ///
        /// </code>
        /// </example>
        public static T Safe<T>(this object source, params T[] defvals)
        {
            return SafeEmpty<T>(source, true, defvals);
        }

        /// <summary>
        /// SafeEmpty
        /// </summary>
        /// <typeparam name="T">The T</typeparam>
        /// <param name="source">The source</param>
        /// <param name="ensureNotEmpty">The ensureNotEmpty</param>
        /// <param name="defvals">The defvals</param>
        /// <returns>T</returns>
        public static T SafeEmpty<T>(this object source, bool ensureNotEmpty, params T[] defvals)
        {
            Type type = typeof(T);

            if (ensureNotEmpty ? !source.IsEmptyOrDefault() : !source.IsNull())
            {
                return (T)TypeConverterExtensions.Convert(source, type);
            }

            // find value from 0 to last element in the sequence
            foreach (T local in defvals)
            {
                if (!local.IsEmptyOrDefault())
                {
                    return local;
                }
            }

            if (type == TypeConst.StringType)
            {
                return string.Empty.As<T>();
            }

            return default(T);
        }

        /// <summary>
        /// This method return safe type of instance if a lamda function returns false.
        /// </summary>
        /// <typeparam name="T">The object type</typeparam>
        /// <param name="source">The source</param>
        /// <param name="predicate">The predicate condition function.</param>
        /// <param name="defval">The default value which will be used if source is null</param>
        /// <returns>T value</returns>
        public static T Safe<T>(this T source, Predicate<T> predicate, T defval = default(T))
        {
            return predicate(source) ? source : defval;
        }

        /// <summary>
        /// if the object this method is called on is null, runs the given function and returns the value.
        /// if the object is null, returns <c>default(T)</c>
        /// </summary>
        /// <typeparam name="T">any object type</typeparam>
        /// <param name="source">The object</param>
        /// <param name="valueSelector">delegate that defines the element to return for. </param>
        /// <returns>T value</returns>
        /// <example>
        /// <code>
        /// var Identity = HttpContext.Current.User.Safe((x) => new User());
        /// </code>
        /// </example>
        public static T Safe<T>(this T source, Func<T, T> valueSelector)
        {
            if (source.IsNull())
            {
                return valueSelector(source);
            }

            return source;
        }

        #endregion object safe
    }
}
