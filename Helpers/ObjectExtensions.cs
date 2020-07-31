using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Routines.Api.Helpers
{
    public static class ObjectExtensions
    {
        public static ExpandoObject ShapeData<T>(this T source, string Fields)
        {
            if (source == null) throw new ArgumentNullException(nameof(source));
            var expandobj = new ExpandoObject();
            if (string.IsNullOrWhiteSpace(Fields))
            {
                var propertyInfos = typeof(T).GetProperties(BindingFlags.IgnoreCase | BindingFlags.Instance | BindingFlags.Public);
                foreach (var propertyInfo in propertyInfos)
                {
                    var propertyValue = propertyInfo.GetValue(source);
                    ((IDictionary<string, object>)expandobj).Add(propertyInfo.Name, propertyValue);//添加找到的映射属性名和属性值到ExpandoObject中
                }
            }
            else
            {
                var fieldsAfterSplit = Fields.Split(",");
                foreach (var field in fieldsAfterSplit)
                {
                    var propertyName = field.Trim();
                    var propertyInfo = typeof(T).GetProperty(propertyName,
                        BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);
                    if (propertyInfo == null)
                    {
                        throw new Exception($"在{typeof(T)}上没有找到{propertyName}这个属性");
                    }
                    var propertyValue = propertyInfo.GetValue(source);
                    ((IDictionary<string, object>)expandobj).Add(propertyInfo.Name, propertyValue);
                }
            }
            return expandobj;
        }

    }
}
