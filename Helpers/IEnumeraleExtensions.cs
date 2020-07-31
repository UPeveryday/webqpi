using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Routines.Api.Helpers
{
    public static class IEnumeraleExtensions
    {
        public static IEnumerable<ExpandoObject> ShapeData<TSource>(this IEnumerable<TSource> sources,string fileds)
        {
            if (sources == null) throw new ArgumentNullException(nameof(sources));

            var expandObjectlist = new List<ExpandoObject>(sources.Count());

            var propertyInfoList = new List<PropertyInfo>();
            if(string.IsNullOrWhiteSpace(fileds))
            {
                var propertyInfos = typeof(TSource).GetProperties(BindingFlags.Public | BindingFlags.Instance);
                propertyInfoList.AddRange(propertyInfos);
            }
            else
            {
                var fieldsAfterSplit = fileds.Split(",");
                foreach (var field in fieldsAfterSplit)
                {
                    var propertyName = field.Trim();
                    var propertyInfo = typeof(TSource).GetProperty(propertyName,
                        BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);//忽略大小写  公开的 实例
                    if (propertyInfo == null)
                    {
                        throw new Exception($"Property: {propertyName} 没有找到：{typeof(TSource)}");
                    }
                    propertyInfoList.Add(propertyInfo);
                }
            }
            foreach (TSource obj in sources)
            {
                var shapedObj = new ExpandoObject();

                foreach (var propertyInfo in propertyInfoList)
                {
                    var propertyValue = propertyInfo.GetValue(obj);
                    ((IDictionary<string, object>)shapedObj).Add(propertyInfo.Name, propertyValue);//添加找到的映射属性名和属性值到ExpandoObject中
                }
                expandObjectlist.Add(shapedObj);
            }
            return expandObjectlist;
        }
    }
}
