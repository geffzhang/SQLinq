using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

namespace SQLinq.Standard
{
    static class EntityMetaData<T>
    {
        static EntityMetaData()
        {
            var typeInfo = typeof(T).GetTypeInfo();
            TableName = typeInfo.GetCustomAttribute<SQLinqTableAttribute>()?.Table ?? typeInfo.Name;

            var propertys = typeInfo.GetProperties();
            Fields = new Dictionary<string, string>(propertys.Length);
            Properties = new Dictionary<string, PropertyInfo>(propertys.Length);
            foreach (var property in propertys)
            {
                var colAttr = property.GetCustomAttribute<SQLinqColumnAttribute>();
                if (colAttr?.Ignore ?? false)
                {
                    continue;
                }
                var fileName = colAttr?.Column ?? property.Name;
                Fields.Add(property.Name, fileName);
                Properties.Add(fileName, property);
            }
        }
        public static string TableName { get; }

        private static Dictionary<string, string> Fields { get; }

        private static Dictionary<string, PropertyInfo> Properties { get; }

        public static string GetFieldName(string propertyName)
        {
            return Fields[propertyName];
        }

        public static IList<string> GetAllFileds()
        {
            return Fields.Values.ToArray();
        }

        public static IEnumerable<Tuple<string, PropertyInfo>> GetFiledInfos()
        {
            foreach (var kvp in Properties)
            {
                yield return new Tuple<string, PropertyInfo>(kvp.Key, kvp.Value);
            }
        }
    }
}
