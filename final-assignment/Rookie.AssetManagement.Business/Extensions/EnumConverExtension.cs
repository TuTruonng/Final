using System;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace Rookie.AssetManagement.Business
{
    public static class EnumConverExtension
    {
        public static string GetNameString<T>(this T enumType) where T : Enum
        {
            return Enum.GetName(typeof(T), enumType);
        }

        public static string GetDisplayName<T>(this T enumType) where T : Enum
		{
            var type = typeof(T);
            var memInfo = type.GetMember(enumType.ToString());
            var attributes = memInfo[0].GetCustomAttributes(typeof(DisplayAttribute), false);
            return (attributes[0] as DisplayAttribute).Name;
        }

        public static T GetValueFromDisplayName<T>(this string displayName) where T : Enum
		{
            var type = typeof(T);
            foreach (var field in type.GetFields())
            {
				if (Attribute.GetCustomAttribute(field, typeof(DisplayAttribute)) is DisplayAttribute attribute)
				{
					if (attribute.Name == displayName)
					{
						return (T)field.GetValue(null);
					}
				}
				else
				{
					if (field.Name == displayName)
						return (T)field.GetValue(null);
				}
			}
            throw new ArgumentOutOfRangeException(nameof(displayName));
        }
    }
}