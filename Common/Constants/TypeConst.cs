using System;
using System.Collections;
using System.Collections.Specialized;
using System.Data;
using System.Dynamic;
using System.Text;
using System.Text.RegularExpressions;

namespace Common.Constants
{
    /// <summary>
    /// Contain all constants of MjsType
    /// </summary>
    public static class TypeConst
    {
        /// <summary>
        /// GIGABYTE
        /// </summary>
        public const long GIGABYTE = 1024 * 1024 * 1024;

        /// <summary>
        /// MEGABYTE
        /// </summary>
        public const long MEGABYTE = 1024 * 1024;

        /// <summary>
        /// KIROBYTE
        /// </summary>
        public const long KIROBYTE = 1024;

        /// <summary>
        /// Define position list tab
        /// </summary>
        public static readonly string[] TabRight = new string[] { "3" };

        /// <summary>
        /// YES
        /// </summary>
        public static readonly string YES = "Y";

        /// <summary>
        /// NO
        /// </summary>
        public static readonly string NO = "N";

        /// <summary>
        /// EXCEPTIONCODE
        /// </summary>
        public static readonly string EXCEPTIONCODE = "EXP0000";

        /// <summary>
        /// Encoding that support extended ASCII (8-bit).
        /// System.Text.ASCIIEncoding is a 7-bit encoding that isn't the right encoding to use for 8-bit communication.
        /// </summary>
        public static readonly Encoding ANSIEncoding = Encoding.GetEncoding(1252);

        /// <summary>
        /// The delimiter chars for path of directory
        /// </summary>
        public static readonly char[] DelimiterPathChars = new char[] { '\\', '/' };

        /// <summary>
        /// The delimiter chars for split
        /// </summary>
        public static readonly char[] DelimiterChars = new char[] { ',', ';' };

        /// <summary>
        /// whitespace array of trim chars
        /// </summary>
        public static readonly char[] TrimChars = new char[] { ' ', '\t', '\r', '\n' };

        /// <summary>
        /// TypeType
        /// </summary>
        public static readonly Type TypeType = typeof(Type);

        /// <summary>
        /// ObjectType
        /// </summary>
        public static readonly Type ObjectType = typeof(object);

        /// <summary>
        /// StringType
        /// </summary>
        public static readonly Type StringType = typeof(string);

        /// <summary>
        /// BoolType
        /// </summary>
        public static readonly Type BoolType = typeof(bool);

        /// <summary>
        /// DecimalType
        /// </summary>
        public static readonly Type DecimalType = typeof(decimal);

        /// <summary>
        /// DateType
        /// </summary>
        public static readonly Type DateType = typeof(DateTime);

        /// <summary>
        /// TimeSpanType
        /// </summary>
        public static readonly Type TimeSpanType = typeof(TimeSpan);

        /// <summary>
        /// GuidType
        /// </summary>
        public static readonly Type GuidType = typeof(Guid);

        /// <summary>
        /// DBNullType
        /// </summary>
        public static readonly Type DBNullType = typeof(DBNull);

        /// <summary>
        /// NullableType
        /// </summary>
        public static readonly Type NullableType = typeof(Nullable<>);

        /// <summary>
        /// DataRecordType
        /// </summary>
        public static readonly Type DataRecordType = typeof(IDataRecord);

        /// <summary>
        /// DictionaryType
        /// </summary>
        public static readonly Type DictionaryType = typeof(IDictionary);

        /// <summary>
        /// NameValueCollType
        /// </summary>
        public static readonly Type NameValueCollType = typeof(NameValueCollection);

        /// <summary>
        /// DynamicProviderType
        /// </summary>
        public static readonly Type DynamicProviderType = typeof(IDynamicMetaObjectProvider);

        /// <summary>
        /// DynamicType
        /// </summary>
        public static readonly Type DynamicType = typeof(DynamicObject);

        /// <summary>
        /// ExpandoType
        /// </summary>
        public static readonly Type ExpandoType = typeof(ExpandoObject);

        /// <summary>
        /// ConvertibleType
        /// </summary>
        public static readonly Type ConvertibleType = typeof(IConvertible);

        /// <summary>
        /// EnumerableType
        /// </summary>
        public static readonly Type EnumerableType = typeof(IEnumerable);

