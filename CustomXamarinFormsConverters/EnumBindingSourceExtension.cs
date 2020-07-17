using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CustomXamarinFormsConverters
{
    [ContentProperty(nameof(EnumType))]
    public class EnumBindingSourceExtension : IMarkupExtension
    {
        private Type _enumType;
        public Type EnumType
        {
            get { return _enumType; }
            set
            {
                if (value != _enumType)
                {
                    if (null != value)
                    {
                        Type enumType = Nullable.GetUnderlyingType(value) ?? value;
                        if (!enumType.IsEnum)
                            throw new ArgumentException("Type must be for an Enum.");
                    }

                    _enumType = value;
                }
            }
        }

        public EnumBindingSourceExtension() { }

        public EnumBindingSourceExtension(Type enumType)
        {
            EnumType = enumType;
        }

        public object ProvideValue(IServiceProvider serviceProvider)
        {
            if (null == _enumType)
                throw new InvalidOperationException("The EnumType must be specified.");

            Type actualEnumType = Nullable.GetUnderlyingType(_enumType) ?? _enumType;
            Array enumValues = Enum.GetValues(actualEnumType);

            if (actualEnumType == _enumType)
                return enumValues;

            Array tempArray = Array.CreateInstance(actualEnumType, enumValues.Length + 1);
            enumValues.CopyTo(tempArray, 1);
            return tempArray;
        }
    }



}



//ORIGINAL
//[ContentProperty(nameof(EnumType))]
//public class EnumBindingSourceExtension : IMarkupExtension
//{
//    private Type _enumType;
//    public Type EnumType
//    {
//        get { return _enumType; }
//        set
//        {
//            if (value != _enumType)
//            {
//                if (null != value)
//                {
//                    Type enumType = Nullable.GetUnderlyingType(value) ?? value;
//                    if (!enumType.IsEnum)
//                        throw new ArgumentException("Type must be for an Enum.");
//                }

//                _enumType = value;
//            }
//        }
//    }

//    public EnumBindingSourceExtension() { }

//    public EnumBindingSourceExtension(Type enumType)
//    {
//        EnumType = enumType;
//    }

//    public object ProvideValue(IServiceProvider serviceProvider)
//    {
//        if (null == _enumType)
//            throw new InvalidOperationException("The EnumType must be specified.");

//        Type actualEnumType = Nullable.GetUnderlyingType(_enumType) ?? _enumType;
//        Array enumValues = Enum.GetValues(actualEnumType);

//        if (actualEnumType == _enumType)
//            return enumValues;

//        Array tempArray = Array.CreateInstance(actualEnumType, enumValues.Length + 1);
//        enumValues.CopyTo(tempArray, 1);
//        return tempArray;
//    }
//}
















//[ContentProperty(nameof(EnumType))]
//public class EnumBindingSourceExtension : IMarkupExtension
//{
//    private Type _enumType;
//    public Type EnumType
//    {
//        get { return _enumType; }
//        set
//        {
//            if (value != _enumType)
//            {
//                if (null != value)
//                {
//                    Type enumType = Nullable.GetUnderlyingType(value) ?? value;
//                    if (!enumType.IsEnum)
//                        throw new ArgumentException("Type must be for an Enum.");
//                }

//                _enumType = value;
//            }
//        }
//    }

//    public EnumBindingSourceExtension() { }

//    public EnumBindingSourceExtension(Type enumType)
//    {
//        EnumType = enumType;
//    }

//    public object ProvideValue(IServiceProvider serviceProvider)
//    {
//        if (null == _enumType)
//            throw new InvalidOperationException("The EnumType must be specified.");

//        Type actualEnumType = Nullable.GetUnderlyingType(_enumType) ?? _enumType;
//        Array enumValues = Enum.GetValues(actualEnumType);

//        var attrs = Attribute.GetCustomAttributes(actualEnumType);  // Reflection.  

//        var converterFound = Attribute.GetCustomAttribute(actualEnumType, typeof(System.ComponentModel.TypeConverterAttribute));

