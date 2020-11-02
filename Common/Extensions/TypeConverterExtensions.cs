using Common.Constants;
using System;
using System.Collections.Generic;
using System.ComponentModel;

[assembly: System.Runtime.CompilerServices.InternalsVisibleTo("TypeConverterTests")]

namespace Common.Extensions
{
    /// <summary>
    /// ITypeConverter
    /// </summary>
    public interface ITypeConverter
    {
        /// <summary>
        /// CanConvert
        /// </summary>
        /// <param name="sourceType">The source type</param>
        /// <param name="destType">The Dest Type</param>
        /// <returns> true/false</returns>
        bool CanConvert(Type sourceType, Type destType);

        /// <summary>
        /// Convert
        /// </summary>
        /// <param name="value">The Value</param>
        /// <param name="destType">The dest type</param>
        /// <returns>object value</returns>
        object Convert(object value, Type destType);
    }

    /// <summary>
    /// Helper class for converting values between various types.
    /// </summary>
    /// <remarks>
    /// This class can help convert from <see cref="DateTime"/> to <see cref="System.Type.IsPrimitive">primitive</see> type.
    /// Also helps to convert from string to number, enum or datetime.
    /// </remarks>
    /// <example>
    /// <code>
    /// int age = (int)TypeConverer.Convert(Request["userage"], typeof(int));
    /// </code>
    /// </example>
    public static class TypeConverterExtensions
    {
        /// <summary>
        /// CustomTypeConverters
        /// </summary>
        public static List<ITypeConverter> CustomTypeConverters = new List<ITypeConverter>();

        /// <summary>
        /// As
        /// </summary>
        /// <typeparam name="T">T</typeparam>
        /// <param name="source">source</param>
        /// <returns>T value</returns>
        public static T As<T>(this object source)
        {
            return (T)source;
        }

        /// <summary>
        /// Try to convert the supplied object into the specified target type.
        /// </summary>
        /// <param name="sourceType">The source type used in the conversion operation</param>
        /// <param name="destType">Type of the dest.</param>
        /// <returns>integer values</returns>
        public static int ConvertScore(Type sourceType, Type destType)
        {
            if (sourceType == null || destType == null)
            {
                return int.MaxValue;
            }

            if (sourceType == destType)
            {
                return 0;
            }
            else if (sourceType.IsEnum)
            {
                sourceType = Enum.GetUnderlyingType(sourceType);
            }
            else if (destType.IsEnum)
            {
                destType = Enum.GetUnderlyingType(destType);
            }

            // if source and target are same type
            if (sourceType == destType)
            {
                return 5;
            }

            if (destType.IsAssignableFrom(sourceType))
            {
                return GetInheritanceDistance(sourceType, destType);
            }

            // can convert form source to dest?
            var customConverter = TypeDescriptor.GetConverter(destType);
            if (customConverter != null && customConverter.CanConvertFrom(sourceType))
            {
                return 1000;
            }

            customConverter = TypeDescriptor.GetConverter(sourceType);
            if (customConverter != null && customConverter.CanConvertTo(destType))
            {
                return 1000;
            }

            return ImplicitConvertScore(sourceType, destType);
        }

        #region Convert methods

        /// <summary>
        /// Convert the supplied object into the specified target type.
        /// </summary>
        /// <param name="value">The source value used in the conversion operation</param>
        /// <param name="targetType">The type into which to convert</param>
        /// <returns>The converted value</returns>
        /// <example>
        /// <code>
        /// int age = (int)TypeConverer.Convert(Request["userage"], typeof(int));
        /// </code>
        /// </example>
        public static object Convert(object value, Type targetType)
        {
            Type sourceType = value.GetType();
            bool isEmptyValue = false;
            bool hasChangedType = false;

            // if source and target are same type
            if (sourceType == targetType || targetType.IsAssignableFrom(sourceType))
            {
                return value;
            }

            isEmptyValue = value.IsEmpty();

            if (targetType.IsNullableType())
            {
                if (isEmptyValue)
                    return null;

                targetType = Nullable.GetUnderlyingType(targetType);
                hasChangedType = true;
            }
            else if (value.IsNull())
            {
                return ConvertHelperForNull(targetType);
            }

            if (sourceType.IsNullableType())
            {
                sourceType = Nullable.GetUnderlyingType(sourceType);
                hasChangedType = true;
            }

            // if source and target are same type
            if (hasChangedType && (sourceType == targetType || targetType.IsAssignableFrom(sourceType)))
            {
                return value;
            }

            if (Type.GetTypeCode(targetType) == TypeCode.Object)
            {
                return ConvertObjectHelper(value, sourceType, targetType);
            }

            if (isEmptyValue)
            {
                return ConvertHelperForNull(targetType);
            }

            if (sourceType.IsEnum)
            {
                return Convert((int)value, targetType);
            }

            return ConvertTypeHelper(value, sourceType, targetType);
        }

        private static object ConvertObjectHelper(object value, Type sourceType, Type targetType)
        {
            var customConverter = TypeDescriptor.GetConverter(sourceType);
            if (customConverter != null && customConverter.CanConvertTo(targetType))
            {
                return customConverter.ConvertTo(value, targetType);
            }

            customConverter = TypeDescriptor.GetConverter(targetType);
            if (customConverter != null && customConverter.CanConvertFrom(sourceType))
            {
                return customConverter.ConvertFrom(value);
            }