        /// <summary>
        /// EnumeratorType
        /// </summary>
        public static readonly Type EnumeratorType = typeof(IEnumerator);

        /// <summary>
        /// ArrayType
        /// </summary>
        public static readonly Type ArrayType = typeof(Array);

        /// <summary>
        /// VoidType
        /// </summary>
        public static readonly Type VoidType = typeof(void);

        /// <summary>
        /// EnumBaseType
        /// </summary>
        //public static readonly Type EnumBaseType = typeof(BaseEnum);
        /// <summary>
        /// Empty array for object
        /// </summary>
        public static readonly object[] EmptyObjectArray = new object[0];

        /// <summary>
        /// Empty array for string
        /// </summary>
        public static readonly string[] EmptyStringArray = new string[0];

        /// <summary>
        /// Empty array for byte
        /// </summary>
        public static readonly byte[] EmptyByteArray = new byte[0];

        /// <summary>
        /// Empty array for Type
        /// </summary>
        public static readonly Type[] EmptyTypeArray = new[] { typeof(object) };

        /// <summary>
        /// Emtpy instance for object
        /// </summary>
        public static readonly object EmptyObject = new object();

        #region Regex Patterns

        /// <summary>
        /// Check the string contains replaced spaces
        /// </summary>
        public static readonly string ReplacedSpaces = @"[  \t]{2,}|[ \t]";

        /// <summary>
        /// Check the string contains only alphabets. [a-zA-Z\-_]
        /// </summary>
        public static Regex AlphaOnly = new Regex(
            @"^[a-zA-Z\-_]+$",
                    RegexOptions.ExplicitCapture | RegexOptions.Compiled);

        /// <summary>
        /// Check the string contains alphabets. [a-zA-Z]email
        /// </summary>
        public static Regex AlphaChar = new Regex(
            @"[a-zA-Z]",
                    RegexOptions.ExplicitCapture | RegexOptions.Compiled);

        /// <summary>
        /// Check the string contains only alphabets and number start with alphabet. [a-zA-Z][\w\-\.]
        /// </summary>
        public static Regex AlphaNumericOnly = new Regex(
            @"^[a-zA-Z][\w\-\.]+$",
                    RegexOptions.ExplicitCapture | RegexOptions.Compiled);

        /// <summary>
        /// Check the string contains only alphabets and number start with alphabet. [a-zA-Z][\w\-\.]
        /// </summary>
        public static Regex AlphaNumeric = new Regex(
            @"[a-zA-Z][\w\-\.]+",
                    RegexOptions.ExplicitCapture | RegexOptions.Compiled);

        /// <summary>
        /// Check the string contains alphabets and number. [a-zA-Z0-9]
        /// </summary>
        public static Regex AlphaDigitOnly = new Regex(
            @"^[a-zA-Z0-9]+$",
                    RegexOptions.ExplicitCapture | RegexOptions.Compiled);

        /// <summary>
        /// Check the string contains alphabets and number. [a-zA-Z0-9]
        /// </summary>
        public static Regex AlphaDigit = new Regex(
            @"[a-zA-Z0-9]",
                    RegexOptions.ExplicitCapture | RegexOptions.Compiled);

        /// <summary>
        /// Check the string contains only numeric. -?\d+(\.\d+)?
        /// </summary>
        public static Regex NumericOnly = new Regex(
            @"^-?\d+(\.\d{1,10})?$",
                    RegexOptions.ExplicitCapture | RegexOptions.Compiled);

        /// <summary>
        /// Check the string contains numeric. -?\d+(\.\d+)?
        /// </summary>
        public static Regex Numeric = new Regex(
            @"-?\d+(\.\d{1,10})?",
                    RegexOptions.ExplicitCapture | RegexOptions.Compiled);

        /// <summary>
        /// Check the string contains only digit. -?[\d]+
        /// </summary>
        public static Regex DigitOnly = new Regex(
            @"^-?\d+$",
                    RegexOptions.ExplicitCapture | RegexOptions.Compiled);

        /// <summary>
        /// Check the string contains digit. -?[\d]+
        /// </summary>
        public static Regex Digit = new Regex(
            @"-?\d+",
                    RegexOptions.ExplicitCapture | RegexOptions.Compiled);

