using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;
using SQLinq.Compiler;

namespace SQLinq.Standard.Compiler
{
    public class SqlExpressionCompilerUpdaterResult : ISqlExpressionResult
    {
        /// <summary>Initializes a new instance of the <see cref="T:System.Object"></see> class.</summary>
        public SqlExpressionCompilerUpdaterResult()
        {
            this.Parameters = new Dictionary<string, object>();
            this.Updater = new List<string>();
        }

        public IDictionary<string, object> Parameters { get; set; }

        public IList<string> Updater { get; set; }

        public string SQL { get; set; }
    }
}