            if (CustomTypeConverters.Count > 0)
            {
                foreach (var converter in CustomTypeConverters)
                {
                    if (converter.CanConvert(sourceType, targetType))
                    {
                        return converter.Convert(value, targetType);
                    }
                }
            }

            if (targetType == TypeConst.GuidType)
            {
                Guid result = Guid.Empty;
                Guid.TryParse(value.Safe(), out result);
                return result;
            }

            if (targetType == TypeConst.TypeType)
            {
                return value.GetType();
            }

            return null;
        }

        private static object ConvertTypeHelper(object value, Type sourceType, Type targetType)
        {
            var srcTypeCode = Type.GetTypeCode(sourceType);
            var destTypeCode = Type.GetTypeCode(targetType);

            // for preventing error of explicit conversions
            switch (destTypeCode)
            {
                case TypeCode.String:
                    if (srcTypeCode == TypeCode.DateTime)
                    {
                        return ((DateTime)value).ToString("s");
                    }

                    return value.ToString();

                case TypeCode.Byte:
                case TypeCode.Int16:
                case TypeCode.UInt16:
                case TypeCode.Int32:
                case TypeCode.UInt32:
                case TypeCode.Int64:
                case TypeCode.UInt64:
                case TypeCode.Decimal:
                case TypeCode.Double:
                case TypeCode.Single:
                    switch (srcTypeCode)
                    {
                        case TypeCode.Char:
                            if (char.IsLetterOrDigit((char)value))
                            {
                                return System.Convert.ChangeType(value, targetType);
                            }

                            return targetType.GetDefaultValue();

                        case TypeCode.Boolean:
                            return System.Convert.ChangeType((bool)value ? 1 : 0, targetType);
                    }

                    break;

                case TypeCode.Boolean:
                    return value.IsTrue();

                case TypeCode.Char:
                    switch (srcTypeCode)
                    {
                        case TypeCode.Boolean:
                            return ((bool)value) ? 'Y' : 'N';

                        case TypeCode.String:
                            return ((string)value)[0];

                        case TypeCode.Decimal:
                        case TypeCode.Double:
                        case TypeCode.Single:
                        case TypeCode.Int16:
                        case TypeCode.UInt16:
                        case TypeCode.Int32:
                        case TypeCode.UInt32:
                        case TypeCode.Int64:
                        case TypeCode.UInt64:
                        case TypeCode.Byte:
                            var dval = System.Convert.ToDecimal(value);
                            if ((dval >= 'a' && dval <= 'z') || (dval >= 'A' && dval <= 'Z'))
                            {
                                return System.Convert.ToChar(value);
                            }

                            if (dval >= 0 && dval <= 9)
                            {
                                return dval.ToString()[0];
                            }

                            break;
                    }

                    break;
            }

            return System.Convert.ChangeType(value, targetType);
        }

        private static object ConvertHelperForNull(Type targetType)
        {
            switch (Type.GetTypeCode(targetType))
            {
                case TypeCode.Boolean:
                    return false;

                case TypeCode.Char:
                    return char.MinValue;

                case TypeCode.DateTime:
                    return DateTime.MinValue;

                case TypeCode.String:
                case TypeCode.DBNull:
                case TypeCode.Empty:
                    return null;

                case TypeCode.Object:
                    if (targetType == TypeConst.GuidType)
                    {
                        return Guid.Empty;
                    }

                    return null;
            }

            return targetType.GetDefaultValue();
        }

        #endregion Convert methods

        private static int ImplicitConvertScore(Type sourceType, Type targetType)
        {
            TypeCode srcTypeCode = Type.GetTypeCode(sourceType);

            switch (Type.GetTypeCode(targetType))
            {
                case TypeCode.Boolean:
                    if (srcTypeCode == TypeCode.Char || srcTypeCode == TypeCode.Byte)
                    {
                        return 1510;
                    }

                    break;

                case TypeCode.Char:
                    if (srcTypeCode == TypeCode.Boolean)
                    {
                        return 1510;
                    }

                    break;

                case TypeCode.Byte:
                    if (srcTypeCode == TypeCode.Char || srcTypeCode == TypeCode.Boolean)
                    {
                        return 1501;
                    }

                    break;

                case TypeCode.Int32:
                case TypeCode.Decimal:
                case TypeCode.Double:
                case TypeCode.Single:
                    if (srcTypeCode == TypeCode.Char || srcTypeCode == TypeCode.Boolean)
                    {
                        return 1501;
                    }
                    else if (srcTypeCode == TypeCode.DateTime)
                    {
                        return 1502;
                    }

                    break;

                case TypeCode.Int64:
                case TypeCode.UInt64:
                case TypeCode.UInt32:
                    if (srcTypeCode == TypeCode.DateTime)
                    {
                        return 1503;
                    }

                    break;

                case TypeCode.String:
                    return 1000;
            }

            return int.MaxValue;
        }

        private static int GetInheritanceDistance(Type sourceType, Type destType)
        {
            int num = 0;

            if (destType.IsInterface)
            {
                num = 5;
            }

            for (Type type = sourceType; type != null && type != destType; type = type.BaseType)
            {
                num++;
            }

            return num * 10;
        }
    }
}
