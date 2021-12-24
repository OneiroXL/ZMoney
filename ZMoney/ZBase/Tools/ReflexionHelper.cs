using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ZBase.Tools
{
    /// <summary>
    /// 映射Help
    /// </summary>
    public class ReflexionHelper
    {
        #region 获取枚举描述
        /// <summary>
        /// 获取枚举描述
        /// </summary>
        /// <param name="enumValue"></param>
        /// <returns></returns>
        public static string GetDescriptionByEnum(System.Enum enumValue)
        {
            string value = enumValue.ToString();
            System.Reflection.FieldInfo field = enumValue.GetType().GetField(value);
            object[] objs = field.GetCustomAttributes(typeof(DescriptionAttribute), false);    //获取描述属性
            if (objs.Length == 0)    //当描述属性没有时，直接返回名称
                return value;
            DescriptionAttribute descriptionAttribute = (DescriptionAttribute)objs[0];
            return descriptionAttribute.Description;
        }
        #endregion

        #region 返回枚举值的描述信息
        /// <summary>
        /// 返回枚举值的描述信息。
        /// </summary>
        /// <param name="value">要获取描述信息的枚举值。</param>
        /// <returns>枚举值的描述信息。</returns>
        public static string GetEnumDescription<T>(int value)
        {
            Type enumType = typeof(T);
            DescriptionAttribute attr = null;

            // 获取枚举常数名称。
            string name = System.Enum.GetName(enumType, value);
            if (name != null)
            {
                // 获取枚举字段。
                FieldInfo fieldInfo = enumType.GetField(name);
                if (fieldInfo != null)
                {
                    // 获取描述的属性。
                    attr = Attribute.GetCustomAttribute(fieldInfo, typeof(DescriptionAttribute), false) as DescriptionAttribute;
                }
            }

            // 返回结果
            if (attr != null && !string.IsNullOrEmpty(attr.Description))
                return attr.Description;
            else
                return string.Empty;
        }

        #endregion

        #region 返回枚举值的Key
        /// <summary>
        /// 返回枚举值的Key
        /// </summary>
        /// <param name="value">要获取描述信息的枚举值。</param>
        /// <returns>枚举值的描述信息。</returns>
        public static string GetEnumKey<T>(int value)
        {
            Type enumType = typeof(T);
            // 获取枚举常数名称。
            string name = System.Enum.GetName(enumType, value);
            if (name != null)
            {
                return name;
            }
            return string.Empty;
        }
        #endregion

        #region 深拷贝
        /// <summary>
        /// 深拷贝
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static T DeepCopyByReflect<T>(T obj)
        {
            //如果是字符串或值类型则直接返回
            if (obj is string || obj.GetType().IsValueType) return obj;
            object retval = Activator.CreateInstance(obj.GetType());
            FieldInfo[] fields = obj.GetType().GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static);
            foreach (FieldInfo field in fields)
            {
                try { field.SetValue(retval, DeepCopyByReflect(field.GetValue(obj))); }
                catch { }
            }
            return (T)retval;
        }
        #endregion

        #region 将类中的字段名和描述转为字典
        /// <summary>
        /// 将类中的字段名和描述转为字典
        /// </summary>
        /// <returns></returns>
        public static Dictionary<string, string> ClassFieldToDictionary<T>()
        {
            Dictionary<string, string> dictionary = new Dictionary<string, string>();
            PropertyInfo[] peroperties = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (PropertyInfo property in peroperties)
            {
                object[] objs = property.GetCustomAttributes(typeof(DescriptionAttribute), true);
                if (objs.Length > 0)
                {
                    dictionary.Add(property.Name, ((DescriptionAttribute)objs[0]).Description);
                }
            }
            return dictionary;
        }
        #endregion

        #region 将类中的JsonProperty字段名和值转为字典
        /// <summary>
        /// 将类中的JsonProperty字段名和值转为字典
        /// </summary>
        /// <returns></returns>
        public static Dictionary<string, string> ClassFieldJsonPropertyToDictionary<T>(T model)
        {
            Dictionary<string, string> dictionary = new Dictionary<string, string>();
            PropertyInfo[] peroperties = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (PropertyInfo property in peroperties)
            {
                object[] objs = property.GetCustomAttributes(typeof(JsonProperty), true);
                if (objs.Length > 0)
                {
                    object value = property.GetValue(model, null);
                    if (value != null) 
                    {
                        dictionary.Add(((JsonProperty)objs[0]).PropertyName, value.ToString());
                    }
                }
            }
            return dictionary;
        }
        #endregion

        #region 反射遍历对象属性(属性值为空的不生效)
        /// <summary>
        /// C#反射遍历对象属性(属性值为空的不生效)
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="model">对象</param>
        public static Dictionary<string, string> ForeachClassPropertiesDic<T>(T model)
        {
            Type t = model.GetType();
            PropertyInfo[] PropertyList = t.GetProperties();

            Dictionary<string, string> dictionary = new Dictionary<string, string>();

            foreach (PropertyInfo item in PropertyList)
            {
                object value = item.GetValue(model, null);
                if (value != null) 
                {
                    string name = item.Name;
                    dictionary[name] = value.ToString();
                }
            }

            return dictionary;
        }
        #endregion


    }
}