        /// <summary>
        /// Check the string contains only digit list separate by comman(,) -?[\d]+
        /// </summary>
        public static Regex DigitList = new Regex(
            @"^-?\d+((,\s*-?\d+)+)?$",
                    RegexOptions.ExplicitCapture | RegexOptions.Compiled);

        /// <summary>
        /// Check the string contains only hex digit. [\dA-F]+
        /// </summary>
        public static Regex HexDigitOnly = new Regex(
            @"^([\dA-F]{2,4}|[\dA-F]{6})$",
                    RegexOptions.ExplicitCapture | RegexOptions.Compiled | RegexOptions.IgnoreCase);

        /// <summary>
        /// Check the string contains hex digit. [\dA-F]+
        /// </summary>
        public static Regex HexDigit = new Regex(
            @"([\dA-F]{2,4}|[\dA-F]{6})",
                    RegexOptions.ExplicitCapture | RegexOptions.Compiled | RegexOptions.IgnoreCase);

        /// <summary>
        /// Check the string contains only white-space. [\s\t\r\n]
        /// </summary>
        public static Regex Whitespace = new Regex(
            @"^[\s\t\r\n]+$",
                    RegexOptions.ExplicitCapture | RegexOptions.Compiled);

        /// <summary>
        /// Check the string contains only word(letters, digits, underscore). \w+
        /// </summary>
        public static Regex WordOnly = new Regex(
            @"^\w+$",
                    RegexOptions.ExplicitCapture | RegexOptions.Compiled);

        /// <summary>
        /// Check the string contains word(letters, digits, underscore). \w+
        /// </summary>
        public static Regex Word = new Regex(
            @"\w+",
                    RegexOptions.ExplicitCapture | RegexOptions.Compiled);

        /// <summary>
        /// Check the string contains carrige return. [\r?\n]
        /// </summary>
        public static Regex CRLF = new Regex(
            @"\r?\n",
                    RegexOptions.ExplicitCapture | RegexOptions.Compiled);

        /// <summary>
        /// Check the string contains carrige return. [\r?\n]
        /// </summary>
        public static Regex CRLFTAB = new Regex(
            @"[\r\n\t]",
                    RegexOptions.ExplicitCapture | RegexOptions.Compiled);

        /// <summary>
        /// Check the string contains any urlencoded string.
        /// </summary>
        public static Regex URLHex = new Regex(
            @"\%(\d[a-fA-F]|u[a-fA-F0-9]{4})",
                    RegexOptions.Compiled | RegexOptions.ExplicitCapture);

        /// <summary>
        /// Check the string contains only boolean. (true|false|yes|no|y|n|0|1)
        /// </summary>
        public static Regex Boolean = new Regex(
            @"^(true|false|yes|no|y|n|0|1)$",
                    RegexOptions.ExplicitCapture | RegexOptions.Compiled | RegexOptions.IgnoreCase);

        /// <summary>
        /// Check the string contains any path.
        /// </summary>
        public static Regex Path = new Regex(
            @"(?<path>([a-zA-Z]\:\\|\\\\[\w\.\-]+\\)(?<dir>[\w\.\-]+\\)*)(?<file>[\w\.\- ]+\.[\w]+)?",
                    RegexOptions.ExplicitCapture | RegexOptions.Compiled);

        /// <summary>
        /// Check the string contains any invalid char for path.
        /// </summary>
        public static Regex InvalidPathChars = new Regex(
            @"[\\/?:*""<>|]",
                    RegexOptions.ExplicitCapture | RegexOptions.Compiled);

        /// <summary>
        /// Check the string contains any invalid char for name.
        /// </summary>
        public static Regex InvalidNameChars = new Regex(
            @"[_\/\.\-\s]",
                    RegexOptions.ExplicitCapture | RegexOptions.Compiled);

        /// <summary>
        /// Check the string contains valid phone nummber.
        /// </summary>
        public static Regex Phone = new Regex(
            @"^[01]?[- .]?(\([2-9]\d{2}\)|[2-9]\d{2})[- .]?\d{3}[- .]?\d{4}$",
                    RegexOptions.ExplicitCapture | RegexOptions.Compiled);

