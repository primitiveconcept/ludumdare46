namespace HackThePlanet
{
    using System;
    using System.Reflection;


    public static class DatabaseDataType
    {
        public const string Boolean = "BIT";
        public const string ShortString = "VARCHAR(16)";
        public const string MediumString = "VARCHAR(256)";
        public const string LongString = "VARCHAR(4096)";
        public const string MaxString = "VARCHAR(MAX)";
        public const string Text = "TEXT";
        public const string Byte = "TINYINT";
        public const string Short = "SMALLINT";
        public const string UShort = "SMALLINT UNSIGNED";
        public const string Integer = "INT";
        public const string UInteger = "INT UNSIGNED";
        public const string Long = "BIGINT";
        public const string ULong = "BIGINT UNSIGNED";
        public const string Float = "DECIMAL";
        public const string Double = "FLOAT";


        public static string GetForProperty(PropertyInfo propertyInfo)
        {
            // TODO
            return null;
        }
        
        
        public static string GetForType(Type type)
        {
            if (type == typeof(bool))
                return Boolean;
            if (type == typeof(string))
                return MaxString;
            if (type == typeof(byte))
                return Byte;
            if (type == typeof(short))
                return Short;
            if (type == typeof(ushort))
                return UShort;
            if (type == typeof(int))
                return Integer;
            if (type == typeof(uint))
                return UInteger;
            if (type == typeof(long))
                return Long;
            if (type == typeof(ulong))
                return ULong;
            if (type == typeof(float))
                return Float;
            if (type == typeof(double))
                return Double;

            return null;
        }
    }
}