//        var attr = (System.ComponentModel.TypeConverterAttribute)(attrs.Where(x => x.GetType() == typeof(System.ComponentModel.TypeConverterAttribute)).First());
//        var converterTypeName = attr.ConverterTypeName;
//        var converterType = Type.GetType(converterTypeName);
//        var converter = Activator.CreateInstance(converterType);


//        var converter2 = TypeDescriptor.GetConverter(converterType);

//        //foreach (var attr in attrs)
//        //{
//        //    var tempType = attr.GetType();
//        //    //var otherType = typeof(TypeConverterAttribute);
//        //    if (attr.GetType() == typeof(System.ComponentModel.TypeConverterAttribute))
//        //    {
//        //        //converterFound = true;
//        //        var type = attr.GetType();
//        //        var methods = attr.GetType().GetMethods();
//        //        var method = attr.GetType().GetMethod("ConvertTo");
//        //    }
//        //}



//        if (actualEnumType == _enumType)
//            return enumValues;

//        Array tempArray = Array.CreateInstance(actualEnumType, enumValues.Length + 1);
//        enumValues.CopyTo(tempArray, 1);
//        return tempArray;
//    }
//}


//Convert to working but type is lost
//[ContentProperty(nameof(EnumType))]
//public class EnumBindingSourceExtension : IMarkupExtension
//{
//    private Type _enumType;
//    public Type EnumType
//    {
//        get { return _enumType; }
//        set
//        {
//            if (value != _enumType)
//            {
//                if (null != value)
//                {
//                    Type enumType = Nullable.GetUnderlyingType(value) ?? value;
//                    if (!enumType.IsEnum)
//                        throw new ArgumentException("Type must be for an Enum.");
//                }

//                _enumType = value;
//            }
//        }
//    }

//    public EnumBindingSourceExtension() { }

//    public EnumBindingSourceExtension(Type enumType)
//    {
//        EnumType = enumType;
//    }

//    public object ProvideValue(IServiceProvider serviceProvider)
//    {
//        if (null == _enumType)
//            throw new InvalidOperationException("The EnumType must be specified.");

//        Type actualEnumType = Nullable.GetUnderlyingType(_enumType) ?? _enumType;
//        Array enumValues = Enum.GetValues(actualEnumType);

//        var attrs = Attribute.GetCustomAttributes(actualEnumType);  // Reflection.  

//        var converterFound = Attribute.GetCustomAttribute(actualEnumType, typeof(System.ComponentModel.TypeConverterAttribute));

//        var attr = (System.ComponentModel.TypeConverterAttribute)(attrs.Where(x => x.GetType() == typeof(System.ComponentModel.TypeConverterAttribute)).First());
//        var converterTypeName = attr.ConverterTypeName;
//        var converterType = Type.GetType(converterTypeName);
//        //var converter = (System.ComponentModel.TypeConverter)Activator.CreateInstance(converterType);



//        var converter2 = TypeDescriptor.GetConverter(converterType);
//        if (converter2 != null)
//        {
//            var descriptions = new object[enumValues.Length];
//            for (int i = 0; i < enumValues.Length; i++)
//            {
//                var data = enumValues.GetValue(i);
//                descriptions[i] = TypeDescriptor.GetConverter(actualEnumType).ConvertTo(data, typeof(string));
//                //descriptions[i] = converter2.ConvertTo(   enumValues.GetValue(i));
//            }

//            return descriptions;
//        }


//        //foreach (var attr in attrs)
//        //{
//        //    var tempType = attr.GetType();
//        //    //var otherType = typeof(TypeConverterAttribute);
//        //    if (attr.GetType() == typeof(System.ComponentModel.TypeConverterAttribute))
//        //    {
//        //        //converterFound = true;
//        //        var type = attr.GetType();
//        //        var methods = attr.GetType().GetMethods();
//        //        var method = attr.GetType().GetMethod("ConvertTo");
//        //    }
//        //}



//        if (actualEnumType == _enumType)
//            return enumValues;

//        Array tempArray = Array.CreateInstance(actualEnumType, enumValues.Length + 1);
//        enumValues.CopyTo(tempArray, 1);
//        return tempArray;
//    }
//}