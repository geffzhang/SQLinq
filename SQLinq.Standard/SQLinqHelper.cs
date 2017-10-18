using System;
using System.Collections.Generic;
using System.Text;
using SQLinq.Dynamic;

namespace SQLinq.Standard
{
    public static class SQLinqHelper
    {
        /// <summary>
        /// Creates a new SQLinq object for the Type of the object specified.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj">The object that defines the Type to use for creating the SQLinq object instance for.</param>
        /// <returns></returns>
        public static SQLinq<T> Create<T>(T obj, string tableName, ISqlDialect dialect)
        {
            return new SQLinq<T>(tableName, dialect);
        }

        /// <summary>
        /// Creates a new SQLinq object for the Type of the object specified.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj">The object that defines the Type to use for creating the SQLinq object instance for.</param>
        /// <returns></returns>
        public static SQLinq<T> Create<T>(T obj, string tableName)
        {
            // initialize the Default ISqlDialect
            var dialect = DialectProvider.Create();
            return Create<T>(obj, tableName, dialect);
        }

        /// <summary>
        /// Creates a new DynamicSQLinq object for the specified table name.
        /// </summary>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public static DynamicSQLinq Create(string tableName, ISqlDialect dialect)
        {
            return new DynamicSQLinq(dialect, tableName);
        }

        /// <summary>
        /// Creates a new DynamicSQLinq object for the specified table name.
        /// </summary>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public static DynamicSQLinq Create(string tableName)
        {
            // initialize the Default ISqlDialect
            var dialect = DialectProvider.Create();
            return Create(tableName, dialect);
        }

        /// <summary>
        /// Creates a new SQLinqInsert object for the specified Object.
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static SQLinqInsert<T> Insert<T>(T data, ISqlDialect dialect)
        {
            return new SQLinqInsert<T>(data, dialect);
        }

        /// <summary>
        /// Creates a new SQLinqInsert object for the specified Object.
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static SQLinqInsert<T> Insert<T>(T data)
        {
            // initialize the Default ISqlDialect
            var dialect = DialectProvider.Create();
            return Insert<T>(data, dialect);
        }

        /// <summary>
        /// Creates a new SQLinqInsert object for the specified Object and table name.
        /// </summary>
        /// <param name="data"></param>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public static SQLinqInsert<T> Insert<T>(T data, string tableName, ISqlDialect dialect)
        {
            return new SQLinqInsert<T>(data, tableName, dialect);
        }

        /// <summary>
        /// Creates a new SQLinqInsert object for the specified Object and table name.
        /// </summary>
        /// <param name="data"></param>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public static SQLinqInsert<T> Insert<T>(T data, string tableName)
        {
            // initialize the Default ISqlDialect
            var dialect = DialectProvider.Create();
            return Insert<T>(data, tableName, dialect);
        }

        /// <summary>
        /// Creates a new SQLinqInsert object for the specified Object.
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static SQLinqUpdate<T> Update<T>(T data, ISqlDialect dialect)
        {
            return new SQLinqUpdate<T>(data, dialect);
        }

        /// <summary>
        /// Creates a new SQLinqInsert object for the specified Object.
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static SQLinqUpdate<T> Update<T>(T data)
        {
            // initialize the Default ISqlDialect
            var dialect = DialectProvider.Create();
            return Update<T>(data, dialect);
        }

        /// <summary>
        /// Creates a new SQLinqInsert object for the specified Object and table name.
        /// </summary>
        /// <param name="data"></param>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public static SQLinqUpdate<T> Update<T>(T data, string tableName, ISqlDialect dialect)
        {
            return new SQLinqUpdate<T>(data, tableName, dialect);
        }

        /// <summary>
        /// Creates a new SQLinqInsert object for the specified Object and table name.
        /// </summary>
        /// <param name="data"></param>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public static SQLinqUpdate<T> Update<T>(T data, string tableName)
        {
            // initialize the Default ISqlDialect
            var dialect = DialectProvider.Create();
            return Update<T>(data, tableName, dialect);
        }
    }
}