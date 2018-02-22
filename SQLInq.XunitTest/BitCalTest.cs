using System;
using System.Collections.Generic;
using System.Text;
using SQLinq;
using SQLinq.Dialect;
using SQLInq.XunitTest.Entity;
using Xunit;

namespace SQLInq.XunitTest
{
    public class BitCalTest
    {
        [Fact]
        public void OperatorTest_Success()
        {
            var sql = new SQLinq<FlagTestEntity>(new MySqlDialect());
            sql.Where(x => (x.MyType & FType.A) == FType.A);
            //sql.Where(x => (x.TestB && true) == true);
            var sqlResult = sql.ToSQL();
            var query = sqlResult.ToQuery();
            Assert.Equal("SELECT * FROM `FlagTestEntity` WHERE (`MyType` & @sqlinq_1) = @sqlinq_2",query);
            Assert.Equal(sqlResult.Parameters["@sqlinq_1"],1);
            Assert.Equal(sqlResult.Parameters["@sqlinq_2"], 1);
        }

        [Fact]
        public void HasFlagTest_Success()
        {
            var sql = new SQLinq<FlagTestEntity>(new MySqlDialect());
            sql.Where(x => x.MyType.HasFlag(FType.A));
            //sql.Where(x => (x.TestB && true) == true);
            var sqlResult = sql.ToSQL();
            var query = sqlResult.ToQuery();
            Assert.Equal("SELECT * FROM `FlagTestEntity` WHERE (`MyType` & @sqlinq_1) = @sqlinq_1", query);
            Assert.Equal(sqlResult.Parameters["@sqlinq_1"],FType.A);
        }
    }
}
