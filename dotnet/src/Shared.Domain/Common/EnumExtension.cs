using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace Shared.Domain.Common
{
    public static class EnumExtensions
    {
        public static string GetDisplayName(this Enum enumValue)
        {
            if (enumValue == null)
                return string.Empty;

            try
            {
                var memberInfo = enumValue.GetType()
                                          .GetMember(enumValue.ToString())
                                          .FirstOrDefault();

                if (memberInfo?.GetCustomAttribute<DisplayAttribute>() is DisplayAttribute displayAttribute)
                {
                    return displayAttribute.GetName() ?? string.Empty;
                }

                return enumValue.ToString();
            }
            catch
            {
                // Optional: log exception here
                return string.Empty;
            }
        }

        public static T? ParseEnum<T>(this string value) where T : struct
        {
            if (!string.IsNullOrEmpty(value))
                return (T)Enum.Parse(typeof(T), value, true);

            return null;
        }

        public static List<string> MapIdsToNames<TEnum>(int[] intList)
        where TEnum : Enum
        {
            var displayNames = new List<string>();
            if (intList != null && intList.Any())
            {
                foreach (int item in intList)
                {
                    displayNames.Add(GetDisplayName((TEnum)(object)item));
                }
            }
            return displayNames;
        }
    }
}
