//Copyright (c) Chris Pietschmann 2015 (http://pietschsoft.com)
//Licensed under the GNU Library General Public License (LGPL)
//License can be found here: http://sqlinq.codeplex.com/license

using SQLinq.Compiler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using SQLinq.Standard.Compiler;

namespace SQLinq
{
    public class SQLinqUpdate<T> : ISQLinqUpdate
    {
        public SQLinqUpdate(ISqlDialect dialect)
        {
            this.Dialect = dialect;
            this.Expressions = new List<Expression>();
        }

        public SQLinqUpdate(T data)
            : this(data, DialectProvider.Create())
        { }

        public SQLinqUpdate(T data, ISqlDialect dialect)
        {
            //this.Data = data;

            this.Expressions = new List<Expression>();

            this.Dialect = dialect;
        }

        private Expression<Func<T>> _updateExpression;

        private SqlExpressionCompiler compiler;

        public void UpdateSet(Expression<Func<T>> setExpression)
        {
            this._updateExpression = setExpression;
        }

        public SQLinqUpdate(T data, string tableNameOverride, ISqlDialect dialect)
            : this(data, dialect)
        {
            this.TableNameOverride = tableNameOverride;
        }

        public ISqlDialect Dialect { get; private set; }

        //public T Data { get; set; }
        public string TableNameOverride { get; set; }

        public List<Expression> Expressions { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="expression"></param>
        /// <returns>The SQLinq instance to allow for method chaining.</returns>
        public SQLinqUpdate<T> Where(Expression<Func<T, bool>> expression)
        {
            this.Expressions.Add(expression);
            return this;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="expression"></param>
        /// <returns>The SQLinq instance to allow for method chaining.</returns>
        public SQLinqUpdate<T> Where(Expression expression)
        {
            this.Expressions.Add(expression);
            return this;
        }

        public ISQLinqResult ToSQL(int existingParameterCount = 0, string parameterNamePrefix = SqlExpressionCompiler.DefaultParameterNamePrefix)
        {
            int _parameterNumber = existingParameterCount;
            _parameterNumber++;

            var type = typeof(T).GetTypeInfo();
            var parameters = new Dictionary<string, object>();
            var fields = new Dictionary<string, string>();

            // Get Table / View Name
            var tableName = this.GetTableName();

            foreach (var p in type.GetProperties())
            {
                var ignoreField = false;
                var fieldName = p.Name;
                var attr = p.GetCustomAttributes(typeof(SQLinqColumnAttribute), true).FirstOrDefault() as SQLinqColumnAttribute;
                if (attr != null)
                {
                    ignoreField = attr.Ignore;
                    if (!string.IsNullOrEmpty(attr.Column))
                    {
                        fieldName = attr.Column;
                    }
                }

                if (!ignoreField)
                {
                    var parameterName = this.Dialect.ParameterPrefix + parameterNamePrefix + _parameterNumber.ToString();

                    fields.Add(fieldName, parameterName);
                    //parameters.Add(parameterName, p.GetValue(this.Data, null));

                    _parameterNumber++;
                }
            }
            _parameterNumber = existingParameterCount + parameters.Count;
            this.compiler = new SqlExpressionCompiler(this.Dialect, _parameterNumber, parameterNamePrefix);
            // ****************************************************
            // **** WHERE *****************************************
            SqlExpressionCompilerResult whereResult = this.ToSQL_Where(_parameterNumber, parameterNamePrefix, parameters);


            // ******************************
            // **** SET *********************
            var set = this.ToSQL_UpdateSet(0, parameterNamePrefix, parameters);

            return new SQLinqUpdateResult(this.Dialect)
            {
                Table = tableName,
                Where = whereResult?.SQL,
                Update = set?.SQL,
                Fields = fields,
                Parameters = parameters
            };
        }

        private SqlExpressionCompilerResult ToSQL_Where(int parameterNumber, string parameterNamePrefix, IDictionary<string, object> parameters)
        {
            SqlExpressionCompilerResult whereResult = null;
            if (this.Expressions.Count > 0)
            {
                whereResult = this.compiler.Compile(this.Expressions);
                foreach (var item in whereResult.Parameters)
                {
                    parameters.Add(item.Key, item.Value);
                }
            }
            return whereResult;
        }

        private SqlExpressionCompilerUpdaterResult ToSQL_UpdateSet(int parameterNumber, string parameterNamePrefix, IDictionary<string, object> parameters)
        {
            if (this._updateExpression != null)
            {
                SqlExpressionCompilerUpdaterResult result = compiler.CompilerUpdater(this._updateExpression,parameters);
                return result;
            }
            return null;
        }

        private string GetTableName()
        {
            var tableName = string.Empty;
            if (!string.IsNullOrEmpty(this.TableNameOverride))
            {
                tableName = this.TableNameOverride;
            }
            else
            {
                // Get Table / View Name
                var type = typeof(T);
                tableName = type.Name;
                var tableAttribute = type.GetCustomAttributes(typeof(SQLinqTableAttribute), false).FirstOrDefault() as SQLinqTableAttribute;
                if (tableAttribute != null)
                {
                    // Table / View name is explicitly set, use that instead
                    tableName = tableAttribute.Table;
                }
            }

            return this.Dialect.ParseTableName(tableName);
        }
    }
}
