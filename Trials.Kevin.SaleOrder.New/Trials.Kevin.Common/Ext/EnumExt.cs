using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
using System.Text;

namespace Trials.Kevin.Common.Ext
{
    /// <summary>
    /// 枚举类型的扩展
    /// </summary>
    public static class EnumExt
    {
        /// <summary>
        /// 获取枚举的描述
        /// </summary>
        /// <param name="val"></param>
        /// <returns></returns>
        public static string GetEnumDesc(this Enum val)
        {
            Type type = val.GetType();
            FieldInfo fieldInfo = type.GetField(val.ToString());
            if (fieldInfo == null)
            {
                return string.Empty;
            }
            string description = fieldInfo.GetCustomAttribute<DescriptionAttribute>()?.Description;
            return description;
        }

        /// <summary>
        /// 根据枚举值获取对应枚举所有值和说明字典
        /// </summary>
        /// <param name="val"></param>
        /// <returns></returns>
        public static Dictionary<int, string> GetEnumValueAndDesc(this Enum val)
        {
            Type type = val.GetType();
            Dictionary<int, string> dic = new Dictionary<int, string>();
            foreach (var item in Enum.GetValues(type))
            {
                var description = item.GetType().GetField(item.ToString()).GetCustomAttribute<DescriptionAttribute>()?.Description;
                dic.Add(Convert.ToInt32(item), description);
            }
            return dic;
        }
    } 
}