        /// <summary>
        /// Check the string contains valid email address.
        /// </summary>
        public static Regex Email = new Regex(
            @"^((?<account>[\w-.]+)@(?<domain>([\w-]+\.)+[a-zA-Z]{2,})|""(?<display>[^""]+)""\s*<(?<account>[\w-.]+)@(?<domain>([\w-]+\.)+[a-zA-Z]{2,})>)$",
                    RegexOptions.ExplicitCapture | RegexOptions.Compiled);

        /// <summary>
        /// Check the string contains valid url address.
        /// </summary>
        public static Regex Url = new Regex(
            @"^(?<protocol>http|https|ftp)\://(?<host>([\w\-]+\.)+[a-zA-Z]{2,})(\:(?<port>\d+))?(/(?<path>.*))?$",
                    RegexOptions.ExplicitCapture | RegexOptions.Compiled);

        /// <summary>
        /// Check the string contains valid zip code.
        /// </summary>
        public static Regex ZipCode = new Regex(
            @"^(\d{5}-\d{4}|\d{5}|\d{9})$|^([a-zA-Z]\d[a-zA-Z] \d[a-zA-Z]\d)$",
                    RegexOptions.ExplicitCapture | RegexOptions.Compiled);

        /// <summary>
        /// Check the string contains valid password.
        /// </summary>
        public static Regex Password = new Regex(
            @"(?!^[0-9]*$)(?!^[a-zA-Z]*$)^([a-zA-Z0-9]{8,10})$",
                    RegexOptions.ExplicitCapture | RegexOptions.Compiled);

        /// <summary>
        /// Validates that the field contains an integer greater than zero.
        /// </summary>
        public static Regex NoneNegativeInteger = new Regex(
           @"^\d+$", RegexOptions.ExplicitCapture | RegexOptions.Compiled);

        /// <summary>
        /// Check the string contains valid IP-address.
        /// </summary>
        public static Regex IPAddress = new Regex(
            @"^[1-9]\d{1,2}\.\d{1,3}\.\d{1,3}\.[1-9]\d{0,2}$",
                    RegexOptions.ExplicitCapture | RegexOptions.Compiled);

        /// <summary>
        /// Check the string contains valid domain name.
        /// </summary>
        public static Regex Domain = new Regex(
            @"^([\w\-]+\.)+[a-zA-Z]{2,}$",
                    RegexOptions.ExplicitCapture | RegexOptions.Compiled);

        /// <summary>
        /// Check the string contains valid curruncy.
        /// </summary>
        public static Regex Currency = new Regex(
            @"^\p{Sc}?(?<dollor>(\d{1,3},)+\d{3}|\d+)(\.(?<cent>\d{1,2}))?$",
                    RegexOptions.ExplicitCapture | RegexOptions.Compiled);

        /// <summary>
        /// Validates a positive currency amount. If there is a decimal point, it requires 2 numeric characters after the decimal point. For example, 3.00 is valid but 3.1 is not.
        /// </summary>
        public static Regex CurrencyNoneNegative = new Regex(
            @"^\d+(\.\d\d)?$", RegexOptions.ExplicitCapture | RegexOptions.Compiled);

        /// <summary>
        /// Validates for a positive or negative currency amount. If there is a decimal point, it requires 2 numeric characters after the decimal point.
        /// </summary>
        public static Regex CurrencyPositive = new Regex(
            @"^(-)?\d+(\.\d\d)?$", RegexOptions.ExplicitCapture | RegexOptions.Compiled);

        /// <summary>
        /// Check the string contains any HTML tag.
        /// </summary>
        public static Regex HTMLTag = new Regex(
            @"<(?<tag>/?[a-zA-Z]+)[^>]*>",
                    RegexOptions.ExplicitCapture | RegexOptions.Compiled | RegexOptions.Singleline);

        /// <summary>
        /// Check the string contains any SQL Inject string.
        /// </summary>
        public static Regex SQLInject = new Regex(
            @"(^(\d+|\w+'|')\s+(or|and)\s+(\d+|'\w+'|[a-zA-Z\._]+)\s*\=|\w+';?\s*--\s*$|@@[a-zA-Z_]+|(exec|execute)(\s+@\w+|\s*\(|\s+([\w\.]+\.)?(xp|sp|sys|dt|r)_)|(set|declare)\s+@\w+|(openquery|opendatasource|openrowset)\s*\(|union\s+(all\s+)?select|select\s+([a-zA-Z_]+\s*\(\s*\)|.+?from)|dbo\.[\w\.]+)",
                    RegexOptions.ExplicitCapture | RegexOptions.Compiled | RegexOptions.IgnoreCase | RegexOptions.Multiline);

