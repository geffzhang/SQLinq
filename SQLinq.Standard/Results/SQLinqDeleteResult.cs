using System;
using System.Collections.Generic;
using System.Text;

namespace SQLinq.Standard.Results
{
    public class SQLinqDeleteResult<T> : ISQLinqResult
    {
        private string _whereStr;
        private string _tableName;
        private IDictionary<string, object> _parameters;

        /// <inheritdoc />
        public SQLinqDeleteResult(string whereStr, IDictionary<string, object> parameters, string tableName)
        {
            this._whereStr = whereStr;
            this._parameters = parameters;
            this._tableName = tableName;
        }

        public IDictionary<string, object> Parameters { get => this._parameters; set => this._parameters = value; }

        public string ToQuery()
        {
            return $"DELETE FROM {this._tableName} WHERE {this._whereStr}";
        }
    }
}
