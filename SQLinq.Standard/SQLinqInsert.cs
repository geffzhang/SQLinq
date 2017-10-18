//Copyright (c) Chris Pietschmann 2015 (http://pietschsoft.com)
//Licensed under the GNU Library General Public License (LGPL)
//License can be found here: http://sqlinq.codeplex.com/license

using SQLinq.Compiler;
using System.Collections.Generic;
using System.Linq;
using SQLinq.Standard;

namespace SQLinq
{
    public class SQLinqInsert<T> : ISQLinqInsert
    {
        public SQLinqInsert(T data)
            : this(data, DialectProvider.Create())
        { }

        public SQLinqInsert(T data, ISqlDialect dialect)
        {
            this.Data = data;
            this.Dialect = dialect;
        }

        public SQLinqInsert(T data, string tableNameOverride, ISqlDialect dialect)
            : this(data, dialect)
        {
            this.TableNameOverride = tableNameOverride;
        }

        public ISqlDialect Dialect { get; private set; }

        public T Data { get; set; }
        public string TableNameOverride { get; set; }

        public ISQLinqResult ToSQL(int existingParameterCount = 0, string parameterNamePrefix = SqlExpressionCompiler.DefaultParameterNamePrefix)
        {
            int parameterNumber = existingParameterCount;
            parameterNumber++;
            
            var parameters = new Dictionary<string, object>();
            var fields = new Dictionary<string, string>();

            // Get Table / View Name
            var tableName = TableNameOverride ?? EntityMetaData<T>.TableName;
            tableName = Dialect.ParseTableName(tableName);

            foreach (var p in EntityMetaData<T>.GetFiledInfos())
            {

                var parameterName = this.Dialect.ParameterPrefix + parameterNamePrefix + parameterNumber.ToString();

                fields.Add(p.Item1, parameterName);
                parameters.Add(parameterName, p.Item2.GetValue(this.Data, null));

                parameterNumber++;

            }

            return new SQLinqInsertResult(this.Dialect)
            {
                Table = tableName,
                Fields = fields,
                Parameters = parameters
            };
        }

    }
}