        /// <summary>
        /// Check the string contains any resolve tag.
        /// </summary>
        public static Regex Resolve = new Regex(
            @"\$\{(?<name>[^=}\s]+)(=(?<default>[^}]+))?\}",
                    RegexOptions.Compiled | RegexOptions.ExplicitCapture);

        /// <summary>
        /// When saving code type, Check the string contains limited special characte.
        /// </summary>
        public static Regex LimitedCodeType = new Regex(
            @"[∬—""'\s\\]",
                    RegexOptions.Compiled | RegexOptions.ExplicitCapture);

        /// <summary>
        /// When using code type, Check the string contains limited special character.
        /// </summary>
        public static Regex LimitedCodeTypeForUsing = new Regex(
            @"[∬—""'\\]",
                    RegexOptions.Compiled | RegexOptions.ExplicitCapture);

        /// <summary>
        /// Check the string contains limited special character for code name.
        /// </summary>
        public static Regex LimitedCodeName = new Regex(
            @"[∬—""'\\]",
                    RegexOptions.Compiled | RegexOptions.ExplicitCapture);

        /// <summary>
        /// Check the string contains limited special character for general type
        /// </summary>
        public static Regex LimitedGeneralType = new Regex(
            @"[∬—]",
                    RegexOptions.Compiled | RegexOptions.ExplicitCapture);

        /// <summary>
        /// Check the string contains limited special character for search type.
        /// </summary>
        public static Regex LimitedSearchType = new Regex(
            @"[—""']",
                    RegexOptions.Compiled | RegexOptions.ExplicitCapture);

        /// <summary>
        /// Check the string contains JavaScript
        /// </summary>
        public static Regex JavaScript = new Regex(
            @"<script|</script>",
                    RegexOptions.Compiled | RegexOptions.ExplicitCapture);

        /// <summary>
        /// When saving code, Check the string contains replaced CodeType
        /// </summary>
        public static Regex ReplacedCodeType = new Regex(
            @"[&|?=]",
                    RegexOptions.Compiled | RegexOptions.ExplicitCapture);

        /// <summary>
        /// When using code, Check the string contains replaced CodeType
        /// </summary>
        public static Regex ReplacedCodeTypeForUsing = new Regex(
            @"[&|?=]|^ *| *$",
            RegexOptions.Compiled | RegexOptions.ExplicitCapture);

        /// <summary>
        /// Check the string contains replaced CodeName
        /// </summary>
        public static Regex ReplacedCodeName = new Regex(
            @"[&|?=]|^ *| *$",
            RegexOptions.Compiled | RegexOptions.ExplicitCapture);

        /// <summary>
        /// Check the string contains replaced GeneralType
        /// </summary>
        public static Regex ReplacedGeneralType = new Regex(
            @"[&|?=]|^ *| *$",
            RegexOptions.Compiled | RegexOptions.ExplicitCapture);

        /// <summary>
        /// Check the string contains replaced SearchType
        /// </summary>
        public static Regex ReplacedSearchType = new Regex(
            @"[&|?=]",
            RegexOptions.Compiled | RegexOptions.ExplicitCapture);

        /// <summary>
        /// Check the string contains replaced BoardType
        /// </summary>
        public static Regex ReplacedBoardType = new Regex(
            @"^ *| *$",
            RegexOptions.Compiled | RegexOptions.ExplicitCapture);

        /// <summary>
        /// Check the string contains replaced round brackets
        /// </summary>
        public static Regex SubRexCdRoundBrackets = new Regex(@"[(][\s\S]*[)]", RegexOptions.Compiled | RegexOptions.ExplicitCapture);

        /// <summary>
        /// Check the string contains order form
        /// </summary>
        public static Regex OrderForm = new Regex(@"\s[aA][0-9]{1,2}$", RegexOptions.Compiled | RegexOptions.ExplicitCapture);

        #endregion Regex Patterns
    }
}